using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace UtilityExtensions_Core.Classes.Validations
{
    public static class Validate
    {
        /// <summary>
        /// Starts the validation chain by creating the Validation object and adding the first value
        /// to be validated.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="value"> </param>
        /// <param name="argName"> </param>
        /// <returns> </returns>
        public static Validation<T> With<T>(T value, string argName = "")
        {
            return new Validation<T>(value, argName);
        }

        /// <summary>
        /// Adds another value to be validated
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="value"> </param>
        /// <param name="argName"> </param>
        /// <returns> </returns>
        public static Validation<T> And<T>(this Validation<T> item, T value, string argName = "")
        {
            item.AddParameter(value, argName);

            return item;
        }

        #region Generic

        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            var result = item.ValidateAll((v) => v != null);

            if (!result.IsSuccess)
                throw new ArgumentNullException(result.paramName);

            return item;
        }

        #endregion Generic

        #region String

        [DebuggerHidden]
        public static Validation<string> ShorterThan(this Validation<string> item, int max)
        {
            var result = item.ValidateAll((v) => v.Length < max);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be shorter than {max} chars");

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> LongerThan(this Validation<string> item, int min)
        {
            var result = item.ValidateAll((v) => v.Length > min);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be longer than {min} chars");

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> HasLength(this Validation<string> item, int length)
        {
            var result = item.ValidateAll((v) => v.Length == length);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must have a length of {length} chars");

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> StartsWith(this Validation<string> item, string pattern)
        {
            var result = item.ValidateAll((v) => v.StartsWith(pattern));

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must start with '{pattern}'");

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> NotEmpty(this Validation<string> item)
        {
            var result = item.ValidateAll((v) => !string.IsNullOrEmpty(v));

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must not be empty");

            return item;
        }

        #endregion String

        #region Int

        [DebuggerHidden]
        public static Validation<int> InRange(this Validation<int> item, int min, int max)
        {
            var result = item.ValidateAll((v) => v >= min && v <= max);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be between {min} and {max}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> LessThan(this Validation<int> item, int max)
        {
            var result = item.ValidateAll((v) => v < max);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be less than {max}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> GreaterThan(this Validation<int> item, int min)
        {
            var result = item.ValidateAll((v) => v > min);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be greater than {min}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> EqualTo(this Validation<int> item, int target)
        {
            var result = item.ValidateAll((v) => v == target);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be equal to {target}");

            return item;
        }

        #endregion Int

        #region Decimal

        [DebuggerHidden]
        public static Validation<decimal> InRange(this Validation<decimal> item, decimal min, decimal max)
        {
            var result = item.ValidateAll((v) => v >= min && v <= max);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be between {min} and {max}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> LessThan(this Validation<decimal> item, decimal max)
        {
            var result = item.ValidateAll((v) => v < max);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be less than {max}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> GreaterThan(this Validation<decimal> item, decimal min)
        {
            var result = item.ValidateAll((v) => v > min);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be greater than {min}");

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> EqualTo(this Validation<decimal> item, decimal target)
        {
            var result = item.ValidateAll((v) => v == target);

            if (!result.IsSuccess)
                throw new ArgumentException($"Parameter {result.paramName} must be equal to {target}");

            return item;
        }

        #endregion Decimal
    }
}