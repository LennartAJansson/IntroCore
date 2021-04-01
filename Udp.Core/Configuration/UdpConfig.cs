using Udp.Abstract.Configuration;

namespace Udp.Core.Configuration
{
    public class UdpConfig : IUdpConfig
    {
        public int BroadcastPort { get; set; }

        public int SendPort { get; set; }
    }
}