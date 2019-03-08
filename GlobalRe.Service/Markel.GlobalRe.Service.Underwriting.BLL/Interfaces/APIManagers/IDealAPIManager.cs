using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;
using System;
using System.Net.Http;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IDealAPIManager : IBaseManager
    {
        #region Entity Actions

        EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity;

        #endregion Entity Actions

        #region Deals
        //EntityResult<IPaginatedList<BLL_Deal>> SearchDeals(SearchCriteria criteria);

        EntityResult<IEnumerable<BLL_Deal>> GetDeals(SearchCriteria criteria);

        EntityResult<BLL_Deal> UpdateDeal(BLL_Deal bLL_Deal);


        EntityResult<BLL_Deal> GetDeal(int dealNumber);

        #endregion

        #region Deal Documents

        EntityResult<IEnumerable<BLL_KeyDocuments>> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure);

        //EntityResult<BLL_KeyDocuments> AddKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);
        //EntityResult<BLL_KeyDocuments> DeleteKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);
        Boolean CheckIfDealExistsInSystem(int filenumber);

        string GetFileType(int filenumber);

        HttpResponseMessage GetRegisterToken(string baseUrl, string apiUser, string apiPwd);

        HttpResponseMessage GetDocumentSchema(string baseUrl, string token, string dealNumber);

        HttpResponseMessage GetDocuments(string baseUrl, string token, string documentId, Boolean includeFileContents);

        HttpResponseMessage GetDocumentContent(string baseUrl, string token, string documentId, Boolean includeFileContents, int pageNumber);
        #endregion

        #region Deal Summaries

        EntityResult<IEnumerable<BLL_DealStatusSummary>> GetAllDealStatusSummaries(SearchCriteria criteria);

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

        #region CheckList
        EntityResult<IEnumerable<BLL_ChkCategoryTree>> GetAllDealChecklists(int dealnum);
        EntityResult<BLL_ChkCategoryTree> UpdateCheckList(BLL_CheckListParameter bLL_checklist);
        #endregion checklist

        #region Contract Type
        ////Added for GRS-508 Contract type 
        EntityResult<IEnumerable<BLL_ContractTypes>> GetContractTypes();
        #endregion

        #region SubDivisions


        #endregion

        #region ExposureTree

        EntityResult<IEnumerable<BLL_ExposureTree>> GetGlobalReExposureTree();

        #endregion


		//EntityResult<BLL_PersonProfile> GetProfile();

		#region Deal Lock
		bool LockDeal(int dealNumber);
		bool UnlockDeal(int dealNumber);
		EntityResult<BLL_EntityLock> GetDealLocks(int dealnum);

		#endregion
	}
}
