using System;
using System.Collections.Generic;

namespace UtilityExtensions.Core.Transactions
{
    public sealed class TransactionManager
    {
        private class TransactionHandler
        {
            public Transaction transaction;
            public Action<Transaction> onExecute;
            public Action<Transaction> onRollback;
        }

        public struct Settings
        {
            public bool throwExceptionOnFail;
            public bool rollbackOnError;
            public Action<TransactionException> onExecuteError;
            public Action<TransactionException> onRollbackError;
        }

        public static readonly Settings DEFAULT = new Settings { throwExceptionOnFail = true, rollbackOnError = true };

        private List<TransactionHandler> transactions = new List<TransactionHandler>();
        private Settings settings;

        private TransactionManager()
        {
        }

        /// <summary>
        /// Initiates the transaction manager with default settings
        /// </summary>
        /// <returns> </returns>
        public static TransactionManager Init()
        {
            return Init(DEFAULT);
        }

        /// <summary>
        /// Initiates the transaction manager
        /// </summary>
        /// <param name="settings"> </param>
        /// <returns> </returns>
        public static TransactionManager Init(Settings settings)
        {
            return new TransactionManager { settings = settings };
        }

        /// <summary>
        /// Adds a transaction to the manager
        /// </summary>
        /// <param name="transaction"> </param>
        /// <param name="onExecuteTransaction"> </param>
        /// <param name="onRollbackTransaction"> </param>
        /// <returns> </returns>
        public TransactionManager AddTransaction(Transaction transaction, Action<Transaction> onExecuteTransaction = null, Action<Transaction> onRollbackTransaction = null)
        {
            transactions.Add(new TransactionHandler { transaction = transaction, onExecute = onExecuteTransaction, onRollback = onRollbackTransaction });
            return this;
        }

        /// <summary>
        /// Executes all transactions in the order they were added
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                var execute = transactions[i];
                if (execute.transaction != null)
                {
                    try
                    {
                        //Try to execute the transaction.
                        execute.transaction.Execute();
                        try
                        {
                            execute.onExecute?.Invoke(execute.transaction);
                        }
                        catch { }
                    }
                    catch (Exception e)
                    {
                        //If a transaction failed, rollback all previous transactions.
                        if (settings.rollbackOnError)
                            Rollback();

                        var transactionException = new TransactionException($"Transaction {i + 1} ({execute.transaction.GetType()}) failed execution with error: {e.Message}", execute.transaction, e);

                        try
                        {
                            settings.onExecuteError?.Invoke(transactionException);
                        }
                        catch { }

                        if (settings.throwExceptionOnFail)
                            throw transactionException;
                    }
                }
            }
        }

        /// <summary>
        /// Rollbacks all transactions in the reverse order they were added
        /// </summary>
        public void Rollback()
        {
            for (int i = transactions.Count - 1; i >= 0; i--)
            {
                var rollback = transactions[i];
                if (rollback.transaction != null)
                {
                    try
                    {
                        //Try to rollback the transaction.
                        rollback.transaction.Rollback();
                        try
                        {
                            rollback.onRollback?.Invoke(rollback.transaction);
                        }
                        catch { }
                    }
                    catch (Exception e)
                    {
                        var transactionException = new TransactionException($"Transaction {i + 1} ({rollback.transaction.GetType()}) failed rollback with error: {e.Message}", rollback.transaction, e);

                        try
                        {
                            settings.onRollbackError?.Invoke(transactionException);
                        }
                        catch { }

                        if (settings.throwExceptionOnFail)
                            throw transactionException;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all transactions from the manager
        /// </summary>
        /// <returns> </returns>
        public void Clear()
        {
            transactions.Clear();
        }
    }
}