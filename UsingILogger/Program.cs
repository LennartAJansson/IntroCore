using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace UsingILogger
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var spBuilder = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole().AddConfiguration(configuration))
                .AddScoped<DIUserClass>()
                .AddScoped<ITestService, TestService>()
                .AddSingleton<IConfiguration>(configuration);

            var serviceProvider = spBuilder.BuildServiceProvider();

            Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            serviceProvider
                .GetService<DIUserClass>()
                .RunAsync()
                .GetAwaiter();

            //Needed to let the ILogger flush its content before main thread exits.
            //It will dispose all object created by the ServiceProvider, ILogger fx

            serviceProvider.Dispose();
        }
    }
}