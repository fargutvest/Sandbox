using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int platform = 0;
            if (Environment.Is64BitOperatingSystem)
                platform = 64;
            else 
                platform = 32;

            Console.WriteLine(String.Format("Разрядность этой опреционной системмы: {0} бит", platform));
            Console.ReadKey();
        }
    }
}
