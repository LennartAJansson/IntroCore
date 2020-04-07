using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UsingScopes
{
    internal class Program
    {
        //On line 49, alter the registration of InjectedService between AddScoped and AddTransient and run the application
        //The output will show if the InjectedService object is recreated between MyMainService and MySecondService or not
        //MySecondService will run within the scope of MyMainService
        public static async Task Main(string[] args)
        {
            //First we build the Host so we can access its ServiceProvider
            var host = CreateHostBuilder(args).Build();

            //Using the ServiceProvider we can create our own intermediate scope
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    //scopedService will only live through this scope
                    var scopedService = services.GetRequiredService<MyMainService>();
                    await scopedService.RunAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred.");
                }
            }

            //Finally we run the Host
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScoped<MyMainService>();
                    services.AddScoped<MySecondService>();
                    //TODO! Alter this between AddScoped and AddTransient and observe the difference in how it gets created in this example
                    //services.AddScoped<InjectedService>();
                    services.AddTransient<InjectedService>();
                });
    }

    internal class MyMainService
    {
        private readonly ILogger<MyMainService> logger;
        private readonly InjectedService injectedService;
        private readonly MySecondService mySecondService;

        //We inject our first InjectedService here
        public MyMainService(ILogger<MyMainService> logger, InjectedService injectedService, MySecondService mySecondService)
        {
            this.logger = logger;
            this.injectedService = injectedService;
            this.mySecondService = mySecondService;
        }

        public async Task RunAsync()
        {
            logger.LogInformation($"In MyMainService, Injected service id: {injectedService.Id.ToString()}");
            await mySecondService.RunAsync();
        }
    }

    internal class MySecondService
    {
        private readonly ILogger<MySecondService> logger;
        private readonly InjectedService injectedService;

        //We inject our second InjectedService here
        public MySecondService(ILogger<MySecondService> logger, InjectedService injectedService)
        {
            this.logger = logger;
            this.injectedService = injectedService;
        }

        public Task RunAsync()
        {
            logger.LogInformation($"In MySecondService, Injected service id: {injectedService.Id.ToString()}");
            return Task.CompletedTask;
        }
    }

    internal class InjectedService
    {
        public Guid Id { get; set; }

        public InjectedService()
        {
            Id = Guid.NewGuid();
        }
    }
}