using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Udp.Abstract.Service;
using Udp.Core.Configuration;
using Udp.Core.Service;

namespace Udp.Extensions
{
    public static class UdpMiddlewareExtensions
    {
        public static IHostBuilder UseUdpListener(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                serviceCollection.Configure<UdpListenerConfig>(options =>
                    hostBuilderContext.Configuration.GetSection(UdpListenerConfig.SectionName).Bind(options));

                serviceCollection.AddTransient<IUdpListenerService, UdpListenerService>();
            });

            return hostBuilder;
        }

        public static IHostBuilder UseUdpSpeaker(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                serviceCollection.Configure<UdpSpeakerConfig>(options =>
                    hostBuilderContext.Configuration.GetSection(UdpSpeakerConfig.SectionName).Bind(options));

                serviceCollection.AddTransient<IUdpSpeakerService, UdpSpeakerService>();
            });

            return hostBuilder;
        }

        public static IServiceCollection AddUdpSpeaker(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<UdpSpeakerConfig>(options =>
                configuration.GetSection(UdpSpeakerConfig.SectionName).Bind(options));

            serviceCollection.AddTransient<IUdpSpeakerService, UdpSpeakerService>();

            return serviceCollection;
        }
    }
}