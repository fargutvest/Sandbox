using System.Windows.Automation;

namespace RPA
{
    public class ExectutionContext
    {
        private ExectutionContext() { }

        public static ExectutionContext Get(string path, string mainWindowTitle = null)
        {
            Cache.AutomationElements.TryGetValue(path, out var root);

            return new ExectutionContext()
            {
                ExecutableFilePath = path,
                MainWindowTitle = mainWindowTitle,
                AutomationElement = root
            };
        }
       
        public AutomationElement AutomationElement { get; set; }

        public string ExecutableFilePath { get; set; }
        public string MainWindowTitle { get; set; }
    }
}
