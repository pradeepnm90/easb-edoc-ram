using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
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
    [RoutePrefix(RouteHelper.LookupsDealStatusesLookupRoutePrefix)]
    public class DealStatusesLookupController : BaseApiController
    {
        private IDealStatusesLookupManager _dealStatusesManager { get; }

        public DealStatusesLookupController(IUserManager userManager, IDealStatusesLookupManager dealStatusesLookupManager) : base(userManager)
        {
            _dealStatusesManager = dealStatusesLookupManager ?? throw new NullReferenceException(typeof(IDealStatusesLookupManager).ToString());
        }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<NameValuePair>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] DealStatusesSearchCriteria searchCriteria)
        {
            var lookupValues = searchCriteria?.StatusGroup == null ? _dealStatusesManager.GetAll() : _dealStatusesManager.GetByConfig(searchCriteria.StatusGroup);
            
            var lookupValueslist = lookupValues.Select(l => new GroupNameValuePair(l)).ToList();
            if (lookupValueslist.Count == 0)
                return StatusCode(HttpStatusCode.NotFound);
            else
                return LookupResponse(lookupValueslist);
        }
    }
}
