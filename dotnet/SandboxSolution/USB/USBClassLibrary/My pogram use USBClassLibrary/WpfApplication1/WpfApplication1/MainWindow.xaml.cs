﻿using System;
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
using USBClassLibrary;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const uint MyDeviceVID = 0X04D8; //Microchip ICD2 VID
        private const uint MyDevicePID = 0X8001; //Microchip ICD2 PID
        private USBClassLibrary.USBClass USBPort;
        private USBClassLibrary.USBClass.DeviceProperties USBDeviceProperties;
        bool MyUSBDeviceConnected;
        public MainWindow()
        {
            InitializeComponent();
            VIDTextBox.Text = MyDeviceVID.ToString("X4");
            PIDTextBox.Text = MyDevicePID.ToString("X4");


            //USB Connection
            USBPort = new USBClass();
            USBDeviceProperties = new USBClass.DeviceProperties();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
            /*USBPort.RegisterForDeviceChange(true, this.Handle);
            USBTryMyDeviceConnection();
            MyUSBDeviceConnected = false;*/
        }
        #region USB
        /// <summary>
        /// Try to connect to the device.
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
        private bool USBTryMyDeviceConnection()
        {
            Nullable<UInt32> MI = 0;

            if (MITextBox.Text != String.Empty)
            {
                MI = uint.Parse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                MI = null;
            }

            InitializeDeviceTextBoxes();

            if (USBClass.GetUSBDevice(uint.Parse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, SerialPortCheckBox.Checked, MI))
            {
                //My Device is attached
              /*  DeviceTypeTextBox.Text = USBDeviceProperties.DeviceType;
                FriendlyNameTextBox.Text = USBDeviceProperties.FriendlyName;
                DeviceDescriptionTextBox.Text = USBDeviceProperties.DeviceDescription;
                DeviceManufacturerTextBox.Text = USBDeviceProperties.DeviceManufacturer;
                DeviceClassTextBox.Text = USBDeviceProperties.DeviceClass;
                DeviceLocationTextBox.Text = USBDeviceProperties.DeviceLocation;
                DevicePathTextBox.Text = USBDeviceProperties.DevicePath;
                DevicePhysicalObjectNameTextBox.Text = USBDeviceProperties.DevicePhysicalObjectName;
                SerialPortTextBox.Text = USBDeviceProperties.COMPort;*/
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

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            bool IsHandled = false;

            USBPort.ProcessWindowsMessage(m.Msg, m.WParam, m.LParam, ref IsHandled);

            //base.WndProc(ref m);
        }

        private void Connect()
        {
            //TO DO: Insert your connection code here
            MessageBox.Show("Connected!");
            ConnectionToolStripStatusLabel.Text = "Connected";
        }

        private void Disconnect()
        {
            //TO DO: Insert your disconnection code here
            MessageBox.Show("Disconnected!");
            ConnectionToolStripStatusLabel.Text = "Disconnected";
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

        private void VIDTextBox_Leave(object sender, EventArgs e)
        {
            uint VID = 0;

            if (!uint.TryParse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out VID))
            {
                VIDTextBox.Focus();
                ErrorProvider.SetError(VIDTextBox, "VID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
            }
            else
            {
                ErrorProvider.SetError(VIDTextBox, "");
            }
        }

        private void PIDTextBox_Leave(object sender, EventArgs e)
        {
            uint PID = 0;

            if (!uint.TryParse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out PID))
            {
                PIDTextBox.Focus();
                ErrorProvider.SetError(PIDTextBox, "PID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
            }
            else
            {
                ErrorProvider.SetError(PIDTextBox, "");
            }
        }

        private void MITextBox_Leave(object sender, EventArgs e)
        {
            uint MI = 0;

            ErrorProvider.SetError(MITextBox, "");
            if (MITextBox.Text != String.Empty)
            {
                if (!uint.TryParse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out MI))
                {
                    MITextBox.Focus();
                    ErrorProvider.SetError(MITextBox, "MI is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            USBTryMyDeviceConnection();
        }

    }
}
