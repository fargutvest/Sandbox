using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adani.Detector;

namespace testDetectorCalibration
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        Detector detector;
        taskDisplay taskDisplayImage;



        PixelAttributes[] CalibrPxAttr = new PixelAttributes[1024]; //pixel attributes after calibration


        public Form1()
        {
            InitializeComponent();
            ExceptionLog.ExceptionEvent += ExceptionLog_ExceptionEvent;
            rbStatic.Checked = true;
            lvPixelInfo.FullRowSelect = true;
            lvPixelInfo.GridLines = true;
            lvPixelInfo.View = System.Windows.Forms.View.Details;

        }
        void ExceptionLog_ExceptionEvent(object sender, EventArgs e)
        {
            Exception ex = (Exception)sender;
            rchtbMonCmd.Invoke(new Action<string>((str) => rchtbMonCmd.Text += str), string.Format(">{0}\r\n", ex.Message));
        }
        void detector_ResponceToTheCommand(object sender, Object e)
        {
            CmdEntry cmdEntry = (CmdEntry)e;
            rchtbMonCmd.Invoke(new Action<string>((str) => rchtbMonCmd.Text += str), string.Format("<{0}\r\n", cmdEntry.Resp));
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            //apply settings
            detector = new Detector();
            detector.ResponceToTheCommand += detector_ResponceToTheCommand;
            taskDisplayImage = new taskDisplay(detector); //при вызове контруктора task запускается
            rbRealTime.Checked = true;
            detector.ImageReceiveStart();
            detector.AsyncSendCommand(ProtocolDetector.Operation_of_calibration_switch("W", "G", 1));
            detector.AsyncSendCommand(ProtocolDetector.Operation_of_calibration_switch("W", "O", 1));
            //test();
        }

        void btGainCalibr_Click(object sender, EventArgs e)
        {
            int i = 255;
            string d = i.ToString("x8");
            detector.AsyncSendCommand(ProtocolDetector.Set_gain_and_offset_correction_value_for_a_channel("W", "G", tbPixelGain.Text, tbValueGain.Text));
        }
        void btOffseCalibr_Click(object sender, EventArgs e)
        {
            detector.AsyncSendCommand(ProtocolDetector.Set_gain_and_offset_correction_value_for_a_channel("W", "O", tbPixelOffset.Text, tbValueOffset.Text));
        }
        void rchtbMonCmd_TextChanged(object sender, EventArgs e)
        {
            rchtbMonCmd.Select(rchtbMonCmd.TextLength - 1, 0);
            rchtbMonCmd.ScrollToCaret();
        }
        void taskDisplayImage_DataImageEvent(object sender, EventArgs e)
        {
            pictureBox1.Invoke(new Action<System.Drawing.Bitmap>(bmp => pictureBox1.Image = bmp), taskDisplayImage.bitmapOut);
        }
        void KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string s = e.KeyChar.ToString().ToUpper();
            //tb.Text += s;
            //e.Handled = true;
        }
        void TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
        }
        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SettingsDetector.Instance.IP = "192.168.0.1";
            SettingsDetector.Instance.Logging = false;
            SettingsDetector.Instance.TypePortCMD = SettingsDetector.eTPCMD.Tcp;
            SettingsDetector.Instance.TypePortIMG = SettingsDetector.eTPIMG.Udp;
            SettingsDetector.Instance.EmuleCMDport = checkBox1.Checked;
            SettingsDetector.Instance.EmuleIMGport = checkBox1.Checked;
        }
        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            SettingsDetector.Instance.Logging = checkBox2.Checked;
        }
        void rbRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRealTime.Checked)
            {
                if (taskDisplayImage != null)
                {
                    taskDisplayImage.DataImageEvent += taskDisplayImage_DataImageEvent;
                }
            }
        }
        void rbStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatic.Checked)
            {
                if (taskDisplayImage != null)
                {
                    taskDisplayImage.DataImageEvent -= taskDisplayImage_DataImageEvent;
                }
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            if (taskDisplayImage != null)
            {
                taskDisplayImage_DataImageEvent(sender, e);
                lvPixelInfo.Clear();
                lvPixelInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
            columnHeader4});

                int pxCount = taskDisplayImage.ImageData[0].Count() * SettingsDetector.Instance.EnergyCount;

                for (int pxNum = 0; pxNum < pxCount; pxNum++)
                {
                    string pixel = pxNum.ToString();
                    string value = "";
                    if (detector.pixelAttributes[pxNum] != null)
                    {
                        detector.pixelAttributes[pxNum].Value = pxNum < pxCount / 2 ? taskDisplayImage.ImageData[0][pxNum / 2] : taskDisplayImage.ImageData[1][pxNum / 2];
                        value = detector.pixelAttributes[pxNum].Value.ToString();
                    }
                    string gain = detector.pixelAttributes[pxNum] != null ? detector.pixelAttributes[pxNum].Gain.ToString() : "";
                    string offset = detector.pixelAttributes[pxNum] != null ? detector.pixelAttributes[pxNum].Offset.ToString() : "";
                    ListViewItem lvitem = new ListViewItem(new string[] { pixel, value, gain, offset }, 0);
                    lvPixelInfo.Invoke(new Action<ListViewItem>(lvi => lvPixelInfo.Items.Add(lvitem)), lvitem);
                }
            }
        }

        private void btReadGainOffset_Click(object sender, EventArgs e)
        {

            for (int pixel = 0; pixel < 1024; pixel++)
            {
                detector.AsyncSendCommand(ProtocolDetector.Set_gain_and_offset_correction_value_for_a_channel("R", pixel.ToString("X2")));
            }
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            detector.AsyncSendCommand(new CmdEntry(tbCommand.Text));
        }

        private void btResetGainOffset_Click(object sender, EventArgs e)
        {
            detector.AsyncSendCommand(ProtocolDetector.Reset_the_offset_and_gain_correction_values(1, 1));
        }

        

        private void btCalcG_O_Click(object sender, EventArgs e)
        {
            double notAmplifedValue;
            int NormalizationValue = Convert.ToInt32(tbNormVal.Text);
            for (int i = 0; i < 1024; i++)
            {
                if (detector.pixelAttributes[i] != null)
                {
                    notAmplifedValue = Convert.ToDouble(detector.pixelAttributes[i].Value) / Convert.ToDouble(detector.pixelAttributes[i].Gain);
                    CalibrPxAttr[i] = new PixelAttributes();
                    CalibrPxAttr[i].Gain = Convert.ToInt32(NormalizationValue / notAmplifedValue);
                }
            }
        }

        private void btWriteNewG_O_Click(object sender, EventArgs e)
        {
            for (int pixel = 0; pixel < 1024; pixel++)
            {
                if (CalibrPxAttr[pixel] != null)
                {
                    CmdEntry ce = ProtocolDetector.Set_gain_and_offset_correction_value_for_a_channel("W", "G", pixel.ToString("X2"), CalibrPxAttr[pixel].Gain.ToString("X2"));
                    detector.AsyncSendCommand(ce);
                }
                //detector.AsyncSendCommand(ProtocolDetector.Set_gain_and_offset_correction_value_for_a_channel("W", "O", pixel.ToString("X2"), CalibrPxAttr[pixel].Offset.ToString("X2")));
            }
        }

        private void btStartFrame_Click(object sender, EventArgs e)
        {
            detector.ImageReceiveStart();
        }

        private void btStopFrame_Click(object sender, EventArgs e)
        {
            detector.ImageReceiveStop();
        }


    }
}

