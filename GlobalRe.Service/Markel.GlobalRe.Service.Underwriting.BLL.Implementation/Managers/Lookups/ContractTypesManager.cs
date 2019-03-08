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
    public class ContractTypesManager : BaseGlobalReManager<BLL_ContractTypes>, IContractTypesManager
    {
        #region Private Variable 


        private IContractTypesLookupRepository _contractTypesRepository;
        private IContractTypesTransformationManager _contractTypesTransformationManager;



        private enum FilterParameters
        {
            dealnumber
        }
        #endregion

        #region Constructors

        public ContractTypesManager(IUserManager userManager,
              ICacheStoreManager cacheStoreManager,
              ILogManager logManager,
              IContractTypesLookupRepository contractTypesLookupRepository,
              IContractTypesTransformationManager contractTypesTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _contractTypesRepository = ValidateRepository(contractTypesLookupRepository);
            _contractTypesTransformationManager = ValidateManager(contractTypesTransformationManager);

        }


        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_ContractTypes entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
            //entityActionTypes.Add(EntityActionType.Action_Update);

            return new EntityAction(
               entityType: EntityType.ContractTypes,
                entityId: Convert.ToInt32(entity.value),
               entityActionTypes: entityActionTypes
           );
        }


       

        public EntityResult<IEnumerable<BLL_ContractTypes>> GetContractTypes()
        {
            return new EntityResult<IEnumerable<BLL_ContractTypes>>(_contractTypesTransformationManager.Transform(_contractTypesRepository.GetContractTypes()));
        }



        #endregion

        #region Private Methods

        public IPaginatedList<BLL_ContractTypes> Search(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsContractType, bool>> FilterDealNumber(string assumedCededFlag)
        {
            if (assumedCededFlag.Equals("assumed"))
            {
                return s => s.CededFlag == 1;
            }
            else
            {
                return s => s.AssumedFlag == 1;
            }
        }

        private void ValidateSearchCriteria(bool globalReData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (globalReData)
            {
                validFilterParameters = _contractTypesRepository.GetFilterParameters();
                validSortParameters = _contractTypesRepository.GetSortParameters();
            }
            //else
            //{
            //    validFilterParameters = _dealNotesRepository.GetFilterParameters();
            //    validSortParameters = _dealNotesRepository.GetSortParameters();
            //}
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }

        #endregion
    }
}
