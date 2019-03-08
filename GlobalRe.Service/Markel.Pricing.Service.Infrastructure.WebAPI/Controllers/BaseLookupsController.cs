using Markel.Pricing.Service.Infrastructure.Attributes;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Markel.Pricing.Service.Infrastructure.Controllers
{
    [TrackingActionFilter]
    public abstract class BaseLookupsController : BaseApiController
    {
        private ILookupsManager _lookupsManager;

        protected T GetLookupManager<T>() where T : ILookupsManager { return (T)_lookupsManager; }

        public BaseLookupsController(IUserManager userManager, ILookupsManager lookupsManager) : base(userManager)
        {
            _lookupsManager = ValidateManager(lookupsManager);
        }

        /// <summary>
        /// Call to get items from a generic Lookups Controller. Uses Unity in the child class to get the proper Lookups Manager
        /// </summary>
        /// <param name="group">Optional Group Filter</param>
        /// <returns></returns>
        [HttpGet, Route("")] // Requires CustomDirectRouteProvider in Runtime to override inherit:true so this works
        [ResponseType(typeof(ResponseCollection<GroupNameValuePair>))]
        public IHttpActionResult Get([FromUri]List<string> group)
        {
            IEnumerable<GroupNameValuePair> lookupValues = _lookupsManager.GetAll().Where(l => l.IsActive).Select(l => new GroupNameValuePair(l));
            if (group?.Count > 0)
            {
                lookupValues = lookupValues.Where(l => group.Contains(l.Group, StringComparer.CurrentCultureIgnoreCase));
            }
            return LookupResponse(lookupValues.ToList());
        }
    }
}
