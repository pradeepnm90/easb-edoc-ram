using Markel.GlobalRe.WebServer.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Markel.GlobalRe.WebServer.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("serviceinfo")]
    public class ServiceInfoController : ApiController
    {
        private static string VERSION = ApiUtilities.GetVersion();
        private static string ENVIRONMENT_NAME = ApiUtilities.GetConfigSetting("EnvironmentName");
        private static string TOKEN_ENDPOINT = ApiUtilities.GetConfigSetting("TokenEndpoint");
        private static string IS_IMPERSONATION_ALLOWED = ApiUtilities.GetConfigSetting("IsImpersonationAllowed");

        [Route("")]
        [ResponseType(typeof(Dictionary<string, string>))]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            Dictionary<string, string> response = new Dictionary<string, string>()
            {
                { "Version", VERSION },
                { "EnvironmentName", ENVIRONMENT_NAME },
                { "TokenEndpoint", TOKEN_ENDPOINT },
                { "IsImpersonationAllowed", IS_IMPERSONATION_ALLOWED }
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
