using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using USBClassLibrary;

namespace USBClassLibraryWPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private USBClass USBPort;
        private USBClass.DeviceProperties USBDeviceProperties;

        private bool MyUSBDeviceConnected;
        private bool IsVIDValid;
        private bool IsPIDValid;
        private bool IsMIValid;
        private const uint MyDeviceVID = 0X04D8; //Microchip ICD2 VID
        private const uint MyDevicePID = 0X8001; //Microchip ICD2 PID
        public MainWindow()
        {
            InitializeComponent();

            //Initialize VID and PID TextBoxes
            VIDTextBox.Text = MyDeviceVID.ToString("X4");
            IsVIDValid = true;
            PIDTextBox.Text = MyDevicePID.ToString("X4");
            IsPIDValid = true;
            MITextBox.Text = String.Empty;
            IsMIValid = true;

            //USB Connection
            USBPort = new USBClass();
            USBDeviceProperties = new USBClass.DeviceProperties();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);

            //USB Connection
            USBPort.RegisterForDeviceChange(true, source.Handle);
            USBTryMyDeviceConnection();
            MyUSBDeviceConnected = false;
        }

        #region USB
        /// <summary>
        /// Try to connect to the device.
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
        private bool USBTryMyDeviceConnection()
        {
            Nullable<UInt32> MI = 0;
            bool bGetSerialPort = false;

            if (MITextBox.Text != String.Empty)
            {
                MI = uint.Parse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                MI = null;
            }

            InitializeDeviceTextBoxes();

            if(SerialPortCheckBox.IsChecked == true)
            {
                bGetSerialPort = true;
            }

            if (USBClass.GetUSBDevice(uint.Parse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, bGetSerialPort, MI))
            {
                //My Device is attached
                DeviceTypeTextBox.Text = USBDeviceProperties.DeviceType;
                FriendlyNameTextBox.Text = USBDeviceProperties.FriendlyName;
                DeviceDescriptionTextBox.Text = USBDeviceProperties.DeviceDescription;
                DeviceManufacturerTextBox.Text = USBDeviceProperties.DeviceManufacturer;
                DeviceClassTextBox.Text = USBDeviceProperties.DeviceClass;
                DeviceLocationTextBox.Text = USBDeviceProperties.DeviceLocation;
                DevicePathTextBox.Text = USBDeviceProperties.DevicePath;
                DevicePhysicalObjectNameTextBox.Text = USBDeviceProperties.DevicePhysicalObjectName;
                SerialPortTextBox.Text = USBDeviceProperties.COMPort;
                Connect();

                return true;
            }
            else
            {
                Disconnect();
                return false;
            }
        }

        private void USBPort_USBDeviceAttached(object sender, USBClass.USBDeviceEventArgs e)
        {
            if (!MyUSBDeviceConnected)
            {
                if (USBTryMyDeviceConnection())
                {
                    MyUSBDeviceConnected = true;
                }
            }
        }
        
        private void USBPort_USBDeviceRemoved(object sender, USBClass.USBDeviceEventArgs e)
        {
            if (!USBClass.GetUSBDevice(MyDeviceVID, MyDevicePID, ref USBDeviceProperties, false))
            {
                //My Device is removed
                MyUSBDeviceConnected = false;
                Disconnect();
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            USBPort.ProcessWindowsMessage(msg, wParam, lParam, ref handled);
            
            return IntPtr.Zero;
        }

        private void Connect()
        {
            //TO DO: Insert your connection code here
            MessageBox.Show("Connected!");
            ConnectionStatusLabel.Content = "Connected";
        }

        private void Disconnect()
        {
            //TO DO: Insert your disconnection code here
            MessageBox.Show("Disconnected!");
            ConnectionStatusLabel.Content = "Disconnected";
            InitializeDeviceTextBoxes();
        }

        private void InitializeDeviceTextBoxes()
        {
            DeviceTypeTextBox.Text = String.Empty;
            FriendlyNameTextBox.Text = String.Empty;
            DeviceDescriptionTextBox.Text = String.Empty;
            DeviceManufacturerTextBox.Text = String.Empty;
            DeviceClassTextBox.Text = String.Empty;
            DeviceLocationTextBox.Text = String.Empty;
            DevicePathTextBox.Text = String.Empty;
            DevicePhysicalObjectNameTextBox.Text = String.Empty;
        }
        #endregion

        private void VIDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            uint VID = 0;

            if (!uint.TryParse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out VID))
            {
                IsVIDValid = false;
                ErrorLabel.Content = "VID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F";
            }
            else
            {
                ErrorLabel.Content = "";
                IsVIDValid = true;
            }
        }

        private void PIDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            uint PID = 0;

            if (!uint.TryParse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out PID))
            {
                IsPIDValid = false;
                ErrorLabel.Content = "PID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F";
            }
            else
            {
                ErrorLabel.Content = "";
                IsPIDValid = true;
            }
        }

        private void MITextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            uint MI = 0;

            ErrorLabel.Content = "";
            IsMIValid = true;
            if (MITextBox.Text != String.Empty)
            {
                if (!uint.TryParse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out MI))
                {
                    IsMIValid = false;
                    ErrorLabel.Content = "MI is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F";
                }
            }
        }

        private void UpdateButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsVIDValid && IsPIDValid && IsMIValid)
            {
                USBTryMyDeviceConnection();
            }
        }
    }
}
