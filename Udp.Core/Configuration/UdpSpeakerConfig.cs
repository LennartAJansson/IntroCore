using Udp.Abstract.Configuration;

namespace Udp.Core.Configuration
{
    public class UdpSpeakerConfig : IUdpSpeakerConfig
    {
        public const string SectionName = "UdpSpeakerConfig";

        public int BroadcastTargetPort { get; set; }

        public int SendTargetPort { get; set; }
    }
}