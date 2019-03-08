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
    public class DealCoveragesManager : BaseGlobalReManager<BLL_DealCoverages>, IDealCoveragesManager
    {
        #region Private Variable 

        private IEntityLockManager _dealLockManager;

        private IDealCoveragesRepository _dealCoveragesRepository;
        //private IDealCoveragesManager _dealCoveragesManager;
        private IDealCoveragesTransformationManager _dealCoveragesTransformationManager;

        private enum FilterParameters
        {
            Dealnum
        }
        #endregion

        #region Constructors

        public DealCoveragesManager(IUserManager userManager,
                            ICacheStoreManager cacheStoreManager,
                            ILogManager logManager,
                            IDealCoveragesRepository dealCoveragesRepository,
                            IEntityLockManager dealLockManager,
                            IDealCoveragesTransformationManager dealCoveragesTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _dealCoveragesRepository = ValidateRepository(dealCoveragesRepository);
            _dealLockManager = ValidateManager(dealLockManager);
            _dealCoveragesTransformationManager = ValidateManager(dealCoveragesTransformationManager);
        }

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_DealCoverages entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
            //entityActionTypes.Add(EntityActionType.Action_Update);

            return new EntityAction(
               entityType: EntityType.DealCoverages,
               entityId: entity.Dealnum,
               entityActionTypes: entityActionTypes
           );
        }

       
        public EntityResult<IEnumerable<BLL_DealCoverages>> GetDealCoverages(int dealNumber)
        {
            //if (!dealNumber.IsNumeric()) throw new IllegalArgumentAPIException("Invalid or Missing deal number");
            if (dealNumber <= 0 ) throw new IllegalArgumentAPIException("Invalid deal number");
            var data = _dealCoveragesRepository.Get(FilterDealNumber(dealNumber));
            //if (data == null) throw new NotFoundAPIException("No coverages found for deal #" + dealNumber);
            return new EntityResult<IEnumerable<BLL_DealCoverages>>(_dealCoveragesTransformationManager.Transform(_dealCoveragesRepository.GetDealCoverages(dealNumber)));
        }

        #endregion

        #region Private Methods

        private IPaginatedList<BLL_DealCoverages> Transform<T>(IPaginatedList<T> dbResults) where T : class
        {
            var results = new PaginatedList<BLL_DealCoverages>()
            {
                PageCount = dbResults.PageCount,
                PageIndex = dbResults.PageIndex,
                PageSize = dbResults.PageSize,
                TotalRecordCount = dbResults.TotalRecordCount,
                Items = dbResults.Items.Select(s => s is grs_VGrsDealCoverage ? _dealCoveragesTransformationManager.Transform(s as grs_VGrsDealCoverage) : _dealCoveragesTransformationManager.Transform(s as grs_VGrsDealCoverage)).ToList()
            };

            return results;
        }

        
        private Expression<Func<grs_VGrsDealCoverage, bool>> FilterDealNumber(int dealNumber)
        {
            return s => s.Dealnum == dealNumber;
        }


        public IPaginatedList<BLL_DealCoverages> Search(SearchCriteria criteria)
        {
            bool CoveragesData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.Dealnum);
            return CoveragesData ? Transform(_dealCoveragesRepository.Search(criteria)) : Transform(_dealCoveragesRepository.Search(criteria));
        }

        private void ValidateSearchCriteria(bool CoveragesData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (CoveragesData)
            {
                validFilterParameters = _dealCoveragesRepository.GetFilterParameters();
                validSortParameters = _dealCoveragesRepository.GetSortParameters();
            }
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
