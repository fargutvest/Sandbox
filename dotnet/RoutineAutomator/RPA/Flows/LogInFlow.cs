using RPA.Report;

namespace RPA.Flows
{
    internal class LogInFlow : BaseFlow
    {
        private string _login;
        private string _password;
        private Locator _loginLocator;
        private Locator _passwordLocator;
        private Locator _okBtnLocator;

        public LogInFlow(IReport report, Locator okBtnLocator, (string value, Locator locator)? login = null, (string value, Locator locator)? password =  null) : base(report)
        {
            _login = login?.value;
            _password = password?.value;
            _loginLocator = login?.locator;
            _passwordLocator = password?.locator;
            _okBtnLocator = okBtnLocator;
        }

        protected override FlowExecutionResult ExecuteInternal(ExectutionContext context)
        {
            SetCredentials(context);
            ClickLogin(context);
            return new FlowExecutionResult();
        }

        private void SetCredentials(ExectutionContext context)
        {
            var root = context.AutomationElement;
            if (string.IsNullOrEmpty(_login) == false)
            {
                AutomationProvider.SetValue(root, _loginLocator, _login);
            }
            
            if (string.IsNullOrWhiteSpace(_password) == false)
            {
                AutomationProvider.SetValue(root, _passwordLocator, _password);
            }
        }

        private void ClickLogin(ExectutionContext context)
        {
            new ClickButtonFlow(Report, _okBtnLocator).Execute(context);
        }

    }
}
