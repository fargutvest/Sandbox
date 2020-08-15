using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SandboxIEnumerable
{
    public class Cats : IEnumerable
    {
        private List<Cat> _catsList;

        public Cats()
        {
            _catsList = new List<Cat>();

            
            for (int i = 0; i < 5; i++)
            {
                _catsList.Add(new Cat($"I am cat number {i + 1}")); 
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _catsList.GetEnumerator();
        }
    }
}
