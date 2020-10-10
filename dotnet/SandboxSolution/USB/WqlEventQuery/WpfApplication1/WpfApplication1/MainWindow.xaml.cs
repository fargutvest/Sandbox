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
using System.Management;


namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ManagementEventWatcher watcherAttach;
        private ManagementEventWatcher watcherRemove;
        public MainWindow()
        {
            InitializeComponent();
            USBControl();
        }
        public void USBControl()
        {
            var query = new WqlEventQuery("SELECT * From Win32_USBHub");
            
            // Add USB plugged event watching
            watcherAttach = new ManagementEventWatcher();
            var queryAttach = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcherAttach.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.Start();

            // Add USB unplugged event watching
            watcherRemove = new ManagementEventWatcher();
            var queryRemove = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.EventArrived += new EventArrivedEventHandler(watcher_EventRemoved);
            watcherRemove.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.Start();
        }

        void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            // Debug.WriteLine("watcher_EventArrived");
        }

        void watcher_EventRemoved(object sender, EventArrivedEventArgs e)
        {
            // Debug.WriteLine("watcher_EventRemoved");
        }
    }
}
