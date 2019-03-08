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
    [RoutePrefix(RouteHelper.LookupsContractTypesRoutePrefix)]
    public class ContractTypesLookupController : DealBaseApiController<ContractTypes, BLL_ContractTypes>
    {

        public ContractTypesLookupController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<ContractTypes>))]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                EntityResult<IEnumerable<BLL_ContractTypes>> dataresult = EntityManager.GetContractTypes();
                if (dataresult.Data.IsNullOrEmpty())
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                return this.GetResponse(dataresult);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }

        protected override Enum PrimaryEntityType => EntityType.ContractTypes;

        protected override ContractTypes ToApiModel(BLL_ContractTypes entity)
        {
            if (entity == null) return null;
            return new ContractTypes(entity);
        }


    }

}