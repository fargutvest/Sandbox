using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace PhotosViewer
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
        
        private void SelectFolderClick(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.PhotosFolder = fbd.SelectedPath;
            }
        }

        private void RightClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Right();
        }

        private void LeftClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Left();
        }

        private void MarkClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Mark((sender as ToggleButton).IsChecked == true);
        }

        private void DuplicatesClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Duplicates();
        }
    }
}
