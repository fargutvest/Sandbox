using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBClassLibrary;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private USBClass USBPort;
        private USBClass.DeviceProperties USBDeviceProperties;
        bool MyUSBDeviceConnected;
        private const uint MyDeviceVID = 0X3538; 
        private const uint MyDevicePID = 0X0054; 
        
        
        public Form1()
        {
            InitializeComponent();
            //Initialize VID and PID TextBoxes
            VIDTextBox.Text = MyDeviceVID.ToString("X4");
            PIDTextBox.Text = MyDevicePID.ToString("X4");

            //USB Connection
            USBPort = new USBClass();
            USBDeviceProperties = new USBClass.DeviceProperties();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
            USBPort.RegisterForDeviceChange(true, this.Handle);
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

            if (MITextBox.Text != String.Empty)
            {
                MI = uint.Parse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                MI = null;
            }

            InitializeDeviceTextBoxes();

            uint vid = uint.Parse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            uint pid = uint.Parse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);

            if (USBClass.GetUSBDevice(vid,pid, ref USBDeviceProperties, true, MI))
            {
                USBClass uc = new USBClass();
                IntPtr i = new IntPtr();
                uc.RegisterForDeviceChange(true, i);
                //My Device is attached]
                listView1.Items.Clear();
                listView1.Items.Add("DeviceType:" + USBDeviceProperties.DeviceType);
                listView1.Items.Add("FriendlyName:" + USBDeviceProperties.FriendlyName);
                listView1.Items.Add("DeviceDescription:" + USBDeviceProperties.DeviceDescription);
                listView1.Items.Add("DeviceManufacturer:" + USBDeviceProperties.DeviceManufacturer);
                listView1.Items.Add("DeviceClass:" + USBDeviceProperties.DeviceClass);
                listView1.Items.Add("DeviceLocation:" + USBDeviceProperties.DeviceLocation);
                listView1.Items.Add("DevicePath:" + USBDeviceProperties.DevicePath);
                listView1.Items.Add("DevicePhysicalObjectName:" + USBDeviceProperties.DevicePhysicalObjectName);
                listView1.Items.Add("COMPort:" + USBDeviceProperties.COMPort);

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

        protected override void WndProc(ref Message m)
        {
            bool IsHandled = false;

            USBPort.ProcessWindowsMessage(m.Msg, m.WParam, m.LParam, ref IsHandled);

            base.WndProc(ref m);
        }

        private void Connect()
        {
            //TO DO: Insert your connection code here
           // MessageBox.Show("Connected!");
           // ConnectionToolStripStatusLabel.Text = "Connected";
        }

        private void Disconnect()
        {
            //TO DO: Insert your disconnection code here
            MessageBox.Show("Disconnected!");
           // ConnectionToolStripStatusLabel.Text = "Disconnected";
            InitializeDeviceTextBoxes();
        }

        private void InitializeDeviceTextBoxes()
        {
          /*  DeviceTypeTextBox.Text = String.Empty;
            FriendlyNameTextBox.Text = String.Empty;
            DeviceDescriptionTextBox.Text = String.Empty;
            DeviceManufacturerTextBox.Text = String.Empty;
            DeviceClassTextBox.Text = String.Empty;
            DeviceLocationTextBox.Text = String.Empty;
            DevicePathTextBox.Text = String.Empty;
            DevicePhysicalObjectNameTextBox.Text = String.Empty;*/
        }
        #endregion

        private void VIDTextBox_Leave(object sender, EventArgs e)
        {
            uint VID = 0;

            if (!uint.TryParse(VIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out VID))
            {
                VIDTextBox.Focus();
              //  ErrorProvider.SetError(VIDTextBox, "VID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
            }
            else
            {
               // ErrorProvider.SetError(VIDTextBox, "");
            }
        }

        private void PIDTextBox_Leave(object sender, EventArgs e)
        {
            uint PID = 0;

            if (!uint.TryParse(PIDTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out PID))
            {
                PIDTextBox.Focus();
               // ErrorProvider.SetError(PIDTextBox, "PID is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
            }
            else
            {
              //  ErrorProvider.SetError(PIDTextBox, "");
            }
        }

        private void MITextBox_Leave(object sender, EventArgs e)
        {
            uint MI = 0;

           // ErrorProvider.SetError(MITextBox, "");
            if (MITextBox.Text != String.Empty)
            {
                if (!uint.TryParse(MITextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier, new System.Globalization.CultureInfo("en-US"), out MI))
                {
                    MITextBox.Focus();
                   // ErrorProvider.SetError(MITextBox, "MI is expected to be an hexadecimal number, allowed characters: 0 to 9, A to F");
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            USBTryMyDeviceConnection();
        }
    }
}
