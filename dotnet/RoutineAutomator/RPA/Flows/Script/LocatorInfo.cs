using System.Windows;

namespace RPA.Flows.Script
{
    public class LocatorInfo
    {
        public string FllePath { get; set; }

        public string MainWindowTitle { get; set; }
        public Point Point { get; set; }
        public string AutomationId { get; set; }
        public string LocalizedControlType { get; set; }
        public string Name { get; set; }
    }
}
