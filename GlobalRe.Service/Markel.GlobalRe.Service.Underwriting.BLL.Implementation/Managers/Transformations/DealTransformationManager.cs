using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class DealTransformationManager : BaseManager, IDealTransformationManager
    {

        #region Constructor

        public DealTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_Deal Transform(grs_VGrsDeal dbModel)
        {
            return new BLL_Deal()
            {
                Dealnum = dbModel.Dealnum,
                Dealname = dbModel.Dealname,
                Contractnum = dbModel.Contractnum,
                Inceptdate = dbModel.Inceptdate,
                Expirydate = dbModel.Expirydate,
                ExpiryEod = dbModel.ExpiryEod,
                Targetdt = dbModel.Targetdt,
                ModelPriority = dbModel.ModelPriority,
                Submissiondate = dbModel.Submissiondate,
                Status = dbModel.Status,
                StatusName = dbModel.StatusName,
                Uw1 = dbModel.Uw1,
                Uw1Name = dbModel.Uw1Name,
                Uw2 = dbModel.Uw2,
                Uw2Name = dbModel.Uw2Name,
                Ta = dbModel.Ta,
                TaName = dbModel.TaName,
                Modeller = dbModel.Modeller,
                ModellerName = dbModel.ModellerName,
                Act1 = dbModel.Act1,
                Act1Name = dbModel.Act1Name,
                Broker = dbModel.Broker,
                BrokerName = dbModel.BrokerName,
                BrokerContact = dbModel.BrokerContact,
                BrokerContactName = dbModel.BrokerContactName,
                Division = dbModel.Division,
                Paper = dbModel.Paper,
                PaperName = dbModel.PaperName,
                Team = dbModel.Team,
                TeamName = dbModel.TeamName,
				Cedant = dbModel.Cedant,
				CedantName = dbModel.CedantName,
				Continuous = dbModel.Continuous,
                CedentLocation = dbModel.CedentLocation,
                CedentLocationname = dbModel.CedentLocationname,
                BrokerLocation = dbModel.BrokerLocation,
                BrokerLocationname = dbModel.BrokerLocationname,
                Exposuretype = dbModel.Exposuretype,
                Renewal = dbModel.Renewal,
                Dealtype = dbModel.Dealtype,
                DealtypeName = dbModel.DealtypeName,
                Coveragetype = dbModel.Coveragetype,
                CoveragetypeName = dbModel.CoveragetypeName,
                Policybasis = dbModel.Policybasis,
                PolicybasisName = dbModel.PolicybasisName,
                Currency = dbModel.Currency,
                CurrencyName = dbModel.CurrencyName,
                Domicile = dbModel.Domicile,
                DomicileName = dbModel.DomicileName,
                Region = dbModel.Region,
                RegionName = dbModel.RegionName,
                CedantGroupCode = dbModel.CedentCompanygroupId,
                CedantGroupName = dbModel.CedentCompanygroup,
                BrokerGroupCode = dbModel.BrokerCompanygroupId,
                BrokerGroupName = dbModel.BrokerCompanygroup,
                RenewalName = dbModel.RenewalName,
                ChkUWCompliance = dbModel.ChkUwCompliance,
                ChkPreBindProcessing = dbModel.ChkPreBindProc,
                ChkModelling = dbModel.ChkModeling
            };
        }

        public BLL_Deal Transform(grs_VGrsDealsByStatu dbModel)
        {
            return new BLL_Deal()
            {
                Dealnum = (int)dbModel.Dealnum,
                Dealname = dbModel.Dealname,
                Contractnum = dbModel.Contractnum,
                Inceptdate = dbModel.Inceptdate,
                Expirydate = dbModel.Expirydate,
                ExpiryEod = dbModel.ExpiryEod,
                Targetdt = dbModel.Targetdt,
                ModelPriority = dbModel.ModelPriority,
                Submissiondate = dbModel.Submissiondate,
                Status = dbModel.Status,
                StatusName = dbModel.StatusName1,
                Uw1 = dbModel.Uw1,
                Uw1Name = dbModel.Uw1Name,
                Uw2 = dbModel.Uw2,
                Uw2Name = dbModel.Uw2Name,
                Ta = dbModel.Ta,
                TaName = dbModel.TaName,
                Modeller = dbModel.Modeller,
                ModellerName = dbModel.ModellerName,
                Act1 = dbModel.Act1,
                Act1Name = dbModel.Act1Name,
                Broker = dbModel.Broker,
                BrokerName = dbModel.BrokerName,
                BrokerContact = dbModel.BrokerContact,
                BrokerContactName = dbModel.BrokerContactName,
                Division = dbModel.Division,
                Paper = dbModel.Paper,
                PaperName = dbModel.PaperName,
                Team = dbModel.Team,
                TeamName = dbModel.TeamName,
				Cedant = dbModel.Cedant,
				CedantName = dbModel.CedantName,
				Continuous = dbModel.Continuous,
                CedentLocation = dbModel.CedentLocation,
                CedentLocationname = dbModel.CedentLocationname,
                BrokerLocation = dbModel.BrokerLocation,
                BrokerLocationname = dbModel.BrokerLocationname,
                Exposuretype = dbModel.Exposuretype,
                Renewal = dbModel.Renewal,
                Dealtype = dbModel.Dealtype,
                DealtypeName = dbModel.DealtypeName,
                Coveragetype = dbModel.Coveragetype,
                CoveragetypeName = dbModel.CoveragetypeName,
                Policybasis = dbModel.Policybasis,
                PolicybasisName = dbModel.PolicybasisName,
                Currency = dbModel.Currency,
                CurrencyName = dbModel.CurrencyName,
                Domicile = dbModel.Domicile,
                DomicileName = dbModel.DomicileName,
                Region = dbModel.Region,
                RegionName = dbModel.RegionName,
                CedantGroupCode = dbModel.CedentCompanygroupId,
                CedantGroupName = dbModel.CedentCompanygroup,
                BrokerGroupCode = dbModel.BrokerCompanygroupId,
                BrokerGroupName = dbModel.BrokerCompanygroup,
                RenewalName = dbModel.RenewalName,
                ChkUWCompliance = dbModel.ChkUwCompliance,
                ChkPreBindProcessing = dbModel.ChkPreBindProc,
                ChkModelling = dbModel.ChkModeling
            };
        }

        public List<BLL_ExposureTree> Transform(IList<grs_VExposureTreeExt> exposureTree)
        {
            List<BLL_ExposureTree> data = new List<BLL_ExposureTree>();
            if (exposureTree.Count > 0)
            {
                exposureTree.ToList().ForEach((sGroup) =>
                {

                    data.Add(new BLL_ExposureTree()
                    {
                        SubdivisionCode = sGroup.SubdivisionCode,
                        SubdivisionName = sGroup.SubdivisionName,
                        ProductLineCode = sGroup.ProductLineCode,
                        ProductLineName = sGroup.ProductLineName,
                        ExposureGroupCode = sGroup.ExposureGroupCode,
                        ExposureGroupName = sGroup.ExposureGroupName,
                        ExposureTypeCode = sGroup.ExposureTypeCode,
                        ExposureTypeName = sGroup.ExposureTypeName
                    });

                });
                return data.OrderBy(c => c.ExposureTypeName).ToList();
            }
            return null;
        }


        #endregion Transform

    }
}
