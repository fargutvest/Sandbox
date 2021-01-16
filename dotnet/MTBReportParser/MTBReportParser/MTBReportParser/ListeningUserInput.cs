using System;
using System.Collections.Generic;

namespace MyConsole
{
    public class ListeningUserInput
    {
        private List<string> _userInputHistory = new List<string>();
        private string here = ">";

        public string Listen()
        {
            Console.Write(here);

            ConsoleKeyInfo keyInfo;
            Console.TreatControlCAsInput = true;
            var historyCurrent = 0;

            var chars = new List<char>();
            do
            {
                keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key == ConsoleKey.UpArrow || (keyInfo.Key == ConsoleKey.DownArrow && historyCurrent > 0)) && _userInputHistory.Count > 0)
                {
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        historyCurrent--;
                    }
                    else if (keyInfo.Key ==  ConsoleKey.UpArrow && historyCurrent < _userInputHistory.Count - 1)
                    {
                        historyCurrent++;
                    }

                    chars.Clear();
                    chars.AddRange(_userInputHistory[historyCurrent].ToCharArray());
                    ClearCurrentConsoleLine();
                    Console.Write($"\r{here}{new string(chars.ToArray())}");
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow && Console.CursorLeft > here.Length) || (keyInfo.Key == ConsoleKey.RightArrow && Console.CursorLeft < chars.Count + here.Length))
                {
                    Console.SetCursorPosition(keyInfo.Key == ConsoleKey.LeftArrow ? Console.CursorLeft - 1 : Console.CursorLeft + 1, Console.CursorTop);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && chars.Count > 0 && Console.CursorLeft - here.Length - 1 >= 0)
                {
                    var curPos = Console.CursorLeft - here.Length - 1;
                    chars.RemoveAt(curPos);
                    ClearCurrentConsoleLine();
                    Console.Write($"\r{here}{new string(chars.ToArray())}");
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }
                else if (keyInfo.Key == ConsoleKey.Delete && chars.Count > 0 && Console.CursorLeft < chars.Count + here.Length)
                {
                    var curPos = Console.CursorLeft - here.Length;
                    chars.RemoveAt(curPos);
                    ClearCurrentConsoleLine();
                    Console.Write($"\r{here}{new string(chars.ToArray())}");
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }
                else if (IsDisplayable(keyInfo.Key))
                {
                    var curPos = Console.CursorLeft;
                    chars.Insert(curPos - here.Length, keyInfo.KeyChar);
                    Console.Write($"\r{here}{new string(chars.ToArray())}");
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            var userInput = new string(chars.ToArray());
            if (string.IsNullOrEmpty(userInput) == false && _userInputHistory.Contains(userInput) == false)
            {
                _userInputHistory.Add(userInput);
            }

            Console.WriteLine();
            return userInput;
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }



        private bool IsDisplayable(ConsoleKey key)
        {
            return (key >= ConsoleKey.D0 && key <= ConsoleKey.Z) ||
                   (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.Divide) ||
                   (key >= ConsoleKey.Oem1 && key <= ConsoleKey.Oem102);
        }
    }
}
