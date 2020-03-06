using System.Net;

using Udp.Abstract.Configuration;
using Udp.Abstract.Contract;

namespace Udp.Abstract.Service
{
    public interface IUdpSpeakerService
    {
        IUdpSpeakerConfig SpeakerConfig { get; set; }
        void Send(IUdpMessage message, IPAddress address, int port);
        IUdpTransportMessage SendWithResponse(IUdpMessage message, IPAddress address, int port);
        void Broadcast(IUdpMessage message, int port);
        IUdpTransportMessage BroadcastWithResponse(IUdpMessage message, int port);
    }
}
