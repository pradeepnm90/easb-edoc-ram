using Markel.Pricing.Service.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
   internal static class ReportsSetting
    {
        public const string LayerColumnWidths= "100 100 100 100 100 100";
        public const string SummaryLayer1ColumnWidths = "100";
        public const string SummaryLayer2ColumnWidths = "80 100";
        public const string SummaryLayerMainColumnWidths = "200 200 200";
        public const string MintSummarySubmissionMainColumnWidths = "270 310";
        public const string NonMintLayerListSummaryColumnWidths ="570";
        public const string MintLayerListSummaryColumnWidths = "80 100 100 100 100 100";
        public const string LosshistoryTableColumnWidths = "60 49 49 49 50 50 50 50 50 50 50";
        public const string RMSTableColumnWidths = "40 59 59 59 59";
        public const string ExposureSummaryMainColumnWidths = "280 280";
        public const string NonMintSummarySubmissionMainColumnWidths = "260 300";
        public const string SummarySubmissionColumnWidths = "80 185";
        public const int PageMarginLeft = 20;
        public const int PageMarginTop = 25;
        public const int PageMarginRight = 20;
        public const int PageMarginBottom = 25;

        public const string PropNameQuestionnaire = "Questionnaire";
        public const string PropNameQuestionnaireAnswers = "Answers";
        public const string TableFont = "TimesNewRoman";
        public const string LayerDetailsHeader = "Layer Details";
        public const string PropNameParent = "Parent";
        public const string ParentSubmissionName = "SubmissionName";

        public const string TIVHeader = "TIV";
        public const string TIVORIGINALHeader = "ORIGINAL";
        public const string TIVUSDHeader = "USD";

        public const string TIVLASTYRLABLE= "LAST YR";
        public const string TIVTHISYRLABLE = "THIS YR";
        public const string TIVCHANGEPERLABLE = "CHANGE %";

        public const string PATCIPATIONHeader = "PATCIPATION IN USD";
        public const string PATCIPATIONWTNEXPSLABLE = "WTN EXPS";
        public const string PATCIPATIONSIGNEXPSLABLE="SIGN EXPS";
        public const string PATCIPATIONNETINCOMEDLABLE = "NET INCOMED";

        public const string RMS_IMPACTHeader = "RMS_IMPACT";
        public const string RMS_IMPACTUSEQHeader = "USEQ";
        public const string RMS_IMPACTNAWSHeader = "NAWS";
        public const string RMS_IMPACTJPEQHeader = "JPEQ";
        public const string RMS_IMPACTNZEQHeader = "NZEQ";

        public const string RMS_IMPACTLASTYRLABLE = "LAST YR";
        public const string RMS_IMPACTTHISYRLABLE = "THIS YR";
        public const string RMS_IMPACTCHANGEPERLABLE = "CHANGE %";

        public const string NOTEHeader = "NOTES";
        public const string SUBLIMITSHeader = "";
        public const string SUBLIMITSLABLE = "SUBLIMITS";
        public const string DEDUCTIBLELABLE = "DEDUCTIBLES";
        public const string SUBLIMITS1CA_EQ = "CA_EQ";
        public const string SUBLIMITS2JP_EQ = "JP_EQ";
        public const string SUBLIMITS3EQ = "EQ";
        public const string SUBLIMITS4WS = "WS";
        public const string SUBLIMITS5FL = "FL";
        public const string SUBLIMITS6N_MD = "N_MD";
        public const string SUBLIMITS7CR_FL = "CR_FL";
        public const string SUBLIMITS8N_CBI = "N_CBI";
        public const string SUBLIMITS9UN_CBI = "UN_CBI";
        public const string SUBLIMITS10OTHER = "OTHER";

               
        public const string LOSSHISTORYHeader = "LOSS HISTORY";
        public const string LOSSHISTORYGROSSLABLE = "GROSS";
        public const string LOSSHISTORYNETLABLE = "NET";

        public const string RETURNPERIOD = "RETURN PERIOD";
        public static List<string> RETURNPERIODHeders = new List<string>(){ "1 in 2","1 in 5",   "1 in 10",  "1 in 25",  "1 in 50",  "1 in 100"  ,"1 in 250",    "1 in 500",   "1 in 1000 "  ,"1 in 10000"};
        public const string RETURNPERIODLable1 = "JP EQ";
        public const string RETURNPERIODLable2 = "NA WS";
        public const string RETURNPERIODLable3 = "NZ EQ";
        public const string RETURNPERIODLable4 = "US EQ";

        public const string ParentEffectiveDate = "EffectiveDate";
        public const string ParentUnderwritingTeamName = "UnderwritingTeamName";
        public const string ParentSourceUnderwritingSystemName = "SourceUnderwritingSystemName";
        public const string ParentSourceUnderwritingSystemNumber = "SourceUnderwritingSystemNumber";
        public const string LAYERTABLEHeaderKey = "LayerHeader";
        #region Exposure Summary
        public const string ExposureSummaryTableColumnWidths = "120 70 70";
        public const string ExposureSummary = "LayerHeader";
        public const string TIVPerHeader = "% of Total TIV";

        public const string ExposureSummaryRegionTableHeader = "Exposure Summary - Region";
        public const string ExposureSummaryRegionColumnHeader = "Region";

        public const string ExposureSummaryCountryTableHeader = "Exposure Summary - Country";
        public const string ExposureSummaryCountryColumnHeader = "Country";

        public const string ExposureSummaryStateTableHeader = "Exposure Summary - State";
        public const string ExposureSummaryStateColumnHeader = "State";

        public const string ExposureSummaryCountyTableHeader = "Exposure Summary - County";
        public const string ExposureSummaryCountyColumnHeader = "County";


        public const string ExposureSummaryEarthquakeRegionTableHeader = "Exposure Summary - Earthquake Region";
        public const string ExposureSummaryEarthquakeRegionColumnHeader = "Region";

        public const string ExposureSummaryGeocodeTableHeader = "Exposure Summary - Geocode";
        public const string ExposureSummaryGeocodeColumnHeader = "Match Type";

        public const string ExposureSummaryTornadoTableHeader = "Exposure Summary - Tornado";
        public const string ExposureSummaryTornadoColumnHeader = "Tornado/Hail Index";

        public const string ExposureSummaryConstructionTableTableHeader = "Exposure Summary - Construction (Markel)";
        public const string ExposureSummaryConstructionColumnHeader = "Construction";

        public const string ExposureSummaryOccupancyTableHeader = "Exposure Summary - Occupancy (Markel)";
        public const string ExposureSummaryOccupancyColumnHeader = "Occupancy";

        public const string ExposureSummaryCityTableHeader = "Exposure Summary - City";
        public const string ExposureSummaryCityColumnHeader = "City";

        public const string ExposureSummaryFloodZoneTableHeader = "Exposure Summary - Flood Zone";
        public const string ExposureSummaryFloodZoneColumnHeader = "Flood Zone";

        public const string ExposureSummaryLeveesTableHeader = "Exposure Summary - Levees";
        public const string ExposureSummaryLeveesColumnHeader = "Region";

        public const string ExposureSummaryZonesTableHeader = "Exposure Summary - Zones";
        public const string ExposureSummaryZonesColumnHeader = "Region";

        public const string ExposureSummaryMilestoCoastTableHeader = "Exposure Summary - Miles to Coast";
        public const string ExposureSummaryMilestoCoastColumnHeader = "Region";




        #endregion

        public static readonly Dictionary<string, PropertyConfig> ReportsConfiguration = new Dictionary<string, PropertyConfig>()
        {
            {"PricingAnalysisLayerName",new PropertyConfig {Name="Layer Name",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=1} },
            {"ExposureLimit",new PropertyConfig {Name="Layer Limit",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=2 } },
            {"QuotaShare",new PropertyConfig {Name="Our Limit",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=3 } },
            {"ExposureAttachment",new PropertyConfig {Name="Attachment",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=4 } },
            {"BrokerCommission",new PropertyConfig {Name="Broker Commission %",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=5 } },
            {"SharePercent",new PropertyConfig {Name="Share Percent %",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=6 } },            
            {"SirAmount",new PropertyConfig {Name="SirAmount",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=7} },
            {"OtherChargePercent",new PropertyConfig {Name="OtherChargePercent",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=8 } },
            {"PolicyLayer",new PropertyConfig {Name="Policy Layer",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=9 } },
            { "IsBound",new PropertyConfig {Name="IsBound",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=10 } },
            {"IsSelected",new PropertyConfig {Name="IsSelected",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=11 } },
            {"Deductible",new PropertyConfig {Name="Deductible",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 } },            

        };
        public static readonly Dictionary<string, PropertyConfig> SymmaryReportLayerCoverageConfiguration = new Dictionary<string, PropertyConfig>()
        {
            {"CoverageName",new PropertyConfig {Name="Coverage Name",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=1} },
            {"LimitSublimit",new PropertyConfig {Name="Limit Sublimit",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=2 } },
            {"DeductTypeName",new PropertyConfig {Name="Deduct Type Name",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=3 } },
            {"DeductSublimit",new PropertyConfig {Name="Deduct Sublimit",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=4 } },
            {"MinimumDeduct",new PropertyConfig {Name="Minimum Deduct",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=5 } },
            {"MaximumDeduct",new PropertyConfig {Name="Maximum Deduct",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=6 } },
        };
        public static readonly Dictionary<string, PropertyConfig> ReportsSummaryLayerConfiguration = new Dictionary<string, PropertyConfig>()
        {
            {"RISKREF",new PropertyConfig {Name="RISK REF",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=1,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"LIMIT",new PropertyConfig {Name="LIMIT",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=2,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary, ReportlTypesEnum.NonMintSummary } } },
            {"EXCESS",new PropertyConfig {Name="EXCESS",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=3,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary, ReportlTypesEnum.NonMintSummary } } },
            {"SIRAOP",new PropertyConfig {Name="SIR / AOP",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=4 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary, ReportlTypesEnum.NonMintSummary } } },
            {"100GPI",new PropertyConfig {Name="100 % GPI",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=5 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"BROKAGE",new PropertyConfig {Name="BROKAGE",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=6 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"LEADER",new PropertyConfig {Name="LEADER",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=7,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"WLSL",new PropertyConfig {Name="WL %/ SL %",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=8 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"WTNEXP-USD",new PropertyConfig {Name="WTN EXP -USD",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=9 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"SIGNEXP-USD",new PropertyConfig {Name="SIGN EXP -USD",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=10 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"MKLGPI-USD",new PropertyConfig {Name="MKL GPI-USD",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=11 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"MKLNPI-USD",new PropertyConfig {Name="MKL NPI-USD",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },

            {"FIREPRICE",new PropertyConfig {Name="FIRE PRICE",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=13,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"CATPRICE",new PropertyConfig {Name="CAT PRICE",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=14 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"COMBPRICE",new PropertyConfig {Name="COMB PRICE",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=15 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },
            {"TECHRATIO",new PropertyConfig {Name="TECH RATIO",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=16 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"LYNPI",new PropertyConfig {Name="LY NPI",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=17 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"LAYERPER",new PropertyConfig {Name="LAYER %",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=18 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },
            {"EXPOSURE",new PropertyConfig {Name="EXPOSURE %",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=19,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },
            {"OTHERPER",new PropertyConfig {Name="OTHER %",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=20 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"FINALPER",new PropertyConfig {Name="FINAL %",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=21 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },

            { "WINDADJUSTMENT",new PropertyConfig {Name="Wind Adjustment",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=22 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.NonMintSummary }} },
            {"EQADJUSTMENT",new PropertyConfig {Name="EQ Adjustment",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=23 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.NonMintSummary }} },
            {"FIREADJUSTMENT",new PropertyConfig {Name="Fire Adjustment",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=24 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.NonMintSummary }} },
            {"TOTALADJUSTMENT",new PropertyConfig {Name="Total Adjustment",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=25 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.NonMintSummary }} },

            {"USEQ",new PropertyConfig {Name="US EQ",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=26 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"NAWS-USD",new PropertyConfig {Name="NA WS",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=28 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"JPEQ",new PropertyConfig {Name="JP EQ",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=29 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"NZEQ",new PropertyConfig {Name="NZ EQ",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=30 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"TOTALIMPACT",new PropertyConfig {Name="TOTAL IMPACT",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=31 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },
            {"NETROCPER",new PropertyConfig {Name="NET ROC %",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=32 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },

        };

        public static readonly Dictionary<string, PropertyConfig> ReportsSummarySubmissionConfiguration = new Dictionary<string, PropertyConfig>()
        {
            {"INSURED",new PropertyConfig {Name="INSURED",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=1,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary }} },
            {"INCEPTIONEXP",new PropertyConfig {Name="INCEPTION/EXP",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=2 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary ,ReportlTypesEnum.NonMintSummary }} },
            {"REINSURED",new PropertyConfig {Name="REINSURED",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=3 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"RENEWALAGE",new PropertyConfig {Name="RENEWAL AGE",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=4 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"BROKER",new PropertyConfig {Name="BROKER",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=5 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"BROKERINGHOUSE",new PropertyConfig {Name="BROKERING HOUSE",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=6 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"UMR",new PropertyConfig {Name="UMR",controlType=ControlTypesEnum.NUMBER_INPUT_CURRENCY,decimalPoints=0,sequenceNumber=7,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"PROGRAMREF",new PropertyConfig {Name="PROGRAM REF",controlType=ControlTypesEnum.NUMBER_INPUT_PERCENT,decimalPoints=2,sequenceNumber=8 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"ERMSDEALNUM",new PropertyConfig {Name="ERMS DEAL NUM",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=9 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            { "CURRNCYFX",new PropertyConfig {Name="CURRNCY/FX",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=10 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary,ReportlTypesEnum.NonMintSummary  }} },
            {"OCCUPANCY",new PropertyConfig {Name="OCCUPANCY",controlType=ControlTypesEnum.CHECKBOX_INPUT,decimalPoints=0,sequenceNumber=11 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"LIMITTYPE",new PropertyConfig {Name="LIMITTYPE",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },

            {"LLOYDSRC",new PropertyConfig {Name="LLOYDS RC",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"TERRITORY",new PropertyConfig {Name="TERRITORY",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"PERIL",new PropertyConfig {Name="PERIL",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"SDD",new PropertyConfig {Name="SDD",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"PERMFREQ",new PropertyConfig {Name="PERM FREQ",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },
            {"AGREEPARTY",new PropertyConfig {Name="AGREE PARTY",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },

            {"CUSTOMERTYPE",new PropertyConfig {Name="CUSTOMER TYPE",controlType=ControlTypesEnum.TEXT_INPUT,decimalPoints=0,sequenceNumber=12 ,reportTypes= new ReportlTypesEnum[] { ReportlTypesEnum.MintSummary }} },


        };

        public static readonly List<string> ExcludeQuetions = new List<string>() {
            "[TPS_OUR_SHARE_BROKER_COMMISSION]","[TPS_BROKER_COMMISSION]" ,
            "[TPS_OUR_SHARE_PROFIT]","[TPS_PROFIT]",
            "[TPS_OUR_SHARE_TOTAL_SURPLUS]","[TPS_TOTAL_SURPLUS]",
            "[TPS_OUR_SHARE_RETURN_ON_CAPITAL]","[TPS_RETURN_ON_CAPITAL]"};
    }
}
