using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

namespace UsingBackgroundService
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            //Show simplified!
            IHostBuilder host = Host.CreateDefaultBuilder(args);
            host.ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddHostedService<Worker2>();
                services.AddHostedService<Worker3>();
                services.AddHostedService<Worker4>();
            });
            return host;
        }
    }

    internal class Worker4 : BackgroundService
    {
        private readonly ILogger<Worker4> logger;

        public Worker4(ILogger<Worker4> logger)
        {
            this.logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Executing");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }

    internal class Worker3 : BackgroundService
    {
        private readonly ILogger<Worker3> logger;

        public Worker3(ILogger<Worker3> logger)
        {
            this.logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Executing");
            return Task.CompletedTask;
        }
    }

    internal class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> logger;

        public Worker2(ILogger<Worker2> logger)
        {
            this.logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Executing");
            return Task.CompletedTask;
        }
    }
}