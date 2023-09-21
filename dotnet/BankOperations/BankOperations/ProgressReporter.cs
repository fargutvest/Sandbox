using System;

namespace BankOperations
{
    public class ProgressReporter
    {
        public event Action<string> OnProgress;

        public void Report(string text)
        {
            OnProgress.Invoke(text);
        }

    }
}
