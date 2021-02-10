using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsingIHostApplicationLifetime
{
    internal class Worker : BackgroundService
    {
        private Timer timer;
        private readonly ILogger logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly TimerSettings timerSettings;

        public Worker(ILogger<Worker> logger,
                        IHostApplicationLifetime appLifetime,
                        IOptionsMonitor<TimerSettings> timerOptions)
            : base()
        {
            this.logger = logger;
            this.appLifetime = appLifetime;

            timerSettings = timerOptions.CurrentValue;
            logger.LogInformation("Constructing service");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

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

        private void OnStarted() =>
            logger.LogInformation("OnStarted");

        private void OnStopping() =>
            logger.LogInformation("OnStopping");

        private void OnStopped() =>
            logger.LogInformation("OnStopped");

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