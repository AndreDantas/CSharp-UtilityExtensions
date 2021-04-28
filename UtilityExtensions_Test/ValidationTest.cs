using NUnit.Framework;
using System;
using UtilityExtensions.Core;
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

                Validator.Init<string>()
                        .Add(string1, nameof(string1))
                        .Add(string2, nameof(string2))
                        .Add(string3, nameof(string3))
                        .NotEmpty();
            }
            catch (Exception)
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

                Validator.Init<string>()
                        .Add(string1, nameof(string1))
                        .Add(string2, nameof(string2))
                        .Add(string3, nameof(string3))
                        .LongerThan(2);
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}