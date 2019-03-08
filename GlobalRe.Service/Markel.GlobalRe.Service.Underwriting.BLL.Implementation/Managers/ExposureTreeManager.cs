using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using System;
using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class ExposureTreeManager : BaseGlobalReManager<BLL_ExposureTree>, IExposureTreeManager
    {

        #region Private Variable 


        private IExposureTreeRepository _exposureTreeRepository;
        private IExposureTreeTransformationManager _exposureTreeTransformationManager;


        #endregion

        public ExposureTreeManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager
            , IExposureTreeRepository exposureTreeRepository, IExposureTreeTransformationManager exposuretreeTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _exposureTreeRepository = ValidateRepository(exposureTreeRepository);
            _exposureTreeTransformationManager = ValidateManager(exposuretreeTransformationManager);
        }


        public EntityResult<IEnumerable<BLL_ExposureTree>> GetGlobalReExposureTree()
        { //need caching
            #region Caching

            IList<BLL_ExposureTree> cachedExposureTree = CacheManager.GetItem<IList<BLL_ExposureTree>>("ExposureTree", (action) =>
            {
                return new CacheItem(_exposureTreeTransformationManager.Transform(_exposureTreeRepository.GetGlobalReExposureTree()));
            }, false);
            return new EntityResult<IEnumerable<BLL_ExposureTree>>(cachedExposureTree);

            #endregion
        }
    }
}
