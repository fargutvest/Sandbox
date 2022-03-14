using System.Windows.Automation;
using RPA.Report;

namespace RPA.Flows
{
    internal class ClickButtonFlow : BaseFlow
    {
        private Locator _btnLocator;
        private int _timeoutMilliseconds;

        public ClickButtonFlow(IReport report, Locator btnLocator) : base(report)
        {
            _btnLocator = btnLocator;
        }

        protected override void ExecuteInternal(ExectutionContext context)
        {
            var root = context.AutomationElement;
            AutomationProvider.Invoke(root, _btnLocator);
        }
    }
}
