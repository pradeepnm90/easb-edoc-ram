using Microsoft.Owin;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        // Constants
        private static readonly string HttpContextBaseKey = "MS_HttpContext";
        private static readonly string HttpOwinContextKey = "MS_OwinContext";

        /// <summary>
        /// Gets client IP address from request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(this HttpRequestMessage request)
        {
            string clientIP = null;

            if (request != null && request.Properties.ContainsKey(HttpContextBaseKey))
            {
                clientIP = ((HttpContextWrapper)request.Properties[HttpContextBaseKey]).Request.UserHostAddress;
            }
            else if (request != null && request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                clientIP = ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;
            }
            else if (request != null && request.Properties.ContainsKey(HttpOwinContextKey))
            {
                clientIP = ((OwinContext)request.Properties[HttpOwinContextKey]).Request.RemoteIpAddress;
            }
            else if (HttpContext.Current != null)
            {
                clientIP = HttpContext.Current.Request.UserHostAddress;
            }

            return clientIP;
        }
    }
}
