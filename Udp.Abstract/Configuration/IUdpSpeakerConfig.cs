namespace Udp.Abstract.Configuration
{
    public interface IUdpSpeakerConfig
    {
        int BroadcastTargetPort { get; set; }
        int SendTargetPort { get; set; }
    }
}
