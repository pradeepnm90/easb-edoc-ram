using Markel.GlobalRe.Service.Underwriting.BLL.Constants;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class CedantManager : BaseGlobalReManager<BLL_Cedant>, ICedantManager
	{

		#region Private Variable 

		private ICedantRepository _cedantRepository;
		private ICedantTransformationManager _cedantTransformationManager;

		private enum FilterParameters
		{
			Cedant
		}
		#endregion

		#region Constructors

		public CedantManager(IUserManager userManager,
							ICacheStoreManager cacheStoreManager,
							ILogManager logManager,
							ICedantRepository cedantRepository,
							ICedantTransformationManager cedantTransformationManager)
			: base(userManager, cacheStoreManager, logManager)
		{
			_cedantRepository = ValidateRepository(cedantRepository);
			_cedantTransformationManager = ValidateManager(cedantTransformationManager);
		}

		#endregion

		#region Entity Actions
		public EntityAction GetEntityActions(BLL_Cedant entity)
		{
			List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
			//entityActionTypes.Add(EntityActionType.Action_Update);

			return new EntityAction(
			   entityType: EntityType.Cedants,
			   entityId: entity.Cedantid,
			   entityActionTypes: entityActionTypes
		   );
		}

		public EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid)
		{
			var cedantSearchRequestResult = _cedantRepository.GetCedants(cedants, cedantsparentgroup, cedantsid, cedantsparentgroupid, cedantslocationid);

			if (cedantSearchRequestResult.Count() == 0)
				throw new NotFoundAPIException("No matching Cedant Records found ...");

			return new EntityResult<IEnumerable<BLL_Cedant>>(_cedantTransformationManager.Transform(cedantSearchRequestResult));
		}
		#endregion

		#region Private Methods

		private IPaginatedList<BLL_Cedant> Transform<T>(IPaginatedList<T> dbResults) where T : class
		{
			var results = new PaginatedList<BLL_Cedant>()
			{
				PageCount = dbResults.PageCount,
				PageIndex = dbResults.PageIndex,
				PageSize = dbResults.PageSize,
				TotalRecordCount = dbResults.TotalRecordCount,
				Items = dbResults.Items.Select(s => s is grs_VGrsCedant ? _cedantTransformationManager.Transform(s as grs_VGrsCedant) : _cedantTransformationManager.Transform(s as grs_VGrsCedant)).ToList()
			};

			return results;
		}


		// Adding filter by cedent name 
		private Expression<Func<grs_VGrsCedant, bool>> FilterCedants(string cedents)
		{
			return s => s.Cedant == cedents;
		}

		// Adding filter on papernum, isGRSDisplay and Active flag 
		public IPaginatedList<BLL_Cedant> Search(SearchCriteria criteria)
		{
			bool CedantsData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.Cedant);
			ValidateSearchCriteria(CedantsData, criteria);
			return CedantsData ? Transform(_cedantRepository.Search(criteria)) : Transform(_cedantRepository.Search(criteria));
		}

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }
        private void ValidateSearchCriteria(bool CedantsData, SearchCriteria criteria)
		{
			IList<string> validFilterParameters = new List<string>();
			IList<string> validSortParameters = new List<string>();
			if (CedantsData)
			{
				validFilterParameters = _cedantRepository.GetFilterParameters();
				validSortParameters = _cedantRepository.GetSortParameters();
			}
			criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
		}
		#endregion

		#region Public Methods
		public bool CedantHasReinsuranceCedantGroup(int? cedantid)
		{
			var cedant = _cedantRepository.Get(c => c.Cedantid == cedantid);
			bool result = (cedant?.Parentgrouptypeid == CompanyCategory.ReinsuranceCedantGroup);
			return result;
		}


		#endregion

	}
}
