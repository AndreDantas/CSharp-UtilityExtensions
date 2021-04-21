using NUnit.Framework;
using System;
using UtilityExtensions.Core.Validations;

namespace UtilityExtensions_Test
{
    public class ValidationTest
    {
        [Test]
        public void ValidateStrings_EmptyString()
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
        public void ValidateStrings_InvalidLength()
        {
            try
            {
                var string1 = "123";
                var string2 = "321";
                var string3 = "12";

                Validate.With(string1, nameof(string1))
                        .And(string2, nameof(string2))
                        .And(string3, nameof(string3))
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