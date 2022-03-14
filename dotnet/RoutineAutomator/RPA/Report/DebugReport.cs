using System;
using System.Diagnostics;

namespace RPA.Report
{
    public class DebugReport : IReport
    {
        public void ReadKey()
        {
            Debug.WriteLine(nameof(ReadKey));
        }

        public void WriteLine(Exception ex)
        {
            Debug.WriteLine(ex);
        }

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

        public void Write(string message)
        {
            Debug.Write(message);
        }
    }
}
