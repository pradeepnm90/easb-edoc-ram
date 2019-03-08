using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
	[Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.DealsRoutePrefix)]
    public class DealCoveragesController : DealBaseApiController<DealCoverages, BLL_DealCoverages>
    {
        public DealCoveragesController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

		[Route("{dealnumber}/coverages")]
        [ResponseType(typeof(ResponseItem<DealCoverages>))]
        [HttpGet]
        public IHttpActionResult Get(int dealNumber)
        {
            return dealNumber <= 0 ? StatusCode(HttpStatusCode.NotAcceptable) : this.GetResponse(EntityManager.GetDealCoverages(dealNumber));
        }

        protected override Enum PrimaryEntityType => EntityType.DealCoverages;

        protected override DealCoverages ToApiModel(BLL_DealCoverages entity)
        {
            //if (entity == null) return null;
            return new DealCoverages(entity);
        }
    }
}
