using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adani;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static TcpPortServer server;
        static void Main(string[] args)
        {
            ConsoleTraceListener ctl = new ConsoleTraceListener();
            Debug.Listeners.Add(ctl);

            server = new TcpPortServer();
            server.Open("192.168.2.133", 56789);
            server.DataReceived += server_DataReceived;

            Console.ReadKey();
        }

        static void server_DataReceived()
        {
            Debug.WriteLine(string.Format("Receive: {0}", Encoding.ASCII.GetString(server.Read())));
        }
    }
}
