using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

using Udp.Abstract.Contract;
using Udp.Abstract.Service;
using Udp.Extensions;

namespace Udp.TimedSender
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private Timer timer;
        private readonly IUdpSpeakerService speaker;
        private readonly TimerSettings timerSettings;

        public Worker(ILogger<Worker> logger, IUdpSpeakerService speaker, IOptions<TimerSettings> options)
            : base()
        {
            this.logger = logger;
            this.speaker = speaker;
            this.timerSettings = options.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(timerSettings.TimerSeconds));

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            timer?.Dispose();

            base.Dispose();
        }

        private void DoWork(object state)
        {
            string messageText = $"Sending from Udp.TimedSender at {DateTimeOffset.Now}";
            logger.LogInformation($"Broadcasting message [{messageText}] to port {speaker.SpeakerConfig.BroadcastPort}");

            IUdpMessage message = UdpMessageFactory.CreateUdpMessage(messageText);

            IUdpTransportMessage response = speaker.BroadcastWithResponse(message, speaker.SpeakerConfig.BroadcastPort);

            logger.LogInformation($"Received response from {response.Address}:{response.Port}: [{response.Message.Text}]");
        }
    }
}

//192.168.0.0
//192.168.0.255
//255.255.255.0
//192.168.1 - 192.168.0.254

//10.0.0.0
//255.0.0.0
//10.255.255.255
//10.0.0.1 - 10.255.255.254
//10.0.0.1 = 10.255.255.254

//10.0.0.0
//255.255.0.0
//10.0.0.1 != 10.1.0.1