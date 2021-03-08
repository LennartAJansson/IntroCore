using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Udp.TimedSender.Extensions
{
    public static class InternalExtensions
    {
        public static IHostBuilder UseServiceTimer(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
                serviceCollection.Configure<TimerSettings>(options =>
                    hostBuilderContext.Configuration.GetSection(TimerSettings.SectionName).Bind(options)));

            return hostBuilder;
        }
    }
}