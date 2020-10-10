using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static bool done;

        static void Main()
        {
            new System.Threading.Thread(Go).Start();
            Go();   
        }
        static void Go()
        {
            if (!done) { Console.WriteLine("Done"); done = true; }
        }
    }
}
