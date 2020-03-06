using Udp.Abstract.Configuration;

namespace Udp.Core.Configuration
{
    public class UdpSpeakerConfig : IUdpSpeakerConfig
    {
        public int BroadcastTargetPort { get; set; }
        public int SendTargetPort { get; set; }
    }
}
