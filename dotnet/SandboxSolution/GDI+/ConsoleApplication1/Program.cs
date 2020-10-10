using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class GraphicsForm : Form
{
    public static int Main()
    {
        Form fm = new GraphicsForm();
        fm.ShowDialog();
        return 0;
    }
    protected override void OnPaint(PaintEventArgs a)
    {
        DoPaint(a.Graphics, a.ClipRectangle);
    }

    protected void DoPaint(Graphics g, Rectangle clipBox)
    {
        RectangleF bounds = clipBox;
        string welcome = "Welcome, GDI+ !";
        Bitmap bg = new Bitmap("BACKGRND.gif");
        g.DrawImage(bg, bounds);
        LinearGradientBrush brush = new LinearGradientBrush(bounds,
            Color.FromArgb(130, 255, 0, 0), Color.FromArgb(255, 0, 0, 255),
            LinearGradientMode.BackwardDiagonal);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        Font font = new Font("Arial", 48, FontStyle.Bold);
        g.DrawString(welcome, font, brush, bounds, format);
    }
}
