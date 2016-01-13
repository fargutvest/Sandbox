using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
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
            Get();
        }

        private void Get()
        {
            //method 1
            using (var groups = UserPrincipal.Current.GetGroups())
            {
                foreach (var u in groups)
                {
                    Debug.WriteLine("{0} ", u.Name);
                }  
            }     

            //method 2
            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            foreach (var u in wi.Groups)
            {
                Debug.WriteLine("{0} ", u.Value);
            }

         
        }
    }
}
