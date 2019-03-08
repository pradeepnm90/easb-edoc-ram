using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class DealDocumentsManager : BaseGlobalReManager<BLL_Cedant>, IDealDocumentsManager
    {

        #region Private Variable 

        private IEntityLockManager _dealLockManager;

        private IDealDocumentsRepository _dealDocumentsRepository;
        private IDealDocumentsTransformationManager _dealDocumentsTransformationManager;

        private enum FilterParameters
        {
            FileNumber
        }
        #endregion

        #region Constructors

        public DealDocumentsManager(IUserManager userManager,
                            ICacheStoreManager cacheStoreManager,
                            ILogManager logManager,
                            IDealDocumentsRepository dealDocumentsRepository,
                            IEntityLockManager dealLockManager,
                            IDealDocumentsTransformationManager dealDocumentsTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _dealDocumentsRepository = ValidateRepository(dealDocumentsRepository);
            _dealLockManager = ValidateManager(dealLockManager);
            _dealDocumentsTransformationManager = ValidateManager(dealDocumentsTransformationManager);
        }

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_KeyDocuments entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
            //entityActionTypes.Add(EntityActionType.Action_Update);

            return new EntityAction(
               entityType: EntityType.FileNumber,
               entityId: entity.FileNumber,
               entityActionTypes: entityActionTypes
           );
        }

        public EntityResult<IEnumerable<BLL_KeyDocuments>> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure)
        {
            int resultCount = 0;
            var keyDocumentsRequestResult = _dealDocumentsRepository.GetKeyDocuments(filenumber, producer, isDocTypeStructure);

            if (!keyDocumentsRequestResult.IsNullOrEmpty()) resultCount = keyDocumentsRequestResult.Count();
            if (resultCount == 0) throw new NotFoundAPIException("No matching reinsurance document types found ...");

            return new EntityResult<IEnumerable<BLL_KeyDocuments>>(_dealDocumentsTransformationManager.Transform(keyDocumentsRequestResult));
        }

        public Boolean CheckIfDealExistsInSystem(int filenumber) => _dealDocumentsRepository.IsDealExists(filenumber);

        public string GetFileType(int filenumber) => _dealDocumentsRepository.GetFileType(filenumber);

        public HttpResponseMessage GetRegisterToken(string baseUrl, string apiUser, string apiPwd)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(item: new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient.PostAsync("token", new StringContent("grant_type=password&username=" + apiUser + "&password=" + apiPwd)).Result;
        }

        public HttpResponseMessage GetDocumentSchema(string baseUrl, string token, string dealNumber)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

            //GRS-801 for Filetype : //httpClient.GetAsync("IR/file/search/" + dealnumber).Result;
            return httpClient.GetAsync("IR/file/" + dealNumber + "/" + GetFileType(int.Parse(dealNumber))).Result;
        }

        public HttpResponseMessage GetDocuments(string baseurl, string token, string documentid, Boolean includeFileContents)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

            return httpClient.GetAsync("IR/documents/" + documentid + "?IncludeFileContents=" + includeFileContents).Result;
        }

        public HttpResponseMessage GetDocumentContent(string baseUrl, string token, string documentId, Boolean includeFileContents, int pageNumber)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

            return httpClient.GetAsync("IR/documents/" + documentId + "?IncludeFileContents=" + includeFileContents).Result;
        }


        //public EntityResult<BLL_KeyDocuments> AddKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments)
        //{
        //    try
        //    {
        //        TbDocChkValue tbDocChkValue = new TbDocChkValue
        //        {
        //            Key1 = (Int32)bLL_KeyDocuments.FileNumber,
        //            DmsDocId = bLL_KeyDocuments.Docid,
        //            DmsDocName = bLL_KeyDocuments.DocName,
        //            DmsDocType = bLL_KeyDocuments.DocType,
        //            FileFlag = 1,
        //            DmsFlag = 1,
        //            DmsIsValid = true,
        //            DmsFileType = bLL_KeyDocuments.FileType,
        //            DmsCreated = bLL_KeyDocuments.DmsCreated
        //            // tbDocChkValue.ItemId  -> to be populated from tbdocchkItem
        //        };

        //        _tbDocChkValueRepository.Add(tbDocChkValue);
        //        _tbDocChkValueRepository.Save();
        //        return new EntityResult<BLL_KeyDocuments>(_dealDocumentsTransformationManager.Transform(_dealDocumentsRepository.GetKeyDocuments(bLL_KeyDocuments.FileNumber.ToString(),null,true).SingleOrDefault()));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public EntityResult<BLL_KeyDocuments> DeleteKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments)
        //{
        //    try
        //    {
        //        TbDocChkValue tbDocChkValue = new TbDocChkValue
        //        {
        //            Key1 = (Int32)bLL_KeyDocuments.FileNumber,
        //            DmsDocId = bLL_KeyDocuments.Docid,
        //            DmsDocName = bLL_KeyDocuments.DocName,
        //            DmsDocType = bLL_KeyDocuments.DocType,
        //        };

        //        _tbDocChkValueRepository.Delete(tbDocChkValue);
        //        _tbDocChkValueRepository.Save();
        //        return new EntityResult<BLL_KeyDocuments>(_dealDocumentsTransformationManager.Transform(_dealDocumentsRepository.GetKeyDocuments(bLL_KeyDocuments.FileNumber.ToString(), null, true).SingleOrDefault()));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #endregion

        #region Private Methods

        private IPaginatedList<BLL_KeyDocuments> Transform<T>(IPaginatedList<T> dbResults) where T : class
        {
            var results = new PaginatedList<BLL_KeyDocuments>()
            {
                PageCount = dbResults.PageCount,
                PageIndex = dbResults.PageIndex,
                PageSize = dbResults.PageSize,
                TotalRecordCount = dbResults.TotalRecordCount,
                Items = dbResults.Items.Select(s => s is grs_VKeyDocument ? _dealDocumentsTransformationManager.Transform(s as grs_VKeyDocument) : _dealDocumentsTransformationManager.Transform(s as grs_VKeyDocument)).ToList()
            };

            return results;
        }


        private Expression<Func<grs_VKeyDocument, bool>> FilterDealDocuments(string filenumber)
        {
            return s => s.Filenumber == int.Parse(filenumber);
        }

        public IPaginatedList<BLL_KeyDocuments> Search(SearchCriteria criteria)
        {
            bool DealDocumentsData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.FileNumber);
            ValidateSearchCriteria(DealDocumentsData, criteria);
            return DealDocumentsData ? Transform(_dealDocumentsRepository.Search(criteria)) : Transform(_dealDocumentsRepository.Search(criteria));
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private void ValidateSearchCriteria(bool DealDocumentData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (DealDocumentData)
            {
                validFilterParameters = _dealDocumentsRepository.GetFilterParameters();
                validSortParameters = _dealDocumentsRepository.GetSortParameters();
            }
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }
        #endregion
    }
}
