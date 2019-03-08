using Markel.Pricing.Service.Infrastructure.Dependency;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Filters;
using Markel.Pricing.Service.Infrastructure.Providers;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Markel.Pricing.Service.Infrastructure
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new DependencyManager(container);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Add Request ID to header in all responses
            config.Filters.Add(new RequestIDHeaderFilter());

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Suppress ASP.NET Cancelled Tasks (Known bug in ASP.NET Web API 2)
            config.MessageHandlers.Add(new CancelledTaskBugWorkaroundMessageHandler());

            // Web API configuration and services
            // Documentation: https://msdn.microsoft.com/en-us/magazine/dn532203.aspx
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            MediaTypeHeaderValue appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Convert all response objects to Camel Case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
