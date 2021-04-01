using Udp.Abstract.Configuration;
using Udp.Abstract.Events;

namespace Udp.Abstract.Service
{
    public interface IUdpListenerService
    {
        IUdpConfig ListenerConfig { get; set; }
        void StartRead();
        void StopRead();

        event UdpMessageReceivedEventHandler MessageReceived;
    }
}
