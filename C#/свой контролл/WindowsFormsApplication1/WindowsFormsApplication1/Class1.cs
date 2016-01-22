using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TestCSharpControl
{
    /// <summary>
    /// test embossed drawing - my first do-diez project :).
    /// </summary>
    public class EmbossedProgressBar : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public EmbossedProgressBar()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            clGradientFrom = Color.Gainsboro;
            clGradientTo = Color.Black;
            clBarBorderColor = Color.DimGray;
            iProgress = 30;
            // TODO: Add any initialization after the InitForm call

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // EmbossedProgressBar
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AllowDrop = true;
            this.Name = "EmbossedProgressBar";
            this.Resize += new System.EventHandler(this.EmbossedProgressBar_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EmbossedProgressBar_Paint);

        }
        #endregion

        public Color GradientColorFrom
        {
            get
            {
                return clGradientFrom;
            }
            set
            {
                clGradientFrom = value;
                Invalidate();
            }
        }

        public Color GradientColorTo
        {
            get
            {
                return clGradientTo;
            }
            set
            {
                clGradientTo = value;
                Invalidate();
            }
        }

        public int Progress
        {
            get
            {
                return iProgress;
            }
            set
            {

                iProgress = System.Math.Abs(value);
                if (iProgress > 100)
                    iProgress = 100;

                Invalidate();
            }
        }

        private Color clGradientFrom;
        private Color clGradientTo;
        private Color clBarBorderColor;
        private int iProgress;

        private void EmbossedProgressBar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int r, g, b, r2, g2, b2;

            r = clGradientFrom.R;
            g = clGradientFrom.G;
            b = clGradientFrom.B;

            r2 = clGradientTo.R;
            g2 = clGradientTo.G;
            b2 = clGradientTo.B;

            Color cl;
            int dest = Bounds.Height / 2;
            System.Drawing.SolidBrush br = new System.Drawing.SolidBrush(iProgress > 0 ? clGradientFrom : BackColor);


            if (iProgress > 0)
            {
                int right = (iProgress * Bounds.Width) / 100;

                for (int i = 0; i <= dest; i++)
                {
                    cl = Color.FromArgb(
                        r + (((r2 - r) * i) / dest),
                        g + (((g2 - g) * i) / dest),
                        b + (((b2 - b) * i) / dest)
                        );

                    br.Color = cl;

                    e.Graphics.FillRectangle(br, 0, i, right, 1);

                    e.Graphics.FillRectangle(br, 0, Bounds.Height - (i), right, 1);

                } // for
            } // if(iProgress > 0)

            System.Drawing.Pen pn = new System.Drawing.Pen(clBarBorderColor, 1.0F);
            e.Graphics.DrawRectangle(pn, 0, 0, Bounds.Width - 1, Bounds.Height - 1);
        }

        private void EmbossedProgressBar_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        public Color BorderColor
        {
            get
            {
                return clBarBorderColor;
            }
            set
            {
                clBarBorderColor = value;
                Invalidate();
            }
        }
    }
}