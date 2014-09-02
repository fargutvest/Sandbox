using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int OPEN_EXISTING = 3; 
        const uint GENERIC_READ = 0x80000000; 
        const uint GENERIC_WRITE = 0x40000000; 
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
        List<string> kvp;


        [System.Runtime.InteropServices.DllImport("kernel32")] 
        private static extern int CloseHandle(IntPtr handle); 

        [System.Runtime.InteropServices.DllImport("kernel32")] 
        private static extern int DeviceIoControl (IntPtr deviceHandle, uint ioControlCode, IntPtr inBuffer, int inBufferSize, IntPtr outBuffer, int outBufferSize, ref int bytesReturned, IntPtr overlapped); 

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern IntPtr CreateFile (string filename, uint desiredAccess, uint shareMode, IntPtr securityAttributes, int creationDisposition, int flagsAndAttributes, IntPtr templateFile);


        //Получение списка букв USB накопителей 
        private void UsbDiskList() 
        { 
            string diskName = string.Empty; 
            kvp = new List<string>(); 
            //предварительно очищаем список 
            comboBox1.Items.Clear(); 
            //Получение списка накопителей подключенных через интерфейс USB 
            foreach (ManagementObject drive in new ManagementObjectSearcher( "select * from Win32_DiskDrive where InterfaceType='USB'").Get()) 
            {
                //Получаем букву накопителя 
                foreach (ManagementObject partition in new ManagementObjectSearcher( "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get()) 
                { 
                    foreach (ManagementObject disk in new ManagementObjectSearcher( "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get()) 
                    { 
                        //Получение буквы устройства 
                        diskName = disk["Name"].ToString().Trim(); 
                        comboBox1.Items.Add(diskName + " (" + drive["Model"] + ")"); kvp.Add(diskName); 
                    } 
                } 
            } 
        } 


        //метод для извлечения USB накопителя 
        static void EjectDrive(string driveLetter) 
        {
           // string path = "\\\\.\\" + driveLetter;
            //string path = @"\?usb#vid_0647&pid_1002#0000#{36fc9e60-c465-11cf-8056-444553540000}"; 
            string path = "\\\\.\\USBPDO-8";
            IntPtr handle = CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero); 
            if ((long)handle == -1) 
            { 
                MessageBox.Show("Невозможно извлечь USB устройство!", "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return; 
            } 
            int dummy = 0; 
            DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero); 
            int returnValue = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero); 
            CloseHandle(handle); 
            MessageBox.Show("USB устройство, успешно извлечено!", "Извлечение USB накопителей",MessageBoxButtons.OK,MessageBoxIcon.Information); 
        } 
        private void LoadInfo() 
        { 
            //Загрузка букв USB накопителей при запуске программы 
            UsbDiskList(); 
            
            

        }




        public Form1()
        {
            InitializeComponent();
            test();
        }
        void test()
        {
            LoadInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EjectDrive(kvp[comboBox1.SelectedIndex]); 
            //Обновляем список накопителей 
            LoadInfo();
        }

      





    }
}
