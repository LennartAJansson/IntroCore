using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingUserSecrets
{
    public class Program
    {
        //Add following block to %appdata%\Microsoft\UserSecrets\UsingUserSecrets-3E60BD6A-C1D3-4ED8-8B90-D971C0C669FA\secret.json
        /*
        "SampleUserSecret": {
            "ConnectionString": "This is my connectionstring"
        }
        */
        //Fastest way is to right click your project and select Manage User Secrets
        //https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                //In previous versions we needed to use following configuration, now we only need to add the reference
                //.ConfigureAppConfiguration(config =>
                //    config.AddUserSecrets<Program>(optional: true))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<SampleUserSecret>(options =>
                        hostContext.Configuration.GetSection("SampleUserSecret").Bind(options));
                    services.AddHostedService<Worker>();
                });
    }
}
