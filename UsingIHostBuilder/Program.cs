using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostBuilder
{
    internal class Program
    {
        //This example doesn't do anything, only for demo purpose of how to create an Hosted application
        private static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()

                .RunMyClass()

                .Run();
        }

        //The content of Main could be expressed like this:
        //IHostBuilder hostBuilder = CreateHostBuilder(args);
        //IHost host = hostBuilder.Build();
        //host.GetMyClass();
        //host.Run();

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddScoped<MyClass>());
        }
    }

    //If I would like to extend the functionality of an IHost, it could be something like this:
    public static class Extensions
    {
        public static IHost RunMyClass(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();

            scope.ServiceProvider
                .GetRequiredService<MyClass>()
                .Execute();

            return host;
        }
    }
}