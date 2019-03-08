

using System;
using System.Linq;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.ComponentModel;
 
/// <summary>
/// Auto generated enumerations. The contents of this file are automatically generated from the database; 
/// therefore, any edits made to this file will be lost the next time the file is re-generated.
/// </summary>
namespace Markel.GlobalRe.Service.Underwriting.Data.Enums
{
    #region dbo.tb_currencyEnum

    /// <summary> dbo.tb_currency - auto generated enumeration </summary>
    [GeneratedCode("EnumTemplatingFileGenerator", "10")]
    public enum tb_currencyEnum
    {
        [Description("INACTIVE - Andorran Peseta")]
        ADP = 10014,
        [Description("United Arab Emirates Dirham")]
        AED = 10001,
        [Description("Afghani")]
        AFN = 10011,
        [Description("Armenian Dram")]
        AMD = 10009,
        [Description("NETHERLANDS ANTILLES")]
        ANG = 47,
        [Description("ANGOLAN KWANZA")]
        AOA = 74,
        [Description("ARGENTINE PESO")]
        ARS = 28,
        [Description("AUSTRIAN SCHILLING")]
        ATS = 18,
        [Description("AUSTRALIAN DOLLAR")]
        AUD = 10,
        [Description("Aruban Guilder")]
        AWG = 89,
        [Description("Azerbaijanian New Manat")]
        AZN = 91,
        [Description("BARBADIAN DOLLAR")]
        BBD = 94,
        [Description("Bangladesh Taka")]
        BDT = 93,
        [Description("BELGIUN FRANC")]
        BEF = 11,
        [Description("BULGARIAN LEV")]
        BGN = 503,
        [Description("Bahraini Dinar")]
        BHD = 92,
        [Description("BERMUDA DOLLAR")]
        BMD = 72,
        [Description("BRUNEI DOLLAR")]
        BND = 106,
        [Description("Boliviano")]
        BOB = 10003,
        [Description("BRAZILIAN REAL")]
        BRL = 602,
        [Description("BAHAMAS DOLLAR")]
        BSD = 501,
        [Description("Bhutan Ngultrum")]
        BTN = 99,
        [Description("BOTSWANA PULA")]
        BWP = 502,
        [Description("Belize Dollar")]
        BZD = 97,
        [Description("CANADIAN DOLLAR")]
        CAD = 8,
        [Description("Congolian Franc")]
        CDF = 120,
        [Description("SWISS FRANC")]
        CHF = 7,
        [Description("Chilean Unidad de Fomento")]
        CLF = 10000,
        [Description("CHILEAN PESO")]
        CLP = 39,
        [Description("CHINESE RENMIMBI")]
        CNY = 61,
        [Description("COLUMBIAN PESO")]
        COP = 604,
        [Description("Costa Rican Colon")]
        CRC = 79,
        [Description("CYPRUS POUND")]
        CYP = 608,
        [Description("CZECH KORUNA")]
        CZK = 63,
        [Description("DEUTSCHE MARK")]
        DEM = 4,
        [Description("DANISH KRONE")]
        DKK = 13,
        [Description("DOMINICAN PESO")]
        DOP = 128,
        [Description("ALGERIAN DINAR")]
        DZD = 500,
        [Description("ECUADORIAN SUCRE")]
        ECS = 69,
        [Description("Estonian Kroon")]
        EEK = 80,
        [Description("EGYPTIAN POUND")]
        EGP = 607,
        [Description("SPANISH PESETA")]
        ESP = 12,
        [Description("Ethiopian Birr")]
        ETB = 132,
        [Description("EURO")]
        EUR = 17,
        [Description("FINNISH MARKKA")]
        FIM = 15,
        [Description("Fiji Dollar")]
        FJD = 78,
        [Description("FRENCH FRANC")]
        FRF = 3,
        [Description("BRITISH POUNDS STERLING")]
        GBP = 2,
        [Description("Georgian Lari")]
        GEL = 142,
        [Description("GHANA CEDI")]
        GHC = 506,
        [Description("Gibraltar Pound")]
        GIP = 143,
        [Description("GREEK DRACHME")]
        GRD = 40,
        [Description("Guatemala Quetzal")]
        GTQ = 148,
        [Description("Guyana Dollar")]
        GYD = 151,
        [Description("HONG KONG DOLLAR")]
        HKD = 31,
        [Description("Honduras Lempira")]
        HNL = 154,
        [Description("CROATIAN KUNA")]
        HRK = 504,
        [Description("HUNGARY FORINT")]
        HUF = 41,
        [Description("INDONESIAN RUPIAH")]
        IDR = 44,
        [Description("IRISH PUNT")]
        IEP = 20,
        [Description("ISRAELI SHEQUEL")]
        ILS = 46,
        [Description("INDIAN RUPEE")]
        INR = 43,
        [Description("Iranian Rial")]
        IRR = 155,
        [Description("Iceland Krona")]
        ISK = 82,
        [Description("ITALIAN LIRA")]
        ITL = 5,
        [Description("JAMAICAN DOLLAR")]
        JMD = 76,
        [Description("Jordanian Dinar")]
        JOD = 160,
        [Description("JAPANESE YEN")]
        JPY = 6,
        [Description("Kenyan Shilling")]
        KES = 163,
        [Description("Won")]
        KPW = 10010,
        [Description("S. KOREAN WON")]
        KRW = 35,
        [Description("Kuwaiti Dinar")]
        KWD = 165,
        [Description("CAYMAN ISLANDS")]
        KYD = 73,
        [Description("Kazakhstani Tenge")]
        KZT = 10015,
        [Description("Lao Kip")]
        LAK = 10013,
        [Description("Lebanese Pound")]
        LBP = 169,
        [Description("SRI LANKA RUPEE")]
        LKR = 510,
        [Description("Liberian Dollar")]
        LRD = 171,
        [Description("LITHUANIA LITAS")]
        LTL = 101,
        [Description("LUXEMBOURG FRANC")]
        LUF = 19,
        [Description("Latvian Lats")]
        LVL = 168,
        [Description("Libyan Dinar")]
        LYD = 172,
        [Description("MORROCCAN DIRHAM")]
        MAD = 507,
        [Description("Myanmar Kyat")]
        MMK = 108,
        [Description("Macau Pataca")]
        MOP = 174,
        [Description("MALTESE LIRA")]
        MTL = 102,
        [Description("MAURITIAN RUPEE")]
        MUR = 104,
        [Description("Maldives Rufiyaa")]
        MVR = 178,
        [Description("Malawi Kwacha")]
        MWK = 177,
        [Description("MEXICAN PESO")]
        MXN = 601,
        [Description("MALAYSIAN RINGGIT ")]
        MYR = 24,
        [Description("Namibia Dollar")]
        NAD = 191,
        [Description("NIGERIAN NAIRA")]
        NGN = 71,
        [Description("Cordoba Oro")]
        NIO = 195,
        [Description("NETHERLAND GUILDER")]
        NLG = 9,
        [Description("NORWEGIAN KRONE")]
        NOK = 14,
        [Description("Nepalese Rupee")]
        NPR = 193,
        [Description("NEW ZEALAND DOLLAR")]
        NZD = 22,
        [Description("Rial Omani")]
        OMR = 202,
        [Description("OTHER CURRENCY")]
        OTR = 9999,
        [Description("PANAMA BALBOA")]
        PAB = 49,
        [Description("PERU SOL")]
        PEN = 50,
        [Description("Kina")]
        PGK = 204,
        [Description("PHILLIPINE PESO")]
        PHP = 34,
        [Description("PAKISTAN RUPEE")]
        PKR = 508,
        [Description("POLAND ZLOTY")]
        PLN = 52,
        [Description("PORTUGUESE ESCUDO")]
        PTE = 51,
        [Description("Guarani")]
        PYG = 206,
        [Description("Qatari Rial")]
        QAR = 208,
        [Description("Romanian Leu")]
        ROL = 210,
        [Description("RUSSIAN RUBBLE")]
        RUB = 32,
        [Description("Russian ruble (old)")]
        RUR = 10004,
        [Description("SAUDI RIYAL")]
        SAR = 54,
        [Description("Solomon Islands Dollar")]
        SBD = 226,
        [Description("Sudanese Dinar")]
        SDD = 230,
        [Description("Sudanese Pound")]
        SDG = 10002,
        [Description("SPECIAL DRAWING RGTS")]
        SDR = 509,
        [Description("SWEDISH KRONE")]
        SEK = 16,
        [Description("SINGAPORE DOLLAR")]
        SGD = 23,
        [Description("SLOVENIA TOLAR")]
        SIT = 103,
        [Description("Slovak Koruna")]
        SKK = 224,
        [Description("Surinamese Guilder(new)")]
        SRD = 10007,
        [Description("Surinamese Guilder(old)")]
        SRG = 10006,
        [Description("EL SALVADORIAN COLÓN")]
        SVC = 77,
        [Description("Syrian Pound")]
        SYP = 234,
        [Description("Lilangeni")]
        SZL = 233,
        [Description("THAILAND BAHT")]
        THB = 56,
        [Description("Tongan Paanga")]
        TOP = 10012,
        [Description("TURKISH LIRA")]
        TRL = 62,
        [Description("TURKISH LIRA(New)")]
        TRY = 10008,
        [Description("Trinidad and Tobago Dollar")]
        TTD = 609,
        [Description("TAIWAN DOLLAR")]
        TWD = 55,
        [Description("Tanzanian Shilling")]
        TZS = 236,
        [Description("Hryvnia")]
        UAH = 246,
        [Description("Uganda Shilling")]
        UGX = 245,
        [Description("U.S. DOLLARS")]
        USD = 1,
        [Description("URUGUAY PESO")]
        UYU = 68,
        [Description("VENEZUELA BOLIVAR")]
        VEB = 603,
        [Description("Bolivar fuerte")]
        VEF = 100,
        [Description("VIETNAM DONG")]
        VND = 511,
        [Description("Vanuatu vatu")]
        VUV = 10016,
        [Description("Samoan Tala")]
        WST = 217,
        [Description("CFA Franc BEAC")]
        XAF = 111,
        [Description("East Caribbean Dollar")]
        XCD = 87,
        [Description("CFP Franc")]
        XPF = 137,
        [Description("Yemeni Rial")]
        YER = 254,
        [Description("SOUTH AFRICAN RAND")]
        ZAR = 33,
        [Description("Zambian kwacha")]
        ZMW = 10017,
        [Description("ZIMBABWE DOL")]
        ZWD = 512,
        [Description("Zimbambwe (old)")]
        ZWO = 10005
    }

    #endregion dbo.tb_currency

}


