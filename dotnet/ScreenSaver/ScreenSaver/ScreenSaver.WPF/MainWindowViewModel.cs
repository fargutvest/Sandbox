using Share;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace ScreenSaver.WPF
{
    public class MainWindowViewModel : NotifyPropertyChangeBase
    {
        public ObservableCollection<EllipseItem> EllipseItems { get; set; }
        private Share.Share share;

        public MainWindowViewModel()
        {
            share = new Share.Share(new Size(800, 600));
            
            share.DrawEllipse += Share_DrawEllipse;

            EllipseItems = new ObservableCollection<EllipseItem>();
           // Test();
        }

        private void Share_DrawEllipse(object sender, EllipseItem e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (Action)(()=> 
            {
                EllipseItems.Add(e);
            }));
        }
        

        public void Test()
        {
            var size = 7;
            EllipseItems.Add(new EllipseItem()
            {
                Width = size,
                Height = size,
                X = 20,
                Y = 50
            });
        }
       
    }
}
