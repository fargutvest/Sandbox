using System.ComponentModel;
using System;

namespace WPF_ScreenSaver
{
    /// <summary>
    /// A simple SelectableImageUrl bindable object
    /// </summary>
    public class SelectableImageUrl : INotifyPropertyChanged
    {
        #region Data
        private String imageUrl;
        private Boolean isSelected;
        #endregion

        #region Public Properties

        public String ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (value == imageUrl)
                    return;

                imageUrl = value;
                this.OnPropertyChanged("ImageUrl");
            }
        }

        public Boolean IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected)
                    return;
                isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}
