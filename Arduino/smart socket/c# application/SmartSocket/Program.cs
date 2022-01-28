using System.Configuration;
using System.IO.Ports;

namespace SmartSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            var portName = ConfigurationManager.AppSettings["portName"];
            using (var port = new SerialPort() { BaudRate = 9600, PortName = portName })
            {
                port.Open();
                int hadRead = port.ReadByte();
                port.Write(new byte[] { hadRead > 0 ? (byte)0 : (byte)255 }, 0, 1);
                port.Close();
            }
        }
    }
}
