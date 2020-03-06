
using Udp.Abstract.Contract;

namespace Udp.Core.Contract
{
    public class UdpMessage : IUdpMessage
    {
        public string Text { get; set; }
    }
}
