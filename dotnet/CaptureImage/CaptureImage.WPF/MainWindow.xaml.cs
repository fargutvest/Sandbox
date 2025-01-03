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

namespace CaptureImage.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            // Получаем размеры экрана
            var screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 2;
            var screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна по всему экрану
            this.Width = screenWidth;
            this.Height = screenHeight;
            var polygonGeometry = new PathGeometry();
            var figure = new PathFigure(new Point(-1920, 0), new PathSegment[]
            {
                new LineSegment(new Point(0, 0), true), // Верхний правый угол
                new LineSegment(new Point(screenWidth, 0), true), // Нижний правый угол
                new LineSegment(new Point(screenWidth, screenHeight), true), // Нижний центр
                new LineSegment(new Point(0, screenHeight), true), // Нижний левый угол
            }, true);

            polygonGeometry.Figures.Add(figure);

            // Устанавливаем форму окна
            this.Clip = polygonGeometry;

            // Устанавливаем начальное положение окна в левый верхний угол
            this.Left = -1920;
            this.Top = 0;
        }
    }
}
