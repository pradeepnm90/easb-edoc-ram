using Markel.GlobalRe.WebServer.Extensions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Markel.GlobalRe.WebServer.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("whoami")]
    public class WhoAmIController : ApiController
    {
        [Route("")]
        [ResponseType(typeof(string))]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            string userName = ApiUtilities.GetUserName(User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK, userName);
        }
    }
}
