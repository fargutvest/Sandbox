using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int s = 33;
            int res = bbb1(s);
        }


        static int bbb(int arg)
        {
            if ((10 < arg) && (arg < 20)) { return 1; }
            else if ((30 < arg) && (arg < 40)) { return 2; }
            else if ((50 < arg) && (arg < 60)) { return 3; }
            else if ((70 < arg) && (arg < 80)) { return 4; }
            else if ((90 < arg) && (arg < 100)) { return 5; }
            else return 0;
        }

        static int bbb1(int arg)
        {

            const int a = arg;

            switch (true)
            {
                case (10 < a && a < 20):
                    return 1;

                case (20 < a && a < 30):
                    return 2;
            }

        }
    }
}
