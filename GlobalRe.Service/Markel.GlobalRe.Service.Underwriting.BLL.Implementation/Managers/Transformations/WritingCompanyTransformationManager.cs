
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class WritingCompanyTransformationManager : BaseManager, IWritingCompanyTransformationManager
    {
        #region Constructor

        public WritingCompanyTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_WritingCompany Transform(grs_VPaperExt dbModel)
        {
            return new BLL_WritingCompany()
            {

                //
                Papernum = dbModel.Papernum,
                Companyname = dbModel.Companyname,
                Cpnum = dbModel.Cpnum,
                //Relatedcompany = dbModel.Relatedcompany,
                Addr1 = dbModel.Addr1,
                Addr2 = dbModel.Addr2,
                Addr3 = dbModel.Addr3,
                City = dbModel.City,
                State = dbModel.State,
                Postalcode = dbModel.Postalcode,
                Country = dbModel.Cty,
                Phone = dbModel.Phone,
                Fax = dbModel.Fax,
                Imagefilename = dbModel.Imagefilename,
                CompanyShortname = dbModel.CompanyShortname,
                SlTrequired = dbModel.SlTrequired,
                IpTrequired = dbModel.IpTrequired,
                Territory = dbModel.Territory,
                Currency = dbModel.Currency,
                Active = dbModel.Active,
                CloseDate = dbModel.CloseDate,
                RestrictDate = dbModel.RestrictDate,
                PaperToken = dbModel.PaperToken,
                RptLastCloseDate = dbModel.RptLastCloseDate,
                HideUnusedClaimCategory = dbModel.HideUnusedClaimCategory,
                ComplianceCloseDate = dbModel.ComplianceCloseDate,
                RptLastComplianceCloseDate = dbModel.RptLastComplianceCloseDate,
                OutwardCloseDate = dbModel.OutwardCloseDate,
                OutwardRestrictDate = dbModel.OutwardRestrictDate,
                RptLastOutwardCloseDate = dbModel.RptLastOutwardCloseDate,
                JeCode = dbModel.JeCode,
                ClaimsCloseDate = dbModel.ClaimsCloseDate,
                RptLastClaimsCloseDate = dbModel.RptLastClaimsCloseDate,
                RptExclude = dbModel.RptExclude,
                RptJe = dbModel.RptJe,
                CompanyPrintName = dbModel.CompanyPrintName,
                AssumedFlag = dbModel.AssumedFlag,
                AssumedSortOrder = dbModel.AssumedSortOrder,
                CededFlag = dbModel.CededFlag,
                CededSortOrder = dbModel.CededSortOrder
            };
        }

        public List<BLL_WritingCompany> Transform(IList<grs_VPaperExt> CoverageList)
        {

            List<BLL_WritingCompany> coverDataItems = new List<BLL_WritingCompany>();

            CoverageList.GroupBy(a => a.Papernum).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                statusGroupData.ForEach((summary) =>
                {
                    coverDataItems.Add(new BLL_WritingCompany()
                    {
                        Papernum = summary.Papernum,
                        Companyname = summary.Companyname,
                        Cpnum = summary.Cpnum,
                        //Relatedcompany = summary.Relatedcompany,
                        Addr1 = summary.Addr1,
                        Addr2 = summary.Addr2,
                        Addr3 = summary.Addr3,
                        City = summary.City,
                        State = summary.State,
                        Postalcode = summary.Postalcode,
                        Country = summary.Cty,
                        Phone = summary.Phone,
                        Fax = summary.Fax,
                        Imagefilename = summary.Imagefilename,
                        CompanyShortname = summary.CompanyShortname,
                        SlTrequired = summary.SlTrequired,
                        IpTrequired = summary.IpTrequired,
                        Territory = summary.Territory,
                        Currency = summary.Currency,
                        Active = summary.Active,
                        CloseDate = summary.CloseDate,
                        RestrictDate = summary.RestrictDate,
                        PaperToken = summary.PaperToken,
                        RptLastCloseDate = summary.RptLastCloseDate,
                        HideUnusedClaimCategory = summary.HideUnusedClaimCategory,
                        ComplianceCloseDate = summary.ComplianceCloseDate,
                        RptLastComplianceCloseDate = summary.RptLastComplianceCloseDate,
                        OutwardCloseDate = summary.OutwardCloseDate,
                        OutwardRestrictDate = summary.OutwardRestrictDate,
                        RptLastOutwardCloseDate = summary.RptLastOutwardCloseDate,
                        JeCode = summary.JeCode,
                        ClaimsCloseDate = summary.ClaimsCloseDate,
                        RptLastClaimsCloseDate = summary.RptLastClaimsCloseDate,
                        RptExclude = summary.RptExclude,
                        RptJe = summary.RptJe,
                        CompanyPrintName = summary.CompanyPrintName,
                        AssumedFlag = summary.AssumedFlag,
                        AssumedSortOrder = summary.AssumedSortOrder,
                        CededFlag = summary.CededFlag,
                        CededSortOrder = summary.CededSortOrder
                    });
                });
            });

            return coverDataItems.OrderBy(c => c.Companyname).ToList();
        }

        #endregion Transform

    }
}
