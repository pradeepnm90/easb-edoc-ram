using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
	public interface ICompanyAPIManager : IBaseManager
    {
        #region Entity Actions

        EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity;

        #endregion Entity Actions

       
        #region Search

        EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid);
        

        #endregion

	}
}
