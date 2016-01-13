using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bool bRightAdmin = isAdministrator();
            Test();
        }

        private static string RegKeyLast = @"SOFTWARE\Adani\MedXTera\";

        private void Test()
        {
            try
            {
                RegistryKey readRk = Registry.LocalMachine.OpenSubKey(RegKeyLast);
                if (readRk != null)
                {
                    object value = readRk.GetValue("keyName");
                }

                RegistryKey rk = Registry.LocalMachine.CreateSubKey(string.Format("{0}1.1", RegKeyLast));
                if (rk != null)
                {
                    rk.SetValue("TestLastWarmingTime", DateTime.Now);
                    rk.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public static bool isAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
