using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_WritingCompany : BaseGlobalReBusinessEntity
    {
        public int Papernum { get; set; } // papernum
        public string Companyname { get; set; } // companyname
        public int? Cpnum { get; set; } // cpnum
        public string Addr1 { get; set; } // addr1
        public string Addr2 { get; set; } // addr2
        public string Addr3 { get; set; } // addr3
        public string City { get; set; } // city
        public string State { get; set; } // state
        public string Postalcode { get; set; } // postalcode
        public string Country { get; set; } // cty
        public string Phone { get; set; } // phone
        public string Fax { get; set; } // fax
        public string Imagefilename { get; set; } // imagefilename
        public string CompanyShortname { get; set; } // company_shortname
        public byte? SlTrequired { get; set; } // SLTrequired
        public byte? IpTrequired { get; set; } // IPTrequired
        public int? Territory { get; set; } // Territory
        public int? Currency { get; set; } // Currency
        public byte Active { get; set; } // active
        public DateTime CloseDate { get; set; } // CloseDate
        public DateTime RestrictDate { get; set; } // RestrictDate
        public string PaperToken { get; set; } // paper_token
        public DateTime? RptLastCloseDate { get; set; } // RptLastCloseDate
        public bool HideUnusedClaimCategory { get; set; } // HideUnusedClaimCategory
        public DateTime? ComplianceCloseDate { get; set; } // ComplianceCloseDate
        public DateTime? RptLastComplianceCloseDate { get; set; } // RptLastComplianceCloseDate
        public DateTime? OutwardCloseDate { get; set; } // OutwardCloseDate
        public DateTime? OutwardRestrictDate { get; set; } // OutwardRestrictDate
        public DateTime? RptLastOutwardCloseDate { get; set; } // RptLastOutwardCloseDate
        public string JeCode { get; set; } // JECode
        public DateTime? ClaimsCloseDate { get; set; } // ClaimsCloseDate
        public DateTime? RptLastClaimsCloseDate { get; set; } // RptLastClaimsCloseDate
        public bool RptExclude { get; set; } // RptExclude
        public string RptJe { get; set; } // RptJE
        public string CompanyPrintName { get; set; } // companyPrintName
        public int? AssumedFlag { get; set; } // AssumedFlag
        public string AssumedSortOrder { get; set; } // AssumedSortOrder
        public int? CededFlag { get; set; } // CededFlag
        public string CededSortOrder { get; set; } // CededSortOrder

    }
}