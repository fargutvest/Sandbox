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

            string s = "564F4C5441283233362E373434290D0A564F4C5441283233362E373938290D0A564F4C5441283233362E373639290D0A03";
            int[] arr = new int[s.Length/2];
            int bcc = 0;
            for (int i=0; i<s.Length/2;i++)
            {
                arr[i] = Convert.ToInt32(s.Substring(i*2, 2),16);
                bcc += arr[i];
            }

            Console.ReadLine();

        }
    }
}
