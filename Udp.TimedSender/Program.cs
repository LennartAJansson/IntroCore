using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Udp.Extensions;
using Udp.TimedSender.Extensions;

namespace Udp.TimedSender
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceTimer()
                .ConfigureServices((hostContext, services) =>
                    services
                        .AddUdpSpeaker(hostContext.Configuration.GetSection("UdpSpeakerConfig"))
                        .AddHostedService<Worker>());
    }
}