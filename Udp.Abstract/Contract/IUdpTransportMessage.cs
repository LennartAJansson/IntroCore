using System.Net;

namespace Udp.Abstract.Contract
{
    public interface IUdpTransportMessage
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public IUdpMessage Message { get; set; }
    }
}
