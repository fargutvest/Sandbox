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

namespace WindowsFormsApplication1
{
     
    public partial class Form1 : Form
    {
        Socket _socketCMD;
        IPAddress ipAddr = new IPAddress(new byte[] { 192, 168, 2, 1 });
        int CMDPort = 3000;

        public Form1()
        {
            InitializeComponent();

        }
        private void Connect()
        {
            // port.inizialise();
            _socketCMD = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.RemoteEndPoint = new IPEndPoint(ipAddr, CMDPort);
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Connected);
                _socketCMD.ConnectAsync(e);
            }
            catch (Exception ex)
            {
                //DebugMessage(ex);
                return;
            }
        }
        void Connected(object sender, SocketAsyncEventArgs e)
        {
            if (e.ConnectSocket != null)
            {
            }
        }
        private void Disconnect()
        {
            if (_socketCMD.Connected)
            {
                _socketCMD.Disconnect(true);
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }
    }
}
