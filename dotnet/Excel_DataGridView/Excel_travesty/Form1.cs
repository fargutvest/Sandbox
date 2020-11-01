using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using excel=Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Reflection;


namespace Excel_travesty
{
    public partial class Form1 : Form
    {       int index,Rowindex,Columnindex;
            DataGridView[] dg = new DataGridView[10];
            double function;
            string filename;
            bool savedone;
            Object ob;
        
        public Form1()
        {
            InitializeComponent();
            index = 1;
            savedone = false;
           

        }
                
        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0; int k = 1;
            button2.Visible = false;                     
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                dataGridView1.Columns.Add(ch.ToString(), ch.ToString());
                dataGridView1.Columns[i].HeaderText = ch.ToString();
                dataGridView1.Rows.Add();
                dataGridView1.Columns[i].Width = 70;
                k = i + 1; 
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                dataGridView1.Rows[i].HeaderCell.Value = k.ToString();
                i++;                             
            }
                    
        }

        private void листToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (index < 10)
            {
                
                int schet, newpagekey;
                schet = tabControl1.TabCount;
                newpagekey = schet + 1;

                TabPage tp = new TabPage();
                tp.BackColor = Color.White;
                tp.Text = "Лист" + newpagekey.ToString();
                tabControl1.TabPages.Add(tp);

                dg[index] = new DataGridView();
                dg[index].Width = 661;
                dg[index].Height = 322;


                int i = 0; int k = 1;
                for (char ch = 'A'; ch <= 'Z'; ch++)
                {
                    dg[index].Columns.Add(ch.ToString(), ch.ToString());
                    dg[index].Columns[i].HeaderText = ch.ToString();
                    dg[index].Rows.Add();
                    dg[index].Columns[i].Width = 70;
                    k = i + 1;
                    dg[index].AllowUserToResizeRows = false;
                    dg[index].AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                    dg[index].Rows[i].HeaderCell.Value = k.ToString();
                    i++;
                }
                tp.Controls.Add(dg[index]);
            }
            else листToolStripMenuItem.Enabled = false;
            index++;
        }       
                    

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
                      
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void форматToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void строкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (tabControl1.SelectedIndex > 0)
            {
                dg[tabControl1.SelectedIndex].Rows.Add();
                int rowcount = dg[tabControl1.SelectedIndex].RowCount;
                int i;
                for (i = 0; i < rowcount; i++)
                {
                    dg[tabControl1.SelectedIndex].AllowUserToResizeRows = false;
                    dg[tabControl1.SelectedIndex].AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                    dg[tabControl1.SelectedIndex].Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            else dataGridView1.Rows.Add();
        }

       
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rowcount = dataGridView1.RowCount;
            int i;
            for (i = 0; i < rowcount; i++)
            {
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                dataGridView1.Rows[i].HeaderCell.Value = (i+1).ToString();
            }
        }
       
        
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string x, y;
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                 x = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                 y = dataGridView1.Rows[e.RowIndex].HeaderCell.Value.ToString();
                 Rowindex = e.RowIndex; Columnindex = e.ColumnIndex;
                 textBox2.Text = x + y;
            }
            
            
            
                      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Visible = true;
            comboBox1.Enabled = false;
            int counter;

            if (comboBox1.SelectedIndex == 0 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    function = Math.Abs(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }

            if (comboBox1.SelectedIndex == 1 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0 )
            {
                counter = 0;
                try
                {
                    function = Math.Cos(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }

            if (comboBox1.SelectedIndex == 2 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    function = Math.Exp(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 3 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    function = Math.Sin(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 4 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    function = Math.Tan(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 5 && dataGridView1.SelectedCells.Count == 1 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    function = Math.Sqrt(Convert.ToDouble(dataGridView1.SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 6 && dataGridView1.SelectedCells.Count==2 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    int a = Convert.ToInt32(dataGridView1.SelectedCells[1].Value);
                    int b = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
                    int result = 0;
                    int result2;
                    result2 =a-(Math.DivRem(a, b, out result)*b);
                    function = result2;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
                
            }
            if (comboBox1.SelectedIndex == 7 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    double multyplex=1;
                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)

                        multyplex =multyplex*Convert.ToDouble(dataGridView1.SelectedCells[counter].Value);
                    function = multyplex;
                 }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 8 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    double summ = 0;
                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)

                        summ = summ + Convert.ToDouble(dataGridView1.SelectedCells[counter].Value);
                    function = summ;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 9 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    double max= 0;
                    double[] mas;
                    mas = new double[dataGridView1.SelectedCells.Count];

                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                        mas[counter] = Convert.ToDouble(dataGridView1.SelectedCells[counter].Value);
                    max = mas[0];
                    for (int i = 1; i < (dataGridView1.SelectedCells.Count); i++)
                        if (max < mas[i]) max = mas[i];
                        function = max;
                    
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 10 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                try
                {
                    double min = 0;
                    double[] mas;
                    mas = new double[dataGridView1.SelectedCells.Count];

                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                        mas[counter] = Convert.ToDouble(dataGridView1.SelectedCells[counter].Value);
                    min = mas[0];
                    for (int i = 1; i < (dataGridView1.SelectedCells.Count); i++)
                        if (min > mas[i]) min = mas[i];
                    function = min;

                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 11 && tabControl1.SelectedIndex==0 )
            {
                counter = 0;
                try
                {

                    double summ = 0;
                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)

                        summ = summ + Convert.ToDouble(dataGridView1.SelectedCells[counter].Value);
                    function = summ/dataGridView1.SelectedCells.Count;

                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 12 && tabControl1.SelectedIndex==0)
            {
                counter = 0;
                int schet=0;
                try
                {
                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                        if (dataGridView1.SelectedCells[counter].Value != null) schet++;                      
                    function = schet;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            //Программый участок для отдельных листов
            //-------------------------------------
            //-------------------------------------
            if (comboBox1.SelectedIndex == 0 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1)
            {
                counter = 0;
                try
                {
                    function = Math.Abs(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }

            if (comboBox1.SelectedIndex == 1 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1 )
            {
                counter = 0;
                try
                {
                    function = Math.Cos(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }

            if (comboBox1.SelectedIndex == 2 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1 )
            {
                counter = 0;
                try
                {
                    function = Math.Exp(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 3 && tabControl1.SelectedIndex > 0  && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1 )
            {
                counter = 0;
                try
                {
                    function = Math.Sin(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 4 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1 )
            {
                counter = 0;
                try
                {
                    function = Math.Tan(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 5 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 1)
            {
                counter = 0;
                try
                {
                    function = Math.Sqrt(Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value));
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 6 && tabControl1.SelectedIndex > 0 && dg[tabControl1.SelectedIndex].SelectedCells.Count == 2 )
            {
                counter = 0;
                try
                {
                    int a = Convert.ToInt32(dg[tabControl1.SelectedIndex].SelectedCells[1].Value);
                    int b = Convert.ToInt32(dg[tabControl1.SelectedIndex].SelectedCells[0].Value);
                    int result = 0;
                    int result2;
                    result2 = a - (Math.DivRem(a, b, out result) * b);
                    function = result2;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }

            }
            if (comboBox1.SelectedIndex == 7 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                try
                {
                    double multyplex = 1;
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)

                        multyplex = multyplex * Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value);
                    function = multyplex;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 8 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                try
                {
                    double summ = 0;
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)

                        summ = summ + Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value);
                    function = summ;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 9 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                try
                {
                    double max = 0;
                    double[] mas;
                    mas = new double[dg[tabControl1.SelectedIndex].SelectedCells.Count];

                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        mas[counter] = Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value);
                    max = mas[0];
                    for (int i = 1; i < (dg[tabControl1.SelectedIndex].SelectedCells.Count); i++)
                        if (max < mas[i]) max = mas[i];
                    function = max;

                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 10 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                try
                {
                    double min = 0;
                    double[] mas;
                    mas = new double[dg[tabControl1.SelectedIndex].SelectedCells.Count];

                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        mas[counter] = Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value);
                    min = mas[0];
                    for (int i = 1; i < (dg[tabControl1.SelectedIndex].SelectedCells.Count); i++)
                        if (min > mas[i]) min = mas[i];
                    function = min;

                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 11 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                try
                {

                    double summ = 0;
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)

                        summ = summ + Convert.ToDouble(dg[tabControl1.SelectedIndex].SelectedCells[counter].Value);
                    function = summ / dg[tabControl1.SelectedIndex].SelectedCells.Count;

                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
            if (comboBox1.SelectedIndex == 12 && tabControl1.SelectedIndex>0)
            {
                counter = 0;
                int schet = 0;
                try
                {
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        if (dg[tabControl1.SelectedIndex].SelectedCells[counter].Value != null) schet++;
                    function = schet;
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                    button2.Visible = false;
                    comboBox1.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Visible = false;
            comboBox1.Enabled = true;
            if (tabControl1.SelectedIndex > 0)
            {
                dg[tabControl1.SelectedIndex].SelectedCells[0].Value = function; 
            }else
            dataGridView1[Columnindex, Rowindex].Value = function;
            
            
            
            
           /* try
                {
                    for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)

                        sum = sum + Convert.ToInt32(dataGridView1.SelectedCells[counter].Value);
                    label1.Text = sum.ToString();
                }
                catch
                {
                    MessageBox.Show("Выбраны не допустимые ячейки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label1.Text = "";
                }*/
 


        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount, columcount;
            rowcount = dataGridView1.RowCount;
            columcount = dataGridView1.ColumnCount;
            
            Object[,] masiv;
            masiv = new Object[rowcount, columcount];
            if (savedone == false && filename!="")
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else 
                if (tabControl1.SelectedIndex >0)
            {         
                    excel.Application app = new excel.Application();
                    excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                    excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                    excel.Range range = wsheet.get_Range("A1", "Z" + rowcount.ToString());
                    for (int i = 0; i < rowcount; i++)
                        for (int j = 0; j < columcount; j++)

                            masiv[i, j] = dg[tabControl1.SelectedIndex][j, i].Value;

                    range.Value2 = masiv;
                    app.Visible = false;
                    wbook.Saved = true;
                    wbook.SaveCopyAs(filename);
                    app.Quit();
                    savedone = true;
                    
                
            } 
                else
            {
                                            
                    
                    excel.Application app = new excel.Application();
                    excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                    excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                    excel.Range range = wsheet.get_Range("A1", "Z" + rowcount.ToString());
                    for (int i = 0; i < rowcount; i++)
                        for (int j = 0; j < columcount; j++)

                            masiv[i, j] = dataGridView1[j, i].Value;
                    range.Value2 = masiv;
                    app.Visible = false;
                    wbook.Saved = true;
                    wbook.SaveCopyAs(filename);
                    app.Quit();
            }

          
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount,columcount;
            rowcount = dataGridView1.RowCount;
            columcount = dataGridView1.ColumnCount;
             Object [,] masiv;
             masiv=new Object [rowcount,columcount];
             
            if (tabControl1.SelectedIndex > 0)
             {
                 if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                 {
                     filename = saveFileDialog1.FileName;
                     excel.Application app = new excel.Application();
                     excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                     excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                     excel.Range range = wsheet.get_Range("A1", "Z" + rowcount.ToString());
                     for (int i = 0; i < rowcount; i++)
                         for (int j = 0; j < columcount; j++)

                             masiv[i, j] = dg[tabControl1.SelectedIndex][j, i].Value;
                                 
                     range.Value2 = masiv;
                     app.Visible = false;
                     wbook.Saved = true;
                     wbook.SaveCopyAs(filename);
                     app.Quit();
                     savedone = true;
                 }
             }
             else
             {

                 if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                 {
                     filename = saveFileDialog1.FileName;
                     excel.Application app = new excel.Application();
                     excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                     excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                     excel.Range range = wsheet.get_Range("A1", "Z" + rowcount.ToString());
                     for (int i = 0; i < rowcount; i++)
                         for (int j = 0; j < columcount; j++)

                             masiv[i, j] = dataGridView1[j, i].Value;
                     range.Value2 = masiv;
                     app.Visible = false;
                     wbook.Saved = true;
                     wbook.SaveCopyAs(filename);
                     app.Quit();
                     savedone = true;
                 }

             }
            
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0) 
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int i;
                    filename = openFileDialog1.FileName;
                    OleDbConnection olcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;data source=" + filename + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                    olcon.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [лист1$]", olcon);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    olcon.Close();
                    da.Dispose();
                    for (i = 0; i < dg[tabControl1.SelectedIndex].ColumnCount; i++)
                        dg[tabControl1.SelectedIndex].Columns.Clear();

                    dg[tabControl1.SelectedIndex].DataSource = dt;
                    i = 0; int k = 1;
                    for (char ch = 'A'; ch <= 'Z'; ch++)
                    {
                        dg[tabControl1.SelectedIndex].Columns.Add(ch.ToString(), ch.ToString());
                        dg[tabControl1.SelectedIndex].Columns[i].HeaderText = ch.ToString();
                        //dataGridView1.Rows.Add();
                        dg[tabControl1.SelectedIndex].Columns[i].Width = 70;
                        k = i + 1;
                        dg[tabControl1.SelectedIndex].AllowUserToResizeRows = false;
                        dg[tabControl1.SelectedIndex].AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                        //dataGridView1.Rows[i].HeaderCell.Value = k.ToString();
                        i++;
                    }

                }
            }
            else
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int i;
                    filename = openFileDialog1.FileName;
                    OleDbConnection olcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;data source=" + filename + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                    olcon.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT     [Лист1$].* FROM         [Лист1$]", olcon);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    olcon.Close();
                    da.Dispose();
                    for (i = 0; i < dataGridView1.ColumnCount; i++)
                        dataGridView1.Columns.Clear();

                    dataGridView1.DataSource = dt;
                    i = 0; int k = 1;
                    for (char ch = 'A'; ch <= 'Z'; ch++)
                    {
                        dataGridView1.Columns.Add(ch.ToString(), ch.ToString());
                        dataGridView1.Columns[i].HeaderText = ch.ToString();
                        //dataGridView1.Rows.Add();
                        dataGridView1.Columns[i].Width = 70;
                        k = i + 1;
                        dataGridView1.AllowUserToResizeRows = false;
                        dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader);
                        //dataGridView1.Rows[i].HeaderCell.Value = k.ToString();
                        i++;
                    }

                }
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1[Columnindex, Rowindex].Value = ob;
        }

        private void столбцыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void диаграммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int char_index,char_index2;
            char_index = 0; char_index2 = 0;
            int i=0;
            for (char ch = 'A'; ch <= 'Z'; ch++)
            { 
                if(ch==Convert.ToChar(textBox1.Text)) char_index=i; 
                else i++;
            }
            i = 0;
            for (char a = 'A'; a <= 'Z'; a++)
            {
                if (a == Convert.ToChar(textBox4.Text)) char_index2 = i; 
                else i++;
            }
            int columncount = char_index2 - char_index + 1;
            int rowcount = Convert.ToInt32(textBox5.Text) - Convert.ToInt32(textBox3.Text)+1;
            Object[,] masiv;               
            masiv = new Object[rowcount,columncount];
            int row=Convert.ToInt32(textBox3.Text)-1 ;
            int col=char_index;

             if (tabControl1.SelectedIndex > 0)
             {
                 for (i = 0; i < rowcount; i++)
                 {
                     for (int j = 0; j < columncount; j++)
                     {
                         masiv[i, j] = dg[tabControl1.SelectedIndex][col, row].Value;
                         col++;
                     }
                     col = char_index;
                     row++;
                 }
                 excel.Application app = new excel.Application();
                 excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                 excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                 string one, two;
                 one = textBox1.Text + textBox3.Text;
                 two = textBox4.Text + textBox5.Text;
                 excel.Range range = wsheet.get_Range(one, two);
                 range.Value2 = masiv;
                 range.Select();
                 range.Value2 = masiv;
                 range.Select();
                 excel.ChartObjects chartobjects = (excel.ChartObjects)wsheet.ChartObjects(Missing.Value);

                 excel.ChartObject chartobject = (excel.ChartObject)chartobjects.Add(15, 100, 450, 250);
                 excel.Chart chart = (excel.Chart)chartobject.Chart;

                 // Call to chart.ChartWizard() is shown using late binding technique solely for the demonstration purposes
                 Object[] args7 = new Object[11];
                 args7[0] = range; // Source
                 args7[1] = excel.XlChartType.xl3DColumn; // Gallery
                 args7[2] = Missing.Value; // Format
                 args7[3] = excel.XlRowCol.xlRows; // PlotBy
                 args7[4] = 0; // CategoryLabels
                 args7[5] = 0; // SeriesLabels
                 args7[6] = true; // HasLegend
                 args7[7] = "Построение диаграммы"; // Title
                 args7[8] = "Категории"; // CategoryTitle
                 args7[9] = "Значения"; // ValueTitle
                 args7[10] = Missing.Value; // ExtraTitle
                 chart.GetType().InvokeMember("ChartWizard", BindingFlags.InvokeMethod, null, chart, args7);
                 app.Visible = false;
                 wbook.Saved = true;
                 if (filename == null)
                 {
                     if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                         filename = saveFileDialog1.FileName;
                     wbook.SaveCopyAs(filename);
                     app.Quit();
                     savedone = true;
                 }
             }

            else{
             for (i = 0; i < rowcount; i++)
             {
                 for (int j = 0; j < columncount; j++)
                 {
                     masiv[i, j] = dataGridView1[col,row].Value;
                      col++; 
                 }
                 col = char_index;
                 row++;
             }
                    excel.Application app = new excel.Application();
                    excel.Workbook wbook = app.Workbooks.Add(excel.XlWBATemplate.xlWBATWorksheet);
                    excel.Worksheet wsheet = (excel.Worksheet)wbook.Worksheets[1];
                    string one, two;
                    one = textBox1.Text + textBox3.Text;
                    two = textBox4.Text + textBox5.Text;                    
                    excel.Range range = wsheet.get_Range(one,two);
                    range.Value2 = masiv;
                    range.Select();
                    range.Value2 = masiv;
                    range.Select();
                    excel.ChartObjects chartobjects = (excel.ChartObjects)wsheet.ChartObjects(Missing.Value);

                    excel.ChartObject chartobject = (excel.ChartObject)chartobjects.Add(15,100,450,250);
                    excel.Chart chart = (excel.Chart)chartobject.Chart;

                    // Call to chart.ChartWizard() is shown using late binding technique solely for the demonstration purposes
                    Object[] args7 = new Object[11];
                    args7[0] = range; // Source
                    args7[1] = excel.XlChartType.xl3DColumn; // Gallery
                    args7[2] = Missing.Value; // Format
                    args7[3] = excel.XlRowCol.xlRows; // PlotBy
                    args7[4] = 0; // CategoryLabels
                    args7[5] = 0; // SeriesLabels
                    args7[6] = true; // HasLegend
                    args7[7] = "Построение диаграммы"; // Title
                    args7[8] = "Категории"; // CategoryTitle
                    args7[9] = "Значения"; // ValueTitle
                    args7[10] = Missing.Value; // ExtraTitle
                    chart.GetType().InvokeMember("ChartWizard", BindingFlags.InvokeMethod, null, chart, args7);
                    app.Visible = false;
                    wbook.Saved = true;
                    if (filename == null) 
                    { if (saveFileDialog1.ShowDialog()==DialogResult.OK)
                        filename = saveFileDialog1.FileName;
                        wbook.SaveCopyAs(filename);                        
                        app.Quit();
                        savedone = true;
                    }
                    
            }
        }

        private void вызовСправкиExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Index, (object)(""));
        }

        private void axChartfx1_LButtonDblClk(object sender, AxChartfxLib._DChartfxEvents_LButtonDblClkEvent e)
        {

        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
             int counter = 0;     
                  DialogResult dr = new DialogResult();
                   dr = fontDialog1.ShowDialog();
                   if (dr == DialogResult.OK)
                   {
                       for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                           dataGridView1.SelectedCells[counter].Style.Font=fontDialog1.Font;
                   }  
            if (tabControl1.SelectedIndex > 0)
            {                  
               if (dr==DialogResult.OK)
               {
                   for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                       dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.Font = fontDialog1.Font;
               }
            }
                 
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter;
            DialogResult dr = new DialogResult();
            dr = colorDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                    dataGridView1.SelectedCells[counter].Style.ForeColor = colorDialog1.Color;
            }
            if (tabControl1.SelectedIndex > 0)
            {
                if (dr == DialogResult.OK)
                {
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.ForeColor = colorDialog1.Color;
                }
            }
        }

        private void фонЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter;
            DialogResult dr = new DialogResult();
            dr = colorDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                    dataGridView1.SelectedCells[counter].Style.BackColor = colorDialog1.Color;
            }
            if (tabControl1.SelectedIndex > 0)
            {
                if (dr == DialogResult.OK)
                {
                    for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.BackColor = colorDialog1.Color;
                }
            }
        }

        private void левомуКраюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter;      
               for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
               dataGridView1.SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
           
            if (tabControl1.SelectedIndex > 0)
           {
               for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                       dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            
           }    
           
        }

        private void цетнруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter;
            for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                dataGridView1.SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (tabControl1.SelectedIndex > 0)
            {
                for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                              
            }    
        }

        private void правомуКраюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter;
            for (counter = 0; counter < (dataGridView1.SelectedCells.Count); counter++)
                dataGridView1.SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (tabControl1.SelectedIndex > 0)
            {
                for (counter = 0; counter < (dg[tabControl1.SelectedIndex].SelectedCells.Count); counter++)
                        dg[tabControl1.SelectedIndex].SelectedCells[counter].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }    
        }

             

     }
}