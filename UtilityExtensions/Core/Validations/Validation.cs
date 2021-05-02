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
    }

    public class Validation<T> : Validation
    {
        public class Parameter
        {
            public T value;
            public string name;
            private List<Result> results = new List<Result>();
            public IReadOnlyCollection<Result> Results => results.AsReadOnly();

            public Parameter(T value, string name)
            {
                this.value = value;
                this.name = name;
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
            public Method method;
        }

        public struct Method
        {
            public string name;
            public Func<T, bool> execute;
            public string onFailError;

            public Method(Func<T, bool> function, string onFailError, string name = "")
            {
                this.execute = function;
                this.onFailError = onFailError;
                this.name = name;
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
        /// <param name="value"> </param>
        /// <param name="paramName"> </param>
        /// <returns> </returns>
        public static Validation<T> Add(T value, string paramName = "")
        {
            var v = new Validation<T>(new Settings { throwExceptionOnFail = true });

            v.AddParameter(value, paramName);

            return v;
        }

        public void AddParameter(T value, string paramName = "")
        {
            parameters.Add(new Parameter(value, paramName));
        }

        /// <summary>
        /// Returns a list with the parameters that failed any validations
        /// </summary>
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
                Result result = new Result { method = method, paramName = param.name };

                try
                {
                    result.state = method.execute(param.value) ? Result.State.Success : Result.State.Failed;
                }
                catch (Exception e)
                {
                    result.state = Result.State.Exception;
                    result.exception = e;
                }

                if (!result.IsSuccess && settings.throwExceptionOnFail)
                    ThrowValidationException(result);

                param.AddResult(result);
                results[i] = result;
            }

            return results;
        }

        /// <summary>
        /// Throws a ValidationException
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="result"> </param>
        protected virtual void ThrowValidationException(Result result)
        {
            throw new ValidationException($"Parameter {result.paramName} failed validation: {result.method.onFailError}", result.paramName, result.method.onFailError, result.exception);
        }
    }
}