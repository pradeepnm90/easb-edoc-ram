using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class CedantTransformationManager : BaseManager, ICedantTransformationManager
    {
        #region Constructor

        public CedantTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_Cedant Transform(grs_VGrsCedant dbModel)
        {
            return new BLL_Cedant()
            {
                Cedantid = dbModel.Cedantid,
                Cedant = dbModel.Cedant,
                Cedantgroupid = dbModel.Cedantgroupid,
                Cedantgroupname = dbModel.Cedantgroupname,
                Cedantcategoryid = dbModel.Cedantcategoryid,
                Cedantcategories = dbModel.Cedantcategories,
                Parentgrouptypeid = dbModel.Parentgrouptypeid,
                Parentgrouptype = dbModel.Parentgrouptype,
                Locationid = dbModel.Locationid,
                Locationname = dbModel.Locationname,
                Locationaddress = dbModel.Locationaddress,
                Locationcity = dbModel.Locationcity,
                Locationpostcode = dbModel.Locationpostcode,
                Locationstate = dbModel.Locationstate,
                Country = dbModel.Country,
                Parentcompanyname = dbModel.Parentcompanyname
            };
        }

        public List<BLL_Cedant> Transform(IList<grs_VGrsCedant> CoverageList)
        {

            List<BLL_Cedant> coverDataItems = new List<BLL_Cedant>();

            CoverageList.GroupBy(a => a.Cedantid).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                statusGroupData.ForEach((summary) =>
                {
                    coverDataItems.Add(new BLL_Cedant()
                    {
                        Cedantid = summary.Cedantid,
                        Cedant = summary.Cedant,
                        Cedantgroupid = summary.Cedantgroupid,
                        Cedantgroupname = summary.Cedantgroupname,
                        Cedantcategoryid = summary.Cedantcategoryid,
                        Cedantcategories = summary.Cedantcategories,
                        Parentgrouptypeid = summary.Parentgrouptypeid,
                        Parentgrouptype = summary.Parentgrouptype,
                        Locationid = summary.Locationid,
                        Locationname = summary.Locationname,
                        Locationaddress = summary.Locationaddress,
                        Locationcity = summary.Locationcity,
                        Locationpostcode = summary.Locationpostcode,
                        Locationstate = summary.Locationstate,
                        Country = summary.Country,
                        Parentcompanyname = summary.Parentcompanyname
                    });
                });
            });

            return coverDataItems.OrderBy(c => c.Cedant).ToList();
        }

        #endregion Transform

    }
}
