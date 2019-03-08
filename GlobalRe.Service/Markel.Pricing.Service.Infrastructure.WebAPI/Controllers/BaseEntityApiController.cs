using Markel.Pricing.Service.Infrastructure.Attributes;
using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Enums;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ObjectMapper;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Markel.Pricing.Service.Infrastructure.Traits;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.Pricing.Service.Infrastructure.Controllers
{
    [TrackingActionFilter]
    public abstract class BaseEntityApiController<API_CLASS, BLL_MANAGER, BLL_CLASS> : BaseApiController
        where API_CLASS : BaseApiModel<BLL_CLASS>
        where BLL_MANAGER : IBaseManager
        where BLL_CLASS : IMessageEntity
    {
        #region Constants

        protected const string ERROR_ENTITY_ID_MISMATCH = "Entity ID '{0}' does not match URL ID '{1}'!";

        private static Dictionary<Type, ComparisonType> DEFAULT_COMPARISON_TYPE = new Dictionary<Type, ComparisonType>()
        {
            { typeof(string), ComparisonType.Contains },
            { typeof(bool), ComparisonType.Equals },
        };

        #endregion Constants

        #region Member Variables

        protected BLL_MANAGER EntityManager { get; private set; }

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes user identity from user name, environment, and domain name in the request header. 
        /// </summary>
        public BaseEntityApiController(IUserManager userManager, BLL_MANAGER entityManager) : base(userManager)
        {
            if (entityManager == null) throw new NullReferenceException(typeof(BLL_MANAGER).ToString());

            EntityManager = entityManager;
        }

        #endregion

        #region Search

        protected IHttpActionResult Search<T>(string filter, T criteria, Func<SearchCriteria, IEnumerable<string>, IEnumerable> distinct, Func<SearchCriteria, IPaginatedList<BLL_CLASS>> search, Func<SearchCriteria, MemoryStream> export) where T : APISearchCriteria, new()
        {
            criteria = criteria ?? new T();
            if (filter.HasValue())
            {
                criteria.FilterParameters.AddRange(JsonConvert.DeserializeObject<List<FilterParameter>>(filter));
            }            

            IEnumerable<string> columns = criteria.Columns?.Select(c => PropertyMapper.GetPropertyName<API_CLASS, BLL_CLASS>(c));
            SearchCriteria searchCriteria = criteria.ToSearchCriteria().MapProperties<API_CLASS, BLL_CLASS>();

            // Set default filter operator based on type
            foreach (FilterParameter filterParameter in searchCriteria.Parameters.Where(f => f.FilterOperator == ComparisonType.None))
            {
                Type propertyType = filterParameter.Name.GetPropertyType<BLL_CLASS>();
                filterParameter.FilterOperator = (DEFAULT_COMPARISON_TYPE.ContainsKey(propertyType)) ? DEFAULT_COMPARISON_TYPE[propertyType] : ComparisonType.Equals;
            }

            if (export != null && criteria.ExportType == ExportTypeEnum.Excel)
            {
                // NOTE: 10/30/18 - This code is not currently used. In fact, it does not work because the MemoryStream is disposed
                // at a lower level. The client currently requests all records and creates an Excel document via ag-grid.
                // We should review in the future with ticket ENTPRC-4890
                MemoryStream memoryStream = export(searchCriteria);
                if (memoryStream == null) { return NotFound(); }

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(memoryStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //response.Content.Headers.ContentLength = memoryStream.Length;
                return ResponseMessage(response);
            }
            else if (distinct != null && criteria.IsDistinct)
            {
                IEnumerable searchResults = distinct(searchCriteria, columns);
                return Ok(searchResults);
            }
            else
            {
                IPaginatedList<BLL_CLASS> searchResults = search(searchCriteria);
                return SearchResponse(searchResults);
            }
        }

        #endregion Search

        #region Helper Methods

        protected IHttpActionResult SearchResponse(IPaginatedList<BLL_CLASS> searchResults)
        {
            // Convert to Output and attach Links
            IList<Item<API_CLASS>> data = ToApiModel(searchResults.Items);

            Item<API_CLASS> summaryData = new Item<API_CLASS>(ToApiModel(searchResults.SummaryItem),null);
            // Build response object
            return SearchResponse<API_CLASS>(data, searchResults, summaryData);
        }

        protected IHttpActionResult GetResponse<T>(EntityResult<IEnumerable<T>> result) where T : BLL_CLASS
        {
            if (result == null) throw new NullReferenceException(string.Format("Unexpected null value for EntityResult<{0}>", typeof(BLL_CLASS)));
            if (result.Data == null) return NotFound();

            var data = result.Data.Select(e => new Item<API_CLASS>(
                data: ToApiModel(e),
                links: GetLinks(BasePath, PrimaryEntityType, e),
                messages: e?.Messages)
            ).ToList();

            return base.GetResponse<API_CLASS>(data);
        }

        protected IHttpActionResult GetResponse<T>(IEnumerable<T> result) where T : BLL_CLASS
        {
            if (result == null) throw new NullReferenceException(string.Format("Unexpected null value for {0}", typeof(BLL_CLASS)));

            var data = result.Select(e => new Item<API_CLASS>(
                data: ToApiModel(e),
                links: GetLinks(BasePath, PrimaryEntityType, e),
                messages: e?.Messages)
            ).ToList();

            return base.GetResponse<API_CLASS>(data);
        }

        protected IHttpActionResult GetResponse<T>(EntityResult<T> entityResult) where T : class, BLL_CLASS
        {
            if (entityResult == null) throw new NullReferenceException(string.Format("Unexpected null value for EntityResult<{0}>", typeof(BLL_CLASS)));
            return GetResponse(entityResult.Data);
        }

        protected IHttpActionResult GetResponse<T>(T entity) where T : BLL_CLASS
        {
            if (entity == null) throw new NullReferenceException(string.Format("Unexpected null value for {0}", typeof(BLL_CLASS)));

            return base.GetResponse<API_CLASS>(
                entity: ToApiModel(entity),
                links: GetLinks(ParentPath, PrimaryEntityType, entity),
                messages: entity.Messages
            );
        }

        protected IHttpActionResult CreatedResponse<T>(EntityResult<T> entityResult) where T : class, BLL_CLASS
        {
            if (entityResult == null) throw new NullReferenceException(string.Format("Unexpected null value for EntityResult<{0}>", typeof(BLL_CLASS)));
            return CreatedResponse(entityResult.Data);
        }

        protected IHttpActionResult CreatedResponse<T>(T entity) where T : BLL_CLASS
        {
            if (entity == null) throw new NullReferenceException(string.Format("Unexpected null value for {0}", typeof(BLL_CLASS)));

            return base.CreatedResponse<API_CLASS>(
                entity: ToApiModel(entity),
                links: GetLinks(BasePath, PrimaryEntityType, entity),
                messages: entity.Messages
            );
        }

        protected IHttpActionResult OkResponse<T>(EntityResult<T> entityResult) where T : class, BLL_CLASS
        {
            if (entityResult == null) throw new NullReferenceException(string.Format("Unexpected null value for EntityResult<{0}>", typeof(BLL_CLASS)));
            return OkResponse(entityResult.Data);
        }

        protected IHttpActionResult OkResponse<T>(T entity) where T : BLL_CLASS
        {
            if (entity == null) throw new NullReferenceException(string.Format("Unexpected null value for {0}", typeof(BLL_CLASS)));

            return base.OkResponse<API_CLASS>(
                entity: ToApiModel(entity),
                links: GetLinks(ParentPath, PrimaryEntityType, entity),
                messages: entity.Messages
            );
        }

        protected IHttpActionResult OkResponse()
        {
            return base.OkResponse<API_CLASS>(
                entity: null,
                links: GetLinks(ParentPath, PrimaryEntityType, default(BLL_CLASS)),
                messages: null
            );
        }

        protected IHttpActionResult DeleteResponse(Result result)
        {
            return base.MessageResponse(result.Messages);
        }

        protected NegotiatedContentResult<ResponseOptions> OptionsResponse()
        {
            return base.OptionsResponse(GetLinks(ParentPath, null, default(BLL_CLASS)));
        }

        protected T GetDynamicValue<T>(dynamic entity, string propertyName, T defaultValue = default(T))
        {
            foreach (var item in entity.Children())
            {
                if (string.Equals(item.Name.ToString().Trim(), propertyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (item.Value != null)
                        return Convert.ChangeType(item.Value, typeof(T));
                }
            }
            return defaultValue;
        }

        #region Abstract Methods

        protected abstract Enum PrimaryEntityType { get; }

        protected abstract IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_CLASS entity);

        protected abstract API_CLASS ToApiModel(BLL_CLASS entity);

        protected virtual IList<Item<API_CLASS>> ToApiModel(IList<BLL_CLASS> searchResults)
        {
            return searchResults.AsParallel().AsOrdered().Select(e => new Item<API_CLASS>(
                data: ToApiModel(e),
                links: GetLinks(BaseSearchPath, PrimaryEntityType, e),
                messages: e.Messages)
            ).ToList();
        }

        #endregion Abstract Methods

        #endregion Helper Methods
    }
}
