using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class CheckListsManager : BaseGlobalReManager<BLL_ChkCategoryTree>, ICheckListsManager
    {
        #region Private Variable 

        private ITblCheckListRepository _CheckListRepository;
        private ICheckListsTransformationManager _CheckListTransformationManager;

        #endregion
		 
        #region Constructors

        public CheckListsManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager
          , ITblCheckListRepository userViewRepository, ICheckListsTransformationManager userViewTransformationManager)
          : base(userManager, cacheStoreManager, logManager)
        {
            _CheckListRepository = ValidateRepository(userViewRepository);
            _CheckListTransformationManager = ValidateManager(userViewTransformationManager);
        }



        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_ChkCategoryTree entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
            //entityActionTypes.Add(EntityActionType.ReferenceEntity);

            return new EntityAction(
               entityType: EntityType.Checklists,
               entityActionTypes: entityActionTypes
           );
        }

        public IPaginatedList<BLL_ChkCategoryTree> Search(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }
      

        public EntityResult<IEnumerable<BLL_ChkCategoryTree>> GetAllDealChecklists(int dealnum)
        {
            var data = _CheckListRepository.GetAllDealChecklists(dealnum);
            if (data.Count() == 0) throw new NotFoundAPIException("Either deal is not available or checklists are not configured for given deal.");
            return new EntityResult<IEnumerable<BLL_ChkCategoryTree>>(_CheckListTransformationManager.Transform(data));
        }


         public EntityResult<BLL_ChkCategoryTree> GetCheckNumByDealChecklists(int dealnum, int checklistnum)
        {
            var data = _CheckListRepository.GetCheckNumByDealChecklists(dealnum, checklistnum);
            if (data.Count() == 0) throw new NotFoundAPIException("Either deal is not available or checklists are not configured for given deal.");
            return new EntityResult<BLL_ChkCategoryTree>(_CheckListTransformationManager.Transform(data, 1));
        }

		private void DeleteCheckListByTaskID(int chklistID, int dealnumber)
        {
            try
            {
                var chklistdata = _CheckListRepository.Get(d => d.Key1 == dealnumber && d.Chklistnum == chklistID);
                if (chklistdata == null)
                {
                    throw new NotFoundAPIException(String.Format("User checklist does not exists with deal number '{0}'", dealnumber));
            }
                _CheckListRepository.Delete(chklistdata);
                _CheckListRepository.Save(chklistdata);
            }
            catch (Exception ex)
            {
                throw new NotFoundAPIException("Input value is not valid");
            }
        }


        private string GetPersonNameByID(int? personid)
        {
            if(personid==null)
            {
                throw new NotFoundAPIException("PersonID is invalid");
        }
            string personName = _CheckListRepository.GetPersonByUserId(Convert.ToInt32(personid));
            if (personName == null)
            {
                throw new NotFoundAPIException("PersonID is invalid");
            }
            return personName;
        }

        public EntityResult<BLL_ChkCategoryTree> UpdateCheckList(BLL_CheckListParameter bll_checklist)
        {
            int workflow = -1;
            try
            {
                int dealcheckedstaus = _CheckListRepository.IsValidDealCheckedStatus(bll_checklist.Dealnumber, bll_checklist.Checklistnum);
                if (dealcheckedstaus == -1)
                {
                    throw new NotFoundAPIException("Deal and Checklistnumber combination are not found");
                }
                int checklistAddstatus = -1;
                TbChklistval chklistvaldata = null;

                if (bll_checklist.check == true && dealcheckedstaus == 0)
                {
                    checklistAddstatus = AddChekcList(bll_checklist);
                    workflow = 1;
                }
                else if (bll_checklist.check == true && dealcheckedstaus == 1)
                {
                    chklistvaldata = _CheckListRepository.Get(d => d.Key1 == bll_checklist.Dealnumber && d.Chklistnum == bll_checklist.Checklistnum);
                    if (chklistvaldata.PersonId == bll_checklist.PersonId)
                    {
                        OnApplyChanges(chklistvaldata, bll_checklist);
                _CheckListRepository.Save(chklistvaldata);
            }
                    else
                    {
                        throw new NotFoundAPIException("Update is failed due to PersonID is different.");
                    }
                    workflow = 2;
                }
                else if (bll_checklist.check == false && dealcheckedstaus == 1)
                {
                    DeleteCheckListByTaskID(bll_checklist.Checklistnum, bll_checklist.Dealnumber);
                    workflow = 3;
                    return null;
                }
                else if (bll_checklist.check == false && dealcheckedstaus == 0)
                {
                    throw new NotFoundAPIException("Checklist number not found");
                }
                if (workflow != -1)
                {
                    return GetCheckNumByDealChecklists(bll_checklist.Dealnumber, bll_checklist.Checklistnum);
                }
                else
                {
                    throw new NotFoundAPIException("Record not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private int AddChekcList(BLL_CheckListParameter bLL_chklist)
        {
            int status = -1;
            try
            {
                TbChklistval tbUserView = new TbChklistval();
                AssignDefaults(tbUserView, bLL_chklist);
                _CheckListRepository.Add(tbUserView);
                _CheckListRepository.Save(tbUserView);
                status = 1;
            }
            catch (Exception ex)
            {
                throw new NotFoundAPIException("Input value is not valid");
            }
            return status;
        }

        private void AssignDefaults(TbChklistval currentval, BLL_CheckListParameter newEntity)
        {
            DateTime dtchekdatetime = DateTime.Now;
            try
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-US", true);
                dtchekdatetime = DateTime.ParseExact(newEntity.CompletedDateTime, "MM/dd/yyyy HH:mm:ss", theCultureInfo);
                newEntity.Completed = dtchekdatetime;
        }
            catch (Exception ex)
        {
                throw new NotFoundAPIException("Invalid Date format to MM/dd/yyyy HH:mm:ss");
            }

            newEntity.PersonName = GetPersonNameByID(newEntity.PersonId);

            currentval.Key1 = newEntity.Dealnumber;
            currentval.Chklistnum = newEntity.Checklistnum;
            currentval.Completed = Convert.ToDateTime(newEntity.Completed);
            currentval.Entitynum = newEntity.Entitynum ?? 1;//default to deal
            currentval.PersonId = newEntity.PersonId;
            currentval.Userid = newEntity.PersonName;
            currentval.Key2 = -1;
            currentval.Key3 = -1;
        }

        private void OnApplyChanges(TbChklistval currentView, BLL_CheckListParameter newEntity)
        {
            newEntity.PersonName = GetPersonNameByID(newEntity.PersonId);
            if(newEntity.Notes == null)
            {
                throw new NotFoundAPIException("Note Parameter is not found");
            }
            else if(newEntity.Notes.Length > 200)
            {
                throw new NotFoundAPIException("Note Character cannot be more than 200");
            }
            else 
            {
                currentView.Notes = newEntity.Notes;
                currentView.Key1 = newEntity.Dealnumber;
                currentView.Chklistnum = newEntity.Checklistnum;
            }
        }


        #endregion

        #region Private Methods

        #endregion
    }
}

