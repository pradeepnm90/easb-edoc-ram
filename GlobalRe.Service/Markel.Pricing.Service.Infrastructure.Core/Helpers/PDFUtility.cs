using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Exceptions;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class PDFUtility
    {
        public static MemoryStream ToPDF<T>(this List<T> list)
        {
            Aspose.Pdf.License license = new Aspose.Pdf.License();
            license.Embedded = true;
            license.SetLicense("Aspose.Pdf.lic");
           
            Document doc = new Document();
            PageInfo pageInfo = doc.PageInfo;
            pageInfo.Margin = new MarginInfo(ReportsSetting.PageMarginLeft, ReportsSetting.PageMarginBottom, ReportsSetting.PageMarginRight, ReportsSetting.PageMarginTop);

            string reportHeader = "";
            var firstLayer = list.FirstOrDefault();
            if (firstLayer != null)
            {
                var parentValue = firstLayer.GetType().GetProperty(ReportsSetting.PropNameParent).GetValue(firstLayer, null);
                reportHeader = GenerateReportHeader(parentValue);
            }
            GetLayerWorksheet(list, doc, list.Count, reportHeader);

            //GenerateSummaryReport(doc, list.Count, reportHeader, ReportlTypesEnum.MintSummary);
            //GenerateSummaryReport(doc, list.Count, reportHeader, ReportlTypesEnum.NonMintSummary);
            //GenerateAccountSummaryReport(doc, list.Count, reportHeader, ReportlTypesEnum.AccountSummary);

            if (doc.Pages.Count < 1)
            {
                doc.Pages.Add();
            }
            MemoryStream stream = new MemoryStream();            
            doc.Save(stream);
            return stream;
        }

        public static MemoryStream GetExposureSummaryReport<T>(T list)
        {
            Aspose.Pdf.License license = new Aspose.Pdf.License();
            license.Embedded = true;
            license.SetLicense("Aspose.Pdf.lic");

            Document doc = new Document();
            PageInfo pageInfo = doc.PageInfo;
            pageInfo.Margin = new MarginInfo(ReportsSetting.PageMarginLeft, ReportsSetting.PageMarginBottom, ReportsSetting.PageMarginRight, ReportsSetting.PageMarginTop);
            string reportHeader = "";
            GenerateAccountSummaryReport(doc, 3, reportHeader, ReportlTypesEnum.AccountSummary);
            if (doc.Pages.Count < 1)
            {
                doc.Pages.Add();
            }
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            return stream;
        }
        public static MemoryStream GetMintSummaryReport<T>(T list)
        {

            Aspose.Pdf.License license = new Aspose.Pdf.License();
            license.Embedded = true;
            license.SetLicense("Aspose.Pdf.lic");

            Document doc = new Document();
            PageInfo pageInfo = doc.PageInfo;            
            pageInfo.Margin = new MarginInfo(ReportsSetting.PageMarginLeft, ReportsSetting.PageMarginBottom, ReportsSetting.PageMarginRight, ReportsSetting.PageMarginTop);           
            string reportHeader = "";
            GenerateSummaryReport(doc, 6, reportHeader, ReportlTypesEnum.MintSummary);
            
            if (doc.Pages.Count < 1)
            {
                doc.Pages.Add();
            }
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            return stream;
        }
        public static MemoryStream GetNonMintSummaryReport<T>(T list)
        {
            Aspose.Pdf.License license = new Aspose.Pdf.License();
            license.Embedded = true;
            license.SetLicense("Aspose.Pdf.lic");

            Document doc = new Document();
            PageInfo pageInfo = doc.PageInfo;
            pageInfo.Margin = new MarginInfo(ReportsSetting.PageMarginLeft, ReportsSetting.PageMarginBottom, ReportsSetting.PageMarginRight, ReportsSetting.PageMarginTop);
            string reportHeader = "";
            GenerateSummaryReport(doc, 5, reportHeader, ReportlTypesEnum.NonMintSummary);
            if (doc.Pages.Count < 1)
            {
                doc.Pages.Add();
            }
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            return stream;
        }
        private static void GetLayerWorksheet<T>(this List<T> list, Document doc, int cout, string reportHeader)
        {
            var myList = ReportsSetting.ReportsConfiguration.OrderBy(Ss => Ss.Value.sequenceNumber).ToList();
            #region Layer questionnaire Worksheet
            foreach (T layer in list)
            {
                Page pdfPage = doc.Pages.Add();
                pdfPage.PageInfo.Height = PageSize.A4.Height;
                pdfPage.PageInfo.Width = PageSize.A4.Width;
                //Page Header With Submission Info
                Table pageHeaderTable = new Table();
                pageHeaderTable.Margin = new MarginInfo();
                pageHeaderTable.Margin.Bottom = 6;
                pageHeaderTable.ColumnWidths = ReportsSetting.LayerColumnWidths;
                Row pageHeaderRow = pageHeaderTable.Rows.Add();
                AddHeaderCell(pageHeaderRow, reportHeader, 10, HorizontalAlignment.Center);
                Paragraphs pageHeaderParagraphs = pdfPage.Paragraphs;
                pageHeaderTable.Border = new BorderInfo(BorderSide.None);
                pageHeaderParagraphs.Add(pageHeaderTable);
                //Layer Details Header
                Table table = new Table();
                table.ColumnWidths = ReportsSetting.LayerColumnWidths;
                Row headerRow = table.Rows.Add();
                AddHeaderCell(headerRow, ReportsSetting.LayerDetailsHeader);
                int lineCount = ReportsSetting.ReportsConfiguration.Count / 3;
                if (ReportsSetting.ReportsConfiguration.Count % 3 != 0)
                    lineCount++;
                int index = 0;
                for (int j = 1; j <= lineCount && index < ReportsSetting.ReportsConfiguration.Count; j++)
                {
                    Row row = table.Rows.Add();
                    row.FixedRowHeight = 12;
                    for (int i = 0; i < 3 && index < ReportsSetting.ReportsConfiguration.Count; i++)
                    {
                        var item = myList[index];
                        var value = layer.GetType().GetProperty(item.Key).GetValue(layer, null);
                        Cell lableCell = row.Cells.Add();
                        lableCell.Margin = new MarginInfo();
                        lableCell.Margin.Left = 2;
                        string strValue = "";
                        if (value != null)
                        {
                            strValue = value.ToString();
                        }
                        TextFragment textFragment = new TextFragment(item.Value.Name);
                        textFragment.TextState.FontSize = 8;
                        textFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                        lableCell.Paragraphs.Add(textFragment);
                        Cell valueCell = row.Cells.Add();
                        TextFragment anstextFragment = GetFormatedValue(strValue, item.Value.controlType);
                        valueCell.Paragraphs.Add(anstextFragment);
                        index++;
                    }

                }
                Paragraphs paragraphs = pdfPage.Paragraphs;
                table.Border = new BorderInfo(BorderSide.All);
                paragraphs.Add(table);
                var propvalue = layer.GetType().GetProperty(ReportsSetting.PropNameQuestionnaire).GetValue(layer, null);
                if (propvalue != null)
                {
                    var questionnaire = propvalue.GetType().GetProperty(ReportsSetting.PropNameQuestionnaireAnswers).GetValue(propvalue, null);
                    var config = new MapperConfiguration(cfg => { });
                    var mapper = config.CreateMapper();
                    var questionnaires = mapper.Map<List<Questionnaire>>(questionnaire);
                    List<Questionnaire> lstQuetions = Questionnaire.BuildQuestionGroupTree(questionnaires);
                    foreach (var item in lstQuetions)
                    {
                        Table qstable = new Table();
                        qstable.Margin = new MarginInfo();
                        qstable.Margin.Top = 3;
                        qstable.ColumnWidths = Constants.ReportsSetting.LayerColumnWidths;
                        AddQuestionnaireTableRow(qstable, item);
                        Paragraphs qsparagraphs = pdfPage.Paragraphs;
                        qstable.Border = new BorderInfo(BorderSide.All);
                        paragraphs.Add(qstable);
                    }
                }
            }
            #endregion
        }
        private static HeaderFooter GetPageHeader(string reportHeader)
        {
            #region Page Header
            HeaderFooter header = new HeaderFooter();
            header.Margin = new MarginInfo(10, 10, 10, 5);
            Table pageHeaderTable = new Table();
            pageHeaderTable.Margin = new MarginInfo();
            pageHeaderTable.Margin.Bottom = 6;
            pageHeaderTable.ColumnWidths = "100 100 100 100 100 100";
            Row pageHeaderRow = pageHeaderTable.Rows.Add();
            AddHeaderCell(pageHeaderRow, reportHeader, 10, HorizontalAlignment.Center);
            pageHeaderTable.Border = new BorderInfo(BorderSide.None);
            header.Paragraphs.Add(pageHeaderTable);
            return header;
            #endregion
        }       
        private static void GenerateAccountSummaryReport(Document doc, int cout, string reportHeader, ReportlTypesEnum reportType)
        {
            doc.PageLayout = new PageLayout();

            Page pdfPage = doc.Pages.Add();
            pdfPage.Header = GetPageHeader(reportHeader);
            pdfPage.PageInfo.Height = PageSize.A4.Height;
            pdfPage.PageInfo.Width = PageSize.A4.Width;
            Paragraphs pageParagraph = pdfPage.Paragraphs;
            Table exposureSummaryTable1 = new Table();
            exposureSummaryTable1.ColumnWidths = ReportsSetting.ExposureSummaryMainColumnWidths;
            exposureSummaryTable1.Margin = new MarginInfo();
            exposureSummaryTable1.Margin.Bottom = 5;
            exposureSummaryTable1.Margin.Top = 5;
            Row mainRowExposure = exposureSummaryTable1.Rows.Add();
            int exposureSummaryCell1TotalRecords = 0;
            int exposureSummaryCell2TotalRecords = 0;
            Cell exposureSummaryCell1 = mainRowExposure.Cells.Add();
            Cell exposureSummaryCell2 = mainRowExposure.Cells.Add();
            #region exposureSummaryCell1
            {

                exposureSummaryCell1.VerticalAlignment = VerticalAlignment.Top;
                exposureSummaryCell1.Margin = new MarginInfo(0, 0, 0, 0);

                Table region = GetExposureSummaryTable(ReportsSetting.ExposureSummaryRegionTableHeader, ReportsSetting.ExposureSummaryRegionColumnHeader, 10);
                region.Margin = new MarginInfo();
                region.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(region);
                exposureSummaryCell1TotalRecords += 10;//

                Table states = GetExposureSummaryTable(ReportsSetting.ExposureSummaryStateTableHeader, ReportsSetting.ExposureSummaryStateColumnHeader, 15);
                states.Margin = new MarginInfo();
                states.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(states);
                exposureSummaryCell1TotalRecords += 15;//


                Table milesToCoast = GetExposureSummaryTable(ReportsSetting.ExposureSummaryMilestoCoastTableHeader, ReportsSetting.ExposureSummaryMilestoCoastColumnHeader, 15);
                milesToCoast.Margin = new MarginInfo();
                milesToCoast.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(milesToCoast);
                exposureSummaryCell1TotalRecords += 15;//


                Table zones = GetExposureSummaryTable(ReportsSetting.ExposureSummaryZonesTableHeader, ReportsSetting.ExposureSummaryZonesColumnHeader, 15);
                zones.Margin = new MarginInfo();
                zones.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(zones);
                exposureSummaryCell1TotalRecords += 15;//

                Table levees = GetExposureSummaryTable(ReportsSetting.ExposureSummaryLeveesTableHeader, ReportsSetting.ExposureSummaryLeveesColumnHeader, 15);
                levees.Margin = new MarginInfo();
                levees.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(levees);
                exposureSummaryCell1TotalRecords += 15;//

                Table floodZone = GetExposureSummaryTable(ReportsSetting.ExposureSummaryFloodZoneTableHeader, ReportsSetting.ExposureSummaryFloodZoneColumnHeader, 15);
                floodZone.Margin = new MarginInfo();
                floodZone.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(floodZone);
                exposureSummaryCell1TotalRecords += 15;//

                Table city = GetExposureSummaryTable(ReportsSetting.ExposureSummaryCityTableHeader, ReportsSetting.ExposureSummaryCityColumnHeader, 140);
                city.Margin = new MarginInfo();
                city.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(city);
                exposureSummaryCell1TotalRecords += 140;//

            }
            #endregion

            #region exposureSummaryCell2
            {

                exposureSummaryCell2.VerticalAlignment = VerticalAlignment.Top;
                exposureSummaryCell2.Margin = new MarginInfo(0, 0, 0, 0);

                Table country = GetExposureSummaryTable(ReportsSetting.ExposureSummaryCountryTableHeader, ReportsSetting.ExposureSummaryCountryColumnHeader, 4);
                country.Margin = new MarginInfo();
                country.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(country);
                exposureSummaryCell2TotalRecords += 4;//

                Table county = GetExposureSummaryTable(ReportsSetting.ExposureSummaryCountyTableHeader, ReportsSetting.ExposureSummaryCountyColumnHeader, 15);
                county.Margin = new MarginInfo();
                county.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(county);
                exposureSummaryCell2TotalRecords += 15;//

                Table occupancy = GetExposureSummaryTable(ReportsSetting.ExposureSummaryOccupancyTableHeader, ReportsSetting.ExposureSummaryOccupancyColumnHeader, 4);
                occupancy.Margin = new MarginInfo();
                occupancy.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(occupancy);
                exposureSummaryCell2TotalRecords += 4;//

                Table construction = GetExposureSummaryTable(ReportsSetting.ExposureSummaryConstructionTableTableHeader, ReportsSetting.ExposureSummaryConstructionColumnHeader, 3);
                construction.Margin = new MarginInfo();
                construction.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(construction);
                exposureSummaryCell2TotalRecords += 3;//

                Table earthquakeRegion = GetExposureSummaryTable(ReportsSetting.ExposureSummaryEarthquakeRegionTableHeader, ReportsSetting.ExposureSummaryEarthquakeRegionColumnHeader, 15);
                earthquakeRegion.Margin = new MarginInfo();
                earthquakeRegion.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(earthquakeRegion);
                exposureSummaryCell2TotalRecords += 15;//

                Table geocode = GetExposureSummaryTable(ReportsSetting.ExposureSummaryGeocodeTableHeader, ReportsSetting.ExposureSummaryGeocodeColumnHeader, 15);
                geocode.Margin = new MarginInfo();
                geocode.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(geocode);
                exposureSummaryCell2TotalRecords += 15;//

                Table tornado = GetExposureSummaryTable(ReportsSetting.ExposureSummaryTornadoTableHeader, ReportsSetting.ExposureSummaryTornadoColumnHeader, 15);
                tornado.Margin = new MarginInfo();
                tornado.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(tornado);
                exposureSummaryCell2TotalRecords += 15;//
            }
            #region Blank Table 
            ///Blank Table is added to match the height of right and left cell
            if (exposureSummaryCell1TotalRecords < exposureSummaryCell2TotalRecords)
            {
                Table blankTable = GetBlankTable((exposureSummaryCell2TotalRecords - exposureSummaryCell1TotalRecords), 260);
                blankTable.Margin = new MarginInfo();
                blankTable.Margin.Bottom = 5;
                exposureSummaryCell1.Paragraphs.Add(blankTable);
            }
            else if (exposureSummaryCell1TotalRecords > exposureSummaryCell2TotalRecords)
            {
                Table blankTable = GetBlankTable((exposureSummaryCell1TotalRecords - exposureSummaryCell2TotalRecords), 260);
                blankTable.Margin = new MarginInfo();
                blankTable.Margin.Bottom = 5;
                exposureSummaryCell2.Paragraphs.Add(blankTable);
            }
            #endregion

            pageParagraph.Add(exposureSummaryTable1);
            #endregion


        } 
        private static void AddQuestionnaireTableRow(Table qstable, Questionnaire question)
        {
            ControlTypesEnum topLevelControlType = ControlTypesEnum.TEXT_INPUT;
            if (!Enum.TryParse(question.QuestionTypeName, out topLevelControlType))
                throw new NotFoundAPIException($"ControlType '{question.QuestionTypeName}' is unknown!");

            if (topLevelControlType == ControlTypesEnum.GROUP_HEADER)
            {
                Row headerRow = qstable.Rows.Add();
                AddHeaderCell(headerRow, question.QuestionText);
                if (question.Questions != null && question.Questions.Count > 0)
                {
                    foreach (var item in question.Questions)
                    {
                        if (item.Questions != null && item.Questions.Count > 0)
                            AddQuestionnaireTableRow(qstable, item);
                        else
                        {
                            int totalRow = question.Questions.Count() / 3;
                            if (question.Questions.Count() % 3 != 0)
                                totalRow++;
                            int qsIndex = 0;
                            for (int rowCount = 0; rowCount < totalRow && qsIndex < question.Questions.Count(); rowCount++)
                            {
                                Row row = qstable.Rows.Add();
                                row.FixedRowHeight = 12;
                                for (int colNum = 0; colNum < 3 && qsIndex < question.Questions.Count(); colNum++)
                                {
                                    TextFragment ansTextFragment = new TextFragment(question.Questions[qsIndex].QuestionText);
                                    ansTextFragment.TextState.FontSize = 8;
                                    ansTextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                                    Cell lableCell = row.Cells.Add();
                                    lableCell.Margin = new MarginInfo();
                                    lableCell.Margin.Left = 2;
                                    ansTextFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
                                    lableCell.Paragraphs.Add(ansTextFragment);
                                    Cell valueCell = row.Cells.Add();

                                    ControlTypesEnum questionControlType = ControlTypesEnum.TEXT_INPUT;
                                    string questionControlTypeName = question.Questions[qsIndex].QuestionTypeName;
                                    if (!Enum.TryParse(questionControlTypeName, out questionControlType))
                                        throw new NotFoundAPIException($"Question ControlType '{questionControlTypeName}' is unknown!");

                                    TextFragment anstextFragment = GetFormatedValue(question.Questions[qsIndex].Answer, questionControlType);                                    
                                    valueCell.Paragraphs.Add(anstextFragment);
                                    qsIndex++;
                                }
                            }
                            break;
                        }
                    }
                }
            }

        }
        private static Cell AddHeaderCell(Row headerRow, string headerText, float fontSize = 8, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
        {
            TextFragment pageHeaderTextFragment = new TextFragment(headerText);
            pageHeaderTextFragment.TextState.FontSize = fontSize;
            pageHeaderTextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont, FontStyles.Bold);
            pageHeaderTextFragment.HorizontalAlignment = horizontalAlignment;
            Cell pageHeaderrCell = headerRow.Cells.Add();
            pageHeaderrCell.Margin = new MarginInfo();
            pageHeaderrCell.Margin.Left = 2;
            pageHeaderrCell.Margin.Top = 2;
            pageHeaderrCell.ColSpan = 6;
            pageHeaderrCell.BackgroundColor = Color.FromRgb(System.Drawing.Color.LightGray);
            pageHeaderTextFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
            pageHeaderrCell.Paragraphs.Add(pageHeaderTextFragment);
            return pageHeaderrCell;

        }
        private static TextFragment GetFormatedValue(string value, ControlTypesEnum ControlType)
        {
            string formattedValue = value == null ? "" : value;
            TextFragment anstextFragment = new TextFragment(formattedValue);
            anstextFragment.TextState.FontSize = 8;
            anstextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
            switch (ControlType)
            {
                case ControlTypesEnum.CHECKBOX_INPUT:
                    {
                        bool flag;
                        if (bool.TryParse(formattedValue, out flag))
                        {
                            if (flag == true)
                            {
                                formattedValue = "Y";
                            }
                            else
                                formattedValue = "N";
                        }
                        anstextFragment = new TextFragment(formattedValue);
                        anstextFragment.TextState.FontSize = 8;
                        anstextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                        return anstextFragment;
                    }
                case ControlTypesEnum.NUMBER_INPUT:
                case ControlTypesEnum.NUMBER_INPUT_CURRENCY:
                    {
                        decimal numValue;
                        if (decimal.TryParse(formattedValue, out numValue))
                        {
                            anstextFragment = new TextFragment(string.Format("{0:n0}", numValue));
                            anstextFragment.TextState.FontSize = 8;                            
                            anstextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                            anstextFragment.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        return anstextFragment;
                    }
                case ControlTypesEnum.NUMBER_INPUT_PERCENT:
                    {
                        decimal numValue;
                        if (decimal.TryParse(formattedValue, out numValue))
                        {
                            anstextFragment = new TextFragment(string.Format("{0:n}", numValue));
                            anstextFragment.TextState.FontSize = 8;                            
                            anstextFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                            anstextFragment.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        return anstextFragment;
                    }
                case ControlTypesEnum.TEXT_AREA_INPUT:
                case ControlTypesEnum.TEXT_INPUT:
                default:
                    return anstextFragment;
            }
        }
        private static string GenerateReportHeader(dynamic pricingAnalysis)
        {
            string submissionName = pricingAnalysis.GetType().GetProperty(ReportsSetting.ParentSubmissionName).GetValue(pricingAnalysis, null);
            DateTime? effectiveDate = pricingAnalysis.GetType().GetProperty(ReportsSetting.ParentEffectiveDate).GetValue(pricingAnalysis, null);
            string sourceUnderwritingSystemName = pricingAnalysis.GetType().GetProperty(ReportsSetting.ParentSourceUnderwritingSystemName).GetValue(pricingAnalysis, null);
            string sourceUnderwritingSystemNumber = pricingAnalysis.GetType().GetProperty(ReportsSetting.ParentSourceUnderwritingSystemNumber).GetValue(pricingAnalysis, null);
            string underwritingTeamName = pricingAnalysis.GetType().GetProperty(ReportsSetting.ParentUnderwritingTeamName).GetValue(pricingAnalysis, null);
            return string.Format("{0}(UW team: {1}, Inception date: {2}) {3}{4}", submissionName, underwritingTeamName, effectiveDate != null ? ((DateTime)effectiveDate).ToShortDateString() : "", sourceUnderwritingSystemName != null ? sourceUnderwritingSystemName : "", sourceUnderwritingSystemNumber != null ? ":" + sourceUnderwritingSystemNumber : "");
        }        
        private static void GenerateSummaryReport(Document doc, int cout, string reportHeader, ReportlTypesEnum reportType = ReportlTypesEnum.NonMintSummary)
        {
            Page pdfPage = doc.Pages.Add();
            pdfPage.PageInfo.Height = PageSize.A4.Height;
            pdfPage.PageInfo.Width = PageSize.A4.Width;
            Paragraphs pageHeaderParagraphs = pdfPage.Paragraphs;
            pdfPage.Header = GetPageHeader(reportHeader);
            Table mainTableSubmission = new Table();
            mainTableSubmission.ColumnWidths = ReportsSetting.NonMintSummarySubmissionMainColumnWidths;
            mainTableSubmission.Margin = new MarginInfo();
            mainTableSubmission.Margin.Bottom = 5;
            Row mainRowSubmission = mainTableSubmission.Rows.Add();
            Cell submissionCell = mainRowSubmission.Cells.Add();
            //submissionCell.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black)); ;
            submissionCell.VerticalAlignment = VerticalAlignment.Top;
            submissionCell.Margin = new MarginInfo(0, 0, 0, 0);
            #region Submission
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Table submissionHeaderTable = GetSubmissionTable(reportType);
                submissionCell.Paragraphs.Add(submissionHeaderTable);
            }
            #endregion                        

            #region TIV
            Cell TIVCell = mainRowSubmission.Cells.Add();
            //TIVCell.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black)); ;
            TIVCell.Margin = new MarginInfo();
            TIVCell.Margin.Right = 2;
            TIVCell.Margin.Left = 0;
            TIVCell.VerticalAlignment = VerticalAlignment.Top;
            Table TIVTable = GetTIVTable(reportType);
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                TIVTable.Margin = new MarginInfo();
                TIVTable.Margin.Bottom = 5;
                TIVCell.Paragraphs.Add(TIVTable);
            }
            else if (reportType == ReportlTypesEnum.NonMintSummary)
            {
                submissionCell.Paragraphs.Add(TIVTable);
            }
            //---------------------
            #endregion

            #region RMS_IMPACT
            Table RMS_IMPACTTable = GetRMSIMPACTTable(reportType);

            RMS_IMPACTTable.Margin = new MarginInfo();
            if (reportType == ReportlTypesEnum.MintSummary)
                RMS_IMPACTTable.Margin.Bottom = 5;
            TIVCell.Paragraphs.Add(RMS_IMPACTTable);

            #endregion

            #region NOTETable
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Table NOTETable = GetNoteTable();
                NOTETable.Margin = new MarginInfo();
                NOTETable.Margin.Bottom = 5;
                TIVCell.Paragraphs.Add(NOTETable);
            }
            #endregion

            #region sublimit
            Row mainRowSublimits = mainTableSubmission.Rows.Add();
            Cell sublimitsCell = mainRowSublimits.Cells.Add();
            sublimitsCell.Margin = new MarginInfo();
            sublimitsCell.Margin.Right = 2;
            sublimitsCell.ColSpan = 2;
            Table sublimitsTable = GetSublimitTable(reportType);
            sublimitsTable.Margin = new MarginInfo();
            sublimitsTable.Margin.Bottom = 5;
            sublimitsCell.Paragraphs.Add(sublimitsTable);
            //GetLosshistoryTable
            #endregion

            #region Loss History
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Row mainRowLossHistory = mainTableSubmission.Rows.Add();

                Cell LossHistoryCell = mainRowLossHistory.Cells.Add();
                //sublimitsCell.VerticalAlignment = VerticalAlignment.Top;
                LossHistoryCell.Margin = new MarginInfo();
                LossHistoryCell.Margin.Right = 2;
                LossHistoryCell.ColSpan = 2;
                Table LossHistoryTable = GetLosshistoryTable();
                LossHistoryTable.Margin = new MarginInfo();
                LossHistoryTable.Margin.Bottom = 5;
                LossHistoryCell.Paragraphs.Add(LossHistoryTable);
            }
            #endregion

            #region GetReturnPeriodTable
            Row mainRowReturnPeriod = mainTableSubmission.Rows.Add();

            Cell ReturnPeriodCell = mainRowReturnPeriod.Cells.Add();
            //sublimitsCell.VerticalAlignment = VerticalAlignment.Top;
            ReturnPeriodCell.Margin = new MarginInfo();
            ReturnPeriodCell.Margin.Right = 2;
            ReturnPeriodCell.ColSpan = 2;
            Table ReturnPeriodTable = GetReturnPeriodTable();
            ReturnPeriodTable.Margin = new MarginInfo();
            ReturnPeriodTable.Margin.Bottom = 5;
            ReturnPeriodCell.Paragraphs.Add(ReturnPeriodTable);
            #endregion

            #region Layers

            Row mainRowLayers = mainTableSubmission.Rows.Add();

            Cell layerCell = mainRowLayers.Cells.Add();
            //sublimitsCell.VerticalAlignment = VerticalAlignment.Top;
            layerCell.Margin = new MarginInfo();
            layerCell.Margin.Right = 2;
            layerCell.ColSpan = 2;
            Table layersodTable = GetLayerListSummary(3, reportType);
            layersodTable.Margin = new MarginInfo();
            layersodTable.Margin.Bottom = 5;
            layerCell.Paragraphs.Add(layersodTable);
            #endregion
            //layerCell.Paragraphs.Add(mainTableLayers);
            pageHeaderParagraphs.Add(mainTableSubmission);

        }
        private static Table GetSubmissionTable(ReportlTypesEnum reportType)
        {
            Table submissionHeaderTable = new Table();
            submissionHeaderTable.Margin = new MarginInfo(0, 0, 0, 0);
            submissionHeaderTable.Margin.Bottom = (reportType == ReportlTypesEnum.MintSummary) ? 10 : 5;
            submissionHeaderTable.ColumnWidths = ReportsSetting.SummarySubmissionColumnWidths;
            foreach (var submissionKey in ReportsSetting.ReportsSummarySubmissionConfiguration.Where(ss => ss.Value.reportTypes.Contains(reportType)).Select(ss => ss.Key))
            {

                Row layerSummaryRow = submissionHeaderTable.Rows.Add();
                layerSummaryRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                Config.PropertyConfig configOption = new Config.PropertyConfig();
                ReportsSetting.ReportsSummarySubmissionConfiguration.TryGetValue(submissionKey, out configOption);
                ControlTypesEnum controlType = ControlTypesEnum.TEXT_INPUT;
                TextFragment lableFragment = GetFormatedValue(configOption.Name, controlType);
                lableFragment.TextState.FontSize = 8;

                lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                Cell lableCell = layerSummaryRow.Cells.Add();
                lableCell.Margin = new MarginInfo();
                //lableCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                lableCell.Margin.Left = 2;
                lableCell.Margin.Bottom = 0.5f;
                lableCell.Paragraphs.Add(lableFragment);
                //lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                //lableCell.Border = new BorderInfo(BorderSide.Bottom, 0.2F, Color.FromRgb(System.Drawing.Color.White));

                Cell valueCell = layerSummaryRow.Cells.Add();
                TextFragment valueFragment = GetFormatedValue("", controlType);
                valueCell.Paragraphs.Add(valueFragment);
                valueCell.Border = new BorderInfo(BorderSide.Left, 0.2F, Color.FromRgb(System.Drawing.Color.Black));                
                layerSummaryRow.MinRowHeight = 10;

            }
            submissionHeaderTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return submissionHeaderTable;

        }
        private static Table GetTIVTable(ReportlTypesEnum reportType)
        {
            Table TIVTable = new Table();
            TIVTable.Margin = new MarginInfo();
            TIVTable.Margin.Left = 0;
            if(reportType==ReportlTypesEnum.MintSummary)
            TIVTable.Margin.Bottom = 4;
            TIVTable.ColumnWidths =ReportsSetting.RMSTableColumnWidths;
            #region Header
            Row TIVHederRow = TIVTable.Rows.Add();
            TIVHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TIVHederRow.FixedRowHeight = 10;


            Cell TIVHeader = TIVHederRow.Cells.Add();

            TextFragment TIVFragment = GetFormatedValue(ReportsSetting.TIVHeader, ControlTypesEnum.TEXT_INPUT);
            TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVHeader.Paragraphs.Add(TIVFragment);
            TIVHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVHeader.Alignment = HorizontalAlignment.Center;
            //TIVHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            Cell TIVORIGINALHeader = TIVHederRow.Cells.Add();
            TIVORIGINALHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVORIGINALFragment = GetFormatedValue(ReportsSetting.TIVORIGINALHeader, ControlTypesEnum.TEXT_INPUT);
            TIVORIGINALFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVORIGINALHeader.Paragraphs.Add(TIVORIGINALFragment);
            TIVORIGINALHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVORIGINALHeader.Alignment = HorizontalAlignment.Center;
            TIVORIGINALHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            Cell TIVUSDHeader = TIVHederRow.Cells.Add();
            TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVUSDFragment = GetFormatedValue(ReportsSetting.TIVUSDHeader, ControlTypesEnum.TEXT_INPUT);
            TIVUSDFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVUSDHeader.Paragraphs.Add(TIVUSDFragment);
            TIVUSDHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVUSDHeader.Alignment = HorizontalAlignment.Center;
            TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Cell PATCIPATIONHeader = TIVHederRow.Cells.Add();
                PATCIPATIONHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONFragment = GetFormatedValue(ReportsSetting.PATCIPATIONHeader, ControlTypesEnum.TEXT_INPUT);
                TIVPATCIPATIONFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                PATCIPATIONHeader.Paragraphs.Add(TIVPATCIPATIONFragment);
                PATCIPATIONHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                PATCIPATIONHeader.ColSpan = 2;
                PATCIPATIONHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                PATCIPATIONHeader.Alignment = HorizontalAlignment.Center;
            }
            #endregion

            #region Value
            Row TIVValueRow = TIVTable.Rows.Add();
            TIVValueRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TIVValueRow.FixedRowHeight = 10;
            Cell TIVLable = TIVValueRow.Cells.Add();
            //TIVLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVLAstYrLableFragment = GetFormatedValue(ReportsSetting.TIVLASTYRLABLE, ControlTypesEnum.TEXT_INPUT);
            //TIVLAstYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVLable.Paragraphs.Add(TIVLAstYrLableFragment);
            //TIVLable.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVLable.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            Cell TIVORIGINALLable = TIVValueRow.Cells.Add();
            TIVORIGINALLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVORIGINALLASTYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
            TIVORIGINALLable.Paragraphs.Add(TIVORIGINALLASTYrFragment);
            //TIVORIGINALLable.BackgroundColor = Color.FromArgb(119, 136, 153);

            Cell TIVUSDLable = TIVValueRow.Cells.Add();
            TIVUSDLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVUSDLAstYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
            TIVUSDLable.Paragraphs.Add(TIVUSDLAstYrFragment);
            //TIVUSDLable.BackgroundColor = Color.FromArgb(119, 136, 153);
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Cell PATCIPATIONLable = TIVValueRow.Cells.Add();
                PATCIPATIONLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONLAstYrLableFragment = GetFormatedValue(ReportsSetting.PATCIPATIONWTNEXPSLABLE, ControlTypesEnum.TEXT_INPUT);
                //TIVPATCIPATIONLAstYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                PATCIPATIONLable.Paragraphs.Add(TIVPATCIPATIONLAstYrLableFragment);
                //PATCIPATIONLable.BackgroundColor = Color.FromArgb(119, 136, 153);

                Cell PATCIPATIONValue = TIVValueRow.Cells.Add();
                PATCIPATIONValue.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONLAstYrValueFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                PATCIPATIONValue.Paragraphs.Add(TIVPATCIPATIONLAstYrValueFragment);
            }
            //------------------------------------
            Row TIVValueThisYrRow = TIVTable.Rows.Add();
            TIVValueThisYrRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            Cell TIVLableThisYr = TIVValueThisYrRow.Cells.Add();
            TextFragment TIVThisYrLableFragment = GetFormatedValue(ReportsSetting.TIVTHISYRLABLE, ControlTypesEnum.TEXT_INPUT);
            //TIVThisYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVLableThisYr.Paragraphs.Add(TIVThisYrLableFragment);
            //TIVLableThisYr.BackgroundColor = Color.FromArgb(119, 136, 153);

            Cell TIVORIGINALThisYrLable = TIVValueThisYrRow.Cells.Add();
            TIVORIGINALThisYrLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVORIGINALThisYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
            TIVORIGINALThisYrLable.Paragraphs.Add(TIVORIGINALThisYrFragment);
            //TIVORIGINALLable.BackgroundColor = Color.FromArgb(119, 136, 153);

            Cell TIVUSDThisYrLable = TIVValueThisYrRow.Cells.Add();
            TIVUSDThisYrLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVUSDThisYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
            TIVUSDLable.Paragraphs.Add(TIVUSDThisYrFragment);
            //TIVUSDLable.BackgroundColor = Color.FromArgb(119, 136, 153);
            if (reportType == ReportlTypesEnum.MintSummary)
            {
                Cell PATCIPATIONThisYrLable = TIVValueThisYrRow.Cells.Add();
                PATCIPATIONThisYrLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONThisYrLableFragment = GetFormatedValue(ReportsSetting.PATCIPATIONSIGNEXPSLABLE, ControlTypesEnum.TEXT_INPUT);
                //TIVPATCIPATIONThisYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                PATCIPATIONThisYrLable.Paragraphs.Add(TIVPATCIPATIONThisYrLableFragment);
                //PATCIPATIONThisYrLable.BackgroundColor = Color.FromArgb(119, 136, 153);

                Cell PATCIPATIONThisYrValue = TIVValueThisYrRow.Cells.Add();
                PATCIPATIONThisYrValue.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONThisYrValueFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                PATCIPATIONThisYrValue.Paragraphs.Add(TIVPATCIPATIONThisYrValueFragment);
                //TIVValueThisYrRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            }
            TIVValueThisYrRow.FixedRowHeight = 10;
            TIVTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            #endregion
            return TIVTable;

        }
        private static Table GetRMSIMPACTTable(ReportlTypesEnum reportType)
        {
            Table RMSTable = new Table();
            RMSTable.Margin = new MarginInfo();
            RMSTable.Margin.Left = 0;
            if (reportType == ReportlTypesEnum.MintSummary)
                RMSTable.Margin.Bottom = 4;
            RMSTable.ColumnWidths = ReportsSetting.RMSTableColumnWidths;
            #region Header
            Row RMSHederRow = RMSTable.Rows.Add();
            RMSHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            RMSHederRow.FixedRowHeight = 10;


            Cell RMSHeader = RMSHederRow.Cells.Add();
            TextFragment TIVFragment = GetFormatedValue(ReportsSetting.RMS_IMPACTHeader, ControlTypesEnum.TEXT_INPUT);
            TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            RMSHeader.Paragraphs.Add(TIVFragment);
            RMSHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            RMSHeader.Alignment = HorizontalAlignment.Center;
            //TIVHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            Cell RMS_IMPACTUSEQHeader = RMSHederRow.Cells.Add();
            RMS_IMPACTUSEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment RMS_IMPACTUSEQFragment = GetFormatedValue(ReportsSetting.RMS_IMPACTUSEQHeader, ControlTypesEnum.TEXT_INPUT);

            RMS_IMPACTUSEQHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            RMS_IMPACTUSEQHeader.Alignment = HorizontalAlignment.Center;
            RMS_IMPACTUSEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            RMS_IMPACTUSEQFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            RMS_IMPACTUSEQHeader.Paragraphs.Add(RMS_IMPACTUSEQFragment);


            Cell TIVUSDHeader = RMSHederRow.Cells.Add();
            TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVUSDFragment = GetFormatedValue(ReportsSetting.RMS_IMPACTNAWSHeader, ControlTypesEnum.TEXT_INPUT);
            TIVUSDFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVUSDHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVUSDHeader.Alignment = HorizontalAlignment.Center;
            TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TIVUSDHeader.Paragraphs.Add(TIVUSDFragment);

            Cell RMS_IMPACTJPEQHeader = RMSHederRow.Cells.Add();
            RMS_IMPACTJPEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment RMS_IMPACTJPEQHeaderFragment = GetFormatedValue(ReportsSetting.RMS_IMPACTJPEQHeader, ControlTypesEnum.TEXT_INPUT);
            RMS_IMPACTJPEQHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            RMS_IMPACTJPEQHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            RMS_IMPACTJPEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            RMS_IMPACTJPEQHeader.Alignment = HorizontalAlignment.Center;
            RMS_IMPACTJPEQHeader.Paragraphs.Add(RMS_IMPACTJPEQHeaderFragment);

            Cell RMS_IMPACTNZEQHeader = RMSHederRow.Cells.Add();
            RMS_IMPACTJPEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment RMS_IMPACTNZEQHeaderFragment = GetFormatedValue(ReportsSetting.RMS_IMPACTNZEQHeader, ControlTypesEnum.TEXT_INPUT);
            RMS_IMPACTNZEQHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            RMS_IMPACTNZEQHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            RMS_IMPACTNZEQHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            RMS_IMPACTNZEQHeader.Alignment = HorizontalAlignment.Center;
            RMS_IMPACTNZEQHeader.Paragraphs.Add(RMS_IMPACTNZEQHeaderFragment);
            #endregion

            #region Value
            {
                Row RMSValueRow = RMSTable.Rows.Add();
                RMSValueRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                RMSValueRow.FixedRowHeight = 10;
                Cell RMSLable = RMSValueRow.Cells.Add();
                //TIVLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment RMSLAstYrLableFragment = GetFormatedValue(ReportsSetting.TIVLASTYRLABLE, ControlTypesEnum.TEXT_INPUT);
                //RMSLAstYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                RMSLable.Paragraphs.Add(RMSLAstYrLableFragment);
                //RMSLable.BackgroundColor = Color.FromArgb(119, 136, 153);
                RMS_IMPACTUSEQHeader.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                for (int i = 0; i < 4; i++)
                {
                    Cell RMSUSEQLable = RMSValueRow.Cells.Add();
                    RMSUSEQLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment TIVORIGINALLASTYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    RMSUSEQLable.Paragraphs.Add(TIVORIGINALLASTYrFragment);

                }               

            }

            //------------------------------------
            {
                Row RMSValueThisYrRow = RMSTable.Rows.Add();
                RMSValueThisYrRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                Cell TIVLableThisYr = RMSValueThisYrRow.Cells.Add();
                TextFragment RMSThisYrLableFragment = GetFormatedValue(ReportsSetting.TIVTHISYRLABLE, ControlTypesEnum.TEXT_INPUT);
                
                TIVLableThisYr.Paragraphs.Add(RMSThisYrLableFragment);
                

                for (int i = 0; i < 4; i++)
                {
                    Cell RMSUSEQLable = RMSValueThisYrRow.Cells.Add();
                    RMSUSEQLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment TIVORIGINALLASTYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    RMSUSEQLable.Paragraphs.Add(TIVORIGINALLASTYrFragment);

                }
                RMSValueThisYrRow.FixedRowHeight = 10;
                RMSTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            }
            #endregion
            #region Charge %            
            {
                Row CHANGEPERLABLERow = RMSTable.Rows.Add();
                Cell RMSCHANGEPER = CHANGEPERLABLERow.Cells.Add();
                TextFragment CHANGEPERCFragment = GetFormatedValue(ReportsSetting.TIVCHANGEPERLABLE, ControlTypesEnum.TEXT_INPUT);
                
                RMSCHANGEPER.Paragraphs.Add(CHANGEPERCFragment);
                

                for (int i = 0; i < 4; i++)
                {
                    Cell RMSUSEQLable = CHANGEPERLABLERow.Cells.Add();
                    RMSUSEQLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment TIVORIGINALLASTYrFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    RMSUSEQLable.Paragraphs.Add(TIVORIGINALLASTYrFragment);

                }

                CHANGEPERLABLERow.FixedRowHeight = 10;
                RMSTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            }
            #endregion
            return RMSTable;

        }
        private static Table GetNoteTable(int columnWidths=276,int rowHeight=100)
        {
            Table NoteTable = new Table();
            NoteTable.Margin = new MarginInfo();
            NoteTable.Margin.Left = 0;
            NoteTable.Margin.Bottom = 4;
            NoteTable.ColumnWidths = columnWidths.ToString();
            #region Header
            Row TIVHederRow = NoteTable.Rows.Add();
            TIVHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TIVHederRow.FixedRowHeight = 10;

            Cell TIVHeader = TIVHederRow.Cells.Add();

            TextFragment TIVFragment = GetFormatedValue(ReportsSetting.NOTEHeader, ControlTypesEnum.TEXT_INPUT);
            TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            TIVHeader.Paragraphs.Add(TIVFragment);
            TIVHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            TIVHeader.Alignment = HorizontalAlignment.Center;
            //TIVHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));


            #endregion

            #region Value
            Row TIVValueRow = NoteTable.Rows.Add();
            TIVValueRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TIVValueRow.FixedRowHeight = rowHeight;
            Cell TIVLable = TIVValueRow.Cells.Add();
            //TIVLable.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment TIVLAstYrLableFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
            TIVLAstYrLableFragment.Margin = new MarginInfo();
            TIVLAstYrLableFragment.Margin.Left = 2;
            TIVLAstYrLableFragment.Margin.Top = 2;
            TIVLable.VerticalAlignment = VerticalAlignment.Top;
            TIVLAstYrLableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
            TIVLable.Paragraphs.Add(TIVLAstYrLableFragment);

            NoteTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            #endregion
            return NoteTable;

        }
        private static Table GetSublimitTable(ReportlTypesEnum reportType)
        {
            Table SublimitTable = new Table();
            SublimitTable.Margin = new MarginInfo();
            SublimitTable.Margin.Left = 0;
            SublimitTable.Margin.Bottom = 4;
            SublimitTable.ColumnWidths = ReportsSetting.LosshistoryTableColumnWidths;
            #region Header
            {
                Row SublimitHederRow = SublimitTable.Rows.Add();
                SublimitHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitHederRow.FixedRowHeight = 10;

                Cell TIVHeader = SublimitHederRow.Cells.Add();
                TextFragment TIVFragment = GetFormatedValue(ReportsSetting.SUBLIMITSHeader, ControlTypesEnum.TEXT_INPUT);
                TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                TIVHeader.Paragraphs.Add(TIVFragment);
                TIVHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                TIVHeader.Alignment = HorizontalAlignment.Center;

                Cell TIVORIGINALHeader = SublimitHederRow.Cells.Add();
                TIVORIGINALHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVORIGINALFragment = GetFormatedValue(ReportsSetting.SUBLIMITS1CA_EQ, ControlTypesEnum.TEXT_INPUT);
                TIVORIGINALFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                TIVORIGINALHeader.Paragraphs.Add(TIVORIGINALFragment);
                TIVORIGINALHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                TIVORIGINALHeader.Alignment = HorizontalAlignment.Center;

                Cell TIVUSDHeader = SublimitHederRow.Cells.Add();
                TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVUSDFragment = GetFormatedValue(ReportsSetting.SUBLIMITS2JP_EQ, ControlTypesEnum.TEXT_INPUT);
                TIVUSDFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                TIVUSDHeader.Paragraphs.Add(TIVUSDFragment);
                TIVUSDHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                TIVUSDHeader.Alignment = HorizontalAlignment.Center;
                TIVUSDHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

                Cell PATCIPATIONHeader = SublimitHederRow.Cells.Add();
                PATCIPATIONHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment TIVPATCIPATIONFragment = GetFormatedValue(ReportsSetting.SUBLIMITS3EQ, ControlTypesEnum.TEXT_INPUT);
                TIVPATCIPATIONFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                PATCIPATIONHeader.Paragraphs.Add(TIVPATCIPATIONFragment);
                PATCIPATIONHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                //PATCIPATIONHeader.ColSpan = 2;
                PATCIPATIONHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                PATCIPATIONHeader.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS4 = SublimitHederRow.Cells.Add();
                SUBLIMITS4.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS4Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS4WS, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS4Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS4.Paragraphs.Add(SUBLIMITS4Fragment);
                SUBLIMITS4.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS4.ColSpan = 2;
                SUBLIMITS4.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS4.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS5 = SublimitHederRow.Cells.Add();
                SUBLIMITS5.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS5FLFragment = GetFormatedValue(ReportsSetting.SUBLIMITS5FL, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS5FLFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS5.Paragraphs.Add(SUBLIMITS5FLFragment);
                SUBLIMITS5.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS5.ColSpan = 2;
                SUBLIMITS5.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS5.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS6 = SublimitHederRow.Cells.Add();
                SUBLIMITS6.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS6Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS6N_MD, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS6Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS6.Paragraphs.Add(SUBLIMITS6Fragment);
                SUBLIMITS6.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS6.ColSpan = 2;
                SUBLIMITS6.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS6.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS7 = SublimitHederRow.Cells.Add();
                PATCIPATIONHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS7Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS7CR_FL, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS7Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS7.Paragraphs.Add(SUBLIMITS7Fragment);
                SUBLIMITS7.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS7.ColSpan = 2;
                SUBLIMITS7.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS7.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS8 = SublimitHederRow.Cells.Add();
                SUBLIMITS8.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS8Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS8N_CBI, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS8Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS8.Paragraphs.Add(SUBLIMITS8Fragment);
                SUBLIMITS8.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS8.ColSpan = 2;
                SUBLIMITS8.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS8.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS9 = SublimitHederRow.Cells.Add();
                SUBLIMITS9.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS9Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS9UN_CBI, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS9Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS9.Paragraphs.Add(SUBLIMITS9Fragment);
                SUBLIMITS9.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS9.ColSpan = 2;
                SUBLIMITS9.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS9.Alignment = HorizontalAlignment.Center;

                Cell SUBLIMITS10 = SublimitHederRow.Cells.Add();
                SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment SUBLIMITS10Fragment = GetFormatedValue(ReportsSetting.SUBLIMITS10OTHER, ControlTypesEnum.TEXT_INPUT);
                SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                SUBLIMITS10.BackgroundColor = Color.FromArgb(119, 136, 153);
                //SUBLIMITS10.ColSpan = 2;
                SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SUBLIMITS10.Alignment = HorizontalAlignment.Center;
            }
            #endregion
            Row sublimitRow = SublimitTable.Rows.Add();
            #region SUBLIMITSLABLE Value          
            {                
                //SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                sublimitRow.FixedRowHeight = 10;

                Cell SublimitHeader = sublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.SUBLIMITSLABLE, ControlTypesEnum.TEXT_INPUT);
                //SublimitFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                //SublimitHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                SublimitHeader.Alignment = HorizontalAlignment.Center;

                for (int i = 1; i <= 10; i++)
                {
                    Cell SUBLIMITS1 = sublimitRow.Cells.Add();
                    SUBLIMITS1.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS1Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS1Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS1.Paragraphs.Add(SUBLIMITS1Fragment);
                    //SUBLIMITS1.BackgroundColor = Color.FromArgb(119, 136, 153);
                    SUBLIMITS1.Alignment = HorizontalAlignment.Center;
                }
            }
            #endregion          
            #region DEDUCTIBLELABLE Value     
            if (reportType == ReportlTypesEnum.NonMintSummary)
            {
                sublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                Row deductibleRow = SublimitTable.Rows.Add();
                deductibleRow.FixedRowHeight = 10;
                Cell SublimitHeader = deductibleRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.DEDUCTIBLELABLE, ControlTypesEnum.TEXT_INPUT);
                //SublimitFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                //SublimitHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                SublimitHeader.Alignment = HorizontalAlignment.Center;

                for (int i = 1; i <= 10; i++)
                {
                    Cell SUBLIMITS1 = deductibleRow.Cells.Add();
                    SUBLIMITS1.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS1Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS1Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS1.Paragraphs.Add(SUBLIMITS1Fragment);
                    //SUBLIMITS1.BackgroundColor = Color.FromArgb(119, 136, 153);
                    SUBLIMITS1.Alignment = HorizontalAlignment.Center;
                }

            }
            #endregion
            SublimitTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return SublimitTable;

        }
        private static Table GetLosshistoryTable()
        {
            Table SublimitTable = new Table();
            SublimitTable.Margin = new MarginInfo();
            SublimitTable.Margin.Left = 0;
            SublimitTable.Margin.Bottom = 4;
            SublimitTable.ColumnWidths = ReportsSetting.LosshistoryTableColumnWidths;
            var lastTenYears = Enumerable.Range(0, 10).Select(i => DateTime.Now.AddYears(i - 10)).Select(date => date.Year.ToString()).ToList();

            #region Header
            {
                Row SublimitHederRow = SublimitTable.Rows.Add();

                SublimitHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                Cell TIVHeader = SublimitHederRow.Cells.Add();
                SublimitHederRow.FixedRowHeight = 15;

                TextFragment TIVFragment = GetFormatedValue(ReportsSetting.LOSSHISTORYHeader, ControlTypesEnum.TEXT_INPUT);
                TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                TIVHeader.Paragraphs.Add(TIVFragment);
                TIVHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                TIVHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in lastTenYears)
                {
                    Cell TIVORIGINALHeader = SublimitHederRow.Cells.Add();
                    TIVORIGINALHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment TIVORIGINALFragment = GetFormatedValue(year, ControlTypesEnum.TEXT_INPUT);
                    TIVORIGINALFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    TIVORIGINALHeader.Paragraphs.Add(TIVORIGINALFragment);
                    TIVORIGINALHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                    TIVORIGINALHeader.Alignment = HorizontalAlignment.Center;
                }

            }
            #endregion

            #region Value          
            {
                Row SublimitRow = SublimitTable.Rows.Add();
                SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;
                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.LOSSHISTORYGROSSLABLE, ControlTypesEnum.TEXT_INPUT);
                SublimitFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                //SublimitHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in lastTenYears)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }

            {
                Row SublimitRow = SublimitTable.Rows.Add();
                //SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;

                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.LOSSHISTORYNETLABLE, ControlTypesEnum.TEXT_INPUT);
                SublimitFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                //SublimitHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in lastTenYears)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }
            #endregion          
            SublimitTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return SublimitTable;

        }
        private static Table GetReturnPeriodTable()
        {
            Table SublimitTable = new Table();
            SublimitTable.Margin = new MarginInfo();
            SublimitTable.Margin.Left = 0;
            SublimitTable.Margin.Bottom = 4;
            SublimitTable.ColumnWidths = "60 49 49 49 50 50 50 50 50 50 50";
            var lastTenYears = Enumerable.Range(0, 10).Select(i => DateTime.Now.AddYears(i - 10)).Select(date => date.Year.ToString()).ToList();

            #region Header
            {
                Row SublimitHederRow = SublimitTable.Rows.Add();

                SublimitHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                Cell TIVHeader = SublimitHederRow.Cells.Add();
                SublimitHederRow.FixedRowHeight = 20;

                TextFragment TIVFragment = GetFormatedValue(ReportsSetting.RETURNPERIOD, ControlTypesEnum.TEXT_INPUT);
                TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                TIVHeader.Paragraphs.Add(TIVFragment);
                TIVHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                TIVHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in ReportsSetting.RETURNPERIODHeders)
                {
                    Cell TIVORIGINALHeader = SublimitHederRow.Cells.Add();
                    TIVORIGINALHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment TIVORIGINALFragment = GetFormatedValue(year, ControlTypesEnum.TEXT_INPUT);
                    TIVORIGINALFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    TIVORIGINALHeader.Paragraphs.Add(TIVORIGINALFragment);
                    TIVORIGINALHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
                    TIVORIGINALHeader.Alignment = HorizontalAlignment.Center;
                }

            }
            #endregion

            #region Value          
            {
                Row SublimitRow = SublimitTable.Rows.Add();
                SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;
                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.RETURNPERIODLable1, ControlTypesEnum.TEXT_INPUT);
                SublimitHeader.Paragraphs.Add(SublimitFragment);                
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in ReportsSetting.RETURNPERIODHeders)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);                    
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }

            {
                Row SublimitRow = SublimitTable.Rows.Add();
                SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;

                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.RETURNPERIODLable2, ControlTypesEnum.TEXT_INPUT);
                
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in ReportsSetting.RETURNPERIODHeders)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }

            {
                Row SublimitRow = SublimitTable.Rows.Add();
                SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;

                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.RETURNPERIODLable3, ControlTypesEnum.TEXT_INPUT);
                
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in ReportsSetting.RETURNPERIODHeders)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    //SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    //SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }

            {
                Row SublimitRow = SublimitTable.Rows.Add();
                //SublimitRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                SublimitRow.FixedRowHeight = 10;

                Cell SublimitHeader = SublimitRow.Cells.Add();
                TextFragment SublimitFragment = GetFormatedValue(ReportsSetting.RETURNPERIODLable4, ControlTypesEnum.TEXT_INPUT);
                
                SublimitHeader.Paragraphs.Add(SublimitFragment);
                
                SublimitHeader.Alignment = HorizontalAlignment.Center;
                foreach (var year in ReportsSetting.RETURNPERIODHeders)
                {
                    Cell SUBLIMITS10 = SublimitRow.Cells.Add();
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    TextFragment SUBLIMITS10Fragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    SUBLIMITS10Fragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    SUBLIMITS10.Paragraphs.Add(SUBLIMITS10Fragment);
                    SUBLIMITS10.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    SUBLIMITS10.Alignment = HorizontalAlignment.Center;
                }
            }
            #endregion          
            SublimitTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return SublimitTable;

        }
        private static Table GetLayerListSummary(int layerCount, ReportlTypesEnum reportType)
        {
            if (reportType == ReportlTypesEnum.MintSummary)
                return GetMintLayerListSummary(layerCount, reportType);
            else
                return GetNonMintLayerListSummary(layerCount, reportType);
        }
        private static Table GetMintLayerListSummary(int layerCount, ReportlTypesEnum reportType)
        {
            #region Layers List            
            Table layersTable = new Table();
            layersTable.ColumnWidths = ReportsSetting.MintLayerListSummaryColumnWidths;            
            layersTable.Margin = new MarginInfo();
            layersTable.Margin.Left = 2;
            layersTable.Margin.Bottom = 6;
            Dictionary<string, Row> layerRowKyes = new Dictionary<string, Row>();
            {
                Row layerSummaryHeaderRow = layersTable.Rows.Add();
                layerSummaryHeaderRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment lableHeaderFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                lableHeaderFragment.TextState.FontSize = 8;
                lableHeaderFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                Cell lableHeaderCell = layerSummaryHeaderRow.Cells.Add();
                lableHeaderCell.Margin = new MarginInfo();
                lableHeaderCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                lableHeaderCell.Margin.Left = 2;
                lableHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                lableHeaderCell.Alignment = HorizontalAlignment.Center;
                lableHeaderCell.Paragraphs.Add(lableHeaderFragment);
                //lableHeaderCell.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                layerSummaryHeaderRow.MinRowHeight = 10;

                layerRowKyes.Add(ReportsSetting.LAYERTABLEHeaderKey, layerSummaryHeaderRow);
                foreach (var item in ReportsSetting.ReportsSummaryLayerConfiguration.Where(ss => ss.Value.reportTypes.Contains(reportType)).Select(ss => ss.Key))
                {
                    Row layerSummaryRow = layersTable.Rows.Add();
                    layerSummaryRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    Config.PropertyConfig configOption = new Config.PropertyConfig();
                    ReportsSetting.ReportsSummaryLayerConfiguration.TryGetValue(item, out configOption);
                    TextFragment lableFragment = GetFormatedValue(configOption.Name, ControlTypesEnum.TEXT_INPUT);
                    lableFragment.TextState.FontSize = 8;
                    lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                    Cell lableCell = layerSummaryRow.Cells.Add();
                    lableCell.Margin = new MarginInfo();
                    lableCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                    lableCell.Margin.Left = 2;
                    lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    //lableCell.Alignment = HorizontalAlignment.Center;
                    lableCell.Paragraphs.Add(lableFragment);
                    lableCell.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    layerSummaryRow.MinRowHeight = 10;
                    layerRowKyes.Add(item, layerSummaryRow);

                }
            }
            #region Comment
            for (int i = 1; i <= layerCount; i++)
            {
                foreach (var item in layerRowKyes.Keys)
                {
                    Row layerSummaryRow;
                    layerRowKyes.TryGetValue(item, out layerSummaryRow);
                    if (layerSummaryRow != null)
                    {
                        Cell lableCell = layerSummaryRow.Cells.Add();
                        if (item == ReportsSetting.LAYERTABLEHeaderKey)
                        {
                            TextFragment lableFragment = GetFormatedValue(i.ToString(), ControlTypesEnum.TEXT_INPUT);
                            lableFragment.TextState.FontSize = 8;
                            lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                            lableCell.Margin = new MarginInfo();
                            lableCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                            lableCell.Alignment = HorizontalAlignment.Center;
                            lableCell.Margin.Left = 2;
                            lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                            lableCell.Paragraphs.Add(lableFragment);
                        }
                        else
                        {
                            layerRowKyes.TryGetValue(item, out layerSummaryRow);
                            Config.PropertyConfig configOption = new Config.PropertyConfig();
                            ReportsSetting.ReportsSummaryLayerConfiguration.TryGetValue(item, out configOption);
                            TextFragment lableFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                            lableFragment.TextState.FontSize = 8;
                            lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                            lableCell.Paragraphs.Add(lableFragment);
                        }
                        lableCell.Margin = new MarginInfo();
                        lableCell.Margin.Left = 2;
                        lableCell.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                        layerSummaryRow.MinRowHeight = 10;
                    }
                }
            }
            layersTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return layersTable;

            #endregion Comment
            #endregion
        }
        private static Table GetNonMintLayerListSummary(int layerCount, ReportlTypesEnum reportType)
        {
            #region Layers List            
            Table layersTable = new Table();
            layersTable.ColumnWidths =ReportsSetting.NonMintLayerListSummaryColumnWidths;
            layersTable.Margin = new MarginInfo();
            layersTable.Margin.Left = 2;
            layersTable.Margin.Bottom = 6;
            int coverageCount = 5;
            for(int layerIndex=1; layerIndex <=layerCount; layerIndex++)
            {
                Row layerMainRow = layersTable.Rows.Add();
                Cell layerDetailCell = layerMainRow.Cells.Add();
                Table layersDetailTable = new Table();
                layersDetailTable.Margin = new MarginInfo();
                layersDetailTable.Margin.Bottom = 2;
                layersDetailTable.ColumnWidths = "85 105 85 100 85 100";
                {
                    TextFragment layerHeaderFragment = GetFormatedValue("Layer_" + layerIndex.ToString(), ControlTypesEnum.TEXT_INPUT);
                    layerHeaderFragment.TextState.FontSize = 8;
                    layerHeaderFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                    Row layerHeaderRow = layersDetailTable.Rows.Add();
                    Cell layerHeaderCell = layerHeaderRow.Cells.Add();
                    layerHeaderCell.Margin = new MarginInfo();
                    layerHeaderCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                    layerHeaderCell.Alignment = HorizontalAlignment.Center;
                    layerHeaderCell.Margin.Left = 2;
                    layerHeaderCell.ColSpan = 6;
                    layerHeaderRow.MinRowHeight = 12;
                    layerHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                    layerHeaderCell.Paragraphs.Add(layerHeaderFragment);
                    layerHeaderRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                }
                int propertyIndex = 0;
                Row layerSummaryRow = layersDetailTable.Rows.Add();
                foreach (var item in ReportsSetting.ReportsSummaryLayerConfiguration.Where(ss => ss.Value.reportTypes.Contains(reportType)).Select(ss => ss.Key))
                {
                    layerSummaryRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    if ((layersDetailTable.Rows.Count -1)== propertyIndex/3)
                    {
                        layerSummaryRow = layersDetailTable.Rows.Add();                        
                    }
                    layerSummaryRow.DefaultCellBorder = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    layerSummaryRow.MinRowHeight = 10;
                    Config.PropertyConfig lableconfigOption = new Config.PropertyConfig();
                    ReportsSetting.ReportsSummaryLayerConfiguration.TryGetValue(item, out lableconfigOption);                                     
                    TextFragment lableFragment = GetFormatedValue(lableconfigOption.Name, ControlTypesEnum.TEXT_INPUT);
                    lableFragment.TextState.FontSize = 8;
                    lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                    Cell lableLableCell = layerSummaryRow.Cells.Add();
                    if (layerSummaryRow.Cells.Count == 1)
                       lableLableCell.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                    lableLableCell.Margin = new MarginInfo();
                    //lableLableCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                    lableLableCell.Margin.Left = 2;
                    lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);                    
                    lableLableCell.Paragraphs.Add(lableFragment);                    
                    Cell lableCell = layerSummaryRow.Cells.Add();
                    Config.PropertyConfig configOption = new Config.PropertyConfig();
                    ReportsSetting.ReportsSummaryLayerConfiguration.TryGetValue(item, out configOption);
                    TextFragment layerFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                    lableFragment.TextState.FontSize = 8;
                    lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                    lableCell.Paragraphs.Add(layerFragment);
                    lableCell.Margin = new MarginInfo();
                    lableCell.Margin.Left = 2;                    
                    propertyIndex++;
                }
                layersDetailTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                layerDetailCell.Paragraphs.Add(layersDetailTable);

                Dictionary<string, Row> layerCoveragesRowKyes = new Dictionary<string, Row>();
                Row layerMainCoverageRow = layersTable.Rows.Add();

                Cell layerCoverageCell = layerMainCoverageRow.Cells.Add();
                layerCoverageCell.VerticalAlignment = VerticalAlignment.Top;
                Table layersCoverageTable = new Table();
                layersCoverageTable.ColumnWidths = "85 95 95 95 95 95";
                layersCoverageTable.Margin = new MarginInfo();
                layersCoverageTable.Margin.Bottom = 2;
                TextFragment coverageHeaderFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                coverageHeaderFragment.TextState.FontSize = 8;
                coverageHeaderFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                for (int i = 0; i <= coverageCount; i++)
                {
                    Row coverageSummaryRow = layersCoverageTable.Rows.Add();
                    coverageSummaryRow.MinRowHeight = 10;
                    foreach (var item in ReportsSetting.SymmaryReportLayerCoverageConfiguration.Keys)
                    {
                        Cell coverageCell = coverageSummaryRow.Cells.Add();
                        TextFragment lableFragment;
                        Config.PropertyConfig configOption = new Config.PropertyConfig();
                        ReportsSetting.SymmaryReportLayerCoverageConfiguration.TryGetValue(item, out configOption);
                        if (layersCoverageTable.Rows.Count == 1 || coverageSummaryRow.Cells.Count == 1)
                        {
                            if (layersCoverageTable.Rows.Count == 1)
                            {
                                lableFragment = GetFormatedValue(configOption.Name, ControlTypesEnum.TEXT_INPUT);
                                coverageSummaryRow.MinRowHeight = 12;
                                coverageSummaryRow.BackgroundColor = Color.FromArgb(119, 136, 153);
                                lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
                            }
                            else
                            {
                                lableFragment = GetFormatedValue("Coverage_" + i.ToString(), ControlTypesEnum.TEXT_INPUT);
                                lableFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
                            }
                            //coverageCell.BackgroundColor = Color.FromArgb(119, 136, 153);
                        }
                        else
                        {
                            lableFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);                            
                        }
                        lableFragment.TextState.FontSize = 8;
                        lableFragment.TextState.Font = FontRepository.FindFont(ReportsSetting.TableFont);
                        coverageCell.Margin = new MarginInfo();
                        coverageCell.Margin.Left = 2;                        
                        if (coverageSummaryRow.Cells.Count != 1)
                            coverageCell.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                        coverageCell.Paragraphs.Add(lableFragment);
                    }
                    if (layersCoverageTable.Rows.Count <= coverageCount)
                        coverageSummaryRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

                }
                layersCoverageTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                layerCoverageCell.Paragraphs.Add(layersCoverageTable);
                Row layerMainNoteRow = layersTable.Rows.Add();
                Cell layerNoteCell = layerMainNoteRow.Cells.Add();
                layerNoteCell.Paragraphs.Add(GetNoteTable(562,35));
                #region Comment                           
            }
            //layersTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            return layersTable;

            #endregion Comment
            #endregion
        }
        private static Table GetBlankTable(int rowCount,int width)
        {
            if (rowCount > 76)
                rowCount = 76;
            Table emptyTable = new Table();
            emptyTable.Margin = new MarginInfo();
            emptyTable.Margin.Left = 0;
            emptyTable.ColumnWidths = width.ToString();
            for (int i = 0; i < rowCount; i++)
            {
                Row tableHederRow = emptyTable.Rows.Add();
                tableHederRow.FixedRowHeight = 10;
                tableHederRow.Cells.Add();
            }           
            
            return emptyTable; 
        }
        #region Exposure Summary
        private static Table GetExposureSummaryTable(string tableHeader,string columnHeader,int count/*count need to replace with object list once the data is available*/)
        {
            Table CountryTable = new Table();
            CountryTable.Margin = new MarginInfo();
            CountryTable.Margin.Left = 0;
            CountryTable.ColumnWidths = ReportsSetting.ExposureSummaryTableColumnWidths;
            #region Table Header
            Row tableHederRow = CountryTable.Rows.Add();
            tableHederRow.DefaultCellBorder= new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            tableHederRow.Border = new BorderInfo(BorderSide.Bottom, 1F, Color.FromRgb(System.Drawing.Color.Black));
            tableHederRow.FixedRowHeight = 12;
            Cell TableHederRowCell = tableHederRow.Cells.Add();
            TableHederRowCell.ColSpan = 3;
            TextFragment TIVFragment = GetFormatedValue(tableHeader, ControlTypesEnum.TEXT_INPUT);
            TIVFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.Black);
            TableHederRowCell.Paragraphs.Add(TIVFragment);
            TableHederRowCell.BackgroundColor = Color.FromArgb(219, 225, 231);
            TableHederRowCell.Alignment = HorizontalAlignment.Left;
            TableHederRowCell.VerticalAlignment = VerticalAlignment.Center;
            #endregion
            #region Coulmn Header
            Row columnHederRow = CountryTable.Rows.Add();
            columnHederRow.FixedRowHeight = 12;
            columnHederRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            Cell tivCountryHeader = columnHederRow.Cells.Add();            
            TextFragment tivCountryHeaderFragment = GetFormatedValue(columnHeader, ControlTypesEnum.TEXT_INPUT);
            tivCountryHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            tivCountryHeader.Paragraphs.Add(tivCountryHeaderFragment);
            tivCountryHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            tivCountryHeader.Alignment = HorizontalAlignment.Center;            

            Cell tivColumnHeader = columnHederRow.Cells.Add();
            tivColumnHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment tivHeaderFragment = GetFormatedValue(ReportsSetting.TIVHeader, ControlTypesEnum.TEXT_INPUT);
            tivHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            tivColumnHeader.Paragraphs.Add(tivHeaderFragment);
            tivColumnHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            tivColumnHeader.Alignment = HorizontalAlignment.Center;
            tivColumnHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            Cell tivPerHeader = columnHederRow.Cells.Add();
            tivPerHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            TextFragment tivPerHeaderFragment = GetFormatedValue(ReportsSetting.TIVPerHeader, ControlTypesEnum.TEXT_INPUT);
            tivPerHeaderFragment.TextState.ForegroundColor = Color.FromRgb(System.Drawing.Color.White);
            tivPerHeader.Paragraphs.Add(tivPerHeaderFragment);
            tivPerHeader.BackgroundColor = Color.FromArgb(119, 136, 153);
            tivPerHeader.Alignment = HorizontalAlignment.Center;
            tivPerHeader.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
            #endregion

            #region Value
            for (int i = 0; i < count; i++)
            {
                Row exposureSummaryCountryValueRow = CountryTable.Rows.Add();
                exposureSummaryCountryValueRow.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                exposureSummaryCountryValueRow.FixedRowHeight = 10;

                Cell tivCountry = exposureSummaryCountryValueRow.Cells.Add();
                TextFragment tivCountryFragment = GetFormatedValue("", ControlTypesEnum.TEXT_INPUT);
                tivCountry.Paragraphs.Add(tivCountryFragment);
                tivCountry.Border = new BorderInfo(BorderSide.Bottom, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

                Cell tiv = exposureSummaryCountryValueRow.Cells.Add();
                tiv.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment tivFragment = GetFormatedValue("", ControlTypesEnum.NUMBER_INPUT);
                tiv.Paragraphs.Add(tivFragment);


                Cell tivPer = exposureSummaryCountryValueRow.Cells.Add();
                tivPer.Border = new BorderInfo(BorderSide.Left, 0.5F, Color.FromRgb(System.Drawing.Color.Black));
                TextFragment tivPerFragment = GetFormatedValue(i.ToString(), ControlTypesEnum.NUMBER_INPUT_PERCENT);
                tivPer.Paragraphs.Add(tivPerFragment);
                CountryTable.Border = new BorderInfo(BorderSide.Box, 0.5F, Color.FromRgb(System.Drawing.Color.Black));

            }
            #endregion
            return CountryTable;

        }
        #endregion
    }
}
