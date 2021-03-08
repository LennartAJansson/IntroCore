using Udp.Abstract.Configuration;

namespace Udp.Core.Configuration
{
    public class UdpListenerConfig : IUdpListenerConfig
    {
        public const string SectionName = "UdpListenerConfig";

        public int BroadcastListenerPort { get; set; }

        public int SendListenerPort { get; set; }
    }
}