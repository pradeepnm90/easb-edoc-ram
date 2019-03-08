
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class DealCoveragesTransformationManager : BaseManager, IDealCoveragesTransformationManager
    {

        #region Constructor

        public DealCoveragesTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_DealCoverages Transform(grs_VGrsDealCoverage dbModel)
        {
            return new BLL_DealCoverages()
            {
                Dealnum = dbModel.Dealnum,
                Cover_Id = dbModel.CoverId,
                Cover_Name = dbModel.CoverName
            };
        }

        public List<BLL_DealCoverages> Transform(IList<grs_VGrsDealCoverage> CoverageList)
        {

            List<BLL_DealCoverages> coverDataItems = new List<BLL_DealCoverages>();

            CoverageList.GroupBy(a => a.CoverId).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                statusGroupData.ForEach((summary) =>
                {
                    coverDataItems.Add(new BLL_DealCoverages() { Dealnum = summary.Dealnum, Cover_Id = summary.CoverId, Cover_Name = summary.CoverName });
                });
            });

            return coverDataItems.OrderBy(c => c.Cover_Id).ToList();
        }

        #endregion Transform

    }
}
