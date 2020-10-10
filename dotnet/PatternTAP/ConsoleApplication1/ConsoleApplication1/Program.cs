using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        AutoResetEvent are = new AutoResetEvent(false);
        int delay = 5000;

        static void Main(string[] args)
        {
            test();

            Console.WriteLine("Асинхронный метод вызван");
            Console.ReadKey();
        }


        async static Task test()
        {
            byte[] bytes = await ReadAsync();
            string res = "";
            foreach (byte b in bytes)
                res += "0x" + b.ToString() + "; ";
            Console.WriteLine("Окончание работы асинхронного метода \r\nрезультат -  {0}", res);
        }

        public static Task<byte[]> ReadAsync()
        {
            AutoResetEvent are = new AutoResetEvent(false);
            int delay = 1000;
            

            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    are.WaitOne(delay);
                    byte[] bytes = new byte[] { 0x1, 0x2, 0x3 };
                    return bytes;
                }
            });
        }

    }



}
