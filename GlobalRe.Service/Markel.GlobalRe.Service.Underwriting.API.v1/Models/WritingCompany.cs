using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class WritingCompany : BaseApiModel<BLL_WritingCompany>
    {
        public WritingCompany() { }
        public WritingCompany(BLL_WritingCompany model) : base(model) { }

        public override BLL_WritingCompany ToBLLModel()
        {
            BLL_WritingCompany bLL_writingCompany = new BLL_WritingCompany()
            {
                Papernum = Papernum,
                Companyname = Companyname,
                Cpnum = Cpnum,
                //Relatedcompany = Relatedcompany,
                Addr1 = Addr1,
                Addr2 = Addr2,
                Addr3 = Addr3,
                City = City,
                State = State,
                Postalcode = Postalcode,
                Country = Country,
                Phone = Phone,
                Fax = Fax,
                Imagefilename = Imagefilename,
                CompanyShortname = CompanyShortname,
                SlTrequired = SlTrequired,
                IpTrequired = IpTrequired,
                Territory = Territory,
                Currency = Currency,
                Active = Active,
                CloseDate = CloseDate,
                RestrictDate = RestrictDate,
                PaperToken = PaperToken,
                RptLastCloseDate = RptLastCloseDate,
                HideUnusedClaimCategory = HideUnusedClaimCategory,
                ComplianceCloseDate = ComplianceCloseDate,
                RptLastComplianceCloseDate = RptLastComplianceCloseDate,
                OutwardCloseDate = OutwardCloseDate,
                OutwardRestrictDate = OutwardRestrictDate,
                RptLastOutwardCloseDate = RptLastOutwardCloseDate,
                JeCode = JeCode,
                ClaimsCloseDate = ClaimsCloseDate,
                RptLastClaimsCloseDate = RptLastClaimsCloseDate,
                RptExclude = RptExclude,
                RptJe = RptJe,
                CompanyPrintName = CompanyPrintName,
                AssumedFlag = AssumedFlag,
                AssumedSortOrder = AssumedSortOrder,
                CededFlag = CededFlag,
                CededSortOrder = CededSortOrder
            };

            return bLL_writingCompany;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_WritingCompany model)
        {
            Papernum = model.Papernum;
            Companyname = model.Companyname;
            Cpnum = model.Cpnum;
            //Relatedcompany = Relatedcompany,
            Addr1 = model.Addr1;
            Addr2 = model.Addr2;
            Addr3 = model.Addr3;
            City = model.City;
            State = model.State;
            Postalcode = model.Postalcode;
            Country = model.Country;
            Phone = model.Phone;
            Fax = model.Fax;
            Imagefilename = model.Imagefilename;
            CompanyShortname = model.CompanyShortname;
            SlTrequired = model.SlTrequired;
            IpTrequired = model.IpTrequired;
            Territory = model.Territory;
            Currency = model.Currency;
            Active = model.Active;
            CloseDate = model.CloseDate;
            RestrictDate = model.RestrictDate;
            PaperToken = model.PaperToken;
            RptLastCloseDate = model.RptLastCloseDate;
            HideUnusedClaimCategory = model.HideUnusedClaimCategory;
            ComplianceCloseDate = model.ComplianceCloseDate;
            RptLastComplianceCloseDate = model.RptLastComplianceCloseDate;
            OutwardCloseDate = model.OutwardCloseDate;
            OutwardRestrictDate = model.OutwardRestrictDate;
            RptLastOutwardCloseDate = model.RptLastOutwardCloseDate;
            JeCode = model.JeCode;
            ClaimsCloseDate = model.ClaimsCloseDate;
            RptLastClaimsCloseDate = model.RptLastClaimsCloseDate;
            RptExclude = model.RptExclude;
            RptJe = model.RptJe;
            CompanyPrintName = model.CompanyPrintName;
            AssumedFlag = model.AssumedFlag;
            AssumedSortOrder = model.AssumedSortOrder;
            CededFlag = model.CededFlag;
            CededSortOrder = model.CededSortOrder;
        }
    }
}