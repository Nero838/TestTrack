using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace MC1
{
    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel()
        {

        }

        public Excel(string path, int Sheet)
        {
            if(File.Exists(path) == false)
            {
                CreateNewFile(path);
            }
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string ReadCell(int row, int column)
        {
            row++;
            column++;
            if (ws.Cells[row, column].Value2 != null)
            {
                return ws.Cells[row, column].Value2;
            }
            else return "";
        }

        public void WriteToCell(int row, int column, string text)
        {
            row++;
            column++;
            ws.Cells[row, column].Value2 = text;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }

        public void Close()
        {
            wb.Close();
        }

        public void CreateNewFile(string path)
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            wb.SaveAs(path);

        }
    }
}
