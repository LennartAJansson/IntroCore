using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntroducingUserSecrets
{
    public static class Program
    {
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
                .ConfigureAppConfiguration(config =>
                    config.AddUserSecrets<SampleUserSecret>())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<SampleUserSecret>(options =>
                        hostContext.Configuration.GetSection("SampleUserSecret").Bind(options));
                    services.AddHostedService<Worker>();
                });
    }
}
