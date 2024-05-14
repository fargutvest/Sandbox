using System.Configuration;

namespace CredentialsPlugin
{
    public class StartupCredentialsCollection : ConfigurationSection
    {
        [ConfigurationProperty("CredentialsCollection")]
        public CredentialsCollection CredentialsCollection
        {
            get { return (CredentialsCollection)base["CredentialsCollection"]; }
        }
    }

    [ConfigurationCollection(typeof(CredentialsElement))]
    public class CredentialsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CredentialsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CredentialsElement)element).Title;
        }

        public CredentialsElement this[int idx]
        {
            get { return (CredentialsElement)BaseGet(idx); }
        }
    }

    public class CredentialsElement : ConfigurationElement
    {

        [ConfigurationProperty("title", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Title
        {
            get { return (string)base["title"]; }
            set { base["title"] = value; }
        }

        [ConfigurationProperty("instanceId", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string InstanceId
        {
            get { return (string)base["instanceId"]; }
            set { base["instanceId"] = value; }
        }

        [ConfigurationProperty("pseudoId", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string PseudoId
        {
            get { return (string)base["pseudoId"]; }
            set { base["pseudoId"] = value; }
        }

        [ConfigurationProperty("userId", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string UserID
        {
            get { return (string)base["userId"]; }
            set { base["userId"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }
    }

}
