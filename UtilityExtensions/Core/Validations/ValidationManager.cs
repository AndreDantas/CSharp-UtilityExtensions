using System;
using System.Collections.Generic;

namespace UtilityExtensions.Core.Validations
{
    public class ValidationManager<T>
    {
        public struct Settings
        {
            public bool throwExceptionOnFail;
        }

        public class Parameter
        {
            public T Value { get; set; }
            public string Name { get; set; }
            private List<Result> results = new List<Result>();
            public IReadOnlyCollection<Result> Results => results.AsReadOnly();

            public Parameter(T value, string name)
            {
                Value = value;
                Name = name;
            }

            public void AddResult(Result result)
            {
                this.results.Add(result);
            }
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

        public struct Validation
        {
            public Func<T, bool> function;
            public string onFailError;

            public Validation(Func<T, bool> function, string onFailError)
            {
                this.function = function;
                this.onFailError = onFailError;
            }
        }

        public Settings settings { get; private set; }
        private List<Parameter> parameters = new List<Parameter>();

        public IReadOnlyCollection<Parameter> Parameters => parameters.AsReadOnly();

        public ValidationManager(Settings settings)
        {
            this.settings = settings;
        }

        public ValidationManager(T value, string paramName)
        {
            AddParameter(value, paramName);
        }

        /// <summary>
        /// Adds a new parameter to this ValidationManager
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        public virtual void AddParameter(T value, string paramName)
        {
            parameters.Add(new Parameter(value, paramName));
        }

        /// <summary>
        /// Returns a list with the parameters that failed any validations
        /// </summary>
        /// <param name="item"> </param>
        /// <returns> </returns>
        public List<Parameter> FailedParameters()
        {
            var failedParameters = new List<Parameter>();

            foreach (var param in parameters)
            {
                foreach (var result in param.Results)
                {
                    if (!result.IsSuccess)
                    {
                        failedParameters.Add(param);
                        break;
                    }
                }
            }

            return failedParameters;
        }

        /// <summary>
        /// Validates all parameters
        /// </summary>
        /// <returns> </returns>
        public virtual Result[] ValidateAll(Validation validation)
        {
            if (validation.function == null)
                return null;

            Result[] results = new Result[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                Parameter param = parameters[i];
                Result result = new Result { state = Result.State.Success };
                result.paramName = param.Name;
                try
                {
                    if (!validation.function(param.Value))
                        result.state = Result.State.Failed;
                }
                catch (Exception e)
                {
                    result.state = Result.State.Exception;
                    result.exception = e;
                }

                if (!result.IsSuccess && settings.throwExceptionOnFail)
                    ThrowValidationException(validation, result);

                param.AddResult(result);
                results[i] = result;
            }

            return results;
        }

        /// <summary>
        /// Throws a ValidationException
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="message"> </param>
        /// <param name="result"> </param>
        private static void ThrowValidationException(Validation validation, Result result)
        {
            throw new ValidationException($"Parameter {result.paramName} failed validation: {validation.onFailError}", result.paramName, validation.onFailError, result.exception);
        }
    }
}