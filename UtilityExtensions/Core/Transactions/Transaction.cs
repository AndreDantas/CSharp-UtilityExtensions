namespace UtilityExtensions.Core.Transactions
{
    public abstract class Transaction
    {
        public enum State
        {
            Start,
            Finished
        }

        public State state { get; private set; } = State.Start;

        public void Execute()
        {
            if (state == State.Start)
            {
                InternalExecute();
                state = State.Finished;
            }
        }

        public void Rollback()
        {
            if (state == State.Finished)
            {
                InternalRollback();
                state = State.Start;
            }
        }

        protected abstract void InternalExecute();

        protected abstract void InternalRollback();
    }
}