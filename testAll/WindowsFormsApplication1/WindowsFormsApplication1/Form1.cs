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
        Generator generator;
        Form2 form2;

        public Form1()
        {
            InitializeComponent();
            generator = new Generator();
            generator.evAutoSearch += generator_evAutoSearch;
            form2 = new Form2(generator);
           
                
        }

        void generator_evAutoSearch()
        {
            form2.Location = new Point(this.Location.X, this.Location.Y + 30);
            if (form2.ShowDialog() == DialogResult.OK)
            {

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            generator.SelectGenerator();
        }

    }
}
