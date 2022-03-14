using System;

namespace RPA.Report
{
    public class Report : IReport
    {
        private Func<string> _getReportOut;
        private Action<string> _setReportOut;
        
        public Report(Func<string> getReportOut, Action<string> setReportOut)
        {
            _getReportOut = getReportOut;
            _setReportOut = setReportOut;
        }

        public void ReadKey()
        {
            var reportOut = _getReportOut?.Invoke();
            _setReportOut?.Invoke($"{reportOut}{Environment.NewLine} ReadKey");
        }

        public void WriteLine(Exception ex)
        {
            var reportOut = _getReportOut?.Invoke();
            _setReportOut?.Invoke($"{reportOut}{Environment.NewLine} {ex.Message} {Environment.NewLine} {ex.StackTrace}");
        }

        public void WriteLine(string message)
        {
            var reportOut = _getReportOut?.Invoke();
            _setReportOut?.Invoke($"{reportOut}{Environment.NewLine} {message}");
        }

        public void Write(string message)
        {
            var reportOut = _getReportOut?.Invoke();
            _setReportOut?.Invoke($"{reportOut}{message}");
        }
    }
}
