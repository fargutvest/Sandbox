using System;
using System.Collections.Generic;

namespace MTBReportParser
{
    public class ListeningUserInput
    {
        private List<string> _userInputHistory;
        private const string HERE = ">";

        public ListeningUserInput(List<string> userInputHistory)
        {
            _userInputHistory = userInputHistory;
        }

        public string Listen(ref int historyCurrent)
        {
            Console.Write(HERE);

            ConsoleKeyInfo keyInfo;
            Console.TreatControlCAsInput = true;

            var chars = new List<char>();
            do
            {
                keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key == ConsoleKey.DownArrow || (keyInfo.Key == ConsoleKey.UpArrow && historyCurrent > 0)) && _userInputHistory.Count > 0)
                {
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        historyCurrent--;
                    }
                    else if (keyInfo.Key ==  ConsoleKey.DownArrow && historyCurrent < _userInputHistory.Count - 1)
                    {
                        historyCurrent++;
                    }

                    chars.Clear();
                    chars.AddRange(_userInputHistory[historyCurrent].ToCharArray());
                    ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow && Console.CursorLeft > HERE.Length) || (keyInfo.Key == ConsoleKey.RightArrow && Console.CursorLeft < chars.Count + HERE.Length))
                {
                    Console.SetCursorPosition(keyInfo.Key == ConsoleKey.LeftArrow ? Console.CursorLeft - 1 : Console.CursorLeft + 1, Console.CursorTop);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && chars.Count > 0 && Console.CursorLeft - HERE.Length - 1 >= 0)
                {
                    var curPos = Console.CursorLeft - HERE.Length - 1;
                    chars.RemoveAt(curPos);
                    ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }
                else if (keyInfo.Key == ConsoleKey.Delete && chars.Count > 0 && Console.CursorLeft < chars.Count + HERE.Length)
                {
                    var curPos = Console.CursorLeft - HERE.Length;
                    chars.RemoveAt(curPos);
                    ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }
                else if (IsDisplayable(keyInfo.Key))
                {
                    var curPos = Console.CursorLeft;
                    chars.Insert(curPos - HERE.Length, keyInfo.KeyChar);
                    WriteCharsToConsole(chars);
                    Console.SetCursorPosition(curPos + 1, Console.CursorTop);
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            var userInput = new string(chars.ToArray());
            if (string.IsNullOrEmpty(userInput) == false && _userInputHistory.Contains(userInput) == false)
            {
                _userInputHistory.Add(userInput);
                historyCurrent = _userInputHistory.IndexOf(userInput) + 1;
            }

            Console.WriteLine();
            return userInput;
        }


        private void WriteCharsToConsole(List<char> chars)
        {
            var regularColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\r{HERE}{new string(chars.ToArray())}");
            Console.ForegroundColor = regularColor;
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
                   (key >= ConsoleKey.Oem1 && key <= ConsoleKey.Oem102) ||
                   key == ConsoleKey.Spacebar;
        }
    }
}
