using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Udp.Abstract.Service;
using Udp.Core.Configuration;
using Udp.Core.Service;

namespace Udp.Extensions
{
    public static class UdpMiddlewareExtensions
    {
        public static IServiceCollection AddUdpListener(this IServiceCollection serviceCollection, IConfigurationSection section)
        {
            serviceCollection.Configure<UdpConfig>(options => section.Bind(options));

            serviceCollection.AddTransient<IUdpListenerService, UdpListenerService>();

            return serviceCollection;
        }

        public static IServiceCollection AddUdpSpeaker(this IServiceCollection serviceCollection, IConfigurationSection section)
        {
            serviceCollection.Configure<UdpConfig>(options => section.Bind(options));

            serviceCollection.AddTransient<IUdpSpeakerService, UdpSpeakerService>();

            return serviceCollection;
        }
    }
}