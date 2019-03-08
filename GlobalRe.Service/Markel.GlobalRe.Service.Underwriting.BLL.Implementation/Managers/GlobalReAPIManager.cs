using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Underwriting.Service.BLL.Interfaces;
using Markel.GlobalRe.Underwriting.Service.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;

namespace Markel.GlobalRe.Underwriting.Service.BLL.Managers
{
    public class GlobalReAPIManager : BaseUnitOfWorkManager, IGlobalReAPIManager
    {

        #region Repositories & Managers
        private IDealManager _dealManager { get; set; }
        private IDealStatusSummariesManager _dealStatusSummariesManager { get; set; }
        private IDealCoveragesManager _dealCoveragesManager { get; set; }
        private IPersonsManager _personManager { get; set; }
        private IEntityLockManager _entityLockManager;
        private IDealDocumentsManager _dealDocumentsManager { get; set; }
        //Added for GRS-473 Notes
        private INotesManager _notesManager { get; set; }
        private IUserViewManager _userViewManager { get; set; }
        //Add for GRS-684 Checklist
        private ICheckListsManager _checklistManager { get; set; }
        //Added for GRS-473 Notes
        private IContractTypesManager _contractTypesManager { get; set; }
        private IWritingCompanyManager _writingCompanyManager { get; set; }
        private ICedantManager _cedantsManager { get; set; }
        private IExposureTreeManager _exposureTreeManager { get; set; }
		private IBrokerManager _brokerManager { get; set; }

		#endregion

		#region Constructor 

