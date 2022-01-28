using System.IO.Ports;

namespace HelloArduinoSerialPort
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = new SerialPort()
            {
                BaudRate = 9600,
                PortName = "COM10"
            };
            
            port.Open();
            int hadRead = port.ReadByte();
            port.Write(new byte[] { hadRead > 0 ? (byte)0 : (byte)255 }, 0, 1);
        }
    }
}
