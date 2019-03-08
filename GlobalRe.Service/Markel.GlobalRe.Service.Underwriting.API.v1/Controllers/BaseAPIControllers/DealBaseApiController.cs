using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
	[ExcludeFromCodeCoverage]
    public abstract class DealBaseApiController<API_CLASS, BLL_CLASS> : BaseEntityApiController<API_CLASS, IDealAPIManager, BLL_CLASS>
    where API_CLASS : BaseApiModel<BLL_CLASS>
    where BLL_CLASS : IMessageEntity
    {
        /// <summary>
        /// Initializes user identity from user name, environment, and domain name in the request header. 
        /// </summary>
        public DealBaseApiController(IUserManager userManager, IDealAPIManager dealAPIManager)
            : base(userManager, dealAPIManager) { }

        protected override IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_CLASS entity)
        {
            if (entity == null)
            {
                //TODO-PS: Revisit

                //// Initialize Pricing Analysis Manager when the Entity is NULL. Required for OPTIONS.
                //int? pricingAnalysisId = RouteHelper.GetIDFromUrl(Request.RequestUri, RouteHelper.RelativePathPricingAnalyses);
                //if (pricingAnalysisId.HasValue)
                //    EntityManager.Get(pricingAnalysisId.Value, false);
            }

            EntityAction entityActions = EntityManager.GetEntityActions(entity);
            if (entityActions != null)
            {
                return RouteHelper.BuildLinks(basePath, primaryEntityType, entityActions);
            }

            return null;
        }
    }
}