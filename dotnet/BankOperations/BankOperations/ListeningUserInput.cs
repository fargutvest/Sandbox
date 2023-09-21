using BankOperations.Helpers;
using System;
using System.Collections.Generic;

namespace BankOperations
{
    public class ListeningUserInput
    {
        private List<string> _userInputHistory;
        private const string HERE = ">";

        private int _historyCurrent;

        private ConsoleHelper _consoleHelper;

        public ListeningUserInput(List<string> userInputHistory, ConsoleHelper consoleHelper)
        {
            _consoleHelper = consoleHelper;
            _userInputHistory = userInputHistory;
            _historyCurrent = _userInputHistory.Count;
        }

        public string Listen()
        {
            _consoleHelper.Write(HERE);

            ConsoleKeyInfo keyInfo;
            _consoleHelper.TreatControlCAsInput();

            var chars = new List<char>();
            do
            {
                keyInfo = _consoleHelper.ReadKey();
                if ((keyInfo.Key == ConsoleKey.DownArrow || (keyInfo.Key == ConsoleKey.UpArrow && _historyCurrent > 0)) && _userInputHistory.Count > 0)
                {
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        _historyCurrent--;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow && _historyCurrent < _userInputHistory.Count - 1)
                    {
                        _historyCurrent++;
                    }

                    chars.Clear();
                    chars.AddRange(_userInputHistory[_historyCurrent].ToCharArray());
                    _consoleHelper.ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                }
                else if ((keyInfo.Key == ConsoleKey.LeftArrow && _consoleHelper.CursorLeft > HERE.Length) || (keyInfo.Key == ConsoleKey.RightArrow && _consoleHelper.CursorLeft < chars.Count + HERE.Length))
                {
                    _consoleHelper.SetHorizontalCursorPosition(keyInfo.Key == ConsoleKey.LeftArrow ? _consoleHelper.CursorLeft - 1 : _consoleHelper.CursorLeft + 1);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && chars.Count > 0 && _consoleHelper.CursorLeft - HERE.Length - 1 >= 0)
                {
                    var curPos = _consoleHelper.CursorLeft - HERE.Length - 1;
                    chars.RemoveAt(curPos);
                    _consoleHelper.ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                    _consoleHelper.SetHorizontalCursorPosition(curPos + 1);
                }
                else if (keyInfo.Key == ConsoleKey.Delete && chars.Count > 0 && _consoleHelper.CursorLeft < chars.Count + HERE.Length)
                {
                    var curPos = _consoleHelper.CursorLeft - HERE.Length;
                    chars.RemoveAt(curPos);
                    _consoleHelper.ClearCurrentConsoleLine();
                    WriteCharsToConsole(chars);
                    _consoleHelper.SetHorizontalCursorPosition(curPos + 1);
                }
                else if (_consoleHelper.IsDisplayableKey(keyInfo.Key))
                {
                    var curPos = _consoleHelper.CursorLeft;
                    chars.Insert(curPos - HERE.Length, keyInfo.KeyChar);
                    WriteCharsToConsole(chars);
                    _consoleHelper.SetHorizontalCursorPosition(curPos + 1);
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            var userInput = new string(chars.ToArray());
            if (string.IsNullOrEmpty(userInput) == false && _userInputHistory.Contains(userInput) == false)
            {
                _userInputHistory.Add(userInput);
                _historyCurrent = _userInputHistory.IndexOf(userInput) + 1;
            }

            _consoleHelper.WriteLine();
            return userInput;
        }


        private void WriteCharsToConsole(List<char> chars)
        {
            _consoleHelper.Write($"\r{HERE}{new string(chars.ToArray())}", ConsoleColor.Yellow);
        }
    }
}
