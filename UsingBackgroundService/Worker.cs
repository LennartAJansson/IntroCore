using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UsingBackgroundService
{
    internal class Worker : BackgroundService
    {
        private Timer timer;

        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger)
            //Normally always call base implementation
            : base()
        {
            this.logger = logger;
            this.logger.LogInformation("Constructing Service");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            //Normally always call base implementation
            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ExecuteAsync");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            //Since BackgroundService declaration of ExecuteAsync is abstract we can not call base
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync");
            timer?.Change(Timeout.Infinite, 0);

            //Normally always call base implementation
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            logger.LogInformation("Dispose");
            timer?.Dispose();

            //Normally always call base implementation
            base.Dispose();
        }

        private void DoWork(object state) =>
            logger.LogInformation("DoWork");
    }
}
