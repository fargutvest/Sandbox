using System;
using System.Text;

namespace BankOperations.Helpers
{
    public class ConsoleHelper
    {

        public int CursorLeft => Console.CursorLeft;

        public int CursorTop => Console.CursorTop;


        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj.ToString());
        }

        public void WriteLine(object obj, ConsoleColor color)
        {
            var regularColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
            Console.ForegroundColor = regularColor;
        }

        public void WriteSeparatorBeginLine()
        {
            WriteLine($"{Environment.NewLine}{new string('*', Console.WindowWidth - 1)}");
        }

        public void WriteSeparatorEndLine()
        {
            WriteLine($"{new string('*', Console.WindowWidth - 1)}{Environment.NewLine}");
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void Write(string text, ConsoleColor color)
        {
            var regularColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = regularColor;
        }

        public void TreatControlCAsInput()
        {
            Console.TreatControlCAsInput = true;
        }

        public void SetHorizontalCursorPosition(int left)
        {
            Console.SetCursorPosition(left, Console.CursorTop);
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void UseUnicode()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
        }

        public string Read()
        {
            return Console.ReadKey().KeyChar.ToString();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(true);
        }

        public bool IsDisplayableKey(ConsoleKey key)
        {
            return (key >= ConsoleKey.D0 && key <= ConsoleKey.Z) ||
                   (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.Divide) ||
                   (key >= ConsoleKey.Oem1 && key <= ConsoleKey.Oem102) ||
                   key == ConsoleKey.Spacebar;
        }
    }
}
