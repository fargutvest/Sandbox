using RPA;
using RPA.Flows;
using RPA.Report;
using System.ComponentModel;
using System.Windows.Automation;

namespace CalculatorPlugin
{
    public class Start : IPlugin
    {
        private IReport _report;

        public Start(IReport report)
        {
            _report = report;
        }

        [DisplayName("Start Calculator")]
        public void StartCalculator()
        {
            LaunchFlow launchFlow = new LaunchFlow(_report, null);
            string exePath = @"C:\Program Files\WindowsApps\Microsoft.WindowsCalculator_11.2401.0.0_x64__8wekyb3d8bbwe\CalculatorApp.exe";
            ExectutionContext context = ExectutionContext.Get(exePath);
            launchFlow.Execute(context);

            ClickButtonFlow clickButton = new ClickButtonFlow(_report,
                new Locator(AutomationElement.AutomationIdProperty, "num8Button"));

            //clickButton.Execute(context);
        }
    }
}
