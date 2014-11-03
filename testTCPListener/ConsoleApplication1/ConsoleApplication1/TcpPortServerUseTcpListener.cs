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
        Byte[] ReadBuffer = new Byte[256];
        String data = null;
        NetworkStream networkstream;
        public event Action DataReceived;
        public event Action<string> Message;


        public TcpPortServer() { }

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
                        GenerateMessage("Waiting for a connection...");
                        Debug.WriteLine("Waiting for a connection...");
                        TcpClient tcpclient = tcplistener.AcceptTcpClient();
                        GenerateMessage("Connected!");
                        Debug.WriteLine("Connected!");

                        networkstream = tcpclient.GetStream();

                        int i;
                        try
                        {
                            while (true)
                            {
                                if ((i = networkstream.Read(ReadBuffer, 0, ReadBuffer.Length)) != 0)
                                {
                                    Array.Resize(ref ReadBuffer, i);
                                    GenerateMessage(string.Format("Receive: {0}", Encoding.ASCII.GetString(ReadBuffer)));
                                    Debug.WriteLine(string.Format("Receive: {0}", Encoding.ASCII.GetString(ReadBuffer)));
                                    if (DataReceived != null)
                                        DataReceived();
                                }
                            }
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

        public void Write(byte[] bytes)
        {
            try
            {
                // Send back a response.
                networkstream.Write(bytes, 0, bytes.Length);
                GenerateMessage(string.Format("Send: {0}", Encoding.ASCII.GetString(bytes)));
                Debug.WriteLine(string.Format("Send: {0}", Encoding.ASCII.GetString(bytes)));
            }
            catch (Exception ex) { }
        }

        public byte[] Read()
        {
            return ReadBuffer;
        }

        void GenerateMessage(string text)
        {
            if (Message != null)
                Message(text);
        }
    }
}
