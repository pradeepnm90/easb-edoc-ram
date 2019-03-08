using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
	[Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.CedantsRoutePrefix)]
    public class CedantsController : BaseEntityApiController<Cedant, ICompanyAPIManager, BLL_Cedant>
    {

        public CedantsController(IUserManager userManager, ICompanyAPIManager companyAPIManager) : base(userManager, companyAPIManager) { }

        [Route(RouteHelper.CedantName)]
        [ResponseType(typeof(ResponseItem<Cedant>))]
        [HttpGet]
        public IHttpActionResult Get(string CedantName)
        {
            if (CedantName.IsNullOrEmpty()) return StatusCode(HttpStatusCode.BadRequest);
            if (CedantName.Length<2) return StatusCode(HttpStatusCode.BadRequest);  //throw new NotAllowedAPIException("Search aborted! Cedant Search requires 2 or more chars... ");
            return GetResponse(EntityManager.GetCedants(CedantName, null, null, null, null));
        }

        [Route("")]
        [ResponseType(typeof(ResponseCollection<Cedant>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] CedantsSearchCriteria criteria)
        {
            if (criteria == null)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            if (criteria.CedantName.IsNullOrEmpty() && criteria.ParentGroupName.IsNullOrEmpty() && criteria.CedantId.IsNullOrEmpty() && criteria.ParentGroupId.IsNullOrEmpty() && criteria.LocationId.IsNullOrEmpty())
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            if (!criteria.CedantName.IsNullOrEmpty())
            {
                if(criteria.CedantName.Length < 2 && criteria.ParentGroupName.IsNullOrEmpty())
                    return StatusCode(HttpStatusCode.BadRequest);  //throw new NotAllowedAPIException("Search aborted! Cedant name search requires 2 or more chars ... ");
            }

            if (!criteria.ParentGroupName.IsNullOrEmpty())
            {
                if(criteria.CedantName.IsNullOrEmpty() && criteria.ParentGroupName.Length < 2)
                    return StatusCode(HttpStatusCode.BadRequest);  //throw new NotAllowedAPIException("Search aborted! Cedant parent group name search requires 2 or more chars ... ");
            }

            if(!criteria.CedantName.IsNullOrEmpty() && !criteria.ParentGroupName.IsNullOrEmpty())
            {
                if (criteria.CedantName.Length < 2 || criteria.ParentGroupName.Length <2)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }

            }

            return this.GetResponse(EntityManager.GetCedants(criteria.CedantName, criteria.ParentGroupName, criteria.CedantId, criteria.ParentGroupId, criteria.LocationId));
        }

		protected override IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_Cedant entity = null)
		{
			EntityAction entityActions = EntityManager.GetEntityActions(entity);
			if (entityActions != null)
			{
				return RouteHelper.BuildLinks(basePath, primaryEntityType, entityActions);
			}

			return null;
		}

		protected override Enum PrimaryEntityType => EntityType.Cedants;

        protected override Cedant ToApiModel(BLL_Cedant entity)
        {
            //if (entity == null) return null;
            return new Cedant(entity);
        }

    }
}