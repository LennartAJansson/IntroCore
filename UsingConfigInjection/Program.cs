using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingConfigInjection
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<TimerSettings>(timerSettings =>
                        hostContext.Configuration.GetSection(TimerSettings.SectionName).Bind(timerSettings));

                    services.AddHostedService<Worker>();
                });
    }
}