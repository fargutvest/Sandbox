using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public partial class MyClass
    {
        public MyClass()
        {
            
            a _a = new a();
            b _b = new b();
            c _c = new c();
            d _d = new d();


            IGeneral igeneral = _c;


            _d._igeneral = igeneral;

            _d.MethodD();

        }

    }



    public class parent
    {
        public void BaseConnect()
        {
            Console.WriteLine("baseConnect1");
            Console.WriteLine("baseConnect2");
            Console.WriteLine("baseConnect3");
        }
        public void BaseDisconnect()
        {
            Console.WriteLine("baseDisconnect1");
            Console.WriteLine("baseDisconnect2");
            Console.WriteLine("baseDisconnect3");
        }
    }

    public class a : parent, IGeneral
    {
        IGeneral igeneral = new b();

        public int Connect()
        {
            BaseConnect();
            Console.WriteLine("A_Connect1");
            return 0;
        }
        public int Disconnect()
        {
            BaseDisconnect();
            Console.WriteLine("A_Disconnect1");
            return 0;
        }
        public void MethodA()
        {
            igeneral.Connect();
        }
    }


    public class b : parent, IGeneral
    {
        public int Connect()
        {
            BaseConnect();
            Console.WriteLine("B_Connect1");
            return 0;
        }
        public int Disconnect()
        {
            BaseDisconnect();
            Console.WriteLine("B_Disconnect1");
            return 0;
        }
        void MethodB()
        { }
    }


    public class c : parent, IGeneral
    {
        public int Connect()
        {
            BaseConnect();
            Console.WriteLine("C_Connect1");
            return 0;
        }
        public int Disconnect()
        {
            BaseDisconnect();
            Console.WriteLine("C_Disconnect1");
            return 0;
        }

        void MethodC()
        { }
    }


    public class d
    {
        public IGeneral _igeneral;

        public void MethodD()
        {
            _igeneral.Connect();
        }
    }




    public interface IGeneral
    {
        int Connect();

        int Disconnect();

    }

}
