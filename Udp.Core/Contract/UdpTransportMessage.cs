using System.Net;

using Udp.Abstract.Contract;

namespace Udp.Core.Contract
{
    class UdpTransportMessage : IUdpTransportMessage
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public IUdpMessage Message { get; set; }
    }
}
