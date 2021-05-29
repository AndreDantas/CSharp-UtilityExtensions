using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace UtilityExtensions.Core.Services
{
    public class BackgroundServiceManager : ServiceManager<BackgroundService>
    {
        private class ServiceHandler
        {
            public BackgroundService service;
            public BackgroundWorker worker;
        }

        private List<ServiceHandler> handlers = new List<ServiceHandler>();

        /// <summary>
        /// </summary>
        /// <exception cref="ArgumentNullException"> </exception>
        /// <param name="service"> </param>
        public override void AddService(BackgroundService service)
        {
            base.AddService(service);

            var bw = new BackgroundWorker();

            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                while (service.enabled)
                {
                    try
                    {
                        settings.OnBeforeExecute?.Invoke(service);

                        service.Execute();

                        settings.OnAfterExecute?.Invoke(service);

                        Thread.Sleep(service.interval);
                    }
                    catch (Exception ex)
                    {
                        settings.OnError?.Invoke(service, ex);
                    }
                }
            });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate (object o, RunWorkerCompletedEventArgs args)
            {
                if (service.enabled)
                    bw.RunWorkerAsync();
            });

            handlers.Add(new ServiceHandler { service = service, worker = bw });
        }

        /// <summary>
        /// </summary>
        /// <exception cref="ArgumentNullException"> </exception>
        /// <param name="services"> </param>
        public void AddServices(List<BackgroundService> services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            foreach (var service in services)
            {
                AddService(service);
            }
        }

        public override void Run()
        {
            foreach (var handler in handlers)
            {
                if (!handler.worker.IsBusy)
                    handler.worker.RunWorkerAsync();
            }
        }
    }
}