using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class Class1
    {
       private string _name = "Имя из dll";
        private string _firstmane = "фамилия из dll";
       private  int _age = 90;

       public Class1()
       {
       }

        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }

        public string Firstname
        {
            get {return _firstmane ;}
            set { _firstmane = value;}
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }


    }
}
