using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
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
	[Category("DealCoverages")]
    [Category("Controllers")]
    [TestFixture]
    public class DealCoveragesControllerTests
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
        public void DealCoveragesController_GetCoverages_OKResponse(int dealnumber)
        {
			#region Arrange

			SetupUserIdentity();
            SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealCoveragesRepository, dealnumber);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/covergaes");
			DealCoveragesController dealCoveragesController = CreateDealCoveragesController(httpRequest, dealCoveragesRepository.Object);

			#endregion

			#region Act

			var response = dealCoveragesController.Get(dealnumber);

			#endregion

			#region Expected Data

			var expectedDealCoverages = new DealCoverages() // grs_VGrsDealCoverage()
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
                    var actualDealCoverages = dealCoveragesData[i].data;

					Assertions.AssertData(expectedDealCoverages, actualDealCoverages);

                    Assert.IsEmpty(dealCoveragesData[i].messages);
                }
            }

            #endregion

        }


        [TestCaseSource(typeof(DealCoverageData), "NegativeTestCases")]
        public void DealsCoverageController_GetCoverage_NotAcceptableResponse(int dealnumber)
        {
            #region Arrange

            SetupUserIdentity();
            SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealCoveragesRepository, dealnumber);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/coverages");
			DealCoveragesController dealCoveragesController = CreateDealCoveragesController(httpRequest, dealCoveragesRepository.Object);

			#endregion

			#region Act

			var response = dealCoveragesController.Get(dealnumber);

            #endregion

            #region Expected Data

            var expectedDealCoverages = new grs_VGrsDealCoverage()
            {
                Dealnum = dealnumber,
                CoverId = 13,
                CoverName = "California Earthquake"
            };

            #endregion

            #region Act & Assert
            IHttpActionResult actionResult = dealCoveragesController.Get(dealnumber);
            Assert.AreEqual(HttpStatusCode.NotAcceptable, ((StatusCodeResult)actionResult).StatusCode);

            #endregion

        }


        [TestCaseSource(typeof(DealCoverageData), "NegativeEmptyTestCases")]
        public void DealCoveragesController_IsNullorEmpty_NotFoundAPIException(int dealnumber)
        {
            #region Arrange

            SetupUserIdentity();
            //SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealCoveragesRepository, dealnumber);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/coverages");
			DealCoveragesController dealCoveragesController = CreateDealCoveragesController(httpRequest, new Mock<IDealCoveragesRepository>().Object);

			#endregion

			#region Assert

			Assert.Throws(typeof(ArgumentNullException), delegate { dealCoveragesController.Get(dealnumber); });
            
			#endregion

        }
		#endregion

		#region Private methods
		private DealCoveragesController CreateDealCoveragesController(HttpRequestMessage httpRequest, IDealCoveragesRepository dealCoveragesRepository)
		{
			DealCoveragesManager dealCoveragesManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealCoveragesRepository, dealLockManager.Object, transformationManager);
			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealCoveragesManager);
			DealCoveragesController dealCoveragesController = new DealCoveragesController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};
			return dealCoveragesController;

		}
		private void SetupDealCoveragesRepository(out Mock<IDealCoveragesRepository> dealCoveragesRepository, int dealnumber)
		{
            dealCoveragesRepository = new Mock<IDealCoveragesRepository>();

			IList<Data.Models.grs_VGrsDealCoverage> coveragesDbData = new List<Data.Models.grs_VGrsDealCoverage>
			{
				new Data.Models.grs_VGrsDealCoverage
				{
					Dealnum = dealnumber,
					CoverId = 13,
					CoverName = "California Earthquake"
				}
			};

            dealCoveragesRepository.Setup(p => p.GetDealCoverages(dealnumber)).Returns(coveragesDbData);
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

            public static IEnumerable NegativeEmptyTestCases
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