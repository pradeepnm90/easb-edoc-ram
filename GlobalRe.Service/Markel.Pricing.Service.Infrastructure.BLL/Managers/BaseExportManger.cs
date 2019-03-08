using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public class BaseExportManger : BaseManager
    {
        #region Licenses

        private static object LicenseLock = new object();
        private static bool IsLicenseRegistered = false;

        private static void RegisterLicenses()
        {
            lock (LicenseLock)
            {
                if (!IsLicenseRegistered)
                {
                    try
                    {
                        // Aspose PDF License
                        Aspose.Pdf.License asposePdfLicense = new Aspose.Pdf.License() { Embedded = true };
                        asposePdfLicense.SetLicense("Aspose.Pdf.lic");

                        // Aspose Cells License
                        Aspose.Cells.License asposeCellsLicense = new Aspose.Cells.License();
                        asposeCellsLicense.SetLicense("Aspose.Cells.lic");
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Aspose License Registration FAILED!");
                    }
                    IsLicenseRegistered = true;
                }
            }
        }

        #endregion Licenses

        #region Constructors

        public BaseExportManger(IUserManager userManager) : base(userManager)
        {
            RegisterLicenses();
        }

        #endregion

        #region Methods

        public MemoryStream ToPDF<T>(T entity, string xslt)
        {
            MemoryStream xmlStream = entity.TransformXsltStream(xslt);
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();
            pdfDoc.BindXml(xmlStream);

            pdfDoc.Info.Author = UserIdentity.UserName;
            pdfDoc.Info.CreationDate = DateTime.Now;
            pdfDoc.Info.Keywords = MarkelConfiguration.ApplicationName;

            MemoryStream pdfStream = new MemoryStream();
            pdfDoc.Save(pdfStream);
            return pdfStream;
        }

        public MemoryStream ToExcel<T>(IPaginatedList<T> list)
        {
            try
            {
                //Create excel workbook
                Aspose.Cells.Workbook book = new Aspose.Cells.Workbook();
                Aspose.Cells.Worksheet sheet = book.Worksheets[0];

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

                sheet.Cells.ImportCustomObjects(list.Items.ToList(), objHeaders.ToArray(), true, 1, 0, list.TotalRecordCount, true, string.Empty, false);

                #region Header style

                Aspose.Cells.Style style = book.CreateStyle();
                style.Font.IsBold = true;
                // Define a style flag struct.
                Aspose.Cells.StyleFlag flag = new Aspose.Cells.StyleFlag();
                flag.FontBold = true;
                // Get the first row in the first worksheet.
                Aspose.Cells.Row row = book.Worksheets[0].Cells.Rows[1];
                // Apply the style to it.
                row.ApplyStyle(style, flag);

                #endregion

                // Auto-fit all the columns
                book.Worksheets[0].AutoFitColumns();

                using (MemoryStream outputStream = new MemoryStream())
                {
                    //Save workbook as stream instead of saving it to the Disk
                    book.Save(outputStream, Aspose.Cells.SaveFormat.Xlsx);

                    //Call dispose methods
                    sheet.Dispose();
                    book.Dispose();

                    outputStream.Position = 0;
                    return outputStream;
                }
            }
            catch (Exception)
            {
                // TODO : Handle out of memory exception
                throw;
            }
        }

        #endregion Methods
    }
}
