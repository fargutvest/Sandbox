using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;

namespace testDetectorCalibration
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableDataSource<Point> source1 = null;
        public MainWindow()
        {
            InitializeComponent();
            chartPlotter1.AddLineGraph(source1, 2, "Data row 1");
            
            Simulation();
        }


        private void Simulation()
        {
            
            double x = 1;
            double y1 = 2;
            Point p1 = new Point(x, y1);
            source1.AppendAsync(Dispatcher, p1);
            
            
        }
    }
}
