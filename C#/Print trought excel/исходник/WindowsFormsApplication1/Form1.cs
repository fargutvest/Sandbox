using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application excelapp = new Excel.Application(); // создаем экземпляр приложения
            excelapp.Visible = true; // показывать документ Excel, false - не показывать

            Excel.Workbook excelappworkbook = excelappworkbook = excelapp.Workbooks.Open(Convert.ToString(System.IO.Directory.GetCurrentDirectory()) + @"\1.xlsx", // создаем экземпляр книги и открываем в этот экземпляр книги файл
             Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing, Type.Missing);
            

            Excel.Sheets excelsheets = excelappworkbook.Worksheets; //получаем массив ссылок на листы выбранной книги
            Excel.Worksheet excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1); //получаем ссылку на лист 1
            
            //*******************************Работа с ячейками ****************************************
            excelworksheet.Cells[1, 1] = "Привет Мир!"; //редактируем ячейку [1,1]

            (excelworksheet.Columns as Excel.Range).ColumnWidth = 5; //ширина столбцов для всех 
            
            (excelworksheet.Rows as Excel.Range).RowHeight = 5; // высота строк для всех

            (excelworksheet.Columns[1] as Excel.Range).ColumnWidth = 20; //ширина конкретного столбца

            (excelworksheet.Rows[1] as Excel.Range).RowHeight = 20; // высота конкретной строки

            (excelworksheet.Cells[1, 1] as Excel.Range).Font.Bold = true;//жирность в клетке

            (excelworksheet.Cells[1, 1] as Excel.Range).Font.Size = 8; //размер шрифта в клетке

            (excelworksheet.Cells[1, 1] as Excel.Range).Font.Name = "Times New Roman"; //название шрифта в клетке

            //  (excelworksheet.Cells[1, 1] as Excel.Range).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble; //стиль нижней (Bottom) границы

            // (excelworksheet.Cells[1, 1] as Excel.Range).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium; //толщина нижней (Bottom) границы

            (excelworksheet.Cells[1, 1] as Excel.Range).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter; //выравнивание по горизонтали

            (excelworksheet.Cells[1, 1] as Excel.Range).VerticalAlignment = Excel.XlVAlign.xlVAlignTop; //выравнивание по вертикали

            (excelworksheet.Cells[1, 1] as Excel.Range).Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle; // подчеркнутый  текст

            (excelworksheet.Cells[5, 5] as Excel.Range).Interior.Color = Color.Red; // цвет фона ячейки

            (excelworksheet.Cells[1, 1] as Excel.Range).Font.Color = Color.DarkGreen; // цвет текста


            //**********************************************************************************************


            // в ячейке 27:T есть символ
            //обвести рамку (верх, лево, право, низ) вокруг испльзующейся области 
           excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = 1;
            excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = 1;
            excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = 1;
            excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = 1;

            //обвести клетки внутри испльзующейся области
             excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = 1;
             excelappworkbook.ActiveSheet.UsedRange.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = 1;

         //    excelappworkbook.PrintOutEx(); // команда на печать в скобках можно передвать параметры: предпросмотр, количество копий, печать в файл
     
            
          //  excelappworkbook.Close(false, Type.Missing); // закрыть файл документа, не сохраняя его(false)



            
            
           
            
            
        }
    }
}
