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
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class BrokerManager : BaseGlobalReManager<BLL_Broker>, IBrokerManager
    {

        #region Private Variable 

        private IBrokerRepository _brokerRepository;
        private IBrokerTransformationManager _brokerTransformationManager;
        #endregion

        #region Constructors

        public BrokerManager(IUserManager userManager,
                            ICacheStoreManager cacheStoreManager,
                            ILogManager logManager,
                            IBrokerRepository brokerRepository,
                            IBrokerTransformationManager brokerTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _brokerRepository = ValidateRepository(brokerRepository);
            _brokerTransformationManager = ValidateManager(brokerTransformationManager);
        }

		#endregion

		#region Public Methods

		public bool BrokerHasReinsuranceBrokerGroup(int? brokerid)
		{
			var broker = _brokerRepository.Get(b => b.Brokerid == brokerid);
			bool result = (broker?.Parentgrouptypeid == CompanyCategory.ReinsuranceBrokerGroup);
			return result;
		}


		#endregion

	}
}
