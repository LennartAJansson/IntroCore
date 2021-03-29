using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace UsingIServiceProvider
{
    internal class Program
    {
        //Show difference between registrations
        //Show difference between async/sync main
        private static void Main(string[] args)
        {
            string envKind = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //GlobalGroup:GlobalValue
                .AddJsonFile($"appsettings.{envKind}.json", optional: true, reloadOnChange: true) //DevelopmentGroup:DevelopmentValue
                .AddEnvironmentVariables() //EnvGroup__EnvValue
                .AddCommandLine(args) //CmdGroup:CmdValue
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection()
                .AddScoped<DIUserClass>()
                .AddScoped<ITestService, TestService>()

                //Using Instance method, remember that you're responsible of disposing the object!
                .AddSingleton<IConfiguration>(configuration);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            using IServiceScope scope = serviceProvider.CreateScope();

            scope.ServiceProvider
                .GetService<DIUserClass>()
                .ExecuteAsync()
                .GetAwaiter();
        }
    }
}