using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        const string s1 = "moveto";
        const string s2 = "preimmerse";
        const string s3 = "transfersample";

        const char splitter = '-';
        const string MoveTo = "moveto";
        const string Preimmerse = "preimmerse";
        const string TransferSample = "transfersample";
        const string Wash = "wash";
        const string Dry = "dry";

        static void Main(string[] args)
        {


            ParseArgs(args);

            string ss = args[0];

            switch (ss)
            {
                case s1:
                    break;
                case s2:
                    break;
                case s3:
                    break;

            }

            
        }

        static void ParseArgs(string[] _args)
        {
            try
            {
                int pupmSpeed = 0;
                foreach (string command in _args)
                {
                    if (command != string.Empty)
                    {
                        //разбор строки на команду и аргументы
                        string[] parts = command.Split(new char[] { splitter });
                        if (parts.Length > 0)
                        {
                            string cmd = parts[0];
                            //идентификация имени команды
                            switch (cmd)
                            {
                                case MoveTo:
                                    break;
                                case Preimmerse:
                                    break;
                                case TransferSample:
                                    break;
                                case Wash:
                                    break;
                                case Dry:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Error syntaxis"); }
        }
    }
}
