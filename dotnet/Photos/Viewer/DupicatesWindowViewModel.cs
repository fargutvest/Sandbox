using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PhotosViewer
{
    public class DuplicatesWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NameAndSizeKey _duplicatesKey;
        public NameAndSizeKey DuplicatesKey
        {
            get
            {
                return _duplicatesKey;
            }
            set
            {
                _duplicatesKey = value;
                DuplicatePhotos = _duplicates.Collection[_duplicatesKey].FindAll(_=> _.Info.FullName != _duplicatesKey.Info.FullName);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DuplicatesKey)));
            }
        }

        private List<FileMetadata> _duplicatePhotos;
        public List<FileMetadata> DuplicatePhotos
        {
            get
            {
                return _duplicatePhotos;
            }
            set
            {
                _duplicatePhotos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DuplicatePhotos)));
            }
        }
        

        private int _index;
        private DuplicatesCollection _duplicates;

        public DuplicatesWindowViewModel()
        {
            Init();
        }

        public void On_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Right();
            }
            if (e.Key == Key.Left)
            {
                Left();
            }
        }

        public void Right()
        {
            if (_duplicates?.Collection.Any() == true && _index < _duplicates.Collection.Keys.Count() - 1)
            {
                _index += 1;
                DuplicatesKey = _duplicates.Collection.Keys.ToArray()[_index];
            }
        }

        public void Left()
        {
            if (_duplicates?.Collection.Any() == true && _index > 0)
            {
                _index -= 1;
                DuplicatesKey = _duplicates.Collection.Keys.ToArray()[_index];
            }
        }


        public void Init()
        {
            var duplicatesHelper = new DuplicatesHelper();
            duplicatesHelper.Init();
            _duplicates = duplicatesHelper.Duplicates;
            DuplicatesKey = _duplicates.Collection.Keys.ToArray()[0];
        }
    }

}
