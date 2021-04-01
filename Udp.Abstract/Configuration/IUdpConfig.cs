namespace Udp.Abstract.Configuration
{
    public interface IUdpConfig
    {
        int BroadcastPort { get; set; }

        int SendPort { get; set; }
    }
}