using RPA.Report;
using System;
using System.Threading.Tasks;

namespace RPA.Flows
{
    public abstract class BaseFlow
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

        public FlowExecutionResult Execute(ExectutionContext context)
        {
            return OnSafe(()=>
            {
                return ExecuteInternal(context);
            });
        }

        protected abstract FlowExecutionResult ExecuteInternal(ExectutionContext context);

        private FlowExecutionResult OnSafe(Func<FlowExecutionResult> toDo)
        {
            try
            {
               return toDo?.Invoke();
            }
            catch (Exception e)
            {
                Report?.WriteLine(e);
                Report?.ReadKey();

                return new FlowExecutionResult() { ResultEnum = Result.ResultEnum.Error};
            }
        }
    }
}