using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using ZedGraph;

namespace testDetectorCalibration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test()
        {
            //http://jenyay.net/ZedGraph/Simple

            //Получаем панель для рисования
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            myPane.CurveList.Clear();

            //обозначение осей
            myPane.Title.Text = "Detector Graphic";
            myPane.XAxis.Title.Text = "Pixel";
            myPane.YAxis.Title.Text = "Level";
            
            //формат осей
            myPane.XAxis.Type = AxisType.Linear;
            myPane.XAxis.Scale.Min = 0;// new XDate(2011, 08, 15);
            myPane.XAxis.Scale.Max = 100;// new XDate(2011, 09, 01);




            // Создадим 2 списка точек
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();

            double xmin = -50;
            double xmax = 50;

            // Заполняем списки точек
            for (double x1 = xmin; x1 <= xmax; x1 += 0.01)
            {
                // добавим в список точку
                list1.Add(x1, f1(x1));
            }
            for (double x2 = xmin; x2 <= xmax; x2 += 0.01)
            {
                // добавим в список точку
                list2.Add(x2, f2(x2));
            }
            


            //построение графиков
            LineItem myCurve1 = myPane.AddCurve("High", list1, Color.Red, SymbolType.None);
            LineItem myCurve2 = myPane.AddCurve("Low", list2, Color.Blue, SymbolType.None);

            
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();

        }

        double f1(double x)
        {
            if (x == 0)
            {
                return 1;
            }
            return Math.Sin(x) / x;
        }
        double f2(double x)
        {
            if (x == 0)
            {
                return 1;
            }
            return Math.Sin(x);
        }
    }
}

