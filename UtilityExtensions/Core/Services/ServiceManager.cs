using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityExtensions.Core.Services
{
    public abstract class ServiceManager<T> where T : IService
    {
        public struct Settings
        {
            public Action<T> OnBeforeExecute;
            public Action<T> OnAfterExecute;
            public Action<T, Exception> OnError;
        }

        protected Settings settings;

        protected List<T> services = new List<T>();
        public IReadOnlyCollection<T> Services => services.AsReadOnly();

        /// <summary>
        /// Adds a service to this manager
        /// </summary>
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"> </exception>
        /// <param name="service"> </param>
        public virtual void AddService(T service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            services.Add(service);
        }

        /// <summary>
        /// Runs all services in this manager
        /// </summary>
        /// <inheritdoc />
        public abstract void Run();
    }
}