using Markel.Pricing.Service.Infrastructure.Extensions;
using System.Web.Http.Filters;

namespace Markel.Pricing.Service.Infrastructure.Filters
{
    public class RequestIDHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.ApplyRequestIDHeader(actionExecutedContext.Request);
        }
    }
}
