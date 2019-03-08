using Markel.Pricing.Service.Infrastructure;
using Markel.Pricing.Service.Infrastructure.Dependency;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Filters;
using Markel.Pricing.Service.Runtime.GlobalExceptionHandling;
using Markel.Pricing.Service.Runtime.Providers;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Markel.Pricing.Service.Runtime
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new DependencyManager(container);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Add Request ID to header in all responses
            config.Filters.Add(new RequestIDHeaderFilter());

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            //config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Convert all response objects to Camel Case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
