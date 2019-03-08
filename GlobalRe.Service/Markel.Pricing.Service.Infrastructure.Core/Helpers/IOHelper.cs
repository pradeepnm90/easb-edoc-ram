using ExcelDataReader;
using Markel.Pricing.Service.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class IOHelper
    {
        #region Basic File IO

        /// <summary>
        /// Creates specified directory if it does not already exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True is created or already exists.</returns>
        public static bool CreateDirectory(string path, bool file = false)
        {
            bool dirCreated = false;
            if (string.IsNullOrEmpty(path) == false)
            {
                try
                {
                    if (file == true)
                    {
                        path = Path.GetDirectoryName(path);
                    }
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    if (dirInfo.Exists == false)
                    {
                        dirInfo.Create();
                    }
                    dirCreated = true;
                }
                catch (Exception ex)
                {
                    Debugger.Log(1, "Error", ex.Message);
                }
            }
            return dirCreated;
        }

        public static void DeleteTempFileOnServer(string path)
        {
            if (File.Exists(path) && path.StartsWith(MarkelConfiguration.TempFilePath))
            {
                File.Delete(path);
            }
        }

        public static string ReadFirstLine(string fileName, bool skipBlankLines = true)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            StreamReader streamReader = fileInfo.OpenText();

            try
            {
                if (!skipBlankLines) return streamReader.ReadLine();

                string rowText;
                while ((rowText = streamReader.ReadLine()) != null)
                {
                    bool isBlank = (string.IsNullOrEmpty(rowText) || string.IsNullOrEmpty(rowText.Trim()));
                    if (!isBlank)
                    {
                        return rowText;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                streamReader.Close();
            }
        }

        #endregion Basic File IO

        #region Temp File Name

        public static string GenerateTemporaryFileName(string path, string environmentName, string userName, string fileName, bool maintainFileExtension = false)
        {
            string fileExtension = "dat";
            if (maintainFileExtension && fileName.Contains('.'))
            {
                string[] fileParts = fileName.Split('.');
                fileExtension = fileParts[fileParts.Length - 1];
                fileName = fileName.Replace("." + fileExtension, "");
            }

            return string.Format("{0}\\{1}_{2}_{3}_{4}.{5}", path, environmentName, userName, fileName, Guid.NewGuid(), fileExtension);
        }

        #endregion Temp File Name

        #region Read as DataSet

        #region Supported File Types

        private static string CONTENT_VND_MS_EXCEL = "application/vnd.ms-excel";
        private static string CONTENT_VND_MS_EXCEL_MACRO = "application/vnd.ms-excel.sheet.macroEnabled.12";
        private static string CONTENT_VND_MS_OPENXML = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private static string CONTENT_TXT_PLAIN = "text/plain";
        private static string CONTENT_TXT_TAB = "text/tab-separated-values";
        private static string CONTENT_TXT_CSV = "text/csv";

        // http://www.iana.org/assignments/media-types/media-types.xhtml
        public static List<string> SupportedFileTypes = new List<string>() {
            CONTENT_VND_MS_EXCEL,
            CONTENT_VND_MS_EXCEL_MACRO,
            CONTENT_VND_MS_OPENXML,
            CONTENT_TXT_PLAIN,
            CONTENT_TXT_TAB,
            CONTENT_TXT_CSV
        };

        public static string GetContentTypeFromFileName(string fileName)
        {
            if (fileName.EndsWith(".xls"))
            {
                return IOHelper.CONTENT_VND_MS_EXCEL;
            }
            else if (fileName.EndsWith(".xlsx"))
            {
                return IOHelper.CONTENT_VND_MS_OPENXML;
            }
            else if (fileName.EndsWith(".xlsm"))
            {
                return IOHelper.CONTENT_VND_MS_EXCEL_MACRO;
            }
            else
            {
                return IOHelper.CONTENT_TXT_PLAIN;
            }
        }

        #endregion Supported File Types

        /// <summary>
        /// Saves file to temp directory and reads as DataSet.
        /// </summary>
        /// <param name="tempFileName">Local path to temp file</param>
        /// <param name="contentType">Optional: Content Type (application/vnd.ms-excel, text/plain, text/tab-separated-values, etc...)</param>
        /// <param name="defaultSheetName">Optional: Name for Sheet1 if no sheet specified</param>
        /// <returns>DataSet with N number of tables for each sheet</returns>
        public static DataSet ReadFileAsDataSet(string tempFileName, string contentType = null, string defaultSheetName = "Sheet1")
        {
            DataSet excelDataSet = null;

            contentType = string.IsNullOrEmpty(contentType) ? GetContentTypeFromFileName(tempFileName) : contentType;

            if (contentType.Equals(CONTENT_VND_MS_EXCEL) || contentType.Equals(CONTENT_VND_MS_OPENXML) || contentType.Equals(CONTENT_VND_MS_EXCEL_MACRO))
            {
                excelDataSet = ReadExcelFileAsDataSet(tempFileName, contentType);
            }
            else if (contentType.Equals(CONTENT_TXT_TAB))
            {
                excelDataSet = ReadTextFileAsDataSet(tempFileName, defaultSheetName, '\t');
            }
            else if (contentType.Equals(CONTENT_TXT_CSV))
            {
                excelDataSet = ReadTextFileAsDataSet(tempFileName, defaultSheetName, ',');
            }
            else if (contentType.Equals(CONTENT_TXT_PLAIN))
            {
                string firstLine = ReadFirstLine(tempFileName);

                // AIR File (Known Versions: 16.0.0, 17.0.0, 17.1.0)
                if (firstLine.StartsWith("CATRADER") && firstLine.Contains("Version") && firstLine.Contains("Analysis Options/Results"))
                {
                    excelDataSet = ReadAIRFileAsDataSet(tempFileName);
                }

                else if (firstLine.Contains('\t'))
                {
                    // Tab
                    excelDataSet = ReadTextFileAsDataSet(tempFileName, defaultSheetName, '\t');
                }

                else if (firstLine.Contains(','))
                {
                    // CSV
                    excelDataSet = ReadTextFileAsDataSet(tempFileName, defaultSheetName, ',');
                }

                else
                {
                    // Single Column
                    excelDataSet = ReadTextFileAsDataSet(tempFileName, defaultSheetName);
                }
            }
            else
            {
                throw new InvalidDataException(string.Format("File content type is not supported: '{0}'", contentType));
            }

            return excelDataSet;
        }

        /// <summary>
        /// https://github.com/ExcelDataReader/ExcelDataReader
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataSet ReadExcelFileAsDataSet(string fileName, string contentType)
        {
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            IExcelDataReader excelReader = null;

            try
            {
                string fileExtension = Path.GetExtension(fileName);
                if (fileExtension.Equals(".xls", StringComparison.CurrentCultureIgnoreCase) ||
                    CONTENT_VND_MS_EXCEL.Equals(contentType, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (fileExtension.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase) ||
                         CONTENT_VND_MS_OPENXML.Equals(contentType, StringComparison.CurrentCultureIgnoreCase) ||
                         CONTENT_VND_MS_EXCEL_MACRO.Equals(contentType, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    throw new InvalidDataException(string.Format("ReadExcelFileAsDataSet does not support '{0}' format.", fileExtension));
                }

                // DataSet - The result of each spreadsheet will be created in the result.Tables
                return excelReader.AsDataSet();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (excelReader != null)
                {
                    // Free resources (IExcelDataReader is IDisposable)
                    excelReader.Close();
                }
            }
        }

        private static DataSet ReadTextFileAsDataSet(string fileName, string sheetName, char? delimiter = null, bool trimWhiteSpace = false)
        {
            DataSet stagingData = new DataSet();

            FileInfo fileInfo = new FileInfo(fileName);
            StreamReader streamReader = fileInfo.OpenText();

            try
            {
                // Staging Sheet
                DataTable stagingSheet = new DataTable(sheetName);

                string rowText;
                while ((rowText = streamReader.ReadLine()) != null)
                {
                    AddRow(stagingSheet, rowText, delimiter, trimWhiteSpace);
                }

                stagingData.Tables.Add(stagingSheet);
                stagingData.AcceptChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                streamReader.Close();
            }

            return stagingData;
        }

        private static DataSet ReadAIRFileAsDataSet(string fileName)
        {
            string ANALYSIS_OPTIONS = "Analysis Options";
            string RESULTS_SUMMARY = "Annual Results Summary - Zone: \"Global\"";
            string EVENT_RESULTS = "Event Results - Zone: \"Global\"";

            string RESULTS_SUB_ZONE_PROGRAM = "Program";
            string RESULTS_SUB_ZONE_LAYER = "Layer";

            string[] skipPattern = new string[] { "*", "-" };

            DataSet stagingData = new DataSet();

            FileInfo fileInfo = new FileInfo(fileName);
            StreamReader streamReader = fileInfo.OpenText();

            try
            {
                string rowText;
                while ((rowText = streamReader.ReadLine()) != null)
                {
                    // Section 1: Analysis Options
                    if (rowText.Equals(ANALYSIS_OPTIONS))
                    {
                        // Read Analysis Options until skip pattern (blank row)
                        DataTable airAnalysis = new DataTable(ANALYSIS_OPTIONS);
                        while ((rowText = streamReader.ReadLine()) != null && !rowText.Equals(RESULTS_SUMMARY))
                        {
                            if (!SkipRow(rowText, skipPattern, true))
                            {
                                AddRow(airAnalysis, rowText, ':', true);
                            }
                        }

                        stagingData.Tables.Add(airAnalysis);
                    }

                    // Section 2: Annual Results Summary - Zone: "Global"
                    if (rowText != null && rowText.Equals(RESULTS_SUMMARY))
                    {
                        string summaryColumns = null;

                        // Next row of content is the column header
                        while ((rowText = streamReader.ReadLine()) != null && string.IsNullOrEmpty(summaryColumns) && !rowText.Equals(EVENT_RESULTS))
                        {
                            if (!SkipRow(rowText, skipPattern, true))
                            {
                                summaryColumns = rowText;
                            }
                        }

                        // Look for Program and Each Layer
                        DataTable currentSummary = null;
                        while ((rowText = streamReader.ReadLine()) != null && !rowText.StartsWith(EVENT_RESULTS))
                        {
                            if (rowText.Equals(RESULTS_SUB_ZONE_PROGRAM) || rowText.StartsWith(RESULTS_SUB_ZONE_LAYER))
                            {
                                currentSummary = new DataTable(rowText);
                                AddRow(currentSummary, summaryColumns, ',', true);
                                stagingData.Tables.Add(currentSummary);
                            }
                            else if (currentSummary != null && !SkipRow(rowText, skipPattern, true))
                            {
                                AddRow(currentSummary, rowText, ',', true);
                            }
                        }
                    }

                    // Section 3: Event Results - Zone: "Global"
                    if (rowText != null && rowText.StartsWith(EVENT_RESULTS))
                    {
                        // Look for Program and Each Layer
                        DataTable currentResults = null;
                        do
                        {
                            if (rowText.StartsWith(EVENT_RESULTS))
                            {
                                currentResults = new DataTable(rowText);
                                stagingData.Tables.Add(currentResults);
                            }
                            else if (currentResults != null && !SkipRow(rowText, skipPattern, true))
                            {
                                AddRow(currentResults, rowText, ',', true);
                            }
                        }
                        while ((rowText = streamReader.ReadLine()) != null);
                    }
                }

                stagingData.AcceptChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                streamReader.Close();
            }

            return stagingData;
        }

        private static void AddRow(DataTable stagingSheet, string rowText, char? delimiter, bool trimWhiteSpace)
        {
            var newRow = stagingSheet.NewRow();

            string[] cells = delimiter != null ? rowText.Split((char)delimiter) : new string[] { rowText };
            for (int columnId = 0; columnId < cells.Length; columnId++)
            {
                string fieldName = TableUtility.GetColumnNameFromNumber(columnId + 1);

                if (stagingSheet.Columns.Count <= columnId)
                {
                    stagingSheet.Columns.Add(fieldName);
                }
                string value = cells[columnId];

                if (trimWhiteSpace && value != string.Empty) value = value.Trim();

                newRow[fieldName] = value;
            }

            stagingSheet.Rows.Add(newRow);
        }

        private static bool SkipRow(string rowText, string[] skipPattern, bool skipBlankRows)
        {
            bool skipLine = skipBlankRows && (string.IsNullOrEmpty(rowText) || string.IsNullOrEmpty(rowText.Trim()));
            if (!skipLine)
            {
                foreach (string ignorePattern in skipPattern)
                {
                    if (rowText.StartsWith(ignorePattern))
                    {
                        skipLine = true;
                        break;
                    }
                }
            }
            return skipLine;
        }

        #endregion Read as DataSet

        #region Assembly Info

        public static string GetServerVersion()
        {
            Version version = IOHelper.GetAssemblyVersion();

            if (version.ToString().Equals("1.0.0.0"))
            {
                return "[DEVELOPMENT]";
            }
            else if (version.Major == 0 && version.Minor == 0)
            {
                return $"[DEVELOPMENT].{version.Build}.{version.Revision}";
            }
            else
            {
                return version.ToString();
            }
        }

        public static DateTime? GetAssemblyDate()
        {
            try
            {
                string assemblyPath = Assembly.GetExecutingAssembly().Location;
                return File.GetLastWriteTime(assemblyPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Version GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion Assembly Info
    }
}