#region no use
/*
 * 
 * 
        void LockControl(bool b)
        {
            groupBox1.Enabled = !b;
            groupBox2.Enabled = !b;
 
        }

 *       void test()
        {
            RefrechIMG();
           pictureBox1.Image = bitmapOut;
        }

        void RefrechIMG()
        {
            int bz = 0;

            DataPacket dp = null;
            while (dp == null)
            {
                dp = detector.GetDataPacket(ref bz);
            }

            while (bz > 0)
            {
                dp = detector.GetDataPacket(ref bz);
            }

            int height = pictureBox1.Height;
            int width = pictureBox1.Width;
            bitmapOut = new Bitmap(width, height); //poligon for drawing
            startPositionX = width;
            Graphics g = Graphics.FromImage(bitmapOut);
            Bitmap bitmapBuff = new Bitmap(512, height); //buffer bitmap poligon width 1px

            int memX = 0;
            int memY = 0;
            int y1;
            int y2;
            int step = Convert.ToInt32(textBox1.Text);


            for (int px = 0; px < 512 * step; px += step)
            {
                //bitmapBuff.SetPixel(px, rand.Next(0,2) , Color.Red);

                //y2 = rand.Next(0, 255);
                y2 = dp.ImageData[0][px / step] / 255;
                g.DrawLine(new Pen(Color.Red), new Point(memX, memY), new Point(px, y2));
                memX = px;
                memY = y2;
            }
            //bitmapOut = bitmapBuff;


            //g.DrawImage(bitmapBuff, x, 0);
        }
 * 
        void GetDtp()
        {

        }

        void DataPacketIntegrate(byte[] udp)
        {
            if (ProtocolDetector.IsHeader(udp))
            {
                _serviceData = ProtocolDetector.GetServiceData(udp, EnergyCount);
                _udpMissing = false;
                if (IsLossPartsDtp()) _udpMissing = true;

                PartCount = _serviceData.dtpLength / udp.Length;
                if (_serviceData.dtpLength % udp.Length != 0) PartCount++; //расчет количества ожидаемых частей фрейма

                dtpContainer = new byte[_serviceData.dtpLength];

                Array.Copy(udp, dtpContainer, udp.Length); //get first part frame
                PartCount--;
                offsetCopy = udp.Length; //memoring offset for next packet
                // and wait next part
            }

            if (!ProtocolDetector.IsHeader(udp) && PartCount > 0)
            {
                if (checkLengthUdp(udp))
                {
                    Array.Copy(udp, 0, dtpContainer, offsetCopy, udp.Length); // copy two part frame
                    offsetCopy += udp.Length;
                    PartCount--;
                    // and wait next part
                }
                else
                {
                    _udpMissing = true;
                    PartCount = 0;
                    offsetCopy = 0;
                }
            }

            if (PartCount == 0 && dtpContainer != null) //если болше частей не ожидается
            {
                if (_udpMissing == false)
                {
                    EnqueueDTP(new DataPacket(ProtocolDetector.GetImageData(dtpContainer, EnergyCount), _serviceData));
                }
            }
        }

        public int Receive(ref byte[] b)
        {
            inc++;
            if (inc > 1) inc = 0;

            b = new byte[bufferSize];

            if (inc == 0)
            {
                Array.Copy(GenerateHeader(), b, headerSize);
                Array.Copy(GenerateIMG(len[inc] - headerSize), 0, b, headerSize, len[inc] - headerSize);
            }
            else
            {
                Array.Copy(GenerateIMG(len[inc]), b, len[inc]);
            }
            System.Threading.Thread.Sleep(delay);

            return len[inc];
        }


        static byte[] GenerateIMG(int len)
        {
            byte[] bOut = new byte[len];
            Random rand = new Random();
            for (int i = 0; i < bOut.Length; i++)
            {
                bOut[i] = Convert.ToByte(rand.Next(0, 255));
            }
            return bOut;
        }
        static byte[] GenerateHeader()
        {
            byte[] bOut = new byte[8] { 0xEB, 0x90, 0x00, 0x08, 0x08, 0x01, 0x01, 0x01 };
            return bOut;
        }*/
#endregion