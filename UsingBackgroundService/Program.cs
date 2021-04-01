using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            });
            return host;
        }
    }
}