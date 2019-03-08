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
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("WritingCompany")]
    [Category("Controllers")]
    [TestFixture]
    class WritingCompanyControllerTest
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private WritingCompanyTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion


        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
            transformationManager = new WritingCompanyTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();
        }

        #endregion


        #region TestCases

        [TestCaseSource(typeof(WritingCompanySearchData), "TestCases")]
        public void WritingCompanyController_Get_Returns_OKResponseCode(Boolean isGRSFlag)
        {
            #region Arrange

            SetupUserIdentity();

            SetupWritingCompanyRepository(out Mock<IWritingCompanyRepository> writingCompanyRepository, isGRSFlag);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.WritingCompanyRoutePrefix}");
			WritingCompaniesController writingCompanyController = CreateWritingCompanyController(httpRequest, writingCompanyRepository.Object);

			#endregion

			#region Act

			var response = writingCompanyController.Get(isGRSFlag);

            #endregion

            #region Expected Data

            var expectedWritingCompany = new WritingCompany() 
            {
                Papernum = 16,
                Companyname = "Markel Global Reinsurance Company",
                Cpnum = 201178,
                Addr1 = "535 Springfield Avenue",
                Addr2 = "",
                Addr3 = "",
                City = "Summit",
                State = "NJ",
                Postalcode = "07901-2608",
                Country = "United States",
                Phone = null,
                Fax = null,
                Imagefilename = "images\\Markel.jpg",
                CompanyShortname = "ARUI",
                SlTrequired = 0,
                IpTrequired = 0,
                PaperToken = null,
                Currency = null,
                Territory = null,
                Active = 1,
                HideUnusedClaimCategory = true,
                JeCode = "102",
            };

            #endregion

            #region Assert
            var contentResult = response as NegotiatedContentResult<ResponseCollection<WritingCompany>>;

            if (contentResult != null)
            {
                Assertions.AssertOkResponse(contentResult);

                var writingcompanyData = contentResult.Content.results;

                for (int i = 0; i <= writingcompanyData.Count - 1; i++)
                {
                    var actualWritingCompany = writingcompanyData[i].data;

					Assertions.AssertData(expectedWritingCompany, actualWritingCompany);
                    Assert.IsEmpty(writingcompanyData[i].messages);
                }
            }

            #endregion
        }

        [TestCaseSource(typeof(WritingCompanySearchData), "NegativeTestCases")]
        public void WritingCompanyController_Get_Throw_NotAllowedAPIException(Boolean isGRSFlag)
        {
            #region Arrange

            SetupUserIdentity();

            SetupWritingCompanyRepository(out Mock<IWritingCompanyRepository> writingCompanyRepository, isGRSFlag);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.WritingCompanyRoutePrefix}");
			WritingCompaniesController writingCompanyController = CreateWritingCompanyController(httpRequest, writingCompanyRepository.Object);

			#endregion


			#region Assert

			Assert.Throws(typeof(NotAllowedAPIException), delegate { writingCompanyController.Get(false); });

            #endregion
        }

        [TestCaseSource(typeof(WritingCompanySearchData), "TestCasesRoot")]
        public void WritingCompanyController_Get_Root_Returns_OKResponseCode()
        {
            #region Arrange

            SetupUserIdentity();

            //SetupWritingCompanyRepository(out Mock<IWritingCompanyRepository> writingcompanyRepository, true);

            Mock<IWritingCompanyRepository> writingCompanyRepository = new Mock<IWritingCompanyRepository>();

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.WritingCompanyRoutePrefix}");
			WritingCompaniesController writingCompanyController = CreateWritingCompanyController(httpRequest, writingCompanyRepository.Object);

			IList<Data.Models.grs_VPaperExt> writingCompanyDbData = new List<Data.Models.grs_VPaperExt>
            {
                new Data.Models.grs_VPaperExt
				{
                    Papernum = 16,
                    Companyname = "Markel Global Reinsurance Company",
                    Cpnum = 201178,
                    Addr1 = "535 Springfield Avenue",
                    Addr2 = "",
                    Addr3 = "",
                    City = "Summit",
                    State = "NJ",
					Cty = "United States",
					Postalcode = "07901-2608",
                    Phone = null,
                    Fax = null,
                    Imagefilename = "images\\Markel.jpg",
                    CompanyShortname = "ARUI",
                    SlTrequired = 0,
                    IpTrequired = 0,
                    PaperToken = null,
                    Currency = null,
                    Territory = null,
                    Active = 1,
                    HideUnusedClaimCategory = true,
                    JeCode = "102"
                }
            };

            writingCompanyRepository.Setup(p => p.GetWritingCompany()).Returns(writingCompanyDbData);

            #endregion

            #region Act

			var response = writingCompanyController.Get();

            #endregion

            #region Expected Data

            var expectedWritingCompany = new WritingCompany()
            {
                Papernum = 16,
                Companyname = "Markel Global Reinsurance Company",
                Cpnum = 201178,
                Addr1 = "535 Springfield Avenue",
                Addr2 = "",
                Addr3 = "",
                City = "Summit",
                State = "NJ",
                Postalcode = "07901-2608",
                Country = "United States",
                Phone = null,
                Fax = null,
                Imagefilename = "images\\Markel.jpg",
                CompanyShortname = "ARUI",
                SlTrequired = 0,
                IpTrequired = 0,
                PaperToken = null,
                Currency = null,
                Territory = null,
                Active = 1,
                HideUnusedClaimCategory = true,
                JeCode = "102"
            };

            #endregion

            #region Assert
            var contentResult = response as NegotiatedContentResult<ResponseCollection<WritingCompany>>;

            if (contentResult != null)
            {
                Assertions.AssertOkResponse(contentResult);

                var writingcompanyData = contentResult.Content.results;

                for (int i = 0; i <= writingcompanyData.Count - 1; i++)
                {
                    var actualWritingCompany = writingcompanyData[i].data;

					Assertions.AssertData(expectedWritingCompany, actualWritingCompany);
                    Assert.IsEmpty(writingcompanyData[i].messages);
                }
            }

            #endregion
        }

        [TestCaseSource(typeof(WritingCompanySearchData), "NegativeTestCasesRoot")]
        public void WritingCompanyController_Get_Root_IsNullorEmpty_ValidationAPIException()
        {
            #region Arrange

            SetupUserIdentity();

            SetupWritingCompanyRepository(out Mock<IWritingCompanyRepository> writingCompanyRepository, true);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.WritingCompanyRoutePrefix}");
			WritingCompaniesController writingCompanyController = CreateWritingCompanyController(httpRequest, writingCompanyRepository.Object);

			#endregion


            #region Assert

            Assert.Throws<ArgumentNullException>(delegate { writingCompanyController.Get(); });

            #endregion
        }


		#endregion


		#region Private Methods

		private WritingCompaniesController CreateWritingCompanyController(HttpRequestMessage httpRequest, IWritingCompanyRepository writingCompanyRepository)
		{
			WritingCompanyManager writingCompanyManager = new WritingCompanyManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, writingCompanyRepository, transformationManager);
			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, writingCompanyManager);
			WritingCompaniesController writingCompanyController = new WritingCompaniesController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};
			return writingCompanyController;
		}


		private void SetupWritingCompanyRepository(out Mock<IWritingCompanyRepository> writingCompanyRepository, Boolean isGRSFlag)
        {
            writingCompanyRepository = new Mock<IWritingCompanyRepository>();

            IList<Data.Models.grs_VPaperExt> writingCompanyDbData = new List<Data.Models.grs_VPaperExt>
            {
                new Data.Models.grs_VPaperExt
				{
                    Papernum = 16,
                    Companyname = "Markel Global Reinsurance Company",
                    Cpnum = 201178,
                    Addr1 = "535 Springfield Avenue",
                    Addr2 = "",
                    Addr3 = "",
                    City = "Summit",
                    State = "NJ",
					Cty = "United States",
					Postalcode = "07901-2608",
                    Phone = null,
                    Fax = null,
                    Imagefilename = "images\\Markel.jpg",
                    CompanyShortname = "ARUI",
                    SlTrequired = 0,
                    IpTrequired = 0,
                    PaperToken = null,
                    Currency = null,
                    Territory = null,
                    Active = 1,
                    HideUnusedClaimCategory = true,
                    JeCode = "102"
                }
            };

            writingCompanyRepository.Setup(p => p.GetWritingCompany(isGRSFlag)).Returns(writingCompanyDbData);
        }

        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on Groupings", false },
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


        #region  Search Data

        public class WritingCompanySearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(true);
                }
            }

            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(false);
                    //yield return new TestCaseData(null);
                }
            }

            public static IEnumerable TestCasesRoot
            {
                get
                {
                    yield return new TestCaseData();

                }
            }

            public static IEnumerable NegativeTestCasesRoot
            {
                get
                {
                    yield return new TestCaseData();
                }
            }

        }

        #endregion


    }
}
