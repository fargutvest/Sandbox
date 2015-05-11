using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApplication3
{
    class Program
    {

        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Получаем данные, необходимые для соединения
                Console.WriteLine("Укажите локальный порт");
                localPort = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Укажите удаленный порт");
                remotePort = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Укажите удаленный IP-адрес");
                remoteIPAddress = IPAddress.Parse(Console.ReadLine());

                // Создаем поток для прослушивания
                Thread tRec = new Thread(new ThreadStart(Receiver));
                tRec.Start();

                while (true)
                {
                    Send(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                Console.ReadKey();
            }

        }

        private static void Send(string datagram)
        {
            // Создаем UdpClient
            UdpClient sender = new UdpClient();

            // Создаем endPoint по информации об удаленном хосте
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);

            try
            {
                // Преобразуем данные в массив байтов
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);

                // Отправляем данные
                sender.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                Console.ReadKey();
            }
            finally
            {
                // Закрыть соединение
                sender.Close();
            }
        }

        public static void Receiver()
        {
            // Создаем UdpClient для чтения входящих данных
            
            UdpClient receivingUdpClient = new UdpClient(localPort);
            

            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                Console.WriteLine(
                   "\n-----------*******Общий чат*******-----------");

                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receivingUdpClient.Receive(
                       ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    Console.WriteLine(" --> " + returnData.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                Console.ReadKey();
            }
        }

    }
}
