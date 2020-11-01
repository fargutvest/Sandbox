using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace configform
{
    public partial class Form1 : Form
    {
        string[] список_файлов;
        
        int i = 0;
        int число_файлов;
        public Form1()
        {
            InitializeComponent();



        }


        private void button1_Click_1(object sender, System.EventArgs e)
        {
            DialogResult dialogresult = folderBrowserDialog1.ShowDialog();
            //Надпись выше окна контрола
            folderBrowserDialog1.Description = "Поиск папки";
            string folderName = "";
            if (dialogresult == DialogResult.OK)
            {
                //Извлечение имени папки
                folderName = folderBrowserDialog1.SelectedPath;
            }
            label1.Text = folderName;

            DirectoryInfo dir = new DirectoryInfo(folderName);
            foreach (FileInfo files in dir.GetFiles())
            {
                i++;
                число_файлов = i;
            }
            список_файлов = new string[i];
            i = 0;
            foreach (FileInfo files in dir.GetFiles())
            {
                список_файлов[i] = files.Name;
                i++;
            }

            for (int g = 0; g < число_файлов; g++)
            {
                int число_строк = System.IO.File.ReadAllLines(folderName + @"\" + список_файлов[g]).Length;
                StreamReader sr = File.OpenText(folderName + @"\" + список_файлов[g]);

                string[] массив_строк = new string[60];
                for (int k = 0; k < число_строк; k++)
                {
                    массив_строк[k] = sr.ReadLine();
                    k++;
                }
                GData("insert into ConfigForm (katalog,filename,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13,str14,str15,str16,str17,str18,str19,str20,str21,str22,str23,str24,str25,str26,str27,str28,str29," +
                        "str30,str31,str32,str33,str34,str35,str36,str37,str38,str39,str40,str41,str42,str43,str44,str45,str46,str47,str48,str49,str50,str51,str52,str53,str54,str55,str56,str57,str58,str59,str60) values ('" + folderName + "','" + список_файлов[g] +
                   "','" + массив_строк[1] + "','" + массив_строк[2] + "','" + массив_строк[3] + "','" + массив_строк[4] + "','" + массив_строк[5] + "','" + массив_строк[6] +
                   "','" + массив_строк[7] + "','" + массив_строк[8] + "','" + массив_строк[9] + "','" + массив_строк[10] + "','" + массив_строк[11] + "','" + массив_строк[12] +
                   "','" + массив_строк[13] + "','" + массив_строк[14] + "','" + массив_строк[15] + "','" + массив_строк[16] + "','" + массив_строк[17] + "','" + массив_строк[18] +
                   "','" + массив_строк[19] + "','" + массив_строк[20] + "','" + массив_строк[21] + "','" + массив_строк[22] + "','" + массив_строк[23] + "','" + массив_строк[24] +
                   "','" + массив_строк[25] + "','" + массив_строк[26] + "','" + массив_строк[27] + "','" + массив_строк[28] + "','" + массив_строк[29] + "','" + массив_строк[30] +
                   "','" + массив_строк[31] + "','" + массив_строк[32] + "','" + массив_строк[33] + "','" + массив_строк[34] + "','" + массив_строк[35] + "','" + массив_строк[36] +
                   "','" + массив_строк[37] + "','" + массив_строк[38] + "','" + массив_строк[39] + "','" + массив_строк[40] + "','" + массив_строк[41] + "','" + массив_строк[42] +
                   "','" + массив_строк[43] + "','" + массив_строк[44] + "','" + массив_строк[45] + "','" + массив_строк[46] + "','" + массив_строк[47] + "','" + массив_строк[48] +
                   "','" + массив_строк[49] + "','" + массив_строк[50] + "','" + массив_строк[51] + "','" + массив_строк[52] + "','" + массив_строк[53] + "','" + массив_строк[54] +
                   "','" + массив_строк[55] + "','" + массив_строк[56] + "','" + массив_строк[57] + "','" + массив_строк[58] + "','" + массив_строк[59] + "','" + массив_строк[59] + "')");
            }
        }


           private void GData(string selectCom )
           {
        SqlDataAdapter dataAdapter;
        string connections  = @"Integrated Security=false; User=sa; Password=123456; Initial Catalog=ActionMeters; Connect timeout=300; Data Source=INTEL2160\TESTHOMESERVER";
        dataAdapter = new SqlDataAdapter(selectCom, connections);
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        DataTable table = new DataTable();
        table.Locale = System.Globalization.CultureInfo.InvariantCulture;
        dataAdapter.Fill(table);
        
    }

 }
}    



