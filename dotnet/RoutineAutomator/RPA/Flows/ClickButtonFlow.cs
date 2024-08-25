using RPA.Report;

namespace RPA.Flows
{
    public class ClickButtonFlow : BaseFlow
    {
        private Locator _btnLocator;
        private int _timeoutMilliseconds;

        public ClickButtonFlow(IReport report, Locator btnLocator) : base(report)
        {
            _btnLocator = btnLocator;
        }

        protected override FlowExecutionResult ExecuteInternal(ExectutionContext context)
        {
            var root = context.AutomationElement;
            AutomationProvider.Invoke(root, _btnLocator, 1000);
            return new FlowExecutionResult();
        }
    }
}
