
using Udp.Abstract.Contract;
using Udp.Core.Contract;

namespace Udp.Extensions
{
    public static class UdpMessageFactory
    {
        public static IUdpMessage CreateUdpMessage(string text) =>
            new UdpMessage
            {
                Text = text
            };
    }
}
