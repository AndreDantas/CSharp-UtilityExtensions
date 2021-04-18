using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UtilityExtensions_Core.Classes;

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