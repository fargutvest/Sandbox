using RenderingCommon;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace VideoGenerator
{
    public partial class MainWindow : Window
    {
        private static readonly int height = RenderPreferences.Height;

        private Random random;
        private ServiceProvider serviceProvider;
        private MetricPerSecond metricLps;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            serviceProvider = new ServiceProvider();
            random = new Random();
            metricLps = new MetricPerSecond();

            Task.Run(() =>
            {
                while (true)
                {
                    metricLps.Tick();
                    var generatedVerticalLine = new int[height];
                    for (var i = 0; i < generatedVerticalLine.Length; i++)
                    {
                        generatedVerticalLine[i] = random.Next(0, 0xFFFFFF);
                    }
                    serviceProvider.Channel.NewVerticalLine(generatedVerticalLine);
                }
            });
        }


    }
}

