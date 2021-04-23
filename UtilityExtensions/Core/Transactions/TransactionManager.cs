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
        }

        private List<TransactionHandler> transactions = new List<TransactionHandler>();
        private bool throwExceptionOnFail;

        private TransactionManager()
        {
        }

        /// <summary>
        /// Initiates the transaction manager
        /// </summary>
        /// <param name="throwExceptionOnFail"> </param>
        /// <returns> </returns>
        public static TransactionManager Init(bool throwExceptionOnFail = true)
        {
            return new TransactionManager { throwExceptionOnFail = throwExceptionOnFail };
        }

        /// <summary>
        /// Adds a transaction to the manager
        /// </summary>
        /// <param name="transaction"> </param>
        /// <param name="onExecute"> </param>
        /// <returns> </returns>
        public TransactionManager AddTransaction(Transaction transaction, Action<Transaction> onExecute = null)
        {
            transactions.Add(new TransactionHandler { transaction = transaction, onExecute = onExecute });
            return this;
        }

        /// <summary>
        /// Executes all transactions in the order they were added
        /// </summary>
        /// <param name="onExecuteError"> </param>
        /// <param name="onRollbackError"> </param>
        public void Execute(Action<TransactionException> onExecuteError = null, Action<TransactionException> onRollbackError = null)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                var executeTransaction = transactions[i];
                if (executeTransaction.transaction != null)
                {
                    try
                    {
                        //Try to execute the transaction.
                        executeTransaction.transaction.Execute();
                        executeTransaction.onExecute?.Invoke(executeTransaction.transaction);
                    }
                    catch (Exception executeException)
                    {
                        //If a transaction failed, rollback all previous transactions.
                        Rollback(onRollbackError);

                        var transactionException = new TransactionException($"Transaction {i + 1} ({executeTransaction.GetType()}) failed execution with error: {executeException.Message}", executeTransaction.transaction, executeException);

                        try
                        {
                            onExecuteError?.Invoke(transactionException);
                        }
                        catch { }

                        if (throwExceptionOnFail)
                            throw transactionException;
                    }
                }
            }
        }

        /// <summary>
        /// Rollbacks all transactions in the reverse order they were added
        /// </summary>
        /// <param name="onRollbackError"> </param>
        public void Rollback(Action<TransactionException> onRollbackError = null)
        {
            for (int i = transactions.Count - 1; i >= 0; i--)
            {
                var rollback = transactions[i];
                if (rollback.transaction != null)
                {
                    try
                    {
                        rollback.transaction.Rollback();
                    }
                    catch (Exception e)
                    {
                        var transactionException = new TransactionException($"Transaction {i + 1} ({rollback.GetType()}) failed rollback with error: {e.Message}", rollback.transaction, e);

                        try
                        {
                            onRollbackError?.Invoke(transactionException);
                        }
                        catch { }

                        if (throwExceptionOnFail)
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