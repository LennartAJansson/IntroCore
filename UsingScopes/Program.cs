using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class Program
    {
        private static IHost host;

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
            for (int loop = 1; loop < 3; loop++)
            {
                using var scope = host.Services.CreateScope();

                var services = scope.ServiceProvider;
                System.Console.WriteLine($"Creating a new scope");

                System.Console.WriteLine($"Creating the first instances of our three services, loop {loop}");
                var mySingleton = services.GetRequiredService<MySingletonService>();
                await mySingleton.RunAsync();

                var myScopedService = services.GetRequiredService<MyScopedService>();
                await myScopedService.RunAsync();

                var myTransientService = services.GetRequiredService<MyTransientService>();
                await myTransientService.RunAsync();

                System.Console.WriteLine($"Creating the second instances of our three services, loop {loop}");
                mySingleton = services.GetRequiredService<MySingletonService>();
                await mySingleton.RunAsync();

                myScopedService = services.GetRequiredService<MyScopedService>();
                await myScopedService.RunAsync();

                myTransientService = services.GetRequiredService<MyTransientService>();
                await myTransientService.RunAsync();
                System.Console.WriteLine();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MySingletonService>();
                    services.AddScoped<MyScopedService>();
                    services.AddTransient<MyTransientService>();
                });
    }
}