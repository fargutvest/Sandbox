using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        string[,] abc = { 
                                            /*0/*/{ "а","А","a","A" }, 
                                            /*1/*/{ "б","Б","b","B" },
                                            /*2/*/{ "в","В","v","V" },
                                            /*3/*/{ "г","Г","g","G" },
                                            /*4/*/{ "д","Д","d","D" },
                                            /*5/*/{ "е","Е","e","E" },
                                            /*6/*/{ "ё","Ё","e","E" },
                                            /*7/*/{ "ж","Ж","g","G" },
                                            /*8/*/{ "з","З","s","S" },
                                            /*9/*/{ "и","И","i","I" },
                                            /*10*/{ "й","Й","i","I" },
                                            /*11*/{ "к","К","k","K" },
                                            /*12*/{ "л","Л","l","L" },
                                            /*13*/{ "м","М","m","M" },
                                            /*14*/{ "н","Н","n","N" },
                                            /*15*/{ "о","О","o","O" },
                                            /*16*/{ "п","П","p","P" },
                                            /*17*/{ "р","Р","r","R" },
                                            /*18*/{ "с","С","s","S" },
                                            /*19*/{ "т","Т","t","T" },
                                            /*20*/{ "у","У","u","U" },
                                            /*21*/{ "ф","Ф","f","F" },
                                            /*22*/{ "х","Х","h","H" },
                                            /*23*/{ "ц","Ц","c","C" },
                                            /*24*/{ "ч","Ч","ch","CH" },
                                            /*25*/{ "ш","Ш","sh","SH" },
                                            /*26*/{ "щ","Щ","sch","SCH" },
                                            /*27*/{ "ъ","Ъ","","" },
                                            /*28*/{ "ы","Ы","i","I" },
                                            /*29*/{ "ь","Ь","`","`" },
                                            /*30*/{ "э","Э","e","E" },
                                            /*31*/{ "ю","Ю","ju","JU" },
                                            /*32*/{ "я","Я","ja","JA" }
                                            };
        string[,] pach;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pach = new string[1, 2];
                pach[0, 1] = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.IndexOf(openFileDialog1.SafeFileName));
                pach[0, 0] = openFileDialog1.SafeFileName;
                translate();
            }
            
        }

        private void открытьКаталогToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pach = new string[Directory.GetFiles(folderBrowserDialog1.SelectedPath).Length,2];
                for (int i = 0; i < Directory.GetFiles(folderBrowserDialog1.SelectedPath).Length; i++)
                {
                    pach[i, 1] = folderBrowserDialog1.SelectedPath+"\\";
                    pach[i, 0] = Directory.GetFiles(folderBrowserDialog1.SelectedPath)[i].Substring((folderBrowserDialog1.SelectedPath.Length+1));
                }
                    translate();
            }
        }



        void translate()
        {
            string[] arr = new string[0];
            string mem1;
            string mem2;

            for (int file_n = 0; file_n < pach.Length / 2; file_n++)
            {
                arr = new string[pach[file_n,0].Length];
                for (int i = 0; i < pach[file_n,0].Length; i++)
                {
                    for (int registr = n; registr < n+2; registr++)
                    {
                        for (int j = 0; j < 33; j++)
                        {
                            mem1 = pach[file_n,0].Substring(i, 1);
                            mem2 = abc[j, registr];
                            if (mem1 == mem2)
                            {
                                arr[i] = abc[j, registr + h];
                                j = 0;
                                registr = 0;
                                goto loop1;
                            }
                            else if (j == 32 & registr == 1)
                            {
                                arr[i] = mem1;
                                j = 0;
                                registr = 0;
                                goto loop1;
                            }
                        }
                    }
                loop1: int ggg = 0;
                }
            

            label1.Text = "";
            for (int i = 0; i < arr.Length; i++)
            {
                label1.Text += arr[i];
            }
            File.Move(pach[file_n,1]+pach[file_n,0], pach[file_n,1] + label1.Text);
            }
        }
        int n;
        int h;
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                n = 0;
                h = 2;
            }
            else if (radioButton2.Checked == true)
            {
                n = 2;
                h = -2;
            }
        }


    }
}
