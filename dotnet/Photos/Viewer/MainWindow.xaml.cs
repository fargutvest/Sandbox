using System.Windows;
using System.Windows.Input;

namespace Viewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            (DataContext as MainWindowViewModel).On_KeyDown(sender, e);
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Focus();
        }
    }
}
