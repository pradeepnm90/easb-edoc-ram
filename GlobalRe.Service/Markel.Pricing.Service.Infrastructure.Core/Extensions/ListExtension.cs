using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ListExtension
    {
        public static MemoryStream ToExcel<T>(this List<T> list)
        {
            //configure embeded license
            Aspose.Cells.License license = new Aspose.Cells.License();
            license.SetLicense("Aspose.Cells.lic");

            try
            {
                //Create excel workbook
                Workbook book = new Workbook();
                Worksheet sheet = book.Worksheets[0];

                List<string> objHeaders = new List<string>();
                PropertyInfo[] headerInfo = typeof(T).GetProperties();
                foreach (var property in headerInfo)
                {
                    var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                            .Cast<DisplayNameAttribute>().FirstOrDefault();

                    //Avoid columns from Base Business Entity
                    if (property.Name == "ChangedFields") { break; }
                    objHeaders.Add(property.Name);
                }

                sheet.Cells.ImportCustomObjects(list, objHeaders.ToArray(), true, 1, 0, list.Count, true, string.Empty, false);

                #region Header style

                Style style = book.CreateStyle();
                style.Font.IsBold = true;
                // Define a style flag struct.
                StyleFlag flag = new StyleFlag();
                flag.FontBold = true;
                // Get the first row in the first worksheet.
                Row row = book.Worksheets[0].Cells.Rows[1];
                // Apply the style to it.
                row.ApplyStyle(style, flag);

                #endregion

                // Auto-fit all the columns
                book.Worksheets[0].AutoFitColumns();

                MemoryStream outputStream = new MemoryStream();
                //Save workbook as stream instead of saving it to the Disk
                book.Save(outputStream, SaveFormat.Xlsx);

                //Call dispose methods
                sheet.Dispose();
                book.Dispose();

                outputStream.Position = 0;
                return outputStream;
            }
            catch (Exception)
            {
                // TODO : Handle out of memory exception
                throw;
            }
        }

        public static bool In<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
