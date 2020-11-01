using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Static.MyStruct;

namespace Static
{
    public class Class1
    {
        public MyClass MyTest()
        {
            return MyMethod();
        }
    }

    public struct MyStruct
    {
        public static MyClass MyMethod()
        {
            return new MyClass();
        }
    }

    public class MyClass
    {

    }
}
