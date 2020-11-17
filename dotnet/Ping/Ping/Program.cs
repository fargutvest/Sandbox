using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MSPing = System.Net.NetworkInformation.Ping;

namespace Ping
{
    class Program
    {
        internal static string _address;

        static void Main(string[] args)
        {
            _address = args.Length > 0 ? args[0] : ConfigurationManager.AppSettings["ADDRESS"];
            ComplexPing();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ping.send?view=net-5.0
        /// </summary>
        public static void ComplexPing()
        {
            while (true)
            {
                MSPing pingSender = new MSPing();
                string data = new string('a', 32);
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 5000;
                PingOptions options = new PingOptions(64, true);

                PingReply reply = pingSender.Send(_address, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Reply from {reply.Address} bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
                }
                else
                {
                    Console.WriteLine(reply.Status);
                }

                Task.Delay(1000).Wait();
            }
        }
    }
}
