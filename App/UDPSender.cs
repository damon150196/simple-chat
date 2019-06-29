using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class UDPSender : UDP
    {
        public UDPSender()
        {
            client.JoinMulticastGroup(address);
            endPoint = new IPEndPoint(address, port);
        }

        public void Send(byte[] data)
        {
            client.Send(data, data.Length, endPoint);
        }
    }
}
