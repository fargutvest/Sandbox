using System.Diagnostics;
using RPA.Report;

namespace RPA.Flows
{
    internal class LaunchFlow : BaseFlow
    {
        private string _parameters;

        public LaunchFlow(IReport report, string parametrs = "") : base (report)
        {
            _parameters = parametrs;
        }

        protected override void ExecuteInternal(ExectutionContext context)
        {
            var info = new ProcessStartInfo();
            info.Arguments = _parameters;
            info.FileName = context.ExecutableFilePath;
            Process.Start(info);
            var root = AutomationProvider.GetRootAutomationElement(context);
            context.AutomationElement = root;
        }
        
    }
}
