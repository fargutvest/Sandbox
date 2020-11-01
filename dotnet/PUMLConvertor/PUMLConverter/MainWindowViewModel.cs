using System.ComponentModel;
using System.Runtime.CompilerServices;
using PUMLConverter.Annotations;

namespace PUMLConverter
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Source { get; set; }
        public string Result { get; set; }

        public void ToPUML()
        {
            Result = Source.Replace("\r\n", "\\n");
            OnPropertyChanged(nameof(Result));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
