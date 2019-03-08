using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
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
    public class DealsController : DealBaseApiController<Deal, BLL_Deal>
    {
        public DealsController(IUserManager userManager, IDealAPIManager dealReAPIManager) : base(userManager, dealReAPIManager) { }

        /// <summary>
        [Route("")]
        [ResponseType(typeof(ResponseCollection<Deal>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] DealSearchCriteria criteria)
        {
            if (criteria == null) criteria = new DealSearchCriteria();

            return this.GetResponse(EntityManager.GetDeals(criteria.ToSearchCriteria()));
        }

        [Route(RouteHelper.DealNumber)]
        [ResponseType(typeof(ResponseItem<Deal>))]
        [HttpGet]
        public IHttpActionResult Get(int dealNumber)
        {
            return GetResponse(EntityManager.GetDeal(dealNumber));
        }


        [Route("{dealNumber}")]
        [ResponseType(typeof(ResponseItem<Deal>))]
        [HttpPut]
        public IHttpActionResult Put(int dealNumber, Deal deal)
        {
            if (deal == null) return StatusCode(HttpStatusCode.NoContent); // Return missing data http response
            if (deal.DealNumber != dealNumber) return StatusCode(HttpStatusCode.Ambiguous); // Return data mismatch http response
			return OkResponse(EntityManager.UpdateDeal(deal.ToBLLModel()));
        }

		//      [Route("{dealNumber}")]
		//      [ResponseType(typeof(ResponseItem<Deal>))]
		//[HttpPatch] 
		//      public IHttpActionResult Patch([FromUri]int dealNumber, [FromBody]dynamic deal)
		//      {
		//          if (deal == null) throw new NoContentAPIException();
		//          if (deal.DealNumber == null) deal.DealNumber = dealNumber;
		//          if (deal.DealNumber != dealNumber) throw new IllegalArgumentAPIException(ERROR_ENTITY_ID_MISMATCH, deal.DealNumber, dealNumber);
		//          BLL_Deal blldeal = ApiEntityConversion.ToBLLModel<Deal, BLL_Deal>(deal);
		//	if (deal.LockDeal != null && deal.LockDeal == true)
		//		return OkResponse(EntityManager.Lock(blldeal.Dealnum));
		//	return OkResponse(new EntityResult<BLL_Deal>(blldeal));
		//      }

		protected override IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_Deal entity = null)
		{
			EntityAction entityActions = EntityManager.GetEntityActions(entity);
			if (entityActions != null)
			{
				return RouteHelper.BuildLinks(basePath, primaryEntityType, entityActions);
			}

			return null;
		}

		protected override Enum PrimaryEntityType => EntityType.Deals;

        protected override Deal ToApiModel(BLL_Deal entity)
        {
            if (entity == null) return null;
            return new Deal(entity);
        }
    }
}