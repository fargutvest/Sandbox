using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSOLID
{
    public class EmployeeReport
    {
        // <summary>
        /// Тип отчета
        /// </summary>
        public string TypeReport { get; set; }

        /// <summary>
        /// Отчет по сотруднику
        /// </summary>
        public void GenerateReport(Employee em)
        {
            if (TypeReport == "CSV")
            {
                // Генерация отчета в формате CSV
            }

            if (TypeReport == "PDF")
            {
                // Генерация отчета в формате PDF
            }
        }
    }
}

namespace SOLID
{
    public class IEmployeeReport
    {
        /// <summary>
        /// Метод для создания отчета
        /// </summary>
        public virtual void GenerateReport(Employee em)
        {
            // Базовая реализация, которую нельзя модифицировать
        }
    }

    public class EmployeeCSVReport : IEmployeeReport
    {
        public override void GenerateReport(Employee em)
        {
            // Генерация отчета в формате CSV
        }
    }

    public class EmployeePDFReport : IEmployeeReport
    {
        public override void GenerateReport(Employee em)
        {
            // Генерация отчета в формате PDF
        }
    }

}
