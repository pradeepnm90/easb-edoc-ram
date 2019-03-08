using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Providers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Web;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public class HubAuthorizationHandler : HubPipelineModule
    {
        protected override bool OnBeforeAuthorizeConnect(HubDescriptor hubDescriptor, IRequest request)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(request.Url.Query);

            string token = queryString["token"];

            AuthenticationTicket ticket = AuthProvider.BearerOptions.AccessTokenFormat.Unprotect(token);
            if (ticket?.Identity?.IsAuthenticated != true) return false;
            ClaimsIdentity identity = ticket.Identity;

            bool userNameMatch = string.Equals(queryString["userName"], identity.GetClaimValue("userName"));
            bool environmentNameMatch = string.Equals(queryString["environment"], identity.GetClaimValue("environmentName"));

            return userNameMatch && environmentNameMatch;
        }
    }
}
