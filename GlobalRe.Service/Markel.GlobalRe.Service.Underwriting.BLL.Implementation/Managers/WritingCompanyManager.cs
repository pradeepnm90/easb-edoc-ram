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
    public class WritingCompanyManager : BaseGlobalReManager<BLL_WritingCompany>, IWritingCompanyManager
    {

        #region Private Variable 

        private IWritingCompanyRepository _writingCompanyRepository;
        private IWritingCompanyTransformationManager _writingCompanyTransformationManager;

        private enum FilterParameters
        {
            Papernum,
            Active,
            IsGrsDisplay
        }
        #endregion

        #region Constructors

        public WritingCompanyManager(IUserManager userManager,
                            ICacheStoreManager cacheStoreManager,
                            ILogManager logManager,
                            IWritingCompanyRepository writingCompanyRepository,
                            IWritingCompanyTransformationManager writingCompanyTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _writingCompanyRepository = ValidateRepository(writingCompanyRepository);
            _writingCompanyTransformationManager = ValidateManager(writingCompanyTransformationManager);
        }

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_WritingCompany entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
            //entityActionTypes.Add(EntityActionType.Action_Update);

            return new EntityAction(
               entityType: EntityType.WritingCompany,
               entityId: entity.Papernum,
               entityActionTypes: entityActionTypes
           );
        }

        public EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany(Boolean isGRSFlag)
        {
            // loading only GRS specific writing company records  
            return new EntityResult<IEnumerable<BLL_WritingCompany>>(_writingCompanyTransformationManager.Transform(_writingCompanyRepository.GetWritingCompany(isGRSFlag)));
        }

        public EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany()
        {
            //loading all the papers / writing company records exists in ERMS system 
            return new EntityResult<IEnumerable<BLL_WritingCompany>>(_writingCompanyTransformationManager.Transform(_writingCompanyRepository.GetWritingCompany())); 
        }

        #endregion

        #region Private Methods

        private IPaginatedList<BLL_WritingCompany> Transform<T>(IPaginatedList<T> dbResults) where T : class
        {
            var results = new PaginatedList<BLL_WritingCompany>()
            {
                PageCount = dbResults.PageCount,
                PageIndex = dbResults.PageIndex,
                PageSize = dbResults.PageSize,
                TotalRecordCount = dbResults.TotalRecordCount,
                Items = dbResults.Items.Select(s => s is grs_VPaperExt ? _writingCompanyTransformationManager.Transform(s as grs_VPaperExt) : _writingCompanyTransformationManager.Transform(s as grs_VPaperExt)).ToList()
            };

            return results;
        }


        // Adding filter on Paper Number 
        private Expression<Func<grs_VGrsWritingCompany, bool>> FilterWritingCompany(int paperNumber)
        {
            return s => s.Papernum == paperNumber;
        }

        // Adding filter on papernum, isGRSDisplay and Active flag 
        public IPaginatedList<BLL_WritingCompany> Search(SearchCriteria criteria)
        {
            bool CoveragesData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.Papernum);
            ValidateSearchCriteria(CoveragesData, criteria);
            return CoveragesData ? Transform(_writingCompanyRepository.Search(criteria)) : Transform(_writingCompanyRepository.Search(criteria));
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private void ValidateSearchCriteria(bool CoveragesData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (CoveragesData)
            {
                validFilterParameters = _writingCompanyRepository.GetFilterParameters();
                validSortParameters = _writingCompanyRepository.GetSortParameters();
            }
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }
        #endregion

        #region Caching

        private IList<BLL_WritingCompany> LoadCachedWritingCompany()
        {
            //Loading Writing Company Cached Objects/Records
            IList<BLL_WritingCompany> cachedWritingCompany = CacheManager.GetItem<IList<BLL_WritingCompany>>("GRS_WritingCompany", (action) =>
            {
                return new CacheItem(_writingCompanyRepository.GetWritingCompany());
            }, false);
            return new List<BLL_WritingCompany>(cachedWritingCompany);
        }

        private IList<BLL_WritingCompany> CacheWritingCompany()
        {
            //Loading Writing Company Cached Objects/Records
            IList<BLL_WritingCompany> cachedWritingCompany = CacheManager.GetItem<IList<BLL_WritingCompany>>("GRS_WritingCompany", (action) =>
            {
                return new CacheItem(_writingCompanyRepository.GetWritingCompany());
            }, false);
            return new List<BLL_WritingCompany>(cachedWritingCompany);
        }

        private Dictionary<int, List<BLL_WritingCompany>> CachedAllWritingCompany()
        {
            Dictionary<int, List<BLL_WritingCompany>> cachedWritingCompany = CacheManager.GetItem<Dictionary<int, List<BLL_WritingCompany>>>("GRS_WritingCompany", (action) =>
            {
                return new CacheItem(_writingCompanyRepository.GetWritingCompany());
            }, false);

            return new Dictionary<int, List<BLL_WritingCompany>>(cachedWritingCompany);
        }

        private Dictionary<int, List<BLL_WritingCompany>> CachedGRSWritingCompany()
        {
            Dictionary<int, List<BLL_WritingCompany>> cachedWritingCompany = CacheManager.GetItem<Dictionary<int, List<BLL_WritingCompany>>>("GRS_WritingCompany_GRS", (action) =>
            {
                return new CacheItem(_writingCompanyRepository.GetWritingCompany(true));
            }, false);

            return new Dictionary<int, List<BLL_WritingCompany>>(cachedWritingCompany);
        }

        #endregion
    }
}
