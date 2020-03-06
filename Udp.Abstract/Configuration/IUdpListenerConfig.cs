namespace Udp.Abstract.Configuration
{
    public interface IUdpListenerConfig
    {
        int BroadcastListenerPort { get; set; }
        int SendListenerPort { get; set; }
    }
}
