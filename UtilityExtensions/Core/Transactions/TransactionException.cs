using System;

namespace UtilityExtensions.Core.Transactions
{
    public class TransactionException : Exception
    {
        public ITransaction transaction;

        public TransactionException(string message) : base(message)
        {
        }

        public TransactionException(string message, ITransaction transaction) : base(message)
        {
            this.transaction = transaction;
        }

        public TransactionException(string message, ITransaction transaction, Exception innerException) : base(message, innerException)
        {
            this.transaction = transaction;
        }
    }
}