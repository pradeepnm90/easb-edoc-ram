using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Markel.Pricing.Service.Infrastructure.Filters
{
    public class TrackingActionFilterAttribute : ActionFilterAttribute  
    {
        private Dictionary<string, ServiceMetrics> _serviceMetrics = new Dictionary<string, ServiceMetrics>();

        private ServiceMetrics GetServiceMetrics(string serviceName)
        {
            lock (_serviceMetrics)
            {
                if (!_serviceMetrics.ContainsKey(serviceName))
                {
                    _serviceMetrics.Add(serviceName, new ServiceMetrics(serviceName));
                }

                return _serviceMetrics[serviceName];
            }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
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
            string userHostAddress = GetCallingIPAddress(actionExecutedContext.Request);

            TransactionResult result = TransactionResult.Success;
            if (actionExecutedContext.Exception != null)
            {
                result = TransactionResult.Error;
            }

            GetServiceMetrics(serviceName).EndServiceCall(methodName, result, userHostAddress);            
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