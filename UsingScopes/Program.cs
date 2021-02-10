using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace UsingScopes
{
    internal class Program
    {
        //On line 49, alter the registration of InjectedService between AddScoped and AddTransient and run the application
        //The output will show if the InjectedService object is recreated between MyMainService and MySecondService or not
        //MySecondService will run within the scope of MyMainService
        public static async Task Main(string[] args)
        {
            //First we build the host so we can access its ServiceProvider
            var host = CreateHostBuilder(args).Build();

            //Then we start the host
            await host.StartAsync();

            //Using the ServiceProvider we can create our own intermediate scope
            //If anything is created with AddScoped within this scope then it will be shared between all
            //If anything is created with AddTransient within this scope then it will be a new unique instance for all
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    //scopedService will only live through this scope
                    var scopedService = services.GetRequiredService<MyMainService>();
                    await scopedService.RunAsync();
                    var secondService = services.GetRequiredService<MyThirdService>();
                    await secondService.RunAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred.");
                }
            }

            //Finally we stop the host and dispose it
            using (host)
            {
                await host.StopAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScoped<MyMainService>();
                    services.AddScoped<MySecondService>();
                    services.AddScoped<MyThirdService>();

                    //TODO! Alter this between AddScoped and AddTransient and observe the difference in how it gets created in this example
                    services.AddScoped<InjectedService>();

                    //services.AddTransient<InjectedService>();
                });
    }
}