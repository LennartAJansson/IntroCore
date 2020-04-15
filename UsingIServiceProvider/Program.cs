using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsingIServiceProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddScoped<IDIUserClass, DIUserClass>()
                .AddScoped<ITestService, TestService>()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            serviceProvider
                .GetService<IDIUserClass>()
                .RunAsync()
                .GetAwaiter();
        }
    }
}
