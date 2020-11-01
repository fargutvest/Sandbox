using System;
using System.ComponentModel;
using System.Windows;

namespace ProgressJobDay
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

        protected override void OnClosing(CancelEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm.OnClosing();
        }
    }
}
