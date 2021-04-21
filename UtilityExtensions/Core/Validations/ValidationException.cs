using System;

namespace UtilityExtensions.Core.Validations
{
    public class ValidationException : Exception
    {
        public string paramName;

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, string paramName) : base(message)
        {
            this.paramName = paramName;
        }

        public ValidationException(string message, string paramName, Exception innerException) : base(message, innerException)
        {
            this.paramName = paramName;
        }
    }
}