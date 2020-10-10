using System.Windows;

namespace LongOperation
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            vm.Test();
        }
    }
}
