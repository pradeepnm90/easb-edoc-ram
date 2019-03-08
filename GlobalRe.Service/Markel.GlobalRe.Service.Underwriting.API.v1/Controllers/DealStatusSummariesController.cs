using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.DealStatusSummaryRoutePrefix)]
    public class DealStatusSummariesController : DealBaseApiController<DealStatusSummary, BLL_DealStatusSummary>
    {
        public DealStatusSummariesController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<DealStatusSummary>))]
        [HttpGet]
        public IHttpActionResult GetAll([FromUri] DealSearchCriteria criteria)
        {
            if (criteria == null) criteria = new DealSearchCriteria();
            return this.GetResponse(EntityManager.GetAllDealStatusSummaries(criteria.ToSearchCriteria()));
        }

        protected override Enum PrimaryEntityType => EntityType.DealStatusSummaries;

        protected override DealStatusSummary ToApiModel(BLL_DealStatusSummary entity)
        {
            if (entity == null) return null;
            return new DealStatusSummary(entity);
        }
    }
}