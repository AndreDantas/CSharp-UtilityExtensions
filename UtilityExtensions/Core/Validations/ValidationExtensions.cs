using System;
using System.Collections.Generic;
using System.Diagnostics;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core.Validations
{
    /// <summary>
    /// Helper class to coordinate validations
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Starts the validation chain by creating the ValidationManager object
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static ValidationManager<T> Init<T>()
        {
            return Init(new ValidationManager<T>.Settings { throwExceptionOnFail = true });
        }

        /// <summary>
        /// Starts the validation chain by creating the ValidationManager object with settings
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static ValidationManager<T> Init<T>(ValidationManager<T>.Settings settings)
        {
            return new ValidationManager<T>(settings);
        }

        /// <summary>
        /// Add a value to be validated
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static ValidationManager<T> Add<T>(this ValidationManager<T> item, T value, string paramName = "")
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
        private static ValidationManager<T> Validate<T>(ValidationManager<T> item, Func<T, bool> validationFunc, string validationError)
        {
            item.ValidateAll(new ValidationManager<T>.Validation(validationFunc, validationError));

            return item;
        }

        #region Generic

        [DebuggerHidden]
        public static ValidationManager<T> NotNull<T>(this ValidationManager<T> item) where T : class
        {
            return Validate(item, (v) => v != null, "can't be null");
        }

        #endregion Generic

        #region String

        [DebuggerHidden]
        public static ValidationManager<string> ShorterThan(this ValidationManager<string> item, int max)
        {
            return Validate(item, (v) => v.Length < max, $"must be shorter than {max} chars");
        }

        [DebuggerHidden]
        public static ValidationManager<string> LongerThan(this ValidationManager<string> item, int min)
        {
            return Validate(item, (v) => v.Length > min, $"must be longer than {min} chars");
        }

        [DebuggerHidden]
        public static ValidationManager<string> HasLength(this ValidationManager<string> item, int length)
        {
            return Validate(item, (v) => v.Length == length, $"must have a length of {length} chars");
        }

        [DebuggerHidden]
        public static ValidationManager<string> StartsWith(this ValidationManager<string> item, string pattern)
        {
            return Validate(item, (v) => v.StartsWith(pattern), $"must start with '{pattern}'");
        }

        [DebuggerHidden]
        public static ValidationManager<string> EndsWith(this ValidationManager<string> item, string pattern)
        {
            return Validate(item, (v) => v.EndsWith(pattern), $"must end with '{pattern}'");
        }

        [DebuggerHidden]
        public static ValidationManager<string> NotEmpty(this ValidationManager<string> item)
        {
            return Validate(item, (v) => !string.IsNullOrEmpty(v), "must not be empty");
        }

        [DebuggerHidden]
        public static ValidationManager<string> DigitsOnly(this ValidationManager<string> item)
        {
            return Validate(item, (v) => v.IsDigitsOnly(), "must only have digits");
        }

        [DebuggerHidden]
        public static ValidationManager<string> ValidEmail(this ValidationManager<string> item)
        {
            return Validate(item, (v) => v.IsValidEmail(), "must only have digits");
        }

        #endregion String

        #region Int

        [DebuggerHidden]
        public static ValidationManager<int> InRange(this ValidationManager<int> item, int min, int max)
        {
            return Validate(item, (v) => v >= min && v <= max, $"must be between {min} and {max}");
        }

        [DebuggerHidden]
        public static ValidationManager<int> LessThan(this ValidationManager<int> item, int max)
        {
            return Validate(item, (v) => v < max, $"must be less than {max}");
        }

        [DebuggerHidden]
        public static ValidationManager<int> GreaterThan(this ValidationManager<int> item, int min)
        {
            return Validate(item, (v) => v > min, $"must be greater than {min}");
        }

        [DebuggerHidden]
        public static ValidationManager<int> EqualTo(this ValidationManager<int> item, int target)
        {
            return Validate(item, (v) => v == target, $"must be equal to {target}");
        }

        #endregion Int

        #region Decimal

        [DebuggerHidden]
        public static ValidationManager<decimal> InRange(this ValidationManager<decimal> item, decimal min, decimal max)
        {
            return Validate(item, (v) => v >= min && v <= max, $"must be between {min} and {max}");
        }

        [DebuggerHidden]
        public static ValidationManager<decimal> LessThan(this ValidationManager<decimal> item, decimal max)
        {
            return Validate(item, (v) => v < max, $"must be less than {max}");
        }

        [DebuggerHidden]
        public static ValidationManager<decimal> GreaterThan(this ValidationManager<decimal> item, decimal min)
        {
            return Validate(item, (v) => v > min, $"must be greater than {min}");
        }

        [DebuggerHidden]
        public static ValidationManager<decimal> EqualTo(this ValidationManager<decimal> item, decimal target)
        {
            return Validate(item, (v) => v == target, $"must be equal to {target}");
        }

        #endregion Decimal

        #region List

        [DebuggerHidden]
        public static ValidationManager<List<T>> NotEmpty<T>(this ValidationManager<List<T>> item)
        {
            return Validate(item, (v) => !v?.IsEmpty() ?? false, "must not be empty");
        }

        #endregion List
    }
}