using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostExtensions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .DoSomethingBeforeRun()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<MyClass>();
                    services.AddHostedService<Worker>();
                });
    }
}
