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
            //adabaf
            int dtpLength = 11381679;

            byte lo = (byte)((byte)dtpLength & (byte)0xff);
            byte mi = (byte)(dtpLength >> 8);
            byte hi = (byte)(dtpLength >> 16);


            double d;
            object o;
            byte bt = 0x03;
            byte bt1 = 0x44;
            int i = 3;
            string s = "dfgjdk dkf gkld 8 oe5 34 ";

            bool bl = bt.Equals(i);
            int hash = s.GetHashCode();
            

            
        }


    


    }
}

