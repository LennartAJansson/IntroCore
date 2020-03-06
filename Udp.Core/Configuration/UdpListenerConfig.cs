using Udp.Abstract.Configuration;

namespace Udp.Core.Configuration
{
    public class UdpListenerConfig : IUdpListenerConfig
    {
        public int BroadcastListenerPort { get; set; }
        public int SendListenerPort { get; set; }
    }
}
