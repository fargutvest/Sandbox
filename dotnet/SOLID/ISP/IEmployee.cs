using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSOLID
{
    public interface IEmployee
    {
        bool AddDetailsEmployee();
        bool ShowDetailsEmployee(int id);
    }
}

namespace SOLID
{
    public interface IOperationAdd
    {
        bool AddDetailsEmployee();
    }

    public interface IOperationGet
    {
        bool ShowDetailsEmployee(int id);
    }

}
