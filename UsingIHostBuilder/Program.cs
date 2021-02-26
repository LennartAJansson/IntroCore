using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostBuilder
{
    internal class Program
    {
        //This example doesn't do anything, only for demo purpose of how to create an Hosted application
        private static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .GetMyClass()
                .Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                services.AddScoped<MyClass>());
    }

    public static class Extensions
    {
        public static IHost GetMyClass(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            scope.ServiceProvider.GetService<MyClass>().Execute();
            return host;
        }
    }
}