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

        private ExectutionContext _context;
        private string calculatorExePath = @"C:\Program Files\WindowsApps\Microsoft.WindowsCalculator_11.2401.0.0_x64__8wekyb3d8bbwe\CalculatorApp.exe";
        private string calculatorMainWindowTitle = "Калькулятор";

        public Start(IReport report)
        {
            _report = report;
        }

        [DisplayName("Start Calculator")]
        public void StartCalculator()
        {
            LaunchFlow launchFlow = new LaunchFlow(_report);
            ExectutionContext context = ExectutionContext.Get(calculatorExePath, calculatorMainWindowTitle);
            launchFlow.Execute(context);
            _context = context;
        }

        [DisplayName("Press 8")]
        public void Press_8()
        {
            if (_context == null)
            {
                FindWindowFlow findWindowFlow = new FindWindowFlow(_report);
                ExectutionContext context = ExectutionContext.Get(calculatorExePath, calculatorMainWindowTitle);
                FlowExecutionResult result = findWindowFlow.Execute(context);
                if (result.ResultEnum == RPA.Flows.Result.ResultEnum.Success)
                {
                    _context = context;
                }
                else
                {
                    _report.WriteLine("Can`t find calculator. Make sure it launched.");
                    return;
                }
            }


            if (ValidateContext() == false)
            {
                _context = null;
                _report.WriteLine("Can`t find calculator. Make sure it launched.");
                return;
            }

            ClickButtonFlow clickButtonFlow = new ClickButtonFlow(_report,
           new Locator(AutomationElement.AutomationIdProperty, "num8Button"));

            clickButtonFlow.Execute(_context);
        }

        private bool ValidateContext()
        {
            try
            {
                return _context.AutomationElement.Current.Name == calculatorMainWindowTitle;
            }
            catch
            {
                return false;
            }
        }
    }
}
