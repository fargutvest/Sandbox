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
        }
        public static Font Сформировать_Font(string имя, string размер, string стиль)
        {
            размер = размер.Replace('.', ',');
            float raz = Convert.ToSingle(размер);
            FontStyle stl = new FontStyle();
            stl = (FontStyle)Enum.Parse(typeof(FontStyle), стиль);
            Font f = new Font(имя, raz, stl);
            return f;
        }

    }
}
