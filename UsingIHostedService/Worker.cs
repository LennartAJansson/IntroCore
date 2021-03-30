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
        private readonly ILogger<Worker> logger;
        private Status status = new Status();
        private Timer timer;

        public Worker(ILogger<Worker> logger) => this.logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            //Register a timer that will call the method DoWork every fifth second

            status.CurrentStatus = "running";

            timer = new Timer(DoWork, status, TimeSpan.Zero, TimeSpan.FromSeconds(5));

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
            logger.LogInformation("Dispose");

            timer?.Dispose();
        }

        //Using DateTimeOffset instead of DateTime to get UTC time
        public void DoWork(object state)
        {
            string s = $"CurrentStatus is {(state as Status).CurrentStatus}";
            if (state != null && state is Status)
                logger.LogInformation("Worker running {status} at: {time}", (state as Status).CurrentStatus, DateTimeOffset.Now);
            else
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }
    }

    internal class Status
    {
        public string CurrentStatus { get; set; }
    }
}