namespace UtilityExtensions.Core.Transactions
{
    public interface ITransaction
    {
        void Execute();

        void Rollback();
    }
}