using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

using Udp.Abstract.Configuration;
using Udp.Abstract.Contract;
using Udp.Abstract.Service;
using Udp.Core.Configuration;
using Udp.Core.Contract;
using Udp.Core.Extension;

namespace Udp.Core.Service
{
    public class UdpSpeakerService : IUdpSpeakerService
    {
        private readonly ILogger<UdpSpeakerService> logger;

        public IUdpConfig SpeakerConfig { get; set; }

        public UdpSpeakerService(ILogger<UdpSpeakerService> logger, IOptions<UdpConfig> options)
        {
            this.logger = logger;
            SpeakerConfig = options.Value;
            logger.LogInformation($"Speaker: Speaking on {SpeakerConfig.SendPort}, Broadcasting on {SpeakerConfig.BroadcastPort}");
        }

        public void Send(IUdpMessage message, IPAddress address, int port) =>
            SendMessage(message, address, port, false);

        public IUdpTransportMessage SendWithResponse(IUdpMessage message, IPAddress address, int port) =>
            SendMessage(message, address, port, true);

        public void Broadcast(IUdpMessage message, int port) =>
            SendMessage(message, IPAddress.Broadcast, port, false);

        public IUdpTransportMessage BroadcastWithResponse(IUdpMessage message, int port) =>
            SendMessage(message, IPAddress.Broadcast, port, true);

        private IUdpTransportMessage SendMessage(IUdpMessage message, IPAddress targetAddress, int targetPort, bool waitForResponse)
        {
            IUdpTransportMessage udpResponse = new UdpTransportMessage { Address = IPAddress.None, Port = 0, Message = message };

            if (message == null)
                return udpResponse;

            IPEndPoint remoteEndPoint = new(targetAddress, targetPort);
            bool isBroadcast = targetAddress == IPAddress.Broadcast;

            using (UdpClient client = new(AddressFamily.InterNetwork))
            {
                if (isBroadcast)
                {
                    logger.LogInformation($"Broadcasting [{message.Text}] to ip:port {remoteEndPoint}");
                }
                else
                {
                    logger.LogInformation($"Sending [{message.Text}] to ip:port {remoteEndPoint}");
                }

                string toSend = JsonSerializer.Serialize<UdpMessage>(message as UdpMessage);
                logger.LogInformation($"Message: \"{toSend}\"");
                byte[] buf = Encoding.UTF8.GetBytes(toSend);

                try
                {
                    client.EnableBroadcast = isBroadcast;
                    client.Send(buf, buf.Length, remoteEndPoint);
                    client.EnableBroadcast = false;

                    if (waitForResponse)
                    {
                        IPEndPoint responseEndPoint = new(IPAddress.Any, 0);

                        //Receive blocks thread until message returns
                        byte[] receivedBytes = client.Receive(ref responseEndPoint);
                        string receivedString = Encoding.UTF8.GetString(receivedBytes);

                        var receivedMessage = JsonSerializer.Deserialize<UdpMessage>(receivedString);

                        udpResponse = receivedMessage.ToUdpTransportMessage(responseEndPoint.Address, responseEndPoint.Port);

                        logger.LogInformation($"Response from {udpResponse.Address}:{udpResponse.Port} is: {udpResponse.Message.Text}");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }

                client.Close();
            }

            return udpResponse;
        }
    }
}