using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UtilityExtensions_Core.Classes.Validations;

namespace UtilityExtensions_Test
{
    public class ValidationTest
    {
        [Test]
        public void ValidateMultipleStrings_ValidateEmptyString()
        {
            try
            {
                var string1 = "123";
                var string2 = "321";
                var string3 = "";

                Validate.With(string1, nameof(string1))
                        .And(string2, nameof(string2))
                        .And(string3, nameof(string3))
                        .NotEmpty();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void ValidateMultipleStrings_ValidateLength()
        {
            try
            {
                var string1 = "123";
                var string2 = "321";
                var string3 = "12";

                Validate.With(string1, nameof(string1))
                        .And(string2, nameof(string2))
                        .And(string3, nameof(string3))
                        .NotEmpty()
                        .LongerThan(2);
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}