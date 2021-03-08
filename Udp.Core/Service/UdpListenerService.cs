using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

using Udp.Abstract.Configuration;
using Udp.Abstract.Contract;
using Udp.Abstract.Events;
using Udp.Abstract.Service;
using Udp.Core.Configuration;
using Udp.Core.Contract;
using Udp.Core.Extension;

namespace Udp.Core.Service
{
    public class UdpListenerService : IUdpListenerService
    {
        private IAsyncResult asyncResult;
        private UdpClient client;
        private readonly ILogger<UdpListenerService> logger;

        public IUdpListenerConfig ListenerConfig { get; set; }

        public event UdpMessageReceivedEventHandler MessageReceived;

        public UdpListenerService(ILogger<UdpListenerService> logger, IOptionsMonitor<UdpListenerConfig> options)
        {
            this.logger = logger;
            ListenerConfig = options.CurrentValue;
        }

        public void StartRead()
        {
            IPEndPoint listeningEndPoint = new IPEndPoint(IPAddress.Any, ListenerConfig.BroadcastListenerPort);

            logger.LogInformation($"Opening connection.");

            client = new UdpClient(listeningEndPoint)
            {
                EnableBroadcast = true
            };

            UdpState state = new UdpState
            {
                client = client,
                endPoint = listeningEndPoint
            };

            asyncResult = client.BeginReceive(new AsyncCallback(ReceiveCallback), state);

            logger.LogInformation($"Started listening on {ListenerConfig.BroadcastListenerPort} for broadcast and {ListenerConfig.SendListenerPort} for normal traffic");
        }

        //ReceiveCallback will be called asynchronously
        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpState state = (UdpState)ar.AsyncState;
            UdpClient stateClient = state.client;
            IPEndPoint stateEndPoint = state.endPoint;

            try
            {
                IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, ListenerConfig.BroadcastListenerPort);
                byte[] receivedBytes = stateClient.EndReceive(ar, ref receiveEndPoint);
                try
                {
                    string receivedString = Encoding.UTF8.GetString(receivedBytes);

                    //Translate the incoming IUdpMessage to a IUdpTransport
                    IUdpMessage message = JsonSerializer.Deserialize<UdpMessage>(receivedString);
                    IUdpTransportMessage transportMessage = message.ToUdpTransportMessage(receiveEndPoint.Address, receiveEndPoint.Port);

                    //Post this message as an event
                    MessageReceived?.Invoke(transportMessage);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                finally
                {
                    //Start receiving again
                    asyncResult = stateClient.BeginReceive(new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (ObjectDisposedException)
            {
                //This is, according to Microsoft, a perfectly normal way of ending a BeginReceive when closing the socket
                //https://social.msdn.microsoft.com/Forums/en-US/18a48d01-108c-4eff-b19e-bb59645d42f8/closing-a-socket-connection-created-using-udp-client?forum=vcgeneral
                logger.LogInformation($"Closed connection.");
            }
        }

        public void StopRead()
        {
            client.Close();
            logger.LogInformation($"Stopped listening on {ListenerConfig.BroadcastListenerPort} for broadcast and {ListenerConfig.SendListenerPort} for normal traffic");
        }
    }
}