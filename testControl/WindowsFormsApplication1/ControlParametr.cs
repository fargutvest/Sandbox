using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public struct ControlParametr
    {

        int _a;
        public int A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }

        int _b;
        public int B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }


        public ControlParametr(Point p )
        {
            _a = 16;
            _b = 17;
        }

        public ControlParametr(int a, int b)
        {

            _a = 16;
            _b = 17;

            A = a;
            B = b;
        }

    }
}
