using RPA;
using RPA.Flows.Script;
using RPA.Report;
using System.ComponentModel;

namespace TestPlugin
{
    public class Start : IPlugin
    {
        private IReport _report;

        public Start(IReport report)
        {
            _report = report;
        }

        [DisplayName("Hello world!")]
        public void SayHelloWorld()
        {
            _report.WriteLine("Hello world!");
        }

        [DisplayName("From script")]
        public void FromScript()
        {
            string scriptPath = "C:\\test\\script.txt";
            FromScriptFlow flow = new FromScriptFlow(_report, scriptPath);
            flow.Execute(null);
        }
    }
}
