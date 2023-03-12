using System.ComponentModel;

namespace Piatnashki
{
    internal class SquareViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public bool IsZero { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
