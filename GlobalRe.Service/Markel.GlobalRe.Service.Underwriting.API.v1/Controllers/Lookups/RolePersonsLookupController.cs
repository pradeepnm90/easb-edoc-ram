using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.LookupsRolePersonsRoutePrefix)]
    public class RolePersonsLookupController : BaseApiController
    {
        private IRolePersonsManager _rolePersonsManager { get; }
        public RolePersonsLookupController(IUserManager userManager, IRolePersonsManager rolePersonsManager) : base(userManager)
        {
            _rolePersonsManager = rolePersonsManager ?? throw new NullReferenceException(typeof(IRolePersonsManager).ToString());
        }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<NameValuePair>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] RolePersonSearchCriteria searchCriteria)
        {
            var lookupValues = searchCriteria?.Roles == null ? _rolePersonsManager.GetAll() : _rolePersonsManager.GetByGroupName(searchCriteria.Roles);
            var lookupValueslist = lookupValues.Select(l =>  new GroupNameValuePair(l)).ToList();
            if (lookupValueslist.Count() == 0)
                return StatusCode(HttpStatusCode.NotFound);
            else
                return LookupResponse(lookupValueslist);
        }

    }
}