using System;

namespace ViyarParser
{
    internal class Report : IReport
    {
        public Report()
        {
        }

        public void Write(char v)
        {
            Console.Write(v);
        }
    }
}