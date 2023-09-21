using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace BankOperations
{
    public class Cache
    {
        private string CacheFileName => ConfigurationManager.AppSettings[nameof(CacheFileName)];
        public List<string> UserInputHistory { get; private set; } = new List<string>();


        public void Save()
        {
            if (File.Exists(CacheFileName))
            {
                File.Delete(CacheFileName);
            }
            File.WriteAllLines(CacheFileName, UserInputHistory.Distinct());
        }

        public void Load()
        {
            try
            {
                UserInputHistory = File.ReadAllLines(CacheFileName).ToList();
            }
            catch
            {
            }
        }
    }
}
