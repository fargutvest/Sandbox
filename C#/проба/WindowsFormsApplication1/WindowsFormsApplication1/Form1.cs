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
    }
    public interface IObserver1 // Интерфейс наблюдателя
    {
        void Notify();
    }

    public abstract class Subject // Субъект
    {
        private List<IObserver1> observers = new List<IObserver1>();

        public void Add(IObserver1 o)
        {
            observers.Add(o);
        }

        public void Remove(IObserver1 o)
        {
            observers.Remove(o);
        }

        public void Notify() // Оповестить всех наблюдателей
        {
            foreach (IObserver1 o in observers)
                o.Notify();
        }
    }
}
http://habrahabr.ru/post/153225/