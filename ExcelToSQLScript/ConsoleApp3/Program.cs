using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToSQL
{
    public class Read_From_Excel_To_SQL
    {
        public static void Main(string[] args)
        {
            //Example path: "D:\GEP Work\Assignment2\"
            //Example name: "Sample.xls"
            try
            {
                Console.WriteLine("Enter path for file:");
                string path = Console.ReadLine();
                Console.WriteLine("Enter name of file:");
                string name = Console.ReadLine();
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(path+name);
                int sheetCount = workbook.Sheets.Count;
                for (int k = 1; k <= sheetCount; k++)
                {
                    Excel._Worksheet worksheet = (Excel._Worksheet)workbook.Sheets[k];
                    Excel.Range range = worksheet.UsedRange;
                    int rowCount = range.Rows.Count;
                    int colCount = range.Columns.Count;
                    string[] names = new string[rowCount - 1];
                    string fields = "INSERT INTO " + worksheet.Name + " (";
                    for (int z = 1; z <= colCount; z++)
                    {
                        fields = fields + (range.Cells[1, z] as Excel.Range).Value2.ToString();                     
                        if (z != colCount)
                        {
                            fields = fields + ", ";
                        }
                    }
                   fields = fields + ") VALUES (";
                   int n = 0;
                   for (int i = 2; i <= rowCount; i++)
                   {
                        string insert = fields; 
                        for (int j = 1; j <= colCount; j++)
                        {
                            insert = insert + "'"+ (range.Cells[i, j] as Excel.Range).Value2.ToString()+"'";                          
                            if (j != colCount)
                            {
                                insert = insert + ", ";
                            }
                        }
                        insert = insert + ")";
                        names[n++] = insert;                        
                    }
                    using (StreamWriter sw = new StreamWriter(path + worksheet.Name + ".sql"))
                    {

                    foreach (string s in names)
                        {
                    
                        sw.WriteLine(s);
                        }
                    }            
                    GC.Collect();
                    GC.WaitForPendingFinalizers();                  
                    Marshal.ReleaseComObject(range);
                    Marshal.ReleaseComObject(worksheet);
                   }
                Console.WriteLine("Files Created");
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                app.Quit();
                Marshal.ReleaseComObject(app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}