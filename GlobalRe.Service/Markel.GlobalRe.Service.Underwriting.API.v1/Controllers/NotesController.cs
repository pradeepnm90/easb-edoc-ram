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
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.NotesRoutePrefix)]
    public class NotesController : DealBaseApiController<Notes, BLL_Notes>
    {

        public NotesController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }

        [Route("")]
        //[Route(RouteHelper.DealNotesNumber)]
        [ResponseType(typeof(ResponseCollection<Notes>))]
        [HttpGet]
        public IHttpActionResult Get([FromUri] NotesSearchCriteria criteria)
        {
            //try
            //{
            if (criteria == null)
            {
                return BadRequest("No Parameter found");
            }
            //else if (criteria.DealNumber < 1)
            //{
            //    return StatusCode(HttpStatusCode.BadRequest);
            //}
            else
            {
                //EntityResult<IEnumerable<BLL_DealNotes>> dataresult = EntityManager.GetNotes(criteria.DealNumber);
                //if(dataresult.Data.IsNullOrEmpty())
                //{
                //   return StatusCode(HttpStatusCode.NotFound);
                //}
                //return this.GetResponse(EntityManager.GetNotes(criteria.DealNumber));
                return this.GetResponse(EntityManager.GetNotes(criteria.DealNumber));
            }
            //}
            //catch
            //{
            //    return StatusCode(HttpStatusCode.InternalServerError);
            //}

        }

        [Route(RouteHelper.NoteNumber)]
        [ResponseType(typeof(ResponseItem<Notes>))]
        [HttpGet]
        public IHttpActionResult Get(int noteNumber)
        {
            //   return GetResponse(EntityManager.GetDeal(noteNumber));
            return this.GetResponse(EntityManager.GetNotebyNoteNumber(noteNumber));
        }

        [Route("")]
        [ResponseType(typeof(ResponseItem<Notes>))]
        [HttpPost]
        public IHttpActionResult Post(Notes dealNotes)
        {
            try
            {
                if (dealNotes == null) return StatusCode(HttpStatusCode.NoContent); // Return missing data http response

                if (dealNotes.DealNumber == null || dealNotes.Notetype == null || dealNotes.NoteText == null
                    || dealNotes.NoteText.Equals("") || dealNotes.DealNumber < 1)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }

                return CreatedResponse(EntityManager.AddDealNotes(dealNotes.ToBLLModel()));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [Route("")]
        [ResponseType(typeof(ResponseItem<Notes>))]
        [HttpPut]
        public IHttpActionResult Put(Notes dealNotes)
        {
            if (dealNotes == null) return StatusCode(HttpStatusCode.NoContent); // Return missing data http response
            if (dealNotes.NoteText == null
                || dealNotes.NoteText.Equals("") || dealNotes.Notenum < 1)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            return OkResponse(EntityManager.UpdateDealNotes(dealNotes.ToBLLModel()));
        }
        protected override Enum PrimaryEntityType => EntityType.Notes;

        protected override Notes ToApiModel(BLL_Notes entity)
        {
            if (entity == null) return null;
            return new Notes(entity);
        }


    }

}