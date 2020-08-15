using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        // Создаем устройство 
        private Device device = null;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // заголовок окна 
            this.Text = "Инициализация устройства DirectX";
            // минимальные размеры окна 
            this.MinimumSize = new Size(50, 100);
            // размер клиентской области 
            this.ClientSize = new Size(640, 480);
            InitializeGraphics();
        }

        public void InitializeGraphics()
        {
            // параметры представления 
            PresentParameters presentParams = new PresentParameters();
            // работает ли приложение в полноэкранном режиме - да.
            //presentParams.IsWindowed = true;
            // режим работы буферной подкачки 
            presentParams.SwapEffect = SwapEffect.Discard;
            // Инициализация устройства. 
            device = new Device(0, // идентификатор адаптера 
                DeviceType.Hardware, // тип устройства 
                this.Handle, // дескриптор окна визуализации 
                CreateFlags.SoftwareVertexProcessing, // флаг режима работы устройства 
                presentParams); // параметры представления 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target, // что именно хотим очистить (текущее окно) 
                System.Drawing.Color.Black, // цвет очистки 
                1.0f, // буфер глубины.( Число 0.0 представляет 
                // самое близкое расстояние к зрителю; число 1.0 представляет 
                // самое дальнее расстояние. 
                0);
            device.Present();

            base.OnPaint(e);
        }
    }
}
