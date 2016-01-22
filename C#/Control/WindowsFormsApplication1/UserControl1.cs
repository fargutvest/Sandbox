using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MyControl : UserControl
    {
        public Size MyParametr1
        {
            get;
            set;
        }

        public ControlParametr Myparametr2
        {
            get;
            set;
        }
        public Point Myparametr3
        {
            get;
            set;
        }



        public MyControl()
        {
            Myparametr2 = new ControlParametr(4, 4);
            InitializeComponent();
            
        }


    }
}
