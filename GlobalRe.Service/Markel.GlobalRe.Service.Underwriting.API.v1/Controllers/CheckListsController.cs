using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Exceptions;
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
using System.Text.RegularExpressions;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;




namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.DealsRoutePrefix)]
    public class CheckListsController : DealBaseApiController<ChkCategoryTree, BLL_ChkCategoryTree>
    {
        public CheckListsController(IUserManager userManager, IDealAPIManager dealAPIManager) : base(userManager, dealAPIManager) { }
        
        [Route("{dealnumber}/checklists")]
        [ResponseType(typeof(ResponseItem<ChkCategoryTree>))]
        [HttpGet]
        public IHttpActionResult Get(int dealnumber)
        {
            if (dealnumber > 0)
            {
                return GetResponse(EntityManager.GetAllDealChecklists(dealnumber));
            }
            else
            {
                return BadRequest("Invalid value in Parameter");
            }
        }

        [Route("{dealnumber}/checklists/{checklistnum}")]
        [ResponseType(typeof(ResponseItem<ChkCategoryTree>))]
        [HttpPut]
        public IHttpActionResult Put(int dealnumber, int checklistnum, ChkCategoryTree checklist)
        {
            if (checklist == null)
            {
                return BadRequest("No Parameter found");
            }

            if (dealnumber < 1)
            {
                return BadRequest("Invalid value in Deal Number");
            }


            if (checklist.DealNumber != 0)
            {
                if(checklist.DealNumber!=dealnumber)
                {
                    return BadRequest("JSON Input and Route value of Deal Number are not equal");
                }
            }

            if (checklistnum < 1)
            {
                return BadRequest("Invalid value in checklist number");
            }
            checklist.DealNumber = dealnumber;

            BLL_CheckListParameter bLL_CheckListParameter = new BLL_CheckListParameter();

            bLL_CheckListParameter.Dealnumber = dealnumber;
            bLL_CheckListParameter.Checklistnum = checklistnum;

            if (checklist.Checklists != null)
            {
                if(checklist.Checklists[0].ChkListNum != 0)
                {
                    if(checklist.Checklists[0].ChkListNum != checklistnum)
                    {
                        return BadRequest("JSON Input and Route value of CheckList Number are not equal");
                    }
                }

                if(checklist.Checklists[0].Checked==null)
                {
                    return BadRequest("Checked parameter is required");
                }

                bLL_CheckListParameter.check = checklist.Checklists[0].Checked;
                bLL_CheckListParameter.PersonId = checklist.Checklists[0].PersonId;
                bLL_CheckListParameter.CompletedDateTime = checklist.Checklists[0].CheckedDateTime;
                bLL_CheckListParameter.Notes = checklist.Checklists[0].Note;
                checklist.Checklists[0].ChkListNum = checklistnum;
            }
            else
            {
                return BadRequest("Invalid input value");
            }
            if (checklist.EntityNum > 2 || checklist.EntityNum < 0)
            {
                return BadRequest("Invalid value EntityNumber");
            }
            bLL_CheckListParameter.Entitynum = checklist.EntityNum;
            EntityResult<BLL_ChkCategoryTree> chklistdetail = EntityManager.UpdateCheckList(bLL_CheckListParameter);
            if(chklistdetail ==null)
            {
                Result deletchecklist = new Result(new Information("CheckList", "Successfully Deleted"));
                return DeleteResponse(deletchecklist);
            }
            return OkResponse(chklistdetail);
        }

        protected override Enum PrimaryEntityType => EntityType.Checklists;

        protected override ChkCategoryTree ToApiModel(BLL_ChkCategoryTree entity)
        {
            if (entity == null) return null;
            return new ChkCategoryTree(entity);
        }

    }
}