using RPA.Report;
using System.Windows.Forms;

namespace RPA
{
    public class InputSimulator
    {
        private IReport _report;
        private kernel32 _kernel32 = new kernel32();

        public InputSimulator(IReport report)
        {
            _report = report;
        }


        public void MouseClickLeft()
        {
            _kernel32.MouseEvent(kernel32.MouseEventFlags.LeftDown);
            _kernel32.MouseEvent(kernel32.MouseEventFlags.LeftUp);
            _report.WriteLine($"{nameof(MouseClickLeft)}");
        }

        public void MouseDoubleClickLeft()
        {
            MouseClickLeft();
            MouseClickLeft();
        }

        public void MouseMove(double x, double y)
        {
            MouseMove((int)x, (int)y);
        }

        public void MouseMove(int x, int y)
        {
            _kernel32.SetCursorPosition(x, y);
            _report.WriteLine($"{nameof(MouseMove)} to ({x}:{y})");
        }

        public void TypeText(string text)
        {
            _kernel32.SendString(text);
            _report.WriteLine($"{nameof(TypeText)} '{text}'");
        }

        public void MouseDrag(int xFrom, int yFrom, int xTo, int yTo)
        {
            MouseMove(xFrom, yFrom);
            _kernel32.MouseEvent(kernel32.MouseEventFlags.LeftDown);
            MouseMove(xTo, yTo);
            _kernel32.MouseEvent(kernel32.MouseEventFlags.LeftUp);

            _report.WriteLine($"{nameof(MouseDrag)} from ({xFrom}:{yFrom}) to ({xTo}:{yTo})");
        }

        public void ShortKey(Keys key1, Keys key2)
        {
            KeyDown(key1);
            KeyDown(key2);
            KeyUp(key1);
            KeyUp(key2);

            _report.WriteLine($"{nameof(ShortKey)} {key1.ToString()} + {key2.ToString()}");
        }

        public void KeyDown(Keys key)
        {
            _kernel32.KeyDown(key);
        }

        public void KeyUp(Keys key)
        {
            _kernel32.KeyUp(key);
        }
    }
}
