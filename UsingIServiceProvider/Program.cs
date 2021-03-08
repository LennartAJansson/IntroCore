using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace UsingIServiceProvider
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddScoped<IDIUserClass, DIUserClass>()
                .AddScoped<ITestService, TestService>()
                .AddSingleton<IConfiguration>(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            serviceProvider
                .GetService<IDIUserClass>()
                .RunAsync()
                .GetAwaiter();
        }
    }
}