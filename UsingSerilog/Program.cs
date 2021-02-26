using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace UsingSerilog
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration))

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();

                    services.Configure<TimerSettings>(options =>
                        hostContext.Configuration.GetSection("TimerSettings").Bind(options));

                    services.AddHostedService<Worker>();
                });
    }
}