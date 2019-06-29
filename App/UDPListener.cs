using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public class UDPListener : UDP
    {
        IPEndPoint localEp;
        Form1 output;

        public UDPListener(Form1 output)
        {
            this.output = output;
            client.ExclusiveAddressUse = false;

            localEp = new IPEndPoint(IPAddress.Any, port);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;
            client.Client.Bind(localEp);
            client.JoinMulticastGroup(address);
            state = new UdpState();

            state.client = client;
            state.address = localEp;
        }

        public void Listen()
        {
            while (true)
            {
                byte[] data = client.Receive(ref localEp);

                output.OutputParser(data);
            }
        }
        
        public void ListenNick()
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;
            double elapsedMillisecs = 0;

            while (elapsedMillisecs < 10000 && output.TmpAuthor != "")
            {
                byte[] data = client.Receive(ref localEp);

                output.OutputParser(data);

                endTime = DateTime.Now;
                elapsedMillisecs = ((TimeSpan)(endTime - startTime)).TotalMilliseconds;
            }

            Console.WriteLine("koniec wątku");
            Own.Author = output.TmpAuthor;
            output.TmpAuthor = "";
            output.EnableInput(true);
            output.Listen.Start();
        }
    }
}
