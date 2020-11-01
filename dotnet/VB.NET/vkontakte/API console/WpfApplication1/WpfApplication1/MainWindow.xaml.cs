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
using System.Web;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json.Linq;
using vkontakte;


namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {

        string ACCESS_TOKEN;
        string USER_ID;
        string MY_ID = "6811515";

        Frame myFrame = new Frame();
        public MainWindow()
        {
            InitializeComponent();
        }


    }
}
