using RPA.Report;
using System;
using System.Threading.Tasks;

namespace RPA.Flows
{
    internal abstract class BaseFlow
    {
        protected InputSimulator InputSimulator;

        protected AutomationProvider AutomationProvider;

        protected MsHtmlProvider MsHtmlProvider;

        protected IReport Report;

        public BaseFlow(IReport report)
        {
            Report = report;
            AutomationProvider = new AutomationProvider(Report);
            InputSimulator = new InputSimulator(Report);
            MsHtmlProvider = new MsHtmlProvider(Report);
        }

        protected void DelaySeconds(int seconds)
        {
            Report.WriteLine($"Waiting for {seconds} seconds");
            for (int i = 0; i < seconds; i++)
            {
                Task.Delay(1 * 1000).Wait();
                Report.Write(".");
            }
        }

        public void Execute(ExectutionContext context)
        {
            OnSafe(()=>
            {
                ExecuteInternal(context);
            });
        }

        protected abstract void ExecuteInternal(ExectutionContext context);

        private void OnSafe(Action toDo)
        {
            try
            {
                toDo?.Invoke();
            }
            catch (Exception e)
            {
                Report?.WriteLine(e);
                Report?.ReadKey();
            }
        }
    }
}