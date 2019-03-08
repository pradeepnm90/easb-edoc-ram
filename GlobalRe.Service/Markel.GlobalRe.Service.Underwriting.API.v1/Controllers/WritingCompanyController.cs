using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
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
    [RoutePrefix(RouteHelper.WritingCompanyRoutePrefix)]
    public class WritingCompaniesController : DealBaseApiController<WritingCompany, BLL_WritingCompany>
    {

        public WritingCompaniesController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route(RouteHelper.GRSFlag)]
        [ResponseType(typeof(ResponseItem<WritingCompany>))]
        [HttpGet]
        public IHttpActionResult Get(Boolean GRSFlag)
        {
            //if (GRSFlag.IsNullOrEmpty()) return StatusCode(HttpStatusCode.BadRequest);
            if (!GRSFlag) throw new NotAllowedAPIException("Invalid parameter request 'FALSE' ... ");
            //if (GRSFlag.IsNumeric()) return StatusCode(HttpStatusCode.BadRequest);
            return GetResponse(EntityManager.GetWritingCompany(GRSFlag));
        }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<WritingCompany>))]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return GetResponse(EntityManager.GetWritingCompany());
        }


        protected override Enum PrimaryEntityType => EntityType.WritingCompany;

        protected override WritingCompany ToApiModel(BLL_WritingCompany entity)
        {
            //if (entity == null) return null;
            return new WritingCompany(entity);
        }

    }
}