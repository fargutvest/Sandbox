using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NOSOLID;

namespace LSP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> list = new List<Employee>();

            list.Add(new JuniorEmployee());
            list.Add(new SeniorEmployee());

            foreach (Employee emp in list)
            {
                emp.GetEmployeeDetails(985);
            }
        }
    }
}
