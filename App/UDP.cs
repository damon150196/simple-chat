using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public abstract class UDP
    {
        protected UdpClient client = new UdpClient();
        protected IPAddress address = IPAddress.Parse("239.0.0.222");
        protected int port = 2222;
        protected IPEndPoint endPoint;
        protected UdpState state;
    }

    public struct UdpState
    {
        public UdpClient client;
        public IPEndPoint address;
    }
}
