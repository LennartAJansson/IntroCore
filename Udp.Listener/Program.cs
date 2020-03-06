using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Udp.Extensions;

namespace Udp.Listener
{
    static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .UseUdpSpeaker("SpeakerConfig")
                .UseUdpListener("ListenerConfig")

                .ConfigureServices((hostContext, services) =>
                    //{
                    //  services.AddUdpSpeaker(hostContext.Configuration, (configuration, speakerConfiguration)=> 
                    //  {
                    //      hostContext.Configuration.GetSection("SpeakerConfig").Bind(speakerConfiguration);
                    //  });
                    //}
                    services.AddHostedService<Worker>());
    }
}
