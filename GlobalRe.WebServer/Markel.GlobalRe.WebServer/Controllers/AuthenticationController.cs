using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Markel.GlobalRe.WebServer.Controllers
{
    [RoutePrefix("")]
    public class AuthenticationController : ApiController
    {
        // GET /username
        [HttpGet]
        [Route("username")]
        [Authorize] // this one uses Windows Authentication to get Windows User Name
        public UserNameValue GetValues()
        {
            return new UserNameValue() { UserName = User.Identity.Name };
        }
    }

    /// <summary>
    /// Class to return User Name obtained via Windows Authentication.
    /// Can use some formatting cues? (TODO)
    /// </summary>
    public class UserNameValue
    {
        public string UserName { get; set; }
    }
}
