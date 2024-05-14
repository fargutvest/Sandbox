using System.ComponentModel;
using System.Windows.Data;

namespace CredentialsPlugin
{
    public class Start
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CollectionView CredentialsCollection { get; }


        private CredentialsElement _selectedCredentials;
        public CredentialsElement SelectedCredentials
        {
            get { return _selectedCredentials; }
            set
            {
                _selectedCredentials = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCredentials)));
            }
        }

        public Start()
        {
            var allCredentials = Settings.GetAllCredentials();
            CredentialsCollection = new CollectionView(allCredentials);
            SelectedCredentials = Settings.SelectCredentials();
        }

    }
}
