using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("DealDocuments")]
	[Category("Controllers")]
	[TestFixture]
    class DealDocumentsControllerTest
	{
		#region Private Variables


		private Mock<ILogManager> mockLogManager;
		private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
		private DealDocumentsTransformationManager transformationManager;
		private UserIdentity userIdentity;

		#endregion


		#region Initial Setup

		[OneTimeSetUp]
		public void Init()
		{
			mockLogManager = new Mock<ILogManager>();
			userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
			transformationManager = new DealDocumentsTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();
		}

		#endregion


        #region TestCases

        [TestCaseSource(typeof(DealDocumentsSearchData), "TestCases")]
        public void DealDocumentsController_GetKeyDocuments_OKResponse(DocuemntSearchCriteria criteria)
		{
            #region Arrange

            SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);

			#endregion

			#region Act
			var contentResult = dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.GetDoctypes) as NegotiatedContentResult<ResponseCollection<DealDocuments>>;
			#endregion

				#region Expected Data

				var expectedDocuments = new DealDocuments()
            {
                KeyDocid = 0,
                FileNumber = 103324,
                Producer = 0,
                Docid = "",
                DocName = "",
                SortOrder = 1,
                Location = "",
                Drawer = "",
                Folderid = "06",
                FolderName = "Final Documents",
                DocType = "Contract - Final RI",
                ErmsClassType = "",
                FileType = "",
                DmsPath = "u-rdc-wsdms01\\active\\[Deal]\\06-Final Documents ([DealNum])",
                ItemCategoryid = 4,
                ErmsCategory = "Signed Contract",
                LastUpdatedUser = "",
                LastTimeStamp = DateTime.Parse("2019-01-17T15:29:29.523"),
                DmsCreated = DateTime.Parse("2019-01-17T15:29:29.523"),
                DmsUpdated = DateTime.Parse("2019-01-17T15:29:29.523")
            };

            #endregion

            #region Assert

                Assertions.AssertOkResponse(contentResult);
                for (int i = 0; i <= contentResult.Content.results.Count - 1; i++)
                {
					var actualData = contentResult.Content.results[i].data;
                    Assertions.AssertData(expectedDocuments, actualData);
                    Assert.IsEmpty(contentResult.Content.results[i].messages);
                }

            #endregion
        }


        [TestCaseSource(typeof(DealDocumentsSearchData), "TestCases")]
        public void DealDocumentsController_GetDocumentSchema_OKResponse(DocuemntSearchCriteria criteria)
        {
			#region Arrange

			SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);

			#endregion

			#region Act

			IHttpActionResult actionResult = dealDocumentController.Get(criteria.FileNumber.ToString());

			#endregion

			#region Assert

			Assert.AreEqual(HttpStatusCode.OK, ((ResponseMessageResult)actionResult).Response.StatusCode);

            #endregion
        }


        [TestCaseSource(typeof(DealDocumentsSearchData), "TestCases")]
        public void DealDocumentsController_GetDocuments_OKResponse(DocuemntSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();

            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);
			#endregion

			#region Act
			IHttpActionResult actionResult = dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.DocumentId, criteria.FileContents);
			#endregion

			#region Assert

			Assert.AreEqual(HttpStatusCode.OK, ((ResponseMessageResult)actionResult).Response.StatusCode);

            #endregion
        }


        [TestCaseSource(typeof(DealDocumentsSearchData), "TestCases")]
        public void DealDocumentsController_GetDocumentContent_OKResponse(DocuemntSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);

			#endregion

			#region Act
			IHttpActionResult actionResult = dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.DocumentId, criteria.PageNumber);
			#endregion

			#region Assert

			Assert.AreEqual(HttpStatusCode.OK, ((ResponseMessageResult)actionResult).Response.StatusCode);

            #endregion
        }


        [TestCaseSource(typeof(DealDocumentsSearchData), "NegativeTestCases")]
        public void DealDocumentsController_Documents_NotFoundAPIException(DocuemntSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);

			#endregion

			#region Act

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);

			#endregion

			#region Assert

			Assert.Throws(typeof(NotFoundAPIException), delegate { dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.GetDoctypes); });
            Assert.Throws(typeof(NotFoundAPIException), delegate { dealDocumentController.Get(criteria.FileNumber.ToString()); });
            Assert.Throws(typeof(NotFoundAPIException), delegate { dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.DocumentId, criteria.FileContents); });
            Assert.Throws(typeof(NotFoundAPIException), delegate { dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.DocumentId, criteria.PageNumber); });

			#endregion
        }


        [TestCaseSource(typeof(DealDocumentsSearchData), "KeyDcoumentsNegativeTestCases")]
        public void DealDocumentsController_KeyDocuments_NotFoundAPIException(DocuemntSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);

			#endregion

			#region Act

			#endregion

			#region Assert

			Assert.Throws(typeof(NotFoundAPIException), delegate { dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.GetDoctypes); });

            #endregion
		}

        [TestCaseSource(typeof(DealDocumentsSearchData), "DMSNegativeTestCases")]
        public void DealDocumentsController_Documents_OnFailedDMSRequest(DocuemntSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);
			#endregion

			#region Act
			IHttpActionResult actionResult = dealDocumentController.Get(criteria.FileNumber.ToString(), criteria.DocumentId, criteria.FileContents);
			#endregion

			#region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, ((ResponseMessageResult)actionResult).Response.StatusCode);
            #endregion
        }

		[TestCaseSource(typeof(DealDocumentsSearchData), "DMSNegativeTestCases")]
		public void DealDocumentsController_DocumentsSchema_OnFailedDMSRequest(DocuemntSearchCriteria criteria)
		{
			#region Arrange

			SetupUserIdentity();
			SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{criteria.FileNumber}/keydoctypes/{criteria.GetDoctypes}");
			DealDocumentsController dealDocumentController = CreateDealDocumentsController(httpRequest, dealDocumentsRepository.Object);
			#endregion

			#region Act
			IHttpActionResult actionResult = dealDocumentController.Get(criteria.FileNumber.ToString());
			#endregion

			#region Assert


			Assert.AreEqual(HttpStatusCode.BadRequest, ((ResponseMessageResult)actionResult).Response.StatusCode);

			#endregion
		}


		#endregion


		#region Private Methods

		private DealDocumentsController CreateDealDocumentsController(HttpRequestMessage httpRequest, IDealDocumentsRepository dealDocumentsRepository)
		{

			DealDocumentsManager dealDocumentsManager = new DealDocumentsManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealDocumentsRepository, dealLockManager.Object, transformationManager);

			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealDocumentsManager);

			DealDocumentsController dealDocumentController = new DealDocumentsController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};

			return dealDocumentController;
		}


		private void SetupDealKeyDocumentsRepository(out Mock<IDealDocumentsRepository> dealDocumentsRepository, DocuemntSearchCriteria criteria)
        {
            dealDocumentsRepository = new Mock<IDealDocumentsRepository>();

            IList<Data.Models.grs_VKeyDocument> keyDocsDbData = new List<Data.Models.grs_VKeyDocument>
            {
                new Data.Models.grs_VKeyDocument
                {
                    Keydocid = 0,
                    Filenumber = 103324,
                    Producer = 0,
                    Docid = "",
                    Docname = "",
                    Sortorder = 1,
                    Location =  "",
                    Drawer = "",
                    Folderid =  "06",
                    Foldername = "Final Documents",
                    Doctype =  "Contract - Final RI",
                    Ermsclasstype =  "",
                    Filetype =  "",
                    Dmspath = "u-rdc-wsdms01\\active\\[Deal]\\06-Final Documents ([DealNum])",
                    Itemcategoryid =  4,
                    Ermscategory =  "Signed Contract",
                    Lastupdateduser =  "",
                    Lasttimestamp =  DateTime.Parse("2019-01-17T15:29:29.523"),
                    Dmscreated = DateTime.Parse("2019-01-17T15:29:29.523"),
                    Dmsupdated = DateTime.Parse("2019-01-17T15:29:29.523")
                },
            };

            if (criteria.FileNumber.ToString() == "1234" || criteria.FileNumber.ToString() == "99999988")
            {
                dealDocumentsRepository.Setup(p => p.GetKeyDocuments("103324", null, criteria.GetDoctypes)).Returns(keyDocsDbData);
                dealDocumentsRepository.Setup(p => p.GetFileType(103324)).Returns("Global Reinsurance");
                dealDocumentsRepository.Setup(p => p.IsDealExists(Int32.Parse(criteria.FileNumber))).Returns(true);
            }
            else
            {
                dealDocumentsRepository.Setup(p => p.GetKeyDocuments(criteria.FileNumber.ToString(), null, criteria.GetDoctypes)).Returns(keyDocsDbData);
                if (!string.IsNullOrEmpty(criteria.FileNumber))
                {
                    dealDocumentsRepository.Setup(p => p.GetFileType(Int32.Parse(criteria.FileNumber))).Returns("Global Reinsurance");
                    if (criteria.FileNumber.ToString() == "103324")
                        dealDocumentsRepository.Setup(p => p.IsDealExists(Int32.Parse(criteria.FileNumber))).Returns(true);
                    else
                        dealDocumentsRepository.Setup(p => p.IsDealExists(Int32.Parse(criteria.FileNumber))).Returns(false);
                }
            }
        }

        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on deal number and coverages", false },
                { "Default view based on current user", false },
                { "Default view based on manager", true }
            };

            userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, permission, null, null);
            userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        #endregion


        #region Public Methods 

		[OneTimeTearDown]
		public void Cleanup()
		{
			userManager = null;
			cacheStoreManager = null;
			mockLogManager = null;
			transformationManager = null;
			userIdentity = null;
		}

		#endregion


        #region Deal Documnets Search Data

        public class DealDocumentsSearchData
		{
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "103324", GetDoctypes = true, DocumentId = "502422548", FileContents = true, PageNumber = 1 });
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "103324", GetDoctypes = false, DocumentId = "502422548", FileContents = false, PageNumber = 1 });
                }
            }

            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "1307673", GetDoctypes = true, DocumentId = "412422548", FileContents = true, PageNumber = 1 });
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "", GetDoctypes = true, DocumentId = "412422548", FileContents = true, PageNumber = 1 });
                }
            }

            public static IEnumerable KeyDcoumentsNegativeTestCases
			{
                get
                {
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "1234", GetDoctypes = true, DocumentId = "412422548", FileContents = true, PageNumber = 1 });
                }
            }

            public static IEnumerable DMSNegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(new DocuemntSearchCriteria() { FileNumber = "99999988", GetDoctypes = false, DocumentId = "000000", FileContents = false, PageNumber = 1 });
		}
            }
        }

        public class DocuemntSearchCriteria
		{
            public string FileNumber { get; set; }
            public string Producer { get; set; }
            public string DocumentId { get; set; }
            public Boolean GetDoctypes { get; set; }
            public Boolean FileContents { get; set; }
            public int PageNumber { get; set; }
		}

		#endregion

	}
}
