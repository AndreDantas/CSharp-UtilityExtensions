namespace UtilityExtensions.Core.Transactions
{
    public abstract class Transaction
    {
        public enum State
        {
            Pending,
            Finished
        }

        public State state { get; private set; } = State.Pending;

        public bool Execute()
        {
            if (state == State.Pending)
            {
                InternalExecute();
                state = State.Finished;
                return true;
            }
            return false;
        }

        public bool Rollback()
        {
            if (state == State.Finished)
            {
                InternalRollback();
                state = State.Pending;
                return true;
            }
            return false;
        }

        protected abstract void InternalExecute();

        protected abstract void InternalRollback();
    }
}