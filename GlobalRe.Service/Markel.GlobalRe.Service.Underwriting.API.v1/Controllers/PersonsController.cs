using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.PersonRoutePrefix)]
    public class PersonsController : BaseEntityApiController<Person, IPersonAPIManager, BLL_Person>
    {
        #region constructor
        public PersonsController(IUserManager userManager, IPersonAPIManager personReAPIManager)
            : base(userManager, personReAPIManager) { }
        #endregion

        #region overrides
        protected override Enum PrimaryEntityType => EntityType.Persons;

        protected override Person ToApiModel(BLL_Person entity)
        {
            if (entity == null) return null;
            return new Person(entity);
        }

		protected override IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_Person entity = null)
		{
			EntityAction entityActions = EntityManager.GetEntityActions(entity);
			if (entityActions != null)
			{
				return RouteHelper.BuildLinks(basePath, primaryEntityType, entityActions);
			}

			return null;
		}


		#endregion

		#region actions
		[Route(RouteHelper.PersonId)]
		[ResponseType(typeof(ResponseItem<Person>))]
        [HttpGet]
        public IHttpActionResult Get(int personid)
        {
            return GetResponse(EntityManager.GetPerson(personid));
        }

		[Route("")]
		[ResponseType(typeof(ResponseCollection<Person>))]
		[HttpGet]
		public IHttpActionResult Search([FromUri] PersonSearchCriteria criteria)
		{
			// Search Persons
			var searchResults = EntityManager.SearchPersons(criteria.ToSearchCriteria());
			return SearchResponse(searchResults);
		}
		#endregion

	}
}