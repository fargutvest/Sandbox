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
            listView1.VirtualMode = true;
            listView1.VirtualListSize = 100000000;

            /*
//            for (int i = 0; i < 10000; i++)
            {

            ListViewItem item = new ListViewItem();
            item.Text = "row1";
            item.SubItems.Add("2");
            item.Checked = true;
            item.ImageIndex = 1;
            item.Group = listView1.Groups[0];
            item.StateImageIndex = 3;

            listView1.Items.Add(item);



            item = listView1.Items.Add("row2", 2);
            item.Group = listView1.Groups[1];
         
        }
            */
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem();
            e.Item.Text = "row" + e.ItemIndex.ToString();
            e.Item.SubItems.Add("2");
            e.Item.SubItems.Add("2");


            e.Item.ImageIndex = 1;
            e.Item.Group = listView1.Groups[0];
            e.Item.StateImageIndex = 3;

            //listView1.Items.Add(item);



            //item = listView1.Items.Add("row2", 2);
            //item.Group = listView1.Groups[1];
        }
    }
}
