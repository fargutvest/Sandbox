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
        private AutomationProvider _automationProvider;

        public Strategy(AutomationProvider automationProvider)
        {
            _automationProvider = automationProvider;
        }

        internal AutomationElement FindByPropertyName(AutomationElement element, string name) => _automationProvider.FindChildAutomationElement(element, new Locator(AutomationElement.NameProperty, name));

    }
}

