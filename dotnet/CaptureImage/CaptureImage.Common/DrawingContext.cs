using System;
using System.Drawing;

using System.Windows.Forms;

namespace CaptureImage.Common
{
    public class DrawingContext : ICloneable
    {
        public Image CanvasImage { get; set; }
        public Control CanvasControl { get; set; }

        public object Clone()
        {
            DrawingContext clone = new DrawingContext();

            clone.CanvasImage = CanvasImage.Clone() as Image;
            clone.CanvasControl = CanvasControl;

            return clone;
        }
    }
}
