using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public class AppContext
    {
        private List<Control> controls;
        private Control canvas;

        public DrawingContextsKeeper DrawingContextsKeeper { get; private set; }

        public AppContext()
        {
            this.controls = new List<Control>();
        }

        public void AddControl(Control control, bool isCanvas = false)
        {
            if (isCanvas)
                canvas = control;

            controls.Add(control);
        }

        public void RefreshDrawingContext()
        {
            DrawingContextsKeeper = new DrawingContextsKeeper();

            for (int i = 0; i < controls.Count; i++)
            {
                DrawingContext drawingContext = new DrawingContext()
                {
                    CanvasImage = controls[i].BackgroundImage,
                    CanvasControl = controls[i]
                };

                DrawingContextsKeeper.DrawingContexts.Add(drawingContext);
            }

            DrawingContextsKeeper.SaveContext();
        }

        public void MakeScreenshot(Rectangle rect) 
        {
            Clipboard.SetImage(BitmapHelper.Crop((Bitmap)canvas.BackgroundImage, rect));
        }

        public void UndoDrawing()
        {
            DrawingContextsKeeper.RevertToPreviousContext();

            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].BackgroundImage = DrawingContextsKeeper.DrawingContexts[i].CanvasImage;
                controls[i].Invalidate();
            }
        }
    }
}
    