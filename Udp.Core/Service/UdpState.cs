using System.Net;
using System.Net.Sockets;

namespace Udp.Core.Service
{
    public struct UdpState
    {
        public UdpClient client;
        public IPEndPoint endPoint;

        //Overrides for comparing two UdpState structs
        public override bool Equals(object obj) =>
            obj is UdpState udp && udp.client.Equals(client) && udp.endPoint.Equals(endPoint);

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2167136230;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16770619) ^ client?.GetHashCode() ?? 0;
                hash = (hash * 16770619) ^ endPoint?.GetHashCode() ?? 0;
                return hash;
            }
        }

        public static bool operator ==(UdpState left, UdpState right) => left.Equals(right);

        public static bool operator !=(UdpState left, UdpState right) => !(left == right);
    }
}
