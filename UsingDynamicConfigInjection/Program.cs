using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingDynamicConfigInjection
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //https://docs.microsoft.com/en-gb/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1
                    //https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/fundamentals/configuration/options/samples/3.x/OptionsSample
                    //We are not using the traditional Bind concept here since we would like to be alerted about changes
                    services.Configure<TimerSettings>(hostContext.Configuration.GetSection(TimerSettings.SectionName));

                    services.AddHostedService<Worker>();
                });
    }
}