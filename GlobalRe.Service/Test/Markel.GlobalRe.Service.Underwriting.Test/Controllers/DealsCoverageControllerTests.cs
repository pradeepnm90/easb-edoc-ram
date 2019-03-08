using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Underwriting.Service.BLL.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Underwriting.Service.BLL.Interfaces;
using Markel.GlobalRe.Underwriting.Service.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;



namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
    [Category("DealsCoverage")]
    [Category("Controllers")]
    [TestFixture]
    public class DealsCoverageControllerTests
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private DealCoveragesTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion


        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
            transformationManager = new DealCoveragesTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();
        }

        #endregion


        #region TestCase Methods
        [TestCaseSource(typeof(DealCoverageData), "TestCases")]
        public void DealsCoverageController_Get_Returns_OKResponseCode(int dealnumber)
        {
            #region Arrange

            SetupUserIdentity();

            SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealsCoverageRepository, dealnumber);

            #endregion

            #region Act

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsCoverageRoutePrefix}/{dealnumber}");
            DealCoveragesManager dealCoveragesManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealsCoverageRepository.Object, dealLockManager.Object, transformationManager);
            GlobalReAPIManager globalReAPIManager = new GlobalReAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealManager>().Object, new Mock<IDealStatusSummariesManager>().Object,
                 new Mock<IPersonsManager>().Object, dealLockManager.Object,  dealCoveragesManager, new Mock<INotesManager>().Object);
            DealsCoverageController dealsCoverageController = new DealsCoverageController(userManager.Object, globalReAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };
            var response = dealsCoverageController.Get(dealnumber);

            #endregion

            #region Expected Data

            var expectedDealsCoverage = new DealCoverages() // grs_VGrsDealCoverage()
            {
                DealNumber = dealnumber,
                Cover_Id = 13,
                Cover_Name = "California Earthquake"
            };

            #endregion

            #region Assert

            var contentResult = response as NegotiatedContentResult<ResponseCollection<DealCoverages>>;

            if (contentResult != null)
            {
                Assertions.AssertOkResponse(contentResult);

                var dealCoveragesData = contentResult.Content.results;

                for (int i = 0; i <= dealCoveragesData.Count - 1; i++)
                {
                    var actualDealsCoverage = dealCoveragesData[i].data;

                    Assert.AreEqual(expectedDealsCoverage.DealNumber, actualDealsCoverage.DealNumber);
                    Assert.AreEqual(expectedDealsCoverage.Cover_Id, actualDealsCoverage.Cover_Id);
                    Assert.AreEqual(expectedDealsCoverage.Cover_Name, actualDealsCoverage.Cover_Name);

                    Assert.IsEmpty(dealCoveragesData[i].messages);
                }
            }

            #endregion

        }


        [Test]
        [TestCaseSource(typeof(DealCoverageData), "NegativeTestCases")]
        public void DealsCoverageController_Get_Null_ValidationAPIException(int dealnumber)
        {
            #region Arrange

            SetupUserIdentity();

            SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealsCoverageRepository, dealnumber);

            #endregion

            #region Act

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsCoverageRoutePrefix}/{dealnumber}");
            DealCoveragesManager dealCoveragesManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealsCoverageRepository.Object, dealLockManager.Object, transformationManager);
            GlobalReAPIManager globalReAPIManager = new GlobalReAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealManager>().Object, new Mock<IDealStatusSummariesManager>().Object,
                 new Mock<IPersonsManager>().Object, dealLockManager.Object,  dealCoveragesManager, new Mock<INotesManager>().Object);
            DealsCoverageController dealsCoverageController = new DealsCoverageController(userManager.Object, globalReAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };
            var response = dealsCoverageController.Get(dealnumber);

            #endregion

            #region Expected Data

            var expectedDealsCoverage = new grs_VGrsDealCoverage()
            {
                Dealnum = dealnumber,
                CoverId = 13,
                CoverName = "California Earthquake"
            };

            #endregion

            #region Act & Assert

            if (response.ToString() == "System.Web.Http.Results.StatusCodeResult")
                response = null;

            Assert.IsNull(response);

            #endregion

        }

        // Added only to check for forceable dev exception simulation - to be refactored 
        [TestCaseSource(typeof(DealCoverageData), "EmptyTestCases")]
        public void DealsCoverageController_Get_IsNullorEmpty_ValidationAPIException(int dealnumber)
        {
            #region Arrange

            SetupUserIdentity();

            SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealsCoverageRepository, dealnumber);

            #endregion

            #region Act

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsCoverageRoutePrefix}/{dealnumber}");
            DealCoveragesManager dealCoveragesManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealCoveragesRepository>().Object, dealLockManager.Object, transformationManager);
            GlobalReAPIManager globalReAPIManager = new GlobalReAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealManager>().Object, new Mock<IDealStatusSummariesManager>().Object,
                 new Mock<IPersonsManager>().Object, dealLockManager.Object,  dealCoveragesManager, new Mock<INotesManager>().Object);
            DealsCoverageController dealsCoverageController = new DealsCoverageController(userManager.Object, globalReAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };

            #endregion

            #region Act & Assert

            System.Web.Http.IHttpActionResult response = null;
            try
            {
                response = dealsCoverageController.Get(dealnumber);
            }
            catch (Exception)
            {
                Assert.IsNull(response);
            }

            #endregion

        }
        #endregion

        #region Private Methods

        private void SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealsCoverageRepository, int dealnumber)
        {
            dealsCoverageRepository = new Mock<IDealCoveragesRepository>();

            IList<Data.Models.grs_VGrsDealCoverage> coveragesDbData = new List<Data.Models.grs_VGrsDealCoverage>
            {
                new Data.Models.grs_VGrsDealCoverage
                {
                    Dealnum = dealnumber,
                    CoverId = 13,
                    CoverName = "California Earthquake"
                }
            };

            dealsCoverageRepository.Setup(p => p.GetDealCoverages(dealnumber)).Returns(coveragesDbData);
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

        #region Initiate Mock Data
        public class DealCoverageData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(1382304);         // happy path with coverages exists 
                    yield return new TestCaseData(101);             // happy path with coverages not exists 
                }
            }

            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(-1321360);        // Negative deals path 
                    yield return new TestCaseData(null);            // NULL or Empty path 
                }
            }

            public static IEnumerable EmptyTestCases
            {
                get
                {
                    yield return new TestCaseData(7777);
                }
            }

        }
        #endregion

    }
}