using System;
using System.Collections.Generic;

namespace UtilityExtensions.Core.Validations
{
    public class Validation<T>
    {
        public class Parameter
        {
            public Parameter(T value, string name)
            {
                Value = value;
                Name = name;
            }

            public T Value { get; set; }
            public string Name { get; set; }
        }

        public struct Result
        {
            public enum State
            {
                Success,
                Failed,
                Exception
            }

            public bool IsSuccess => state == State.Success;
            public string paramName;
            public State state;
            public Exception exception;
        }

        private List<Parameter> parameters = new List<Parameter>();

        public IReadOnlyCollection<Parameter> Parameters { get => parameters.AsReadOnly(); }

        private Validation()
        {
        }

        public Validation(T value, string paramName)
        {
            AddParameter(value, paramName);
        }

        public void AddParameter(T value, string paramName)
        {
            parameters.Add(new Parameter(value, paramName));
        }

        /// <summary>
        /// Validates all parameters with the <paramref name="validationFunc" />
        /// </summary>
        /// <param name="validationFunc"> </param>
        /// <returns> </returns>
        public Result ValidateAll(Func<T, bool> validationFunc)
        {
            Result result = new Result { state = Result.State.Success };

            if (validationFunc == null)
                return result;

            try
            {
                foreach (var param in parameters)
                {
                    result.paramName = param.Name;
                    if (!validationFunc(param.Value))
                    {
                        result.state = Result.State.Failed;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                result.state = Result.State.Exception;
                result.exception = e;
            }

            return result;
        }
    }
}