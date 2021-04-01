using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Udp.Extensions;

namespace Udp.Listener
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                    services
                        .AddUdpSpeaker(hostContext.Configuration.GetSection("UdpSpeakerConfig"))
                        .AddUdpListener(hostContext.Configuration.GetSection("UdpListenerConfig"))
                        .AddHostedService<Worker>());
    }
}