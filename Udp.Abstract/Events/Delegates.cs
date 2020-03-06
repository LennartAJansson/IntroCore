using Udp.Abstract.Contract;

namespace Udp.Abstract.Events
{
    public delegate void UdpMessageReceivedEventHandler(IUdpTransportMessage message);
}
