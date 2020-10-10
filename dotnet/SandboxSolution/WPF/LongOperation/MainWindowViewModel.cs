using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LongOperation
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {

        }

        public void Test()
        {
            TestOperationAsync();
        }


        private async void TestOperationAsync()
        {
            var val = await LongOpertation();
            var i = val + 8;
        }

        private Task<int> LongOpertation()
        {
            return Task.Run(() =>
            {
                var counter = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    counter++;
                }
                return counter;
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
