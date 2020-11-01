using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server_tcp_ip
{
    class Program
    {
        static void Main(string[] args)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
           

            try
            {
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                TcpListener listener = new TcpListener(localAddress, 2200);

                listener.Start(1);

               
                while (true)
                {
                    Console.WriteLine("Сервер ожидает {0}", listener.LocalEndpoint);
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream io = client.GetStream();
                    Console.WriteLine("Принято соединение от {0}", client.Client.RemoteEndPoint);
                    byte [] buffer = new byte[1024];
                    io.Read(buffer, 0, 1024);
                    string mess = System.Text.Encoding.UTF8.GetString(buffer);
                    Byte[] answer = encoding.GetBytes("answer server: "+ mess);

                    Console.WriteLine("Отправляем сообщение...");
                    io.Write(answer, 0, answer.Length);

                    //Console.WriteLine("Закрытие соединения");
                    //client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка {0}", e.Message);
            }
        }
    }
}
