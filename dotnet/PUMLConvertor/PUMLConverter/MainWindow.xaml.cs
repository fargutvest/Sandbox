using System.Windows;

namespace PUMLConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dc = DataContext;
            var vm = dc as MainWindowViewModel;
            vm.ToPUML();
        }
    }
}
