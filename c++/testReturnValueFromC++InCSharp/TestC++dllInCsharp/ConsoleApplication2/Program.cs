using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        static void Main(string[] args)
        {

            A a = new A();
        }

    }
    public class A
    {

        private const string pathLib = "C++dll.dll";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Del(int len, IntPtr value);

        private Del _delegate;



        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr GetData(ref int value);

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern void Subscribe(IntPtr callback);

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern void Invoke();

        public A()
        {
            _delegate = MyEventHandler;

            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(_delegate);
            Subscribe(ptr);
            Invoke();

            int len = 0;
            IntPtr value = GetData(ref len);
            byte[] result = new byte[len];
            Marshal.Copy(value, result, 0, len);
            Console.WriteLine(String.Format("Invoke: {0}", BitConverter.ToString(result)));
            Console.ReadKey();
        }

        void MyEventHandler(int len, IntPtr value)
        {
            byte[] result = new byte[len];
            Marshal.Copy(value, result, 0, len);
            Console.WriteLine(String.Format("EventHandler: {0}", BitConverter.ToString(result)));
        }
    }
}
