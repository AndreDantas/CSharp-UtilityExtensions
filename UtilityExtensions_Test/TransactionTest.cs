using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using UtilityExtensions.Core.Transactions;
using NUnit.Framework;

namespace UtilityExtensions_Test
{
    public class TransactionTest
    {
        private class SuccessTransaction : Transaction
        {
            public SuccessTransaction(int value)
            {
                this.value = value;
            }

            public int value { get; private set; }

            protected override void InternalExecute()
            {
                value += 10;
            }

            protected override void InternalRollback()
            {
                value -= 10;
            }
        }

        private class FailTransaction : Transaction
        {
            public FailTransaction(string value)
            {
                this.value = value;
            }

            public string value { get; private set; }

            protected override void InternalExecute()
            {
                value = value.ToLower();
            }

            protected override void InternalRollback()
            {
            }
        }

        [Test]
        public void ExecuteSuccessTransaction()
        {
            var tran1 = new SuccessTransaction(10);
            TransactionManager.Init().AddTransaction(tran1).Execute();

            Assert.IsTrue(tran1.value == 20);
        }

        [Test]
        public void RollbackSuccessTransaction()
        {
            var tran1 = new SuccessTransaction(10);
            var tm = TransactionManager.Init().AddTransaction(tran1);
            tm.Execute();
            tm.Rollback();

            Assert.IsTrue(tran1.value == 10);
        }
        [Test]
        public void ExecuteFailTransaction()
        {
            var tran1 = new FailTransaction(null);
            try
            {
                TransactionManager.Init().AddTransaction(tran1).Execute();

            }
            catch (TransactionException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

    }
}