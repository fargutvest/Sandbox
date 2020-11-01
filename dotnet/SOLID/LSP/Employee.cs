using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSOLID
{

    public abstract class Employee
    {
        public virtual string GetWorkDetails(int id)
        {
            return "Base Work";
        }

        public virtual string GetEmployeeDetails(int id)
        {
            return "Base Employee";
        }
    }

    public class SeniorEmployee : Employee
    {
        public override string GetWorkDetails(int id)
        {
            return "Senior Work";
        }

        public override string GetEmployeeDetails(int id)
        {
            return "Senior Employee";
        }
    }

    public class JuniorEmployee : Employee
    {
        // Допустим, для Junior’a отсутствует информация
        public override string GetWorkDetails(int id)
        {
            throw new NotImplementedException();
        }


        public override string GetEmployeeDetails(int id)
        {
            return "Junior Employee";

        }
    }
}

namespace SOLID
{
    public interface IEmployee
    {
        string GetEmployeeDetails(int employeeId);
    }

    public interface IWork
    {
        string GetWorkDetails(int employeeId);
    }

    public class SeniorEmployee : IWork, IEmployee
    {
        public string GetWorkDetails(int employeeId)
        {
            return "Senior Work";
        }

        public string GetEmployeeDetails(int employeeId)
        {
            return "Senior Employee";
        }
    }

    public class JuniorEmployee : IEmployee
    {
        public string GetEmployeeDetails(int employeeId)
        {
            return "Junior Employee";
        }
    }

}
