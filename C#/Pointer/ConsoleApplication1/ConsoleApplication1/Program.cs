using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            byte[] bytes = new byte[410];
            for (int j =0; j< 410; j++)
            {
                bytes[j] = (byte)j;
            }

            IntPtr LocaleIP = new IntPtr(bytes[25]);
            IntPtr DNS = new IntPtr(bytes[70]);
            
            
        }
    }
}
