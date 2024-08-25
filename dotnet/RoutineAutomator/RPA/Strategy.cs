using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace RPA
{
    internal class Strategy
    {
        internal static AutomationElement FindByNameProperty(AutomationProvider provider, AutomationElement element, string name) =>
            provider.FindChildAutomationElement(element, new Locator(AutomationElement.NameProperty, name));

    }
}

