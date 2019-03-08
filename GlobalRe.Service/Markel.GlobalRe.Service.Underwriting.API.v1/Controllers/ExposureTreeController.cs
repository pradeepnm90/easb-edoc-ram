using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
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
    [RoutePrefix(RouteHelper.LookupsExposureTreeRoutePrefix)]
    public class ExposureTreeController : DealBaseApiController<ExposureTree, BLL_ExposureTree>
    {
        public ExposureTreeController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<ExposureTree>))]
        [HttpGet]
        public IHttpActionResult GetGlobalReExposureTree()
        {
            return this.GetResponse(EntityManager.GetGlobalReExposureTree());
        }

        protected override Enum PrimaryEntityType => EntityType.ExposureTree;

        protected override ExposureTree ToApiModel(BLL_ExposureTree entity)
        {
            if (entity == null) return null;
            return new ExposureTree(entity);
        }
    }
}
