using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assoc = System.Collections.Generic.Dictionary<string, System.Drawing.Point>;

namespace WorkWithMemory
{
    public partial class Form1 : Form
    {
        string mem = "(0;2),(2;2),(4;2),(6;2),(8;2),(10;6),(16;2),(18;4),(22;4),(26;4),(30;3),(33;5),(38;4),(42;120),(162;2),(164;2),(166;4),(170;31),(201;1),(202;1),(203;1),(204;1),(205;1),(206;1),(207;19),(226;2),(228;2),(230;4),(234;31),(265;1),(266;1),(267;1),(268;1),(269;1),(270;1),(271;19),(290;2),(292;2),(294;4),(298;31),(329;1),(330;1),(331;1),(332;1),(333;1),(334;1),(335;19),(354;2),(356;2),(358;4),(362;31),(393;1),(394;1),(395;1),(396;1),(397;1),(398;1),(399;11)";

        //названия регистров
        const string LOCAL_IP = "LOCAL_IP";
        const string SUBNET_MASK = "SUBNET_MASK";
        const string LOCAL_PORT_CH1 = "LOCAL_PORT_CH1";
        const string REMOTE_IP_CH1 = "REMOTE_IP_CH1";
        const string REMOTE_PORT_CH1 = "REMOTE_PORT_CH1";
        const string DOMAIN_NAME_CH1 = "DOMAIN_NAME_CH1";

        Assoc MemoryWizNET = new Assoc();

        public Form1()
        {
            InitializeComponent();
            tbStructMem.Text = mem;

            MemoryWizNET.Add(LOCAL_IP, new Point(18,4));
            MemoryWizNET.Add(SUBNET_MASK, new Point());
            MemoryWizNET.Add(LOCAL_PORT_CH1, new Point());
            MemoryWizNET.Add(REMOTE_IP_CH1, new Point());
            MemoryWizNET.Add(REMOTE_PORT_CH1, new Point());
            MemoryWizNET.Add(DOMAIN_NAME_CH1, new Point(170,31));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string[] s = tbStructMem.Text.Split(',');
            Point[] memStruct = new Point[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                string ss = s[i].Replace("(", "").Replace(")", "");

                memStruct[i].X = Convert.ToInt32(ss.Substring(0, ss.IndexOf(";")));
                string sss = ss.Substring(ss.IndexOf(";") + 1, ss.Length - ss.IndexOf(";") - 1);
                memStruct[i].Y = Convert.ToInt32(sss);
            }

            string[] memarr = new string[410];
            if (rbSpace.Checked)
            {
                memarr = tbDumpMem.Text.Split(' ');
            }
            else if (rbStream.Checked)
            {
                int j = 0;
                for (int i = 0; i < tbDumpMem.Text.Length; i += 2)
                {
                    memarr[j] = tbDumpMem.Text.Substring(i, 2);
                    j++;
                }
            }


            byte[] MemDump = new byte[memarr.Length];
            for (int j = 0; j < memarr.Length; j++)
            {
                MemDump[j] = Convert.ToByte(memarr[j], 16);
            }


            //#################################################
            ListViewItem lvi;
            string strHexBytes;
            string strIntBytes;
            string AsciiBytes;
            UInt64 digitVal = 0;
            for (int t = 0; t < memStruct.Length; t++)
            {
                strHexBytes = "";
                strIntBytes = "";
                AsciiBytes = "";
                for (int f = 0; f < memStruct[t].Y; f++)
                {
                    strHexBytes += string.Format("{0} ", MemDump[memStruct[t].X + f].ToString("X2"));
                    strIntBytes += string.Format("{0} ", MemDump[memStruct[t].X + f].ToString());
                    AsciiBytes += Encoding.ASCII.GetString(new byte[] { MemDump[memStruct[t].X + f] });
                }
                string ggg = strHexBytes.Replace(" ", "");
                if (ggg.Length < 16)
                {
                    digitVal = Convert.ToUInt64(ggg, 16);
                }

                lvi = new ListViewItem(string.Format("({0};{1})", memStruct[t].X, memStruct[t].Y));
                lvi.SubItems.Add(strHexBytes);
                lvi.SubItems.Add(AsciiBytes);
                if (digitVal != 0) lvi.SubItems.Add(digitVal.ToString());
                else
                    lvi.SubItems.Add("");
                lvi.SubItems.Add(strIntBytes);


                listView1.Items.Add(lvi);
            }



            //#####################################################
            Point p;
            MemoryWizNET.TryGetValue(LOCAL_IP, out p);
            tbLocalIP.Text = string.Format("{0}.{1}.{2}.{3}", MemDump[p.X], MemDump[p.X + 1], MemDump[p.X + 2], MemDump[p.X + 3]);
            MemoryWizNET.TryGetValue(DOMAIN_NAME_CH1,out p);
            byte[] b = new byte[p.Y];
            Array.Copy(MemDump,p.X,b,0,p.Y);
            tbDomainCH1.Text = Encoding.ASCII.GetString(b);

           //MemoryWizNET.Add(LOCAL_IP, new Point());
           //MemoryWizNET.Add(SUBNET_MASK, new Point());
           //MemoryWizNET.Add(LOCAL_PORT_CH1, new Point());
           //MemoryWizNET.Add(REMOTE_IP_CH1, new Point());
           //MemoryWizNET.Add(REMOTE_PORT_CH1, new Point());





            


        }
    }
}
