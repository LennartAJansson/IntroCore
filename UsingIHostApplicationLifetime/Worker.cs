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
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IHostEnvironment hostEnvironment;
        private readonly TimerSettings timerSettings;

        public Worker(ILogger<Worker> logger,
                        IHostApplicationLifetime hostApplicationLifetime,
                        IHostEnvironment hostEnvironment,
                        IOptionsMonitor<TimerSettings> timerOptions)
            : base()
        {
            this.logger = logger;
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.hostEnvironment = hostEnvironment;
            timerSettings = timerOptions.CurrentValue;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            hostApplicationLifetime.ApplicationStopped.Register(OnStopped);

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
            logger.LogInformation("OnStarted, current environment: {environmentName}", hostEnvironment.EnvironmentName);

        private void OnStopping() =>
            logger.LogInformation("OnStopping, current environment: {environmentName}", hostEnvironment.EnvironmentName);

        private void OnStopped() =>
            logger.LogInformation("OnStopped, current environment: {environmentName}", hostEnvironment.EnvironmentName);

        public override void Dispose()
        {
            logger.LogInformation("Dispose");

            timer?.Dispose();

            base.Dispose();
        }

        private void DoWork(object state) =>
            logger.LogInformation("{message}: {time}", timerSettings.Message, DateTimeOffset.Now);
    }
}