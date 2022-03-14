using System;

namespace RPA.Report
{
    public interface IReport
    {
        void WriteLine(Exception ex);
        void WriteLine(string message);
        void Write(string message);
        void ReadKey();
    }
}