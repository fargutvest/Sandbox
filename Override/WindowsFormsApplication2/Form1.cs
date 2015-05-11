using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            parrent[] _parrent = new parrent[2];
            _parrent[0] = new child_a();
            _parrent[1] = new child_b();
            this.Text = _parrent[0].s() + " " + _parrent[1].s();
        }
    }

    public class parrent
    {
        public parrent()
        {
            
        }
        public virtual string s()
        {
            return "parrent";
        }
    }

    public class child_a:parrent
    {
        public child_a()
        {
            
        }

        public override string s()
        {
            return "child_a";
        }

    }

    public class child_b:parrent
    {
        public child_b()
        {
            
        }

        public override string s()
        {
            return "child_b";
        }

    }


}
