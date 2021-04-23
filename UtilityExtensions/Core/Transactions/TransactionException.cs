using System;

namespace UtilityExtensions.Core.Transactions
{
    public class TransactionException : Exception
    {
        public Transaction transaction;

        public TransactionException(string message) : base(message)
        {
        }

        public TransactionException(string message, Transaction transaction) : base(message)
        {
            this.transaction = transaction;
        }

        public TransactionException(string message, Transaction transaction, Exception innerException) : base(message, innerException)
        {
            this.transaction = transaction;
        }
    }
}