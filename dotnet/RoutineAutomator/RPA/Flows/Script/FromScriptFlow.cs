using Newtonsoft.Json;
using RPA.Report;
using System.Collections.Generic;
using System.IO;

namespace RPA.Flows.Script
{
    public class FromScriptFlow : BaseFlow
    {
        private string _scriptPath;

        public FromScriptFlow(IReport report, string scriptPath) : base(report)
        {
            _scriptPath = scriptPath;
        }

        protected override void ExecuteInternal(ExectutionContext context)
        {
            var script = File.ReadAllText(_scriptPath);
            var queue = JsonConvert.DeserializeObject<Queue<Node>>(script);

            foreach (var item in queue)
            {
               var el = AutomationProvider.GetRootAutomationElement(ExectutionContext.Get(item.Locator.FllePath));

                switch (item.Pattern == "InvokePatternIdentifiers.Pattern")
                {
                    default:
                        break;
                }
            }
        }
    }
}
