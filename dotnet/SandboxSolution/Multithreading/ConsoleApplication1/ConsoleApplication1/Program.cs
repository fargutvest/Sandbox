using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread t1 = new System.Threading.Thread(potok1);
            System.Threading.Thread t2 = new System.Threading.Thread(potok2);
            System.Threading.Thread t3 = new System.Threading.Thread(potok3);
            System.Threading.Thread t4 = new System.Threading.Thread(potok4);
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            while (true)
                Console.Write("М");
        }

        static void potok1()
        {
            while (true)
                Console.Write("1");
        }
        static void potok2()
        {
            while (true)
                Console.Write("2");
        }
        static void potok3()
        {
            while (true)
                Console.Write("3");
        }
        static void potok4()
        {
            while (true)
                Console.Write("4");
        }
    
    
    }
}
