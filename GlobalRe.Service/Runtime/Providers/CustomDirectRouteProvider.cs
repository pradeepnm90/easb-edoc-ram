using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Markel.Pricing.Service.Runtime.Providers
{
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        /// <summary>
        /// Overwrites default Direct Rout behavior
        /// </summary>
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            // Inherit route attributes decorated on base class controller's actions
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }
}