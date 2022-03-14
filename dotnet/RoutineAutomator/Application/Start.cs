using RPA.Flows.Script;
using RPA.Report;
using System.ComponentModel;

namespace Application
{
    public class Start
    {
        private IReport _report;

        public Start(IReport report)
        {
            _report = report;
        }

        [DisplayName("Hello world!")]
        public void SayHelloWorld(CredentialsElement cred)
        {
            _report.WriteLine("Hello world!");
        }

        [DisplayName("From script")]
        public void FromScript()
        {
            new FromScriptFlow(_report, Settings.SCRIPT_PATH).Execute(null);
        }
    }
}
