using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adani;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpPortServer server = new TcpPortServer();
            server.Open("192.168.2.133", 45678);

            Console.ReadKey();
        }
    }
}
