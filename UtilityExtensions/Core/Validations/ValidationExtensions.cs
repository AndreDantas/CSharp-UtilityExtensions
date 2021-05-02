using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core.Validations
{
    /// <summary>
    /// Helper class to coordinate validations
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Adds a value to be validated
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static Validation<T> Add<T>(this Validation<T> item, T value, string paramName = "")
        {
            item.AddParameter(value, paramName);

            return item;
        }

        /// <summary>
        /// Validates all parameters with the <paramref name="validationFunc" />
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="validationFunc"> </param>
        /// <param name="validationError"> </param>
        private static Validation<T> Validate<T>(Validation<T> item, Func<T, bool> validationFunc, string validationError, [CallerMemberName] string validationName = "")
        {
            item.ValidateAll(new Validation<T>.Method(validationFunc, validationError, validationName));

            return item;
        }

        #region Generic

        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            return Validate(item, (v) => v != null, "can't be null");
        }

        #endregion Generic

        #region String

        [DebuggerHidden]
        public static Validation<string> ShorterThan(this Validation<string> item, int max)
        {
            return Validate(item, (v) => v.Length < max, $"must be shorter than {max} chars");
        }

        [DebuggerHidden]
        public static Validation<string> LongerThan(this Validation<string> item, int min)
        {
            return Validate(item, (v) => v.Length > min, $"must be longer than {min} chars");
        }

        [DebuggerHidden]
        public static Validation<string> HasLength(this Validation<string> item, int length)
        {
            return Validate(item, (v) => v.Length == length, $"must have a length of {length} chars");
        }

        [DebuggerHidden]
        public static Validation<string> StartsWith(this Validation<string> item, string pattern)
        {
            return Validate(item, (v) => v.StartsWith(pattern), $"must start with '{pattern}'");
        }

        [DebuggerHidden]
        public static Validation<string> EndsWith(this Validation<string> item, string pattern)
        {
            return Validate(item, (v) => v.EndsWith(pattern), $"must end with '{pattern}'");
        }

        [DebuggerHidden]
        public static Validation<string> NotEmpty(this Validation<string> item)
        {
            return Validate(item, (v) => !string.IsNullOrEmpty(v), "must not be empty");
        }

        [DebuggerHidden]
        public static Validation<string> DigitsOnly(this Validation<string> item)
        {
            return Validate(item, (v) => v.IsDigitsOnly(), "must only have digits");
        }

        [DebuggerHidden]
        public static Validation<string> ValidEmail(this Validation<string> item)
        {
            return Validate(item, (v) => v.IsValidEmail(), "must only have digits");
        }

        #endregion String

        #region Int

        [DebuggerHidden]
        public static Validation<int> InRange(this Validation<int> item, int min, int max)
        {
            return Validate(item, (v) => v >= min && v <= max, $"must be between {min} and {max}");
        }

        [DebuggerHidden]
        public static Validation<int> LessThan(this Validation<int> item, int max)
        {
            return Validate(item, (v) => v < max, $"must be less than {max}");
        }

        [DebuggerHidden]
        public static Validation<int> GreaterThan(this Validation<int> item, int min)
        {
            return Validate(item, (v) => v > min, $"must be greater than {min}");
        }

        [DebuggerHidden]
        public static Validation<int> EqualTo(this Validation<int> item, int target)
        {
            return Validate(item, (v) => v == target, $"must be equal to {target}");
        }

        #endregion Int

        #region Decimal

        [DebuggerHidden]
        public static Validation<decimal> InRange(this Validation<decimal> item, decimal min, decimal max)
        {
            return Validate(item, (v) => v >= min && v <= max, $"must be between {min} and {max}");
        }

        [DebuggerHidden]
        public static Validation<decimal> LessThan(this Validation<decimal> item, decimal max)
        {
            return Validate(item, (v) => v < max, $"must be less than {max}");
        }

        [DebuggerHidden]
        public static Validation<decimal> GreaterThan(this Validation<decimal> item, decimal min)
        {
            return Validate(item, (v) => v > min, $"must be greater than {min}");
        }

        [DebuggerHidden]
        public static Validation<decimal> EqualTo(this Validation<decimal> item, decimal target)
        {
            return Validate(item, (v) => v == target, $"must be equal to {target}");
        }

        #endregion Decimal

        #region List

        [DebuggerHidden]
        public static Validation<List<T>> NotEmpty<T>(this Validation<List<T>> item)
        {
            return Validate(item, (v) => !v?.IsEmpty() ?? false, "must not be empty");
        }

        #endregion List
    }
}