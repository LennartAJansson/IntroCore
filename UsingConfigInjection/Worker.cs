using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsingConfigInjection
{
    internal class Worker : BackgroundService
    {
        private Timer timer;

        private readonly ILogger<Worker> logger;
        private readonly TimerSettings timerSettings;

        public Worker(ILogger<Worker> logger, IOptions<TimerSettings> timerOptions)
            : base()
        {
            this.logger = logger;
            timerSettings = timerOptions.Value;
            this.logger.LogInformation("Constructing Service");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ExecuteAsync");

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(timerSettings.TimerSeconds));

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync");
            timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            logger.LogInformation("Dispose");
            timer?.Dispose();

            base.Dispose();
        }

        private void DoWork(object state) =>
            logger.LogInformation($"{DateTimeOffset.Now} - Doing work");
    }
}