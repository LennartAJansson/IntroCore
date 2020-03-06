
using System.Net;

using Udp.Abstract.Contract;
using Udp.Core.Contract;

namespace Udp.Core.Extension
{
    public static class MessageTransformer
    {
        public static IUdpTransportMessage ToUdpTransportMessage(this IUdpMessage message, IPAddress address, int port) =>
            new UdpTransportMessage
            {
                Address = address,
                Port = port,
                Message = message
            };
    }
}
