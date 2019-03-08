using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.LookupsDeclineDealStatusesLookupRoutePrefix)]
    public class DeclineDealStatusesLookupController : BaseLookupsController
    {
        public DeclineDealStatusesLookupController(IUserManager userManager, IDealStatusesLookupManager dealStatusesLookupManager) : base(userManager, dealStatusesLookupManager) { }
    }
}
