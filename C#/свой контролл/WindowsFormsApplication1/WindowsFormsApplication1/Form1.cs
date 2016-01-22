using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestCSharpControl;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        EmbossedProgressBar empbr = new EmbossedProgressBar();
        public Form1()
        {
            InitializeComponent();
            this.empbr.Location = new System.Drawing.Point(50, 10);
            this.empbr.Name = "button1";
            this.empbr.Size = new System.Drawing.Size(75, 23);
            this.empbr.TabIndex = 0;
            this.empbr.Text = "button1";
            this.Controls.Add(this.empbr);
            
            // 
        }
    }
}
