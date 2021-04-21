using NUnit.Framework;
using System;
using UtilityExtensions.Core;

namespace UtilityExtensions_Test
{
    public class LogTest
    {
        [Test]
        public void ExceptionLog()
        {
            string nullstring = null;

            string log = "";
            try
            {
                nullstring.ToLower();
            }
            catch (Exception e)
            {
                log = Log.Exception(e);
            }

            Assert.IsNotEmpty(log);
        }
    }
}