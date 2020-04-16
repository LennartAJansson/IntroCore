using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UsingDynamicConfigInjection
{
    internal class Worker : BackgroundService
    {
        private Timer timer;

        private readonly ILogger<Worker> logger;
        //https://docs.microsoft.com/en-gb/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1
        //https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/fundamentals/configuration/options/samples/3.x/OptionsSample
        //
        //If you test this functionality then remember that if you execute in debug you will have to change the appsettings.json 
        //located in the main project folder, NOT the one that is copied to the build output!
        //

        private TimerSettings timerSettings;

        public string CurrentState { get; set; }

        public Worker(ILogger<Worker> logger, IOptionsMonitor<TimerSettings> options)
            : base()
        {
            this.logger = logger;
            timerSettings = options.CurrentValue;
            options.OnChange(OnSettingsChange);

            this.logger.LogInformation("Constructing Service");
        }

        private void OnSettingsChange(TimerSettings timerSettings)
        {
            this.timerSettings = timerSettings;
            timer?.Change(TimeSpan.Zero, TimeSpan.FromSeconds(timerSettings.TimerSeconds));
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
