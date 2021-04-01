using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsingIHostedService
{
    //Because we use a timer in the example we add IDisposable so we can Dispose of it afterwards
    public class Worker : IHostedService, IDisposable
    {
        private bool isDisposed;
        private Timer timer;

        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger) => this.logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            //Register a timer that will call the method DoWork every fifth second

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync");

            //Set the timer to stop trigger
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                logger.LogInformation("Dispose");

                // free managed resources
                timer?.Dispose();
            }

            // free native resources if there are any.
            isDisposed = true;
        }

        //Using DateTimeOffset instead of DateTime to get UTC time
        public void DoWork(object state)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }
    }
}