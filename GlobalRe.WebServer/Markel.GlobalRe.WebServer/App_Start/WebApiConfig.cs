using Markel.GlobalRe.WebServer.Extensions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Markel.GlobalRe.WebServer
{
    public static class WebApiConfig
    {
        private static string CORS_ORIGINS = ApiUtilities.GetConfigSetting("CorsOrigins", false);

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            if (!string.IsNullOrWhiteSpace(CORS_ORIGINS))
            {
                EnableCorsAttribute cors = new EnableCorsAttribute(
                    CORS_ORIGINS,                   // Origin
                    "Accept, Origin, Content-Type", // Request headers
                    "GET, POST"                     // HTTP methods
                );
                cors.PreflightMaxAge = 600;
                cors.SupportsCredentials = true;
                config.EnableCors(cors);
            }

            // Web API Routes
            config.MapHttpAttributeRoutes();
        }
    }
}
