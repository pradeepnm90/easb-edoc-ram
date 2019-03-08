using Microsoft.Owin;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public static class LoggerHelper
    {
        #region Private

        private static readonly string HttpContextBaseKey = "MS_HttpContext";
        private static readonly string HttpOwinContextKey = "MS_OwinContext";

        private static HttpContextWrapper ContextWrapper(this HttpRequestMessage request)
        {
            HttpContextWrapper context = null;
            if (request != null && request.Properties.ContainsKey(HttpContextBaseKey))
            {
                context = request.Properties[HttpContextBaseKey] as HttpContextWrapper;
            }
            return (context != null && context.Request != null)? context : null;
        }

        #endregion Private

        #region Public Methods

        public static LogModel GetLogModel(this HttpRequestMessage request, string message, string applicationIdentifier)
        {
            return new LogModel(
                applicationIdentifier: applicationIdentifier,
                message: message,

                clientBrowser: request.GetBrowserInfo(),
                clientIp: request.GetClientIP(),
                webServer: request.GetHostInfo(),
                url: request.GetUrl(),
                urlReferrer: GetReferer(request)
            );
        }

        public static string GetBrowserInfo(this HttpRequestMessage request)
        {
            var context = request.ContextWrapper();
            return (context != null && context.Request.Browser != null) ? string.Format("{0}-{1}", context.Request.Browser.Browser, context.Request.Browser.Version) : string.Empty;
        }

        /// <summary>
        /// Gets client IP address from request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIP(this HttpRequestMessage request)
        {
            string clientIP = string.Empty;
            var context = request.ContextWrapper();

            if (context != null)
            {
                clientIP = context.Request.UserHostAddress;
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

        public static string GetHostInfo(this HttpRequestMessage request)
        {
            var context = request.ContextWrapper();
            return (context != null) ? context.Request.UserHostName : string.Empty;
        }

        public static string GetReferer(this HttpRequestMessage request)
        {
            var context = request.ContextWrapper();
            return (context != null && context.Request.UrlReferrer != null) ? context.Request.UrlReferrer.AbsoluteUri : string.Empty;
        }

        public static string GetUrl(this HttpRequestMessage request)
        {
            if (request == null || request.RequestUri == null) return string.Empty;

            return $"{request.Method}: {request.RequestUri.AbsoluteUri}";
        }

        #endregion
    }
}
