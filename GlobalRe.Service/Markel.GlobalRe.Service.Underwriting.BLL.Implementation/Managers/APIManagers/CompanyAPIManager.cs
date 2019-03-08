using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class CompanyAPIManager : BaseUnitOfWorkManager, ICompanyAPIManager
    {

        #region Repositories & Managers
        private ICedantManager _cedantManager { get; set; }
		private IBrokerManager _brokerManager { get; set; }

		#endregion

		#region Constructor 

        public CompanyAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,                                 
                                  ICedantManager cedantsManager/*,
								  IBrokerManager brokerManager*/)
       : base(userManager, cacheStoreManager, logManager)
        {
            _cedantManager = ValidateManager(cedantsManager);
			//_brokerManager = ValidateManager(brokerManager);
		}

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity
        {
            if (typeof(BLL_CLASS) == typeof(BLL_Cedant)) return _cedantManager.GetEntityActions(entity as BLL_Cedant);
//			if (typeof(BLL_CLASS) == typeof(BLL_Broker)) return _brokerManager.GetEntityActions(entity as BLL_Broker);
			return null;
        }

		#endregion


        #region Search

        public EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid)
        {
            return RunInContextScope(() =>
            {
                return _cedantManager.GetCedants(cedants, cedantsparentgroup, cedantsid, cedantsparentgroupid, cedantslocationid);
            });
        }
        #endregion
    }
}
