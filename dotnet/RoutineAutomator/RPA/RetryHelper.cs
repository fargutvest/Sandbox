using System;
using System.Threading;
using System.Threading.Tasks;
using RPA.Report;

namespace RPA
{
    public class RetryHelper
    {
        private IReport _report;
        public RetryHelper(IReport report)
        {
            _report = report;
        }

        public T Retry<T>(Func<T> toDo, CancellationToken token) where T : class
        {
            var attempts = 0;
            T result = null;
            while (result == null && token.IsCancellationRequested == false)
            {
                if (attempts > 0)
                {
                    _report.Write(".");
                    Task.Delay(1000).Wait();
                }
                
                result = toDo?.Invoke();
                attempts += 1;
            }

            return result;
        }
    }
}
