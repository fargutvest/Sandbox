using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns
{
    public class MySingleton<T> where T : class, new()
    {

        static T _instance = null;
        MySingleton() { }

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
}
