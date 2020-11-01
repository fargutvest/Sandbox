using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPixelsDisplay
{
    public partial class Form1 : Form
    {
        List<Color> Colors;
        int itherator = 0;

        public Form1()
        {
            InitializeComponent();

            Colors = new List<Color>();
            Colors.Add(Color.White);
            Colors.Add(Color.Red);
            Colors.Add(Color.Green);
            Colors.Add(Color.Blue);
            Colors.Add(Color.Black);
            Colors.Add(Color.FromArgb(0xff, 0xff, 0));
            Colors.Add(Color.FromArgb(0xff, 0, 0xff));
            Colors.Add(Color.FromArgb(0, 0xff, 0xff));

            Cursor.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.BackColor = Colors[itherator];
            this.Refresh();
            itherator++;
            if (itherator == Colors.Count)
                itherator = 0;

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Escape:
                    if (MessageBox.Show("Хотите выйти?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Close();
                    }
                    break;

                case (char)Keys.Space:
                    Form1_Click(this, null);
                    break;
            }

        }
    }
}
