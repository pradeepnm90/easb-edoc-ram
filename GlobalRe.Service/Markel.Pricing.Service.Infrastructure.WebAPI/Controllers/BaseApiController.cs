using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace Markel.Pricing.Service.Infrastructure.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        #region Protected Properties

        protected IUserManager UserManager { get; private set; }

        protected string BasePath
        {
            get
            {
                // Alternate Path: Request.GetRequestContext().VirtualPathRoot
                string path = Request.RequestUri.AbsolutePath;
                if (path.EndsWith("/"))
                {
                    // Removed Trailing '/' for proper URL response
                    path = path.Substring(0, path.Length - 1);
                }

                if (path.StartsWith("/"))
                {
                    // Removes leading '/' for proper URL response
                    path = path.Substring(1);
                }
                return path;
            }
        }

        protected string BaseSearchPath
        {
            get
            {
                // Base Path for the Entity is one level above the search endpoint
                return BasePath.Replace("/search", "");
            }
        }

        protected string ParentPath
        {
            get
            {
                string path = BasePath;
                int length = path.LastIndexOf('/');
                return path.Substring(0, length);
            }
        }

        #endregion Protected Properties

        #region Constructor

        /// <summary>
        /// Initializes user identity from user name, environment, and domain name in the request header. 
        /// </summary>
        public BaseApiController(IUserManager userManager)
        {
            if (userManager == null) throw new NullReferenceException("IUserManager");

            UserManager = userManager;
        }

        #endregion

        #region Override Methods

        protected override StatusCodeResult StatusCode(HttpStatusCode status)
        {
            return base.StatusCode(status);
        }

        #endregion

        #region Methods

        protected UserIdentity UserIdentity
        {
            get
            {
                return UserManager.UserIdentity;
            }
        }

        /// <summary>
        /// Validates manager is not null and returns instance of manager
        /// </summary>
        /// <typeparam name="T">Type of Manager</typeparam>
        /// <param name="manager">Instance of Manager</param>
        /// <returns>Original instance of Manager</returns>
        protected T ValidateManager<T>(T manager) where T : IBaseManager
        {
            return ValidateObject(manager);
        }

        /// <summary>
        /// Validates object is not null and returns object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="instance">Instance of Object</param>
        /// <returns>Original instance of object</returns>
        protected T ValidateObject<T>(T instance)
        {
            if (instance == null) throw new NullReferenceException(typeof(T).ToString());
            return instance;
        }

        #region Single Entity Responses

        protected NegotiatedContentResult<ResponseItem<T>> OkResponse<T>(T entity, IList<Link> links, IEnumerable<Message> messages) where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var responseItem = new ResponseItem<T>(Request.RequestUri.PathAndQuery, entity, links, messages);
            return Content(statusCode, responseItem);
        }

        protected NegotiatedContentResult<ResponseItem<T>> CreatedResponse<T>(T entity, IList<Link> links, IEnumerable<Message> messages) where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.Created;
            var responseItem = new ResponseItem<T>(Request.RequestUri.PathAndQuery, entity, links, messages);
            return Content(statusCode, responseItem);
        }

        protected NegotiatedContentResult<ResponseItem<T>> GetResponse<T>(T entity, IList<Link> links, IEnumerable<Message> messages) where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var responseItem = new ResponseItem<T>(Request.RequestUri.PathAndQuery, entity, links, messages);
            return Content(statusCode, responseItem);
        }

        #endregion Single Entity Responses

        protected NegotiatedContentResult<ResponsePaginatedCollection<T>> SearchResponse<T>(IEnumerable<Item<T>> results, IPagination pagination, Item<T> summaryResult = null)
            where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            int limit = pagination.PageSize;
            int offset = pagination.PageNumber * limit;

            var responseCollection = new ResponsePaginatedCollection<T>(Request.RequestUri.PathAndQuery, results, summaryResult, pagination.TotalRecordCount, offset, limit);
            return Content(statusCode, responseCollection);
        }

        protected NegotiatedContentResult<ResponseCollection<T>> GetResponse<T>(IEnumerable<Item<T>> results)
            where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var responseCollection = new ResponseCollection<T>(Request.RequestUri.PathAndQuery, results);
            return Content(statusCode, responseCollection);
        }

        protected NegotiatedContentResult<ResponseLookup<T>> LookupResponse<T>(IEnumerable<T> results)
            where T : IBaseApiModel
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var responseCollection = new ResponseLookup<T>(Request.RequestUri.PathAndQuery, results);
            return Content(statusCode, responseCollection);
        }

        protected NegotiatedContentResult<ResponseOptions> OptionsResponse(IList<Link> links)
        {
            ResponseOptions response = new ResponseOptions(Request.RequestUri.PathAndQuery, links);
            return Content(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Returns a list of messages to the client with an Http Status Code of OK (200)
        /// </summary>
        /// <param name="messages">List of Messages</param>
        /// <returns></returns>
        protected NegotiatedContentResult<Response> MessageResponse(IEnumerable<Message> messages)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var response = new Response(Request.RequestUri.PathAndQuery, messages);
            return Content(statusCode, response);
        }

        protected void ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                ModelErrorCollection modelErrorList = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).FirstOrDefault();
                ModelError modelError = modelErrorList?.FirstOrDefault();
                if (modelError != null)
                {
                    throw modelError.Exception;
                }
            }
        }

        #endregion Methods
    }
}
