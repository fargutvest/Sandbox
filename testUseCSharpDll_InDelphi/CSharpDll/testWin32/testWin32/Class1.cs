using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace testWin32
{

    [ComVisible(true)]
    public interface IMyInterface
    {
        int Sum(int x, int y);
        string AppendText(string text);
    }


    [ComVisible(true), ClassInterface(ClassInterfaceType.None)]
    public class TestClass : IMyInterface
    {

        public int Sum(int x, int y)
        {
            return x + y;
        }

        public string AppendText(string Text)
        {
            StringBuilder sb = new StringBuilder(Text);
            sb.Append(DateTime.Now.Year);
            return sb.ToString();
        }

    }
}
