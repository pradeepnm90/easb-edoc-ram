using Markel.Pricing.Service.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.WebApi
{
    public class RestClient : IDisposable
    {
        #region Private Members

        private readonly HttpClient _client;
        private readonly Dictionary<string, string> _token;

        private readonly string USER_ENVIRONMENT_HEADER_KEY = "UserEnvironment";
        private readonly string USER_ENVIRONMENT_HEADER_FORMAT = "{0}|{1}|{2}";

        #endregion

        #region Constructor

        public RestClient(string baseAddress, UserIdentity identity) : this(baseAddress, identity.UserName, identity.DomainName, identity.EnvironmentName) { }

        public RestClient(string baseAddress, string userName, string domainName, string environmentName)
        {
            // Verify required parameters
            if (string.IsNullOrEmpty(userName)) { throw new NullReferenceException("User Name cannot be null!"); }
            if (string.IsNullOrEmpty(domainName)) { throw new NullReferenceException("Domain Name cannot be null!"); }
            if (string.IsNullOrEmpty(environmentName)) { throw new NullReferenceException("Environment Name cannot be null!"); }

            #region Client Setup

            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            _client = new HttpClient(handler);
            _client.Timeout = new TimeSpan(0, 10, 0);
            _client.BaseAddress = new Uri(baseAddress);

            #endregion Client Setup

            #region Authentication

            // Add UserEnvironment header to authorization request 
            _client.DefaultRequestHeaders.Add(USER_ENVIRONMENT_HEADER_KEY, string.Format(USER_ENVIRONMENT_HEADER_FORMAT, userName, environmentName, domainName));

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
            postData.Add(new KeyValuePair<string, string>("username", userName));
            postData.Add(new KeyValuePair<string, string>("password", ""));

            HttpContent content = new FormUrlEncodedContent(postData);
            var tokenResult = _client.PostAsync(baseAddress + "/token", content).Result.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON into a Dictionary<string, string>
            _token = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResult);

            Success = false;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if ( !string.IsNullOrEmpty(_token["access_token"]))
            {
                var header = new AuthenticationHeaderValue("Bearer", _token["access_token"]);

                _client.DefaultRequestHeaders.Authorization = header;
            }

            #endregion Authentication
        }

        #region Dispose

        private bool _disposed;

        ~RestClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free other managed objects that implement IDisposable only
                if (_client != null)
                    _client.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Dispose

        #endregion

        #region Public Properties

        public bool Success { get; private set; }
        public string Response { get; private set; }
        private object Result { get; set; }

        public Output GetResult<Output>()
        {
            if (Result == null)
            {
                return default(Output);
            }
            else
            {
                return (Output)Result;
            }
        }

        public string AccessToken
        {
            get
            {
                if (_token != null && _token.ContainsKey("access_token"))
                    return "Bearer " + _token["access_token"];
                return null;
            }
        }

        #endregion

        #region Public Methods

        public void ExecuteGet<Output>(string resource)
        {
            HttpResponseMessage response = _client.GetAsync(resource).Result;
            PrepareResult<Output>(response);
        }

        //public void ExecuteSyncGet(string resource)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(resource);
        //    var response = (HttpWebResponse)request.GetResponse()
        //    prepareSynchResult(response);
        //}

        //private void prepareSynchResult(HttpWebResponse response)
        //{
        //    var responseValue = string.Empty;

        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
        //        throw new ApplicationException(message);
        //    }

        //    // grab the response
        //    using (var responseStream = response.GetResponseStream())
        //    {
        //        if (responseStream != null)
        //            using (var reader = new StreamReader(responseStream))
        //            {
        //                responseValue = reader.ReadToEnd();
        //            }
        //    }

        //    if (response.IsSuccessStatusCode)
        //    {
        //        Success = true;
        //        Response = Convert.ToString(response);
        //        Result = response.Content.ReadAsAsync<Output>().Result;
        //    }
        //    else
        //    {
        //        Success = false;
        //        Result = default(Output);
        //        Response = Convert.ToString(response);
        //    }

        //}

        public void ExecutePost<Input, Output>(string resource, Input model)
        {
            var task = Task.Factory.StartNew(() => _client.PostAsJsonAsync(resource, model));
            task.Wait();
            if (task.IsCompleted)
            {
                HttpResponseMessage response = task.Result.Result;
                PrepareResult<Output>(response);
            }
        }

        public void ExecutePut<Input, Output>(string resource, Input model)
        {
            var task = Task.Factory.StartNew(() => _client.PutAsJsonAsync(resource, model));
            task.Wait();
            if (task.IsCompleted)
            {
                HttpResponseMessage response = task.Result.Result;
                PrepareResult<Output>(response);
            }
        }

        public void ExecuteDelete<Output>(string resource)
        {
            HttpResponseMessage response = _client.DeleteAsync(resource).Result;
            PrepareResult<Output>(response);
        }

        public string MakeRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);


            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";
            //request.Headers.Add("Authorization", "Basic QmVuLlN0b3JlOjI5YmYxNmNjODVjYWEwODUyZjQ5MDQyM2NiNTFlNzFhMzI4NGJmMGM=");


            //if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            //{
            //    var encoding = new UTF8Encoding();
            //    var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
            //    request.ContentLength = bytes.Length;

            //    using (var writeStream = request.GetRequestStream())
            //    {
            //        writeStream.Write(bytes, 0, bytes.Length);
            //    }
            //}

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                var responseStream = response.GetResponseStream();
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        public string GetArrayQueryString<T>(string queryStringVariableName, IList<T> array)
        {
            var sb = new StringBuilder();

            foreach (var arr in array)
                sb.Append(string.Format("{0}={1}",queryStringVariableName, arr.ToString()));

            return sb.ToString();
        }

        #endregion

        #region Private Methods

        private void PrepareResult<Output>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                Success = true;
                Response = Convert.ToString(response);
                Result = response.Content.ReadAsAsync<Output>().Result;
            }
            else
            {
                Success = false;
                Response = response.Content.ReadAsStringAsync().Result;
                Result = default(Output);
            }
        }

        #endregion

    }
}
