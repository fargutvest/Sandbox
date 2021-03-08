using System.Collections.Generic;
using System.Drawing;

namespace ListenCursor
{
    public class ContextMenuResult
    {
        public Locator Locator { get; set; }
        public string Pattern { get; set; }
    }
    public class Locator
    {
        public string FllePath { get; set; }
        public Point Point { get; set; }
        public string AutomationId { get; set; }
        public string LocalizedControlType { get; set; }
        public string Name { get; set; }
    }
}
