using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class BrokerTransformationManager : BaseManager, IBrokerTransformationManager
    {
        #region Constructor

        public BrokerTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_Broker Transform(grs_VGrsBroker dbModel)
        {
            return new BLL_Broker()
            {
                Brokerid = dbModel.Brokerid,
                Broker = dbModel.Broker,
                Brokergroupid = dbModel.Brokergroupid,
                Brokergroupname = dbModel.Brokergroupname,
                Brokercategoryid = dbModel.Brokercategoryid,
                Brokercategories = dbModel.Brokercategories,
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

        public List<BLL_Broker> Transform(IList<grs_VGrsBroker> CoverageList)
        {

            List<BLL_Broker> coverDataItems = new List<BLL_Broker>();

            CoverageList.GroupBy(a => a.Brokerid).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                statusGroupData.ForEach((summary) =>
                {
                    coverDataItems.Add(new BLL_Broker()
                    {
                        Brokerid = summary.Brokerid,
                        Broker = summary.Broker,
                        Brokergroupid = summary.Brokergroupid,
                        Brokergroupname = summary.Brokergroupname,
                        Brokercategoryid = summary.Brokercategoryid,
                        Brokercategories = summary.Brokercategories,
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

            return coverDataItems.OrderBy(c => c.Broker).ToList();
        }

        #endregion Transform

    }
}
