using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridViewComboBoxColumn tt =  new System.Windows.Forms.DataGridViewComboBoxColumn();

           tt =  (System.Windows.Forms.DataGridViewComboBoxColumn)this.dataGridView1.Columns[2];
            
            textBox1.Text = tt.Items[1].ToString();
            
        }
    }
}