		public GlobalReAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
								  IEntityLockManager entityLockManager) : base(userManager, cacheStoreManager, logManager)
		{
			_entityLockManager = ValidateManager(entityLockManager);
		}
		public GlobalReAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
                                    IDealManager dealManager, IDealStatusSummariesManager dealStatusSummariesManager,  IPersonsManager personsManager,
									IEntityLockManager entityLockManager, IDealCoveragesManager dealCoveragesManager) : base(userManager, cacheStoreManager, logManager)
        {
            _dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(entityLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
        }

		public GlobalReAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
                                    IDealManager dealManager, IDealStatusSummariesManager dealStatusSummariesManager,  IPersonsManager personsManager,
									IEntityLockManager dealLockManager,  IDealCoveragesManager dealCoveragesManager,
                                    INotesManager notesManager) : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);

        }

        public GlobalReAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
                                    IDealManager dealManager, IDealStatusSummariesManager dealStatusSummariesManager,  IPersonsManager personsManager,
									IEntityLockManager dealLockManager,  IDealCoveragesManager dealCoveragesManager,
                                    INotesManager notesManager, IContractTypesManager contractTypesManager) : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);

        }

        public GlobalReAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager,
                                IDealManager dealManager, IDealStatusSummariesManager dealStatusSummariesManager,  IPersonsManager personsManager,
								IEntityLockManager dealLockManager,  IDealCoveragesManager dealCoveragesManager,
                                INotesManager notesManager, IContractTypesManager contractTypesManager, 
                                IWritingCompanyManager writingCompanyManager) : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);

        }

        public GlobalReAPIManager(IUserManager userManager,
                                  ICacheStoreManager cacheStoreManager,
                                  ILogManager logManager,
                                  IDealManager dealManager,
                                  IDealStatusSummariesManager dealStatusSummariesManager,
                                  IPersonsManager personsManager,
								  IEntityLockManager dealLockManager,
                                  
                                  IDealCoveragesManager dealCoveragesManager,
                                  INotesManager notesManager,
                                  IContractTypesManager contractTypesManager,
                                  IWritingCompanyManager writingCompanyManager,
                                  ICedantManager cedantsManager)
       : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);
            _cedantsManager = ValidateManager(cedantsManager);
        }

        //GRS-596 Exposure Tree
        public GlobalReAPIManager(IUserManager userManager,
                                  ICacheStoreManager cacheStoreManager,
                                  ILogManager logManager,
                                  IDealManager dealManager,
                                  IDealStatusSummariesManager dealStatusSummariesManager,
                                  IPersonsManager personsManager,
								  IEntityLockManager dealLockManager,
                                  
                                  IDealCoveragesManager dealCoveragesManager,
                                  INotesManager notesManager,
                                  IContractTypesManager contractTypesManager,
                                  IWritingCompanyManager writingCompanyManager,
                                  ICedantManager cedantsManager,
                                  IExposureTreeManager exposureTreeManager)
       : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);
            _cedantsManager = ValidateManager(cedantsManager);
            _exposureTreeManager = ValidateManager(exposureTreeManager);
        }

 //GRS-48 User Views
        public GlobalReAPIManager(IUserManager userManager,
                                  ICacheStoreManager cacheStoreManager,
                                  ILogManager logManager,
                                  IDealManager dealManager,
                                  IDealStatusSummariesManager dealStatusSummariesManager,
                                  
                                  IPersonsManager personsManager,
                                  IEntityLockManager dealLockManager,
                                  
                                  IDealCoveragesManager dealCoveragesManager,
                                  INotesManager notesManager,
                                  IContractTypesManager contractTypesManager,
                                  IWritingCompanyManager writingCompanyManager,
                                  ICedantManager cedantsManager,
                                  IExposureTreeManager exposureTreeManager,
                                  IUserViewManager userViewManager)
       : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);
            _cedantsManager = ValidateManager(cedantsManager);
            _exposureTreeManager = ValidateManager(exposureTreeManager);
            _userViewManager = ValidateManager(userViewManager);
        }


        public GlobalReAPIManager(IUserManager userManager,
                                  ICacheStoreManager cacheStoreManager,
                                  ILogManager logManager,
                                  IDealManager dealManager,
                                  IDealStatusSummariesManager dealStatusSummariesManager,
                                  IPersonsManager personsManager,
                                  IEntityLockManager dealLockManager,
                                  IDealCoveragesManager dealCoveragesManager,
                                  INotesManager notesManager,
                                  IContractTypesManager contractTypesManager,
                                  IWritingCompanyManager writingCompanyManager,
                                  ICedantManager cedantsManager,
                                  IExposureTreeManager exposureTreeManager,
								  IUserViewManager userViewManager,
                                  IDealDocumentsManager dealDocumentsManager)
       : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);
            _cedantsManager = ValidateManager(cedantsManager);
            _exposureTreeManager = ValidateManager(exposureTreeManager);
			_userViewManager = ValidateManager(userViewManager);
            _dealDocumentsManager = ValidateManager(dealDocumentsManager);
        }

        //GRS-684 Checklist
        public GlobalReAPIManager(IUserManager userManager,
                                 ICacheStoreManager cacheStoreManager,
                                 ILogManager logManager,
                                 IDealManager dealManager,
                                 IDealStatusSummariesManager dealStatusSummariesManager,
                                 IPersonsManager personsManager,
                                 IEntityLockManager dealLockManager,
                                 IDealCoveragesManager dealCoveragesManager,
                                 INotesManager notesManager,
                                 IContractTypesManager contractTypesManager,
                                 IWritingCompanyManager writingCompanyManager,
                                 ICedantManager cedantsManager,
                                 IExposureTreeManager exposureTreeManager,
                                 IUserViewManager userViewManager,
                                 IDealDocumentsManager dealDocumentsManager,
                                 ICheckListsManager checkListsManager)
      : base(userManager, cacheStoreManager, logManager)
        {
			_dealManager = ValidateManager(dealManager);
            _dealStatusSummariesManager = ValidateManager(dealStatusSummariesManager);
            
            _personManager = ValidateManager(personsManager);
            
            _entityLockManager = ValidateManager(dealLockManager);
            _dealCoveragesManager = ValidateManager(dealCoveragesManager);
            _notesManager = ValidateManager(notesManager);
            _contractTypesManager = ValidateManager(contractTypesManager);
            _writingCompanyManager = ValidateManager(writingCompanyManager);
            _cedantsManager = ValidateManager(cedantsManager);
            _exposureTreeManager = ValidateManager(exposureTreeManager);
            _userViewManager = ValidateManager(userViewManager);
            _dealDocumentsManager = ValidateManager(dealDocumentsManager);
            _checklistManager = ValidateManager(checkListsManager);
        }
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
            if (typeof(BLL_CLASS) == typeof(BLL_UserView)) return _userViewManager.GetEntityActions(entity as BLL_UserView);
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

        public EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.AddUserView(bLL_UserView);
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

        public EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.UpdateUserView(bLL_UserView);
            });
        }

        public EntityResult<BLL_UserView> GetUserViewbyId(int viewId)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.GetUserViewByID(viewId);
            });
        }

        //GRS-741 for ScreenID search
        public EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenname)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.GetByUserViewSreenName(screenname);
                //return null;
            });
        }

        public Result DeleteUserViewbyId(int viewId, BLL_UserViewDelete userviewdelete)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.DeleteUserViewByID(viewId, userviewdelete);
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


		public EntityResult<BLL_Person> GetPerson(int personId)
		{
			return RunInContextScope(() =>
			{
				return _personManager.GetPerson(personId);
			});
		}
		public IPaginatedList<BLL_Person> SearchPersons(SearchCriteria criteria)
        {
            return RunInContextScope(() =>
            {
                return _personManager.Search(criteria);
            });
        }

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
