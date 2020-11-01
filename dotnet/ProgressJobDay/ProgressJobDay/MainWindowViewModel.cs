using System;
using System.Threading;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;

namespace ProgressJobDay
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private Timer timer;
        private TimeSpan durationJobDay = new TimeSpan(9, 0, 0);
        private string timeFormat = @"hh\:mm";

        #region Properties

        private int progress;
        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private int maximum;

        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                OnPropertyChanged(nameof(Maximum));
            }
        }

        private string selectedBeginJobDay;

        public string SelectedBeginJobDay
        {
            get => selectedBeginJobDay;
            set
            {
                selectedBeginJobDay = value;
                CalculatedEndJobDay = (TimeSpan.Parse(SelectedBeginJobDay) + durationJobDay).ToString(timeFormat);
                OnPropertyChanged(nameof(SelectedBeginJobDay));
            }
        }

        private string calculatedEndJobDay;

        public string CalculatedEndJobDay
        {
            get => calculatedEndJobDay;
            set
            {
                calculatedEndJobDay = value;
                OnPropertyChanged(nameof(CalculatedEndJobDay));
            }
        }

        private string[] beginJobDayVariants = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" };

        public string[] BeginJobDayVariants
        {
            get => beginJobDayVariants;
            set
            {
                beginJobDayVariants = value;
                OnPropertyChanged(nameof(BeginJobDayVariants));
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            SelectedBeginJobDay = GetBeginJobDay().ToString(timeFormat);
            Maximum = (int)durationJobDay.TotalSeconds;
            timer = new Timer(OnTimerTick, null, 0, 100);
        }

        private void SaveToConfigFile(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void OnClosing()
        {
            SaveToConfigFile(nameof(SelectedBeginJobDay), SelectedBeginJobDay);
        }

        
        private void OnTimerTick(object obj)
        {
            try
            {
                Progress = (int)(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) - TimeSpan.Parse(SelectedBeginJobDay)).TotalSeconds;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private TimeSpan GetBeginJobDay()
        {
            TimeSpan value;
            try
            {
                value = new TimeSpan(DateTime.Parse(ConfigurationManager.AppSettings[nameof(SelectedBeginJobDay)]).Ticks);
            }
            catch (Exception)
            {
                value = TimeSpan.Parse(BeginJobDayVariants.First());
            }

            return value;
        }
    }
}
