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

                ValidationManager<string>.Add(string1, nameof(string1))
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

                ValidationManager<string>.UseSettings(new ValidationManager.Settings { throwExceptionOnFail = true, validateImmediately = true })
                                         .Add(string1, nameof(string1))
                                         .Add(string2, nameof(string2))
                                         .Add(string3, nameof(string3))
                                         .ShorterThan(4)
                                         .LongerThan(2);
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void ValidateDecimal_InRange()
        {
            try
            {
                var decim1 = 30.23m;
                var decim2 = -13m;
                var decim3 = 2000m;

                ValidationManager<decimal>.Add(decim1, nameof(decim1))
                                         .Add(decim2, nameof(decim2))
                                         .Add(decim3, nameof(decim3))
                                         .InRange(-100, 100);
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}