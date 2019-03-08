using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Underwriting.Service.BLL.Interfaces;
using Markel.GlobalRe.Underwriting.Service.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
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
    [RoutePrefix(RouteHelper.DealsCoverageRoutePrefix)]
    public class DealsCoverageController : BaseApiController<DealCoverages, BLL_DealCoverages>
    {
        public DealsCoverageController(IUserManager userManager, IGlobalReAPIManager globalReAPIManager) : base(userManager, globalReAPIManager) { }

        [Route(RouteHelper.DealNumber)]
        [ResponseType(typeof(ResponseItem<DealCoverages>))]
        [HttpGet]
        public IHttpActionResult Get(int dealNumber)
        {
            if (dealNumber == 0) return StatusCode(HttpStatusCode.BadRequest);
            if (dealNumber < 0) return StatusCode(HttpStatusCode.NotAcceptable);
            //if (!dealNumber.IsNumeric()) return StatusCode(HttpStatusCode.BadRequest);

            EntityResult<IEnumerable<BLL_DealCoverages>> response = EntityManager.GetDealCoverages(dealNumber);
            //if (response.Data.IsNullOrEmpty())
            //{
            //    return StatusCode(HttpStatusCode.NotFound);
            //}
            return this.GetResponse(response);
        }

        protected override Enum PrimaryEntityType => EntityType.DealCoverages;

        protected override DealCoverages ToApiModel(BLL_DealCoverages entity)
        {
            //if (entity == null) return null;
            return new DealCoverages(entity);
        }
    }
}
