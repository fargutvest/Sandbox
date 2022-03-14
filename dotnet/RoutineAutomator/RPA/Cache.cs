using System.Collections.Generic;
using System.Windows.Automation;

namespace RPA
{
    internal static class Cache
    {
        internal static Dictionary<string, AutomationElement> AutomationElements { get; set; } = new Dictionary<string, AutomationElement>();
    }
}
