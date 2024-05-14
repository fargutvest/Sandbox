using RPA.Report;
using System;
using System.Configuration;

namespace CredentialsPlugin
{
    public static class Settings
    {
        internal static string INSTANCE_TITLE = ConfigurationManager.AppSettings[nameof(INSTANCE_TITLE)];
        internal static string SCRIPT_PATH = ConfigurationManager.AppSettings[nameof(SCRIPT_PATH)];


        public static CredentialsElement SelectCredentials(IReport report = null)
        {
            var section = (StartupCredentialsCollection)ConfigurationManager.GetSection("StartupCredentialsCollection");

            if (section != null)
            {
                foreach (CredentialsElement item in section.CredentialsCollection)
                {
                    if (item.Title == INSTANCE_TITLE)
                    {
                        return item;
                    }
                }

                report?.WriteLine("Select cretentials:");
                var i = 1;
                foreach (CredentialsElement item in section.CredentialsCollection)
                {
                    report?.WriteLine($"{i}: {item.InstanceId}; {item.UserID}; {item.Password}");
                    i++;
                }
                return section.CredentialsCollection[int.Parse(Console.ReadLine()) - 1];
            }
            throw new Exception($"{nameof(StartupCredentialsCollection)} should be presented");
        }

        public static CredentialsCollection GetAllCredentials(IReport report = null)
        {
            var section = (StartupCredentialsCollection)ConfigurationManager.GetSection("StartupCredentialsCollection");

            if (section != null)
            {
                return section.CredentialsCollection;
            }
            throw new Exception($"{nameof(StartupCredentialsCollection)} should be presented");
        }

       
       

        public static HiddenButtonsCollection GetAllHiddenButtons()
        {
            var section = (StartupHiddenButtonsCollection)ConfigurationManager.GetSection("StartupHiddenButtonsCollection");

            if (section != null)
            {
                return section.HiddenButtonsCollection;
            }
            throw new Exception($"{nameof(StartupHiddenButtonsCollection)} should be presented");
        }
    }
}
