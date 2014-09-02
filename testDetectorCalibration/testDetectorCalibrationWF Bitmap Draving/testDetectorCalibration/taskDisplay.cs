using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using Adani.Detector;

namespace Adani.Detector
{
    class taskDisplay : IStopTask
    {
        
        public Bitmap bitmapOut;
        public event EventHandler DataImageEvent;
        public int x = 0; // x - coordinate drawing
        public int detectorImageBufferSize;
        Task task;
        CancellationTokenSource cts; //signal cancel
        Bitmap bitmapBuff;
        Object objectRefDetector; //reference on calss detector
        Object objectDataPacket_Detector;
        Color colorCell = Color.Black;//Color.FromArgb(192, 152, 255);
        Color colorHighEnergy = Color.Red;
        Color colorLowEnergy = Color.Green;
        public UInt16[][] ImageData;
        int startPositionX;
        int sizeCell = 32;
        
        public taskDisplay(object _objectDetector) //constructor
        {
            bitmapOut = new Bitmap(800, 300);
            objectRefDetector = _objectDetector;
            cts = new CancellationTokenSource();
            task = new Task(RefrechIMG, cts.Token);
            task.Start();
        }

        public void Stop()
        {
            if (task != null)
            {
                cts.Cancel();

                while (task.Wait(10) == false)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                task = null;
            }
        }

        void RefrechIMG(object cancell)
        {
            CancellationToken cancellationToken = (CancellationToken)cancell;

            int height = SettingsDetector.Instance.HeightImage;
            int width = SettingsDetector.Instance.WidthImage;
            bitmapOut = new Bitmap(width, height); //poligon for drawing

            startPositionX = width;

            
            try
            {
                while (true)
                {

                    MeterrageTime.Instance.meter();
                    object[] bufferSize = new object[] { 0 };
                    objectDataPacket_Detector = objectRefDetector.GetType().GetMethod("GetDataPacket").Invoke(objectRefDetector, bufferSize);
                    
                    if (objectDataPacket_Detector != null)
                    {
                        detectorImageBufferSize = (int)bufferSize[0];
                        bitmapBuff = new Bitmap(800, 300);
                        Graphics g = Graphics.FromImage(bitmapBuff);

                       ImageData = (UInt16[][])objectDataPacket_Detector.GetType().GetField("ImageData").GetValue(objectDataPacket_Detector);

                        //low energy
                        int memX = 0;
                        int memY = 0;
                        int y2;
                        for (int px = 0; px < 512; px++)
                        {
                            y2 = 256 - ImageData[0][px] / 255;
                            g.DrawLine(new Pen(colorLowEnergy), new Point(memX, memY), new Point(px, y2));
                            memX = px;
                            memY = y2;
                        }

                        //high energy
                        memX = 0;
                        memY = 0;
                        for (int px = 0; px < 512; px++)
                        {
                            y2 = 256 - ImageData[1][px] / 255;
                            g.DrawLine(new Pen(colorHighEnergy), new Point(memX, memY), new Point(px, y2));
                            memX = px;
                            memY = y2;
                        }

                        
                        drawGrid(ref bitmapBuff);
                        bitmapOut = bitmapBuff;

                        if (DataImageEvent != null)
                        {
                            DataImageEvent(this, null);
                        }
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                } //while

            }
            catch (OperationCanceledException ex) { }
        }

        void drawGrid(ref Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Graphics gr = Graphics.FromImage(bitmap);
           
            //vertical line
            for (int px = 0; px < 512; px += sizeCell)
            {
                gr.DrawLine(new Pen(colorCell), new Point(px,0),new Point(px, height));
            }

            //horizontal line
            for (int px = 0; px < height; px += sizeCell)
            {
                gr.DrawLine(new Pen(colorCell), new Point(0, px), new Point(width, px));
            }
            
        }

    }
}
