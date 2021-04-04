using System.Windows;
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Viewer
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (DataContext as MainWindowViewModel);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel?.On_KeyDown(sender, e);
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.PhotosFolder = fbd.SelectedPath;
            }
        }
    }
}
