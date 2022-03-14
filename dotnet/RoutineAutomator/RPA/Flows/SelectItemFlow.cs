using RPA.Report;

namespace RPA.Flows
{
    internal class SelectItemFlow : BaseFlow
    {
        private Locator _itemLocator;

        public SelectItemFlow(IReport report, Locator itemLocator) : base (report)
        {
            _itemLocator = itemLocator;
        }

        protected override void ExecuteInternal(ExectutionContext context)
        {
            AutomationProvider.Select(context.AutomationElement, _itemLocator);
        }
    }
}
