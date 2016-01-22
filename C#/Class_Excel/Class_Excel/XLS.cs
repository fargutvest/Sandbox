using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Linq;

namespace XLS
{
    class Xls
    {
        //поля
        private Microsoft.Office.Interop.Excel.Application excel;
        private Microsoft.Office.Interop.Excel.Workbook book;
        private Microsoft.Office.Interop.Excel.Worksheet sheet;
        
        //свойства
        
        
        //конструктор

       
        //методы
        private void OutCells(int ns, int nk, string str)
        //вывод строки str в ячейку с координатами ns - номер строки, nk - номер колонки
        {
            sheet.Cells[ns, nk] = str;
        }
        private Excel.Range Range(string xy, string yx)
        //выделение области, ограниченной верхн.лев ячейкой ХУ и прав.нижн. вида "А1", на выходе значение типа Excel.Range
        {
            Excel.Range kontur;
            return kontur = (Excel.Range)sheet.get_Range(xy, yx).Cells;
            //kontur.Merge(Type.Missing); объединение области в одну ячейки
            //return kontur;
        }
        private string ConvertIndex(int nk)
        {
            return "";
        }
    }
}
