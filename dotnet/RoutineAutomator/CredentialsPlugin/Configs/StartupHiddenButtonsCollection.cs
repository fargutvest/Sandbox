using System.Configuration;

namespace CredentialsPlugin
{
    public class StartupHiddenButtonsCollection : ConfigurationSection
    {
        [ConfigurationProperty("HiddenButtonsCollection")]
        public HiddenButtonsCollection HiddenButtonsCollection
        {
            get { return (HiddenButtonsCollection)base["HiddenButtonsCollection"]; }
        }
    }

    [ConfigurationCollection(typeof(HiddenButtonElement))]
    public class HiddenButtonsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new HiddenButtonElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HiddenButtonElement)element).Id;
        }

        public HiddenButtonElement this[int idx]
        {
            get { return (HiddenButtonElement)BaseGet(idx); }
        }
    }

    public class HiddenButtonElement : ConfigurationElement
    {
        [ConfigurationProperty("id", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Id
        {
            get { return (string)base["id"]; }
            set { base["id"] = value; }
        }
    }
}
