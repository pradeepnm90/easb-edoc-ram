using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class DealAPIManager : BaseUnitOfWorkManager, IDealAPIManager
    {

        #region Repositories & Managers
        private IDealManager _dealManager { get; set; }
        private IDealStatusSummariesManager _dealStatusSummariesManager { get; set; }
        private IDealCoveragesManager _dealCoveragesManager { get; set; }
        private IEntityLockManager _entityLockManager;
        private IDealDocumentsManager _dealDocumentsManager { get; set; }
        private INotesManager _notesManager { get; set; }
        private ICheckListsManager _checklistManager { get; set; }
        private IContractTypesManager _contractTypesManager { get; set; }
        private IWritingCompanyManager _writingCompanyManager { get; set; }
        private ICedantManager _cedantsManager { get; set; }
        private IExposureTreeManager _exposureTreeManager { get; set; }
		private IBrokerManager _brokerManager { get; set; }

		#endregion

		#region Constructor 

		public DealAPIManager(IUserManager userManager,
								 ICacheStoreManager cacheStoreManager,
								 ILogManager logManager,
								 IDealManager dealManager,
								 IEntityLockManager dealLockManager,
								 IDealStatusSummariesManager dealStatusSummariesManager,
								 IDealCoveragesManager dealCoveragesManager,
								 INotesManager notesManager,
								 IContractTypesManager contractTypesManager,
								 IWritingCompanyManager writingCompanyManager,
								 ICedantManager cedantsManager,
								 IExposureTreeManager exposureTreeManager,
								 IDealDocumentsManager dealDocumentsManager,
								 ICheckListsManager checkListsManager)
	  : base(userManager, cacheStoreManager, logManager)
		{
			_dealManager = ValidateManager(dealManager);
			_dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
			_entityLockManager = ValidateManager(dealLockManager);
			_dealCoveragesManager = ValidateManager(dealCoveragesManager);
			_notesManager = ValidateManager(notesManager);
			_contractTypesManager = ValidateManager(contractTypesManager);
			_writingCompanyManager = ValidateManager(writingCompanyManager);
			_cedantsManager = ValidateManager(cedantsManager);
			_exposureTreeManager = ValidateManager(exposureTreeManager);
			_dealDocumentsManager = ValidateManager(dealDocumentsManager);
			_checklistManager = ValidateManager(checkListsManager);
		}
		#region TDD constructors
		public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
								  IEntityLockManager entityLockManager) : base(userManager, cacheStoreManager, logManager)
		{
			_entityLockManager = ValidateManager(entityLockManager);
		}

		public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IDealStatusSummariesManager dealStatusSummariesManager) 
			: base(userManager, cacheStoreManager, logManager)
        {
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);                        
        }

        public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IDealCoveragesManager dealCoveragesManager) 
			: base(userManager, cacheStoreManager, logManager)
        {
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
        }

        public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, INotesManager notesManager) 
			: base(userManager, cacheStoreManager, logManager)
        {
            _notesManager = ValidateManager(notesManager);
        }

        public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IWritingCompanyManager writingCompanyManager)
			: base(userManager, cacheStoreManager, logManager)
        {
            _writingCompanyManager = ValidateManager(writingCompanyManager);
        }

        public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IDealManager dealManager, IEntityLockManager dealLockManager)
       : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _entityLockManager = ValidateManager(dealLockManager);
        }

        public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IExposureTreeManager exposureTreeManager)
       : base(userManager, cacheStoreManager, logManager)
        {
            _exposureTreeManager = ValidateManager(exposureTreeManager);
        }

		public DealAPIManager(IUserManager userManager,	 ICacheStoreManager cacheStoreManager, ILogManager logManager, IDealDocumentsManager dealDocumentsManager)
			: base(userManager, cacheStoreManager, logManager)
		{
			_dealDocumentsManager = ValidateManager(dealDocumentsManager);
		}


		public DealAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, ICheckListsManager checkListsManager)
	  : base(userManager, cacheStoreManager, logManager)
		{
			_checklistManager = ValidateManager(checkListsManager);
		}
		#endregion
		#endregion

		#region Entity Actions
		public EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity
        {
            if (typeof(BLL_CLASS) == typeof(BLL_Deal)) return _dealManager.GetEntityActions(entity as BLL_Deal);
            if (typeof(BLL_CLASS) == typeof(BLL_DealStatusSummary)) return _dealStatusSummariesManager.GetEntityActions(entity as BLL_DealStatusSummary);
            if (typeof(BLL_CLASS) == typeof(BLL_DealCoverages)) return _dealCoveragesManager.GetEntityActions(entity as BLL_DealCoverages);
            if (typeof(BLL_CLASS) == typeof(BLL_WritingCompany)) return _writingCompanyManager.GetEntityActions(entity as BLL_WritingCompany);
            //Added for GR-678
            if (typeof(BLL_CLASS) == typeof(BLL_ContractTypes)) return _contractTypesManager.GetEntityActions(entity as BLL_ContractTypes);
            if (typeof(BLL_CLASS) == typeof(BLL_Notes)) return _notesManager.GetEntityActions(entity as BLL_Notes);
            if (typeof(BLL_CLASS) == typeof(BLL_ChkCategoryTree)) return _checklistManager.GetEntityActions(entity as BLL_ChkCategoryTree);
            return null;
        }

        #endregion

        #region API Manager

        //protected override T UnitOfWork<T>(long analysisId, Func<T> function, bool persistToDB = false)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region Deals 

        public bool LockDeal(int dealnum)
        {
			//throw new NotAllowedAPIException($"{dealLocks.FirstOrDefault().LockingUser} has locked this deal for edit");
			//throw new NotAllowedAPIException($"Deal locked for edit by another user: {UserIdentity.UserName}");
			return RunInContextScope(() =>
			{
				return _dealManager.Lock(dealnum);
			});
		}
		public bool UnlockDeal(int dealnum)
		{
			return RunInContextScope(() =>
			{
				return _dealManager.Unlock(dealnum);
			});
		}
	public EntityResult<BLL_EntityLock> GetDealLocks(int dealnum)
		{
			return RunInContextScope(() =>
			{
				return _dealManager.GetLocks(dealnum);
			});
		}

		public EntityResult<IEnumerable<BLL_Deal>> GetDeals(SearchCriteria criteria)
        {
            return RunInContextScope(() =>
            {
				return _dealManager.GetWorkbenchDeals(criteria); 
            });
        }

        public EntityResult<BLL_Deal> GetDeal(int dealNumber)
        {
            return RunInContextScope(() =>
            {
                return _dealManager.GetDeal(dealNumber);
            });
        }

        public EntityResult<BLL_Deal> UpdateDeal(BLL_Deal bLL_Deal)
        {
            return RunInContextScope(() =>
            {
                return _dealManager.UpdateDeal(bLL_Deal);
            });
        }

        public EntityResult<IEnumerable<BLL_DealStatusSummary>> GetAllDealStatusSummaries(SearchCriteria criteria)
        {
            return RunInContextScope(() =>
            {
                return _dealStatusSummariesManager.GetAllDealStatusSummaries(criteria);
            });
        }

		#region Checklist

		public EntityResult<IEnumerable<BLL_ChkCategoryTree>> GetAllDealChecklists(int dealnum)
		{
			return RunInContextScope(() =>
			{
				return _checklistManager.GetAllDealChecklists(dealnum);
			});
		}
		//public EntityResult<IEnumerable<BLL_ChkCategoryTree>> UpdateCheckList(BLL_CheckListParameter bLL_checklist)
		public EntityResult<BLL_ChkCategoryTree> UpdateCheckList(BLL_CheckListParameter bLL_checklist)
		{
			return RunInContextScope(() =>
			{
				return _checklistManager.UpdateCheckList(bLL_checklist);
			});
		}
		#endregion checklist

		//Added for GRS-473 Notes
		public EntityResult<IEnumerable<BLL_Notes>> GetNotes(int dealNumber)
        {
            return RunInContextScope(() =>
            {
                return _notesManager.GetNotes(dealNumber);
            });
        }
        public EntityResult<IEnumerable<BLL_Notes>> GetNotebyNoteNumber(int notenumber)
        {
            return RunInContextScope(() =>
            {
                return _notesManager.GetNotebyNoteNumber(notenumber);
            });
        }

        public EntityResult<BLL_Notes> AddDealNotes(BLL_Notes bLL_DealNotes)
        {
            return RunInContextScope(() =>
            {
                return _notesManager.AddDealNotes(bLL_DealNotes);
            });
        }

        public EntityResult<BLL_Notes> UpdateDealNotes(BLL_Notes bLL_DealNotes)
        {
            return RunInContextScope(() =>
            {
                return _notesManager.UpdateDealNotes(bLL_DealNotes);
            });
        }

        //Added for GRS-508 Notes
        public EntityResult<IEnumerable<BLL_ContractTypes>> GetContractTypes()
        {
            return RunInContextScope(() =>
            {
                return _contractTypesManager.GetContractTypes();
            });
        }

        public EntityResult<IEnumerable<BLL_DealCoverages>> GetDealCoverages(int dealNumber)
        {
            return RunInContextScope(() =>
            {
                return _dealCoveragesManager.GetDealCoverages(dealNumber);
            });
        }

		//public EntityResult<BLL_DealCoverages> UpdateDeal(BLL_DealCoverages bLL_DealCoverages)
		//{
		//    return _dealCoveragesManager.UpdateDeal(bLL_DealCoverages);
		//}

        public EntityResult<IEnumerable<BLL_KeyDocuments>> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure)
        {
            return RunInContextScope(() =>
            {
                return _dealDocumentsManager.GetKeyDocuments(filenumber, producer, isDocTypeStructure);
            });
        }

        public Boolean CheckIfDealExistsInSystem(int filenumber)
        {
            return RunInContextScope(() =>
            {
                return _dealDocumentsManager.CheckIfDealExistsInSystem(filenumber);
            });
        }
        public string GetFileType(int filenumber)
        {
            return RunInContextScope(() =>
            {
                return _dealDocumentsManager.GetFileType(filenumber);
            });
        }


		public HttpResponseMessage GetRegisterToken(string baseUrl, string apiUser, string apiPwd)
		{
			return RunInContextScope(() =>
			{
				return _dealDocumentsManager.GetRegisterToken(baseUrl, apiUser, apiPwd);
			});
		}

		public HttpResponseMessage GetDocumentSchema(string baseUrl, string token, string dealNumber)
		{
			return RunInContextScope(() =>
			{
				return _dealDocumentsManager.GetDocumentSchema(baseUrl, token, dealNumber);
			});
		}

		public HttpResponseMessage GetDocuments(string baseUrl, string token, string documentId, Boolean includeFileContents)
		{
			return RunInContextScope(() =>
			{
				return _dealDocumentsManager.GetDocuments(baseUrl, token, documentId, includeFileContents);
			});
		}

		public HttpResponseMessage GetDocumentContent(string baseUrl, string token, string documentId, Boolean includeFileContents, int pageNumber)
		{
			return RunInContextScope(() =>
			{
				return _dealDocumentsManager.GetDocumentContent(baseUrl, token, documentId, includeFileContents, pageNumber);
			});
		}

		//public EntityResult<BLL_KeyDocuments> AddKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments)
		//{
		//    return _dealDocumentsManager.AddKeyDocuments(bLL_KeyDocuments);
		//}

		//public EntityResult<BLL_KeyDocuments> DeleteKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments)
		//{
		//    return _dealDocumentsManager.DeleteKeyDocuments(bLL_KeyDocuments);
		//}



		#endregion


		#region Master Services
		public EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany()
        {
            return RunInContextScope(() =>
            {
                return _writingCompanyManager.GetWritingCompany();
            });
        }

        public EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany(Boolean isGRSFlag)
        {
            return RunInContextScope(() =>
            {
                return _writingCompanyManager.GetWritingCompany(isGRSFlag);
            });
        }
        #endregion

        #region Search

        public EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid)
        {
            return RunInContextScope(() =>
            {
                return _cedantsManager.GetCedants(cedants, cedantsparentgroup, cedantsid, cedantsparentgroupid, cedantslocationid);
            });
        }
        #endregion

        #region ExposureTree
        public EntityResult<IEnumerable<BLL_ExposureTree>> GetGlobalReExposureTree()
        {
            return RunInContextScope(() =>
            {
                return _exposureTreeManager.GetGlobalReExposureTree();
            });
        }

        #endregion

    }
}
