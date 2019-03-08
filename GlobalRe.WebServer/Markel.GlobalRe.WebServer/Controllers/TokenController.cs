using Markel.GlobalRe.WebServer.Extensions;
using Markel.GlobalRe.WebServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace Markel.GlobalRe.WebServer.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("token")]
    public class TokenController : ApiController
    {
        private static string ENVIRONMENT_NAME = ApiUtilities.GetConfigSetting("EnvironmentName");
        private static string TOKEN_ENDPOINT = ApiUtilities.GetConfigSetting("TokenEndpoint");
        private static string IS_IMPERSONATION_ALLOWED = ApiUtilities.GetConfigSetting("IsImpersonationAllowed");
        private static string PRODUCTION = ApiUtilities.GetConfigSetting("production");

        [Route("")]
        [ResponseType(typeof(Dictionary<string, string>))]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]TokenRequest tokenRequest)
        {
            // Impersonate User is only allowed if configured and NOT in PROD
            string userName = ApiUtilities.GetUserName(User.Identity.Name);
            if (!"PROD".Equals(ENVIRONMENT_NAME) && IS_IMPERSONATION_ALLOWED.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                if (!string.IsNullOrEmpty(tokenRequest?.ImpersonatedUser))
                {
                    userName = tokenRequest.ImpersonatedUser;
                }
            }

            Dictionary<string, string> clientSecret = new Dictionary<string, string>()
            {
                { "Machine", Environment.MachineName.ToUpper() },
                { "Environment", ENVIRONMENT_NAME }
            };

            // Request ID is used to verify the response matches the request
            string requestId = Math.Floor((double)(1 + (new Random()).Next()) * 0x10000).ToString();

            var request = WebRequest.Create(TOKEN_ENDPOINT) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.CookieContainer = new CookieContainer();
            request.Accept = "*/*";
            request.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            request.Headers.Add("X-Request-ID", requestId);
            request.Headers.Add("Client-Secret", clientSecret.EncryptJson<AesManaged>(ENVIRONMENT_NAME));
            string authCredentials = $"grant_type=password&username={userName}";
            byte[] bytes = Encoding.UTF8.GetBytes(authCredentials);
            request.ContentLength = bytes.Length;

            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (WebException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, TOKEN_ENDPOINT);
            }

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    // Verify Request ID
                    if (!requestId.Equals(response.Headers["X-Request-ID"])) return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Server returned a Request ID mismatch!");

                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        string responseJson = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(responseJson)) return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Not Authorized!");
                        var obj = JsonConvert.DeserializeObject<ExpandoObject>(responseJson) as IDictionary<string, Object>;
                        var links = ConfigurationManager.AppSettings.AllKeys
                                                        .ToDictionary(k => k, v => ConfigurationManager.AppSettings[v]).Where(k => k.Key.StartsWith("LINK_"));
                        foreach (KeyValuePair<string, string> link in links)
                        {
                            obj.Add(link.Key, link.Value);
                        }
                        obj.Add("impersonation", IS_IMPERSONATION_ALLOWED);
                        obj.Add("production", PRODUCTION);
                        responseJson = JsonConvert.SerializeObject(obj);

                        object responseObject = JsonConvert.DeserializeObject(responseJson);
                        return Request.CreateResponse(HttpStatusCode.OK, responseObject);
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            return new HttpResponseMessage(errorResponse.StatusCode) { Content = new StringContent(error) };
                        }
                    }
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Failed!");
        }
    }
}
