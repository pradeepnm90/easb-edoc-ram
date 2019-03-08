using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IPersonAPIManager : IBaseManager
    {
        #region Entity Actions

        EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity;

        #endregion Entity Actions

		EntityResult<BLL_Person> GetPerson(int personid);


        #region Person
        IPaginatedList<BLL_Person> SearchPersons(SearchCriteria criteria);
        #endregion Person

		//EntityResult<BLL_PersonProfile> GetProfile();

	}
}
