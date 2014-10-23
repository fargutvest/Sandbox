using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {

            byte[] bytes = new byte[] { 32, 33, 34, 35, 36, 37, 38, 39 };
            ushort PACKETID = BitConverter.ToUInt16(bytes, 0);
            ushort LINEID = BitConverter.ToUInt16(bytes, 2);
            ushort PARTID = BitConverter.ToUInt16(bytes, 4); 


            System.Windows.Forms.TextBox textbox = new System.Windows.Forms.TextBox();
            textbox.Text = "one two three";
            System.Windows.Forms.RadioButton radiobutton = new System.Windows.Forms.RadioButton();
            System.Windows.Forms.CheckBox checkbox = new System.Windows.Forms.CheckBox();
            checkbox.Checked = true;
            System.Windows.Forms.Control control = new System.Windows.Forms.Control();
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.Size = new Size(1920, 1080);

            control = textbox;
            control = radiobutton;
            control = checkbox;
            control = form;

            Type typeControl = control.GetType();
            string text = "";
            switch (typeControl.Name)
            {
                case "TextBox":
                     text = control.Text;
                    break;

                case "RadioButton":
                    break;

                case "CheckBox":
                    System.Windows.Forms.CheckBox chb = (System.Windows.Forms.CheckBox)control;
                    text = chb.Checked.ToString();
                    break;

                case "Form":
                    System.Windows.Forms.Form frm = (System.Windows.Forms.Form)control;
                    text = frm.Size.ToString();
                    break;


            }
            
            

            //adabaf
            int dtpLength = 11381679;

            byte lo = (byte)((byte)dtpLength & (byte)0xff);
            byte mi = (byte)(dtpLength >> 8);
            byte hi = (byte)(dtpLength >> 16);


            double d;
            object o;
            byte bt = 0x03;
            byte bt1 = 0x44;
            int i = 3;
            string s = "dfgjdk dkf gkld 8 oe5 34 ";

            bool bl = bt.Equals(i);
            int hash = s.GetHashCode();

            string ASCII = "CP7500";
            Regex reg = new Regex("([0-9]+)|([A-Za-z]+)");
            MatchCollection match = reg.Matches(ASCII);
            string symb = match[0].Value;
            string digit = match[1].Value;


            foreach (Match mat in match)
                Console.WriteLine(mat);
            Console.ReadKey();


            ushort ttt = 43344;
            byte[] b = BitConverter.GetBytes(ttt);
            
        }


    


    }
}

