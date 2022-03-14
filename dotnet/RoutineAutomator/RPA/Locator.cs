using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace RPA
{
    internal class Locator
    {
        internal AutomationProperty Type { get; }
        internal object Value { get; }

        private List<Locator> _andLocators;
        private List<Locator> _orLocators;

        internal List<Locator> GetAndLocators() => _andLocators;
        internal List<Locator> GetOrLocators() => _orLocators;


        internal Locator(AutomationProperty locatorType, object locatorValue)
        {
            Type = locatorType;
            Value = locatorValue;
            _andLocators = new List<Locator>();
            _orLocators = new List<Locator>();
        }

        public override string ToString()
        {
            return "{"+ $"{Type.ProgrammaticName.Split('.').Last()}; '{Value}'" + "}";
        }

        public Locator And(Locator locator)
        {
            _andLocators.Add(locator);
            return this;
        }

        public Locator Or(Locator locator)
        {
            _orLocators.Add(locator);
            return this;
        }
    }
}
