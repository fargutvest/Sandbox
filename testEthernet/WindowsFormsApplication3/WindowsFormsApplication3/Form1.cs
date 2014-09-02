using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            byte[] bytes = new byte[1024];
            try
            {
                IPHostEntry ipHost = Dns.Resolve("192.168.1.2"); //тут пишу статичный IP
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 3000);

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(ipEndPoint);

                Console.WriteLine("Socket connected {0}",
                                   sender.RemoteEndPoint.ToString());
                string theMessage = "Proverka svyazi!";

                byte[] msg = Encoding.ASCII.GetBytes(theMessage + "  <TheEnd>");
                int bytesSent = sender.Send(msg);
                int bytesRec = sender.Receive(bytes);

                Console.WriteLine("The server says : {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}", e.ToString());
                Console.Read();
            }
            

        }
       


        
    }
}
