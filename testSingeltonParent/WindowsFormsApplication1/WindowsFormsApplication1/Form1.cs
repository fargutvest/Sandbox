using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ns
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
            //MyGenericSingleton<a>.Instance.method();
            //this.Text = MyGenericSingleton<a>.Instance.ToString();
            a.Instance.method();
        }
    }

    public class a : MyGenericSingleton<a>
    {
        private a() { }
        public void method()
        {

        }
    }

    public class b
    {
   
    }

    public class MyGenericSingleton<T> where T : class, new()
    {

        static T _instance = null;
        protected MyGenericSingleton() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }

    }

    public class MySingleton
    {
        static MySingleton _instance = null;
       protected MySingleton() { }

        public static MySingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MySingleton();

                return _instance;
            }
        }

    }



}
