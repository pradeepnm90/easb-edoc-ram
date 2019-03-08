using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Markel.Pricing.Service.Runtime.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("")]
    public class DiagnosticsController : ApiController
    {
        /// <summary>
        /// Ping API
        /// </summary>
        /// <returns>Server Date & Time</returns>
        [Route("ping")]
        [ResponseType(typeof(DateTime))]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, DateTime.Now);
        }

        [Route("version")]
        [ResponseType(typeof(String))]
        [HttpGet]
        public HttpResponseMessage Version()
        {
            return Request.CreateResponse(HttpStatusCode.OK, IOHelper.GetServerVersion());
        }

        [Route("environment")]
        [ResponseType(typeof(String))]
        [HttpGet]
        public HttpResponseMessage Environment()
        {
            return Request.CreateResponse(HttpStatusCode.OK, MarkelConfiguration.EnvironmentName);
        }

        [Route("endpoints")]
        [ResponseType(typeof(IList<dynamic>))]
        [HttpGet]
        public HttpResponseMessage Endpoints()
        {
            IList<ApiEndpoint> apiEndpoints = new List<ApiEndpoint>();

            ApiExplorer apiExplorer = new ApiExplorer(ControllerContext.Configuration);
            foreach (var endpoint in apiExplorer.ApiDescriptions)
            {
                ApiEndpoint apiEndpoint = apiEndpoints.FirstOrDefault(a => a.RouteTemplate.Equals(endpoint.Route.RouteTemplate));
                if (apiEndpoint == null)
                {
                    apiEndpoint = new ApiEndpoint(endpoint.Route.RouteTemplate);
                    apiEndpoints.Add(apiEndpoint);
                }

                apiEndpoint.AddMethod(endpoint.HttpMethod.Method);
            }

            return Request.CreateResponse(HttpStatusCode.OK, apiEndpoints.OrderBy(a => a.RouteTemplate));
        }
    }
}
