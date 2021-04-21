using System;
using System.Collections.Generic;

namespace UtilityExtensions.Core.Transactions
{
    public sealed class TransactionManager
    {
        private List<ITransaction> transactions = new List<ITransaction>();
        private bool throwExceptionOnFail;

        private TransactionManager()
        {
        }

        /// <summary>
        /// Initiates the transaction manager
        /// </summary>
        /// <param name="throwExceptionOnFail"> </param>
        /// <returns> </returns>
        public static TransactionManager Begin(bool throwExceptionOnFail = true)
        {
            return new TransactionManager { throwExceptionOnFail = throwExceptionOnFail };
        }

        /// <summary>
        /// Adds a transaction to the manager
        /// </summary>
        /// <param name="transaction"> </param>
        /// <returns> </returns>
        public TransactionManager AddTransaction(ITransaction transaction)
        {
            transactions.Add(transaction);
            return this;
        }

        /// <summary>
        /// Executes all transactions
        /// </summary>
        /// <param name="onExecuteError"> </param>
        /// <param name="onRollbackError"> </param>
        public void Execute(Action<TransactionException> onExecuteError = null, Action<TransactionException> onRollbackError = null)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                var executeTransaction = transactions[i];
                if (executeTransaction != null)
                {
                    try
                    {
                        //Try to execute the transaction.
                        executeTransaction.Execute();
                    }
                    catch (Exception executeException)
                    {
                        //If a transaction failed, rollback all previous transactions.
                        for (int j = i - 1; j >= 0; j--)
                        {
                            var rollbackTransaction = transactions[i];
                            try
                            {
                                rollbackTransaction.Rollback();
                            }
                            catch (Exception rollbackException)
                            {
                                try
                                {
                                    onRollbackError?.Invoke(new TransactionException($"Transaction {j + 1} ({rollbackTransaction.GetType()}) failed rollback with error: {rollbackException.Message}", rollbackTransaction, rollbackException));
                                }
                                catch { }
                            }
                        }
                        var transactionException = new TransactionException($"Transaction {i + 1} ({executeTransaction.GetType()}) failed execution with error: {executeException.Message}", executeTransaction, executeException);

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
        /// Removes all transactions from the manager
        /// </summary>
        /// <returns> </returns>
        public void Clear()
        {
            transactions.Clear();
        }
    }
}