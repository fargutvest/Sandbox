using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Collections;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Listen();

        }
        //Для клиента
       static void Listen()
        {
            int recv; //храним размер полученных данных
            byte[] data = new byte[1024]; //данные, которые будут передаваться или приниматься

            //создаем новый сокет
            //параметр AddressFamily задает схему адресации. В нашем случае это адреса IPv4
            //параметр SocketType указывает, какой тип сокета применяется. 
            //В данном случае SocketType.Dgram это ненадежные сообщения, но для примера хватит.
            //Тем более, что Dgram поддерживает протокол UDP
            //последний параметр, ProtocolType, задает тип протокола
            Socket mysocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //создаем конечную точку по адресу сокета. Т.е. будем "слушать" порт и контролировать все сетевые интерфейсы
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 34001);
            mysocket.Bind(ipep); //привязываем точку к нашему сокету

            //создаем еще одну точку
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 34001);

            //определяем сетевой адрес
            EndPoint Remote = (EndPoint)(sender);

            //отправляем первое сообщение нашему серверу
            //string text = "Hello"; //текст сообщения
            //data = Encoding.ASCII.GetBytes(text); //переводим строку в байты

            //отправляем на указанны адрес
            //первый параметр это сами данные в виде массива байт
            //второй параметр, какая длиная сообщения должна быть передана. 
            //Если указать меньше, то передаст только то число символов, сколько укажете
            //третий параметр указывает поведение сокета при приеме и получении данных. В нашем случае ничего...
            //четвертый парамерт задает адрес и порт сервера, которому нужно отправить сообщение
            //что делает функция _getHost, смотрите чуть ниже...
            //в данном примере используется бродкастовый адрес подсети 192.168.15.255, 
            //если мы не знаем по какому адресу находится сервер. Порт подставляем любой (1111), он все равно будет перезаписан в функции _getHost
            //Использование функции SendTo позволяет заранее не соединяться с сервером
            //mysocket.SendTo(data, data.Length, SocketFlags.None, _getHost("192.168.15.255:1111"));

            //запускаем бесконечный цикл, который будет принимать и отправлять данные
            while (true)
            {
                data = new byte[1024];
                //принимаем данные от сервера. recv содержир размер, т.е. количество принятых символов
                recv = mysocket.Receive(data);
                //в данном примере сервер шлет сообщение, которое разделено ":"
                //разбираем его в массив
                //функция Encoding.ASCII.GetString переводит массив байт в строку
                string[] args = Encoding.ASCII.GetString(data, 0, recv).ToLower().Split(':');
                foreach (string item in args)
                {
                    Console.WriteLine("Сервер отправил " + item);
                }
                //отправим ответ что мы получили. Например, первый элемент массива
               // data = Encoding.ASCII.GetBytes(args[0]);

                //Remote содержит адрес, с которого пришло сообщение. Ему его назад и отправляем
               // mysocket.SendTo(data, data.Length, SocketFlags.None, _getHost(Remote.ToString()));
            }
        }

        //функция _getHost
        static private EndPoint _getHost(string text)
        {
            //вырезаем из строки только IP адрес формата IPv4
            string host = text.Remove(text.IndexOf(":"), text.Length - text.IndexOf(":"));

            //создаем объект адреса. Переменная host уже имеет вид [0-255].[0-255].[0-255].[0-255]
            IPAddress hostIPAddress = IPAddress.Parse(host);

            //создаем конечную точку. В нашем случае это адрес сервера, который слушает порт 9050
            IPEndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, 9050);
            EndPoint To = (EndPoint)(hostIPEndPoint);
            return To;
        }


    }

}
