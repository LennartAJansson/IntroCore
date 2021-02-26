using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

using Udp.Abstract.Contract;
using Udp.Abstract.Service;
using Udp.Extensions;

namespace Udp.Listener
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IUdpSpeakerService speaker;
        private readonly IUdpListenerService listener;

        public Worker(ILogger<Worker> logger, IUdpSpeakerService speaker, IUdpListenerService listener)
            : base()
        {
            this.logger = logger;
            this.speaker = speaker;
            this.listener = listener;
            this.listener.MessageReceived += ListenerMessageReceived;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            listener.StartRead();
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            listener.MessageReceived -= ListenerMessageReceived;
            listener.StopRead();

            await base.StopAsync(cancellationToken);
        }

        private void ListenerMessageReceived(IUdpTransportMessage context)
        {
            string response = $"Received from {context.Address}:{context.Port}: {context.Message.Text}";
            logger.LogInformation(response);

            speaker.Send(UdpMessageFactory.CreateUdpMessage(response), context.Address, context.Port);
        }
    }
}