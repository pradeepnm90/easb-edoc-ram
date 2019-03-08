using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Markel.Pricing.Service.Infrastructure.Attributes
{
    public class TrackingActionFilterAttribute : ActionFilterAttribute  
    {
        private object ServiceMetricsLock = new object();
        private ServiceMetrics ServiceMetrics { get; set; }

        private ServiceMetrics GetServiceMetrics(string serviceName)
        {
            lock (ServiceMetricsLock)
            {
                if (ServiceMetrics == null)
                {
                    ServiceMetrics = new ServiceMetrics(serviceName);
                }

                return ServiceMetrics;
            }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            System.Diagnostics.Debug.WriteLine($"{request.Method} - {request.RequestUri.OriginalString}");

            HttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor;
            string serviceName = actionDescriptor.ControllerDescriptor.ControllerName;
            string methodName = actionDescriptor.ActionName;

            GetServiceMetrics(serviceName).StartServiceCall(methodName);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            HttpActionDescriptor actionDescriptor = actionExecutedContext.ActionContext.ActionDescriptor;
            string serviceName = actionDescriptor.ControllerDescriptor.ControllerName;
            string methodName = actionDescriptor.ActionName;
            Exception exception = actionExecutedContext.Exception;
            string userHostAddress = GetCallingIPAddress(actionExecutedContext.Request);

            GetServiceMetrics(serviceName).EndServiceCall(methodName, exception, userHostAddress);            
        }

        private static string GetCallingIPAddress(HttpRequestMessage request)
        {
            try
            {
                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    var httpContext = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                    if (httpContext != null)
                    {
                        return httpContext.Request.UserHostAddress;
                    }
                }

                return "LOCAL";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}