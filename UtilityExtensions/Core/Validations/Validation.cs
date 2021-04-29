using System;
using System.Collections.Generic;

namespace UtilityExtensions.Core.Validations
{
    public abstract class Validation
    {
        public struct Settings
        {
            public bool throwExceptionOnFail;
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
    }

    public class Validation<T> : Validation
    {
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

        public struct Method
        {
            public Func<T, bool> execute;
            public string onFailError;

            public Method(Func<T, bool> function, string onFailError)
            {
                this.execute = function;
                this.onFailError = onFailError;
            }
        }

        public Settings settings { get; protected set; }
        protected List<Parameter> parameters = new List<Parameter>();

        public IReadOnlyCollection<Parameter> Parameters => parameters.AsReadOnly();

        protected Validation()
        {
        }

        protected Validation(Settings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Starts the validation chain by creating the ValidationManager object with settings
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static Validation<T> UseSettings(Settings settings)
        {
            return new Validation<T>(settings);
        }

        /// <summary>
        /// Adds a value to be validated
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> </param>
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static Validation<T> Add(T value, string paramName = "")
        {
            var v = new Validation<T>(new Settings { throwExceptionOnFail = true });
            v.parameters.Add(new Parameter(value, paramName));

            return v;
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
        public virtual Result[] ValidateAll(Method method)
        {
            if (method.execute == null)
                return null;

            Result[] results = new Result[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                Parameter param = parameters[i];
                Result result = new Result { state = Result.State.Success };
                result.paramName = param.Name;
                try
                {
                    if (!method.execute(param.Value))
                        result.state = Result.State.Failed;
                }
                catch (Exception e)
                {
                    result.state = Result.State.Exception;
                    result.exception = e;
                }

                if (!result.IsSuccess && settings.throwExceptionOnFail)
                    ThrowValidationException(method, result);

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
        protected virtual void ThrowValidationException(Method method, Result result)
        {
            throw new ValidationException($"Parameter {result.paramName} failed validation: {method.onFailError}", result.paramName, method.onFailError, result.exception);
        }
    }
}