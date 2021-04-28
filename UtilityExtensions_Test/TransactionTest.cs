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
        private class Add10Transaction : Transaction
        {
            public int value { get; private set; }

            public Add10Transaction(int value)
            {
                this.value = value;
            }

            protected override void InternalExecute()
            {
                value += 10;
            }

            protected override void InternalRollback()
            {
                value -= 10;
            }
        }

        private class StringUpperCaseTransaction : Transaction
        {
            public string result { get; private set; }
            public string initial;

            public StringUpperCaseTransaction(string value)
            {
                initial = value;
                this.result = value;
            }

            protected override void InternalExecute()
            {
                result = result.ToUpper();
            }

            protected override void InternalRollback()
            {
                result = initial;
            }
        }

        [Test]
        public void ExecuteSuccessTransaction()
        {
            var tran1 = new Add10Transaction(10);
            TransactionManager.Init().AddTransaction(tran1).Execute();

            Assert.IsTrue(tran1.value == 20);
        }

        [Test]
        public void RollbackSuccessTransaction()
        {
            var tran1 = new Add10Transaction(10);
            var tm = TransactionManager.Init().AddTransaction(tran1);
            tm.Execute();
            tm.Rollback();

            Assert.IsTrue(tran1.value == 10);
        }

        [Test]
        public void ExecuteFailTransaction()
        {
            var tran1 = new StringUpperCaseTransaction(null);
            try
            {
                TransactionManager.Init().AddTransaction(tran1).Execute();
            }
            catch (TransactionException e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}