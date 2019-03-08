using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
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
    [RoutePrefix(RouteHelper.UserviewRoutePrefix)]
    public class UserViewController : BaseEntityApiController<UserView, IUserPreferencesAPIManager, BLL_UserView>
    {
        const string screenname = "GRS.UW_Workbench";
        enum viewname { };
        public UserViewController(IUserManager userManager, IUserPreferencesAPIManager userViewAPIManager) : base(userManager, userViewAPIManager) { }

        [Route("")]
        // [Route(RouteHelper.ViewID)]
        [ResponseType(typeof(ResponseCollection<UserView>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] UserViewSearchCriteria userViewSearchCriteria)
        {
            //int screenid = userViewSearchCriteria.ScreenName;
           
            if(userViewSearchCriteria==null || string.IsNullOrWhiteSpace(userViewSearchCriteria.ScreenName))
            {
                return BadRequest("No Parameter found");
            }
        //    string screenid = userViewSearchCriteria.ScreenName;
            return this.GetResponse(EntityManager.GetByUserViewSreenName(userViewSearchCriteria.ScreenName));
           // return null;
        }

        [Route(RouteHelper.ViewID)]
        [ResponseType(typeof(ResponseItem<UserView>))]
        [HttpGet]
        public IHttpActionResult Get(int viewId)
        {
            return GetResponse(EntityManager.GetUserViewbyId(viewId));
        }


        [Route(RouteHelper.ViewID)]
        [ResponseType(typeof(ResponseItem<UserView>))]
        [HttpDelete]
        public IHttpActionResult Delete(int viewId, [FromBody] dynamic userviewdelete)
        {
            // return DeleteResponse(EntityManager.DeleteUserViewbyId(viewId));
            BLL_UserViewDelete blluserview = null;

            if (userviewdelete == null)
            {
                return BadRequest("Json Input not found");
            }
            else
            {
                blluserview = ApiEntityConversion.ToBLLModel<UserViewDelete, BLL_UserViewDelete>(userviewdelete);
                if(blluserview.ScreenName==null)
                {
					return BadRequest("Screen name not found");
                }
                else if(!blluserview.ScreenName.ToLower().Equals(screenname.ToLower()))
                {
					return BadRequest("Screen name is not equal to GRS.UW_Workbench");
                }
            }
            return DeleteResponse(EntityManager.DeleteUserViewbyId(viewId, blluserview));
            //return BadRequest("Invalid Input");
        }

        [Route("")]
        [ResponseType(typeof(ResponseItem<UserView>))]
        [HttpPost]
        public IHttpActionResult Post(UserView userview)
        {
            if (userview == null) return StatusCode(HttpStatusCode.NoContent); // Return missing data http response

            if (userview.ViewName == null || userview.ViewName.Equals("") ||
                userview.ScreenName == null || userview.ScreenName.Equals("") )
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }


            if (userview.CustomView ?? true && (
                userview.Layout == null || userview.Layout.Equals("")))
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return CreatedResponse(EntityManager.AddUserView(userview.ToBLLModel()));

        }

        // [Route("")]
        [Route("{viewId}")]
        [ResponseType(typeof(ResponseItem<UserView>))]
        [HttpPut]
       public IHttpActionResult Put(int viewId ,UserView userView)
       // public IHttpActionResult Put( UserView userView)
        {
            if (userView == null)
            {
                return BadRequest("No Parameter found");
            }
               // return StatusCode(HttpStatusCode.NoContent); // Return missing data http response
           if (userView.ViewId != viewId)
            {
                return BadRequest("ViewId is not equal with URL");
            }
            if ((userView.ViewId < 1) || ((userView.Layout == null || userView.Layout.Equals("")) && (userView.Default == null)))
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

           return OkResponse(EntityManager.UpdateUserView(userView.ToBLLModel()));
        }
		protected override IList<Link> GetLinks(string basePath, Enum primaryEntityType, BLL_UserView entity = null)
		{
			EntityAction entityActions = EntityManager.GetEntityActions(entity);
			if (entityActions != null)
			{
				return RouteHelper.BuildLinks(basePath, primaryEntityType, entityActions);
			}

			return null;
		}
		protected override Enum PrimaryEntityType => EntityType.UserViews;

        protected override UserView ToApiModel(BLL_UserView entity)
        {
            if (entity == null) return null;
            return new UserView(entity);
        }
    }
}
