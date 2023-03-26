using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Navigation;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace PhotosViewer
{
    public partial class DuplicatesWindow : Window
    {
        private DuplicatesWindowViewModel ViewModel => (DataContext as DuplicatesWindowViewModel);

        public DuplicatesWindow()
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
        
        private void RightClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Right();
        }

        private void LeftClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Left();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(Path.GetDirectoryName(e.Uri.ToString())));
            e.Handled = true;
        }
    }
}
