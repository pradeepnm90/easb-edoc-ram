using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class PersonsManager : BaseGlobalReManager<BLL_Person>, IPersonsManager
    {
        #region Private Variable 

        private IPersonRepository _personRepository;
        private IPersonTransformationManager _personTransformationManager;
        private enum FilterParameters
        {
            PersonId
        }
        #endregion
        public PersonsManager(IUserManager userManager,
                              ICacheStoreManager cacheStoreManager,
                              ILogManager logManager,
                              IPersonRepository personRepository,
                              IPersonTransformationManager personTransformationManager) : base(userManager, cacheStoreManager, logManager)
        {
            _personRepository = personRepository ?? throw new NullReferenceException(typeof(IPersonRepository).ToString());
            _personTransformationManager = personTransformationManager ?? throw new NullReferenceException(typeof(IPersonTransformationManager).ToString());
        }

		public EntityResult<BLL_Person> GetPerson(int personId)
		{
			TbPerson personData = _personRepository.GetPerson(personId);
			if (personData == null) throw new NotFoundAPIException("Person is not found");
			return new EntityResult<BLL_Person>(_personTransformationManager.Transform(personData));
		}

		public IPaginatedList<BLL_Person> Search(SearchCriteria criteria)
        {
            GetSearchCriteria(criteria);
            ValidateSearchCriteria(criteria);
			var data = _personRepository.Search(criteria);
			return Transform(_personRepository.Search(criteria));
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private void ValidateSearchCriteria(SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            validFilterParameters = _personRepository.GetFilterParameters();
            validSortParameters = _personRepository.GetSortParameters();

            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }

        private IPaginatedList<BLL_Person> Transform<T>(IPaginatedList<T> dbResults) where T : class
        {
            var results = new PaginatedList<BLL_Person>()
            {
                PageCount = dbResults.PageCount,
                PageIndex = dbResults.PageIndex,
                PageSize = dbResults.PageSize,
                TotalRecordCount = dbResults.TotalRecordCount,
                Items = dbResults.Items.Select((s) => _personTransformationManager.Transform(s as TbPerson)).ToList()
            };

            return results;
        }

        private void GetSearchCriteria(SearchCriteria criteria)
        {
            var isExist = criteria.Parameters.ToList().Find((u) => u.Name.Equals("PersonId", StringComparison.OrdinalIgnoreCase)
                && Convert.ToInt32(u.Value) > 0);

            if (isExist == null)
                criteria.Parameters.ToList().Find((u) => u.Name.Equals("PersonId", StringComparison.OrdinalIgnoreCase)).Value = UserIdentity.NameId;
        }

		#region Entity Actions
		public EntityAction GetEntityActions(BLL_Person entity)
		{
			List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
			//entityActionTypes.Add(EntityActionType.Action_Update);

			return new EntityAction(
			   entityType: EntityType.Persons,
			   entityId: entity.PersonId,
			   entityActionTypes: entityActionTypes
		   );
		}

		#endregion
	}
}
