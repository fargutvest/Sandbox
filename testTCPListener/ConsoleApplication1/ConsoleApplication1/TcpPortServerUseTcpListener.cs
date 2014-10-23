using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adani
{
    public class TcpPortServer
    {
        TcpListener tcplistener;
        Task task;
        bool bStopTask;
        AutoResetEvent Infinity;
        int delay = 1;
        IPEndPoint LocalIpEndPoint;
        Byte[] bytes = new Byte[256];
        String data = null;
        

        public TcpPortServer()  {}
        
        public void Open(string IpLocal, ushort PortLocal)
        {
            try
            {
                LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(IpLocal), PortLocal);
                tcplistener = new TcpListener(LocalIpEndPoint);
                tcplistener.Start();
                task = Task.Factory.StartNew(() => 
                { 
                    while (!bStopTask)
                    {
                        Debug.WriteLine("Waiting for a connection...");
                        TcpClient tcpclient = tcplistener.AcceptTcpClient();
                        Debug.WriteLine("Connected!");
                        NetworkStream networkstream = tcpclient.GetStream();

                        int i;
                        try
                        {
                            while ((i = networkstream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                Array.Resize(ref bytes, i);

                                // Send back a response.
                                networkstream.Write(bytes, 0, bytes.Length);
                                Debug.WriteLine("Sent: {0}", Encoding.ASCII.GetString(bytes));
                            }
                            tcpclient.Close();
                        }
                        catch (Exception ex) { }

                    }
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }


        }
        

    }
}
