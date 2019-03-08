using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
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
    public class DealLocksController : DealBaseApiController<EntityLock, BLL_EntityLock>
    {
        public DealLocksController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

		protected override Enum PrimaryEntityType => EntityType.Deals;

		[Route(RouteHelper.DealLocksPrefix)]
		[ResponseType(typeof(ResponseItem<EntityLock>))]
		[HttpGet]
		public IHttpActionResult GetLocks(int dealNumber)
		{
			var response = EntityManager.GetDealLocks(dealNumber);
			return (response != null) ? GetResponse(response) : OkResponse();
		}

		[Route(RouteHelper.DealLocksPrefix)]
		[ResponseType(typeof(ResponseItem<EntityLock>))]
		[HttpPost]
		public IHttpActionResult Lock(int dealNumber)
		{
			return (EntityManager.LockDeal(dealNumber)) ? StatusCode(HttpStatusCode.OK) : StatusCode(HttpStatusCode.Conflict);
		}

		[Route(RouteHelper.DealLocksPrefix)]
		[ResponseType(typeof(ResponseItem<EntityLock>))]
		[HttpDelete]
		public IHttpActionResult Unlock(int dealNumber)
		{
			return EntityManager.UnlockDeal(dealNumber) ? StatusCode(HttpStatusCode.OK) : StatusCode(HttpStatusCode.ExpectationFailed);
		}

		protected override EntityLock ToApiModel(BLL_EntityLock entity)
		{
			return new EntityLock(entity);
		}
	}
}