using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class Form2 : Form
    {
        private SolidBrush[] brushes;
        private Form1 form1;

        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void moveStarTb_Scroll(object sender, EventArgs e)
        {
            var value = (sender as TrackBar).Value;
            moveStarValue.Text = value.ToString();
            form1.StarSpeed = value;
        }

        private void starSizeTb_Scroll(object sender, EventArgs e)
        {
            var value = (sender as TrackBar).Value;
            starSizeValue.Text = value.ToString();
            form1.StarSize = value;
        }

        private void starCountTb_Scroll(object sender, EventArgs e)
        {
            var value = (sender as TrackBar).Value;
            starCountValue.Text = value.ToString();
            form1.CountOfStars = value;
        }

        private void starColorTb_Scroll(object sender, EventArgs e)
        {
            var brush = brushes[(sender as TrackBar).Value];
            starColorValue.Text = brush.Color.Name;
            form1.StarColor = brush;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            brushes = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(_=> (SolidBrush)_.GetValue(null)).ToArray();

            starColorTb.Maximum = brushes.Length -1;
            starColorTb.Value = Array.IndexOf(brushes, form1.StarColor);

            starCountTb.Maximum = form1.CountOfStars;
            starCountTb.Value = form1.CountOfStars;

            starSizeTb.Value = form1.StarSize;

            moveStarTb.Value = form1.StarSpeed;
        }
    }
}
