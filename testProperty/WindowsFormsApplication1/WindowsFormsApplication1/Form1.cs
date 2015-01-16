using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            bool b = _udpMissing;
            _udpMissing = true;
            b = _udpMissing;
            _udpMissing = false;
            b = _udpMissing;
        }


        bool _udpMissing
        {
            get;

            set
            {
                _udpMissing = value;
                if (_udpMissing == true)
                {

                }
            }
        }

    }
}
