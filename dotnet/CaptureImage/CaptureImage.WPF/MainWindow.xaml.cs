using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System.Windows;
using CaptureImage.Common.Converters;
using System.Windows.Media;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interop;

namespace CaptureImage.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSelecting;
        private Point selectingMousePos;
        private Point selectingMouseStartPos;
       
        private bool isDragging;
        private Point draggingMouseStartPos;
        private Point draggingMousePos;

        public MainWindow()
        {
            InitializeComponent();

            RenderOptions.ProcessRenderMode = RenderMode.Default;

            DescktopInfo info = ScreensHelper.GetDesktopInfo();

            this.Width = info.ClientSize.Width;
            this.Height = info.ClientSize.Height;

            this.Clip = info.PolygonGeometry;

            this.Left = info.Location.X;
            this.Top = info.Location.Y;
            this.Background = BitmapHelper.ChangeOpacity(info.Background, 0.5f).Convert();

            Canvas.SetLeft(Thumb, 0);
            Canvas.SetTop(Thumb, 0);
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isSelecting)
            {
                selectingMousePos = e.GetPosition(this);
                Thumb.Visibility = Visibility.Hidden;

                double thumbWidth = Math.Abs(selectingMousePos.X - selectingMouseStartPos.X);
                double thumbHeight = Math.Abs(selectingMousePos.Y - selectingMouseStartPos.Y);
                Thumb.Width = thumbWidth;
                Thumb.Height = thumbHeight;

                if (selectingMouseStartPos.X < selectingMousePos.X && selectingMouseStartPos.Y < selectingMousePos.Y)
                {
                    Canvas.SetLeft(Thumb, selectingMouseStartPos.X);
                    Canvas.SetTop(Thumb, selectingMouseStartPos.Y);
                }

                if (selectingMouseStartPos.X > selectingMousePos.X && selectingMouseStartPos.Y < selectingMousePos.Y)
                {
                    Canvas.SetLeft(Thumb, selectingMousePos.X);
                    Canvas.SetTop(Thumb, selectingMouseStartPos.Y);
                }

                if (selectingMouseStartPos.X < selectingMousePos.X && selectingMouseStartPos.Y > selectingMousePos.Y)
                {
                    Canvas.SetLeft(Thumb, selectingMouseStartPos.X);
                    Canvas.SetTop(Thumb, selectingMousePos.Y);
                }

                if (selectingMouseStartPos.X > selectingMousePos.X && selectingMouseStartPos.Y > selectingMousePos.Y)
                {
                    Canvas.SetLeft(Thumb, selectingMousePos.X);
                    Canvas.SetTop(Thumb, selectingMousePos.Y);
                }


                Thumb.Visibility = Visibility.Visible;
                Thumb.Focus();
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Thumb.Width = 0;
                Thumb.Height = 0;

                selectingMouseStartPos = e.GetPosition(this);

                isSelecting = true;
            }
        }

        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isSelecting = false;
            }
        }


        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                draggingMousePos = e.GetPosition(MainCanvas);

                Canvas.SetLeft(Thumb, draggingMousePos.X - draggingMouseStartPos.X);
                Canvas.SetTop(Thumb, draggingMousePos.Y - draggingMouseStartPos.Y);
            }

            // e.Handled = true;
        }

        private void Thumb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                draggingMouseStartPos = e.GetPosition(Thumb);
                Thumb.CaptureMouse();
            }

            e.Handled = true;
        }

        private void Thumb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Thumb.ReleaseMouseCapture();

            // e.Handled = true;
        }
    }
}
