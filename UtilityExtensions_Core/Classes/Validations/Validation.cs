using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace UtilityExtensions_Core.Classes.Validations
{
    public class Validation<T>
    {
        public class ValidationParameter
        {
            public ValidationParameter(T value, string name)
            {
                Value = value;
                Name = name;
            }

            public T Value { get; set; }
            public string Name { get; set; }
        }

        public struct ValidationResult
        {
            public enum State
            {
                Success,
                ValidationFailed,
                Exception
            }

            public bool IsSuccess => state == State.Success;
            public string paramName;
            public State state;
        }

        private List<ValidationParameter> parameters = new List<ValidationParameter>();

        public IReadOnlyCollection<ValidationParameter> Parameters { get => parameters.AsReadOnly(); }

        private Validation()
        {
        }

        public Validation(T value, string argName)
        {
            AddParameter(value, argName);
        }

        public void AddParameter(T value, string argName)
        {
            parameters.Add(new ValidationParameter(value, argName));
        }

        /// <summary>
        /// Validates all parameters with the <paramref name="validationFunc" />
        /// </summary>
        /// <param name="validationFunc"> </param>
        /// <returns> </returns>
        public ValidationResult ValidateAll(Func<T, bool> validationFunc)
        {
            ValidationResult result = new ValidationResult { state = ValidationResult.State.Success };

            if (validationFunc == null)
                return result;

            try
            {
                foreach (var param in parameters)
                {
                    result.paramName = param.Name;
                    if (!validationFunc(param.Value))
                    {
                        result.state = ValidationResult.State.ValidationFailed;
                        break;
                    }
                }
            }
            catch
            {
                result.state = ValidationResult.State.Exception;
            }

            return result;
        }
    }
}