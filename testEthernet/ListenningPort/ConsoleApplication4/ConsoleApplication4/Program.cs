using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient sender = new UdpClient(64900);
            IPEndPoint remote1 = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 34001);
            IPEndPoint remote2 = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50002);

            byte[] b = new byte[402];
            b[0] = 0x01;
            b[1] = 0xEF;
            sender.Send(b, b.Length, remote1);
            sender.Close();

            sender = new UdpClient(50001);
            b = Encoding.ASCII.GetBytes("FIND");
            sender.Send(b, b.Length, remote2);


            UdpClient recever = new UdpClient(64900);
            IPEndPoint iep = null;
            b = recever.Receive(ref iep);
        }
    }
}
