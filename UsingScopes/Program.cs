using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class Program
    {
        private static IHost host;

        //On line 49, alter the registration of InjectedService between AddScoped and AddTransient and run the application
        //The output will show if the InjectedService object is recreated between MyMainService and MySecondService or not
        //MySecondService will run within the scope of MyMainService
        public static async Task Main(string[] args)
        {
            //First we build the host so we can access its ServiceProvider
            host = CreateHostBuilder(args).Build();

            //Start the host
            await host.StartAsync();

            await PlayAsync();

            //Finally we stop the host and dispose it
            using (host)
            {
                await host.StopAsync();
            }
        }

        private static async Task PlayAsync()
        {
            var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Playing around a bit");

            //firstService will live through this scope
            var mySingleton = services.GetRequiredService<MySingleton>();
            await mySingleton.RunAsync();

            //secondService will live through this scope
            var secondService = services.GetRequiredService<MyThirdService>();
            await secondService.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MySingleton>();
                    services.AddScoped<MySecondService>();
                    services.AddScoped<MyThirdService>();

                    //TODO! Alter this between AddScoped and AddTransient and observe the difference in how it gets created in this example
                    //services.AddScoped<MyFourthService>();

                    services.AddTransient<MyBaseService>();
                });
    }
}