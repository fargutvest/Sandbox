using System;

namespace RPA.Report
{
    public class ConsoleReport : IReport
    {
        public void WriteLine(Exception ex)
        {
            Console.WriteLine(ex);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }
    }
}