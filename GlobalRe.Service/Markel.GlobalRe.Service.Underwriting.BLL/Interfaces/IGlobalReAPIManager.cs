using Markel.GlobalRe.Underwriting.Service.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;
using System;

namespace Markel.GlobalRe.Underwriting.Service.BLL.Interfaces
{
    public interface IGlobalReAPIManager : IBaseManager
    {
        #region Entity Actions

        EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity;

        #endregion Entity Actions

        #region Deals
        //EntityResult<IPaginatedList<BLL_Deal>> SearchDeals(SearchCriteria criteria);

        EntityResult<IEnumerable<BLL_Deal>> GetDeals(SearchCriteria criteria);

        EntityResult<BLL_Deal> UpdateDeal(BLL_Deal bLL_Deal);


        EntityResult<BLL_Deal> GetDeal(int dealNumber);

        EntityResult<IEnumerable<BLL_KeyDocuments>> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure);

		//EntityResult<BLL_KeyDocuments> AddKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);
		//EntityResult<BLL_KeyDocuments> DeleteKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);
		Boolean CheckIfDealExistsInSystem(int filenumber);
        string GetFileType(int filenumber);
        #endregion

        #region Deal Summaries

        EntityResult<IEnumerable<BLL_DealStatusSummary>> GetAllDealStatusSummaries(SearchCriteria criteria);
		EntityResult<BLL_Person> GetPerson(int personid);

		#endregion

		#region Master and Config services
		EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany();
        EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany(Boolean isGRSFlag);
        #endregion

        #region Search

        EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid);
        

        #endregion


        #region Deal Coverages

        //EntityResult<BLL_DealCoverages> UpdateDeal(BLL_DealCoverages bLL_DealCoverages);

        EntityResult<IEnumerable<BLL_DealCoverages>> GetDealCoverages(int dealNumber);
        #endregion

        #region Deal Notes
        ////Added for GRS-473 Notes 
        EntityResult<IEnumerable<BLL_Notes>> GetNotes(int dealNumber);
        EntityResult<IEnumerable<BLL_Notes>> GetNotebyNoteNumber(int notenumber);
        EntityResult<BLL_Notes> AddDealNotes(BLL_Notes bLL_DealNotes);
        EntityResult<BLL_Notes> UpdateDealNotes(BLL_Notes bLL_DealNotes);

        #endregion

        #region UserViews
        EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView);
        EntityResult<BLL_UserView> GetUserViewbyId(int viewId);
        Result DeleteUserViewbyId(int viewId, BLL_UserViewDelete userviewdelete);
        EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView);

        //GRS-741 for ScreenID search
        EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenname);
        #endregion

        #region CheckList
        EntityResult<IEnumerable<BLL_ChkCategoryTree>> GetAllDealChecklists(int dealnum);
        EntityResult<BLL_ChkCategoryTree> UpdateCheckList(BLL_CheckListParameter bLL_checklist);
        #endregion checklist
        
        ////Added for GRS-508 Contract type 
        EntityResult<IEnumerable<BLL_ContractTypes>> GetContractTypes();

       

        #region SubDivisions


        #endregion

        #region ExposureTree

        EntityResult<IEnumerable<BLL_ExposureTree>> GetGlobalReExposureTree();

        #endregion

        #region Person
        IPaginatedList<BLL_Person> SearchPersons(SearchCriteria criteria);
        #endregion Person

		//EntityResult<BLL_PersonProfile> GetProfile();

		#region Deal Lock
		bool LockDeal(int dealNumber);
		bool UnlockDeal(int dealNumber);
		EntityResult<BLL_EntityLock> GetDealLocks(int dealnum);

		#endregion
	}
}
