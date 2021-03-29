using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace UsingILogger
{
    public class Program
    {
        //Show difference between registrations
        //Show difference between async/sync main
        private static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            IServiceCollection spBuilder = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole().AddConfiguration(configuration))
                .AddScoped<DIUserClass>()
                .AddScoped<ITestService, TestService>()

                //Using ImplementationFactory method
                .AddSingleton<IConfiguration>(sp => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build());

            ServiceProvider serviceProvider = spBuilder.BuildServiceProvider();

            Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            using IServiceScope scope = serviceProvider.CreateScope();

            scope.ServiceProvider
                .GetService<DIUserClass>()
                .ExecuteAsync()
                .GetAwaiter();

            //Need to dispose the ServiceProvider to let the ILogger flush its content before main thread exits.
            //But since we are using 'using' it will be handled automatically
            //Try doing the same without using and the logger will not be able to finish its flush in proper time
        }
    }
}