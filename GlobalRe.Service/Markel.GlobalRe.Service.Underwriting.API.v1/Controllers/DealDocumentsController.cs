using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;
using System.Web.Configuration;


namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.DealsRoutePrefix)]
    public class DealDocumentsController : DealBaseApiController<DealDocuments, BLL_KeyDocuments>
    {
        public DealDocumentsController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route("{dealnumber}/keydoctypes/{getDocTypes}")]
        [ResponseType(typeof(ResponseItem<DealDocuments>))]
        [HttpGet]
        public IHttpActionResult Get(string dealnumber, Boolean getDocTypes = false)
        {
            if (!dealnumber.IsInteger() || dealnumber.IsNullOrEmpty() || !EntityManager.CheckIfDealExistsInSystem(Int32.Parse(dealnumber))) throw new NotFoundAPIException("Invalid deal number");
            getDocTypes = !getDocTypes ? false : true;

            return GetResponse(EntityManager.GetKeyDocuments(filenumber: dealnumber, producer: null, isDocTypeStructure: getDocTypes));
        }


        [Route("{dealnumber}/documents")]
        [HttpGet]
        public IHttpActionResult Get(string dealnumber)
        {
            if (!dealnumber.IsInteger() || dealnumber.IsNullOrEmpty() || !EntityManager.CheckIfDealExistsInSystem(Int32.Parse(dealnumber))) throw new NotFoundAPIException("Invalid deal number");

            var tokenResponse = EntityManager.GetRegisterToken(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], WebConfigurationManager.AppSettings["DmsApiUser"], WebConfigurationManager.AppSettings["DmsApiPwd"]);
            var tokenKey = tokenResponse.IsSuccessStatusCode
                ? (string)((dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(tokenResponse.Content.ReadAsStringAsync().Result)).access_token
                : null;
            var documentResponse = EntityManager.GetDocumentSchema(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], tokenKey, dealnumber);

            return documentResponse.IsSuccessStatusCode
                ? ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.DeserializeObject(new Newtonsoft.Json.Linq.JArray(Newtonsoft.Json.Linq.JObject.Parse(documentResponse.Content.ReadAsStringAsync().Result)).ToString())))
                : ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't retrive document details from dealnumber " + dealnumber));
        }


        [Route("{dealnumber}/documents/{documentid}/{filecontents}")]
        [HttpGet]
        public IHttpActionResult Get(string dealnumber, string documentid, Boolean filecontents = false)
        {
            if (!dealnumber.IsInteger() || dealnumber.IsNullOrEmpty() || !EntityManager.CheckIfDealExistsInSystem(Int32.Parse(dealnumber))) throw new NotFoundAPIException("Invalid deal number");

            var tokenResponse = EntityManager.GetRegisterToken(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], WebConfigurationManager.AppSettings["DmsApiUser"], WebConfigurationManager.AppSettings["DmsApiPwd"]);
            var tokenKey = tokenResponse.IsSuccessStatusCode
                ? (string)((dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(tokenResponse.Content.ReadAsStringAsync().Result)).access_token
                : null;
            filecontents = !filecontents ? false : true;
            var documentResponse = EntityManager.GetDocuments(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], tokenKey, documentid, filecontents);

            return documentResponse.IsSuccessStatusCode
                ? ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.DeserializeObject(documentResponse.Content.ReadAsStringAsync().Result)))
                : ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't retrive document details from document id " + documentid));
        }


        [Route("{dealnumber}/documents/{documentid}/pages/{pagenumber}")]
        [HttpGet]
        public IHttpActionResult Get(string dealnumber, string documentid, int pagenumber)
        {
            if (!dealnumber.IsInteger() || dealnumber.IsNullOrEmpty() || !EntityManager.CheckIfDealExistsInSystem(Int32.Parse(dealnumber))) throw new NotFoundAPIException("Invalid deal number");

            var tokenResponse = EntityManager.GetRegisterToken(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], WebConfigurationManager.AppSettings["DmsApiUser"], WebConfigurationManager.AppSettings["DmsApiPwd"]);
            var tokenKey = tokenResponse.IsSuccessStatusCode
                ? (string)((dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(tokenResponse.Content.ReadAsStringAsync().Result)).access_token
                : null;
            var documentResponse = EntityManager.GetDocumentContent(WebConfigurationManager.AppSettings["DmsApiBaseUrl"], tokenKey, documentid, true, pagenumber);
            Newtonsoft.Json.Linq.JObject jsonObject = Newtonsoft.Json.Linq.JObject.Parse(documentResponse.Content.ReadAsStringAsync().Result);
            jsonObject["Files"] = new Newtonsoft.Json.Linq.JArray(jsonObject["Files"][pagenumber - 1]);

            return documentResponse.IsSuccessStatusCode
                ? ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObject.ToString())))
                : ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Couldn't retrive page details from document id " + documentid));
            }


        //[Route("{dealnumber}/documents/{getDocTypes}")]
        //[ResponseType(typeof(ResponseItem<DealDocuments>))]
        //[HttpPost]
        //public IHttpActionResult Post(DealDocuments requestKeyDocuments)
        //{
        //        return CreatedResponse(EntityManager.AddKeyDocuments(requestKeyDocuments.ToBLLModel()));
        //    }

        //[Route("{dealnumber}/documents/{getDocTypes}")]
        //[ResponseType(typeof(ResponseItem<DealDocuments>))]
        //[HttpDelete]
        //public IHttpActionResult Delete(DealDocuments requestKeyDocuments)
        //{
        //        return CreatedResponse(EntityManager.DeleteKeyDocuments(requestKeyDocuments.ToBLLModel()));
        //    }


        protected override Enum PrimaryEntityType => EntityType.FileNumber;

        protected override DealDocuments ToApiModel(BLL_KeyDocuments entity)
        {
            //if (entity == null) return null;
            return new DealDocuments(entity);
        }

    }
}