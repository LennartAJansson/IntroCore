using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Udp.TimedSender.Extensions
{
    public static class InternalExtensions
    {
        public static IHostBuilder UseServiceTimer(this IHostBuilder hostBuilder, string configGroup)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                serviceCollection.Configure<TimerSettings>(options =>
                    hostBuilderContext.Configuration.GetSection(configGroup).Bind(options));
            });

            return hostBuilder;
        }
    }
}