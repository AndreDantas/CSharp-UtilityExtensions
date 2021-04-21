using System.Diagnostics;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core.Validations
{
    public static class Validate
    {
        /// <summary>
        /// Starts the validation chain by creating the Validation object and adding the first value
        /// to be validated.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static Validation<T> With<T>(T value, string paramName = "")
        {
            return new Validation<T>(value, paramName);
        }

        /// <summary>
        /// Adds another value to be validated
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static Validation<T> And<T>(this Validation<T> item, T value, string paramName = "")
        {
            item.AddParameter(value, paramName);

            return item;
        }

        /// <summary>
        /// Throws a ValidationException
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="message"> </param>
        /// <param name="result"> </param>
        private static void ThrowValidationException<T>(string message, Validation<T>.Result result)
        {
            throw new ValidationException(message, result.paramName, result.exception);
        }

        #region Generic

        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            var result = item.ValidateAll((v) => v != null);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} can't be null", result);

            return item;
        }

        #endregion Generic

        #region String

        [DebuggerHidden]
        public static Validation<string> ShorterThan(this Validation<string> item, int max)
        {
            var result = item.ValidateAll((v) => v.Length < max);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be shorter than {max} chars", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> LongerThan(this Validation<string> item, int min)
        {
            var result = item.ValidateAll((v) => v.Length > min);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be longer than {min} chars", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> HasLength(this Validation<string> item, int length)
        {
            var result = item.ValidateAll((v) => v.Length == length);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must have a length of {length} chars", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> StartsWith(this Validation<string> item, string pattern)
        {
            var result = item.ValidateAll((v) => v.StartsWith(pattern));

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must start with '{pattern}'", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> EndsWith(this Validation<string> item, string pattern)
        {
            var result = item.ValidateAll((v) => v.EndsWith(pattern));

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must end with '{pattern}'", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> NotEmpty(this Validation<string> item)
        {
            var result = item.ValidateAll((v) => !string.IsNullOrEmpty(v));

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must not be empty", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> DigitsOnly(this Validation<string> item)
        {
            var result = item.ValidateAll((v) => v.IsDigitsOnly());

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must only have digits", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> ValidEmail(this Validation<string> item)
        {
            var result = item.ValidateAll((v) => v.IsValidEmail());

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must only have digits", result);

            return item;
        }

        #endregion String

        #region Int

        [DebuggerHidden]
        public static Validation<int> InRange(this Validation<int> item, int min, int max)
        {
            var result = item.ValidateAll((v) => v >= min && v <= max);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be between {min} and {max}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> LessThan(this Validation<int> item, int max)
        {
            var result = item.ValidateAll((v) => v < max);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be less than {max}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> GreaterThan(this Validation<int> item, int min)
        {
            var result = item.ValidateAll((v) => v > min);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be greater than {min}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<int> EqualTo(this Validation<int> item, int target)
        {
            var result = item.ValidateAll((v) => v == target);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be equal to {target}", result);

            return item;
        }

        #endregion Int

        #region Decimal

        [DebuggerHidden]
        public static Validation<decimal> InRange(this Validation<decimal> item, decimal min, decimal max)
        {
            var result = item.ValidateAll((v) => v >= min && v <= max);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be between {min} and {max}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> LessThan(this Validation<decimal> item, decimal max)
        {
            var result = item.ValidateAll((v) => v < max);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be less than {max}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> GreaterThan(this Validation<decimal> item, decimal min)
        {
            var result = item.ValidateAll((v) => v > min);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be greater than {min}", result);

            return item;
        }

        [DebuggerHidden]
        public static Validation<decimal> EqualTo(this Validation<decimal> item, decimal target)
        {
            var result = item.ValidateAll((v) => v == target);

            if (!result.IsSuccess)
                ThrowValidationException($"Parameter {result.paramName} must be equal to {target}", result);

            return item;
        }

        #endregion Decimal
    }
}