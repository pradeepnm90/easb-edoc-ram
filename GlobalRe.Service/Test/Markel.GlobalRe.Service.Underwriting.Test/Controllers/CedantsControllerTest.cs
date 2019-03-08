using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
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
	[Category("Cedants")]
    [Category("Controllers")]
    [TestFixture]
    class CedantsControllerTest
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private CedantTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion


        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
            transformationManager = new CedantTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();
        }

        #endregion


        #region TestCases

        [TestCaseSource(typeof(CedantsSearchData), "TestCases")]
        public void CedantsController_GetCedant_OKResponse(CedantsSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupCedantRepository(out Mock<ICedantRepository> cedantRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.CedantsRoutePrefix}");
			CedantsController cedantsController = CreateCedantController(httpRequest, cedantRepository.Object);

			#endregion

			#region Act


            #endregion

            #region Expected Data

            var expectedCedants = new Cedant() // grs_VGrsCedant()
            {
                Cedantid = 56495,
                Name = "Starr Indemnity & Liability Company",
                //Parentgrouptypeid = 10011,
                //Parentgrouptype = "Reinsurance Cedant Group",
                Cedantgroupid = 1019169,
                Cedantgroupname = "Starr International Company, Inc.",
                Locationid = 244894,
                //Locationname = "Starr Indemnity",
                Locationaddress = "8401 N Central Expressway",
                Locationcity = "Dallas",
                Locationstate = "TX",
                Locationpostcode = null,
                Country = "United States"
                //Parentcompanyname = null,
                //Cedantcategories = "Global Re Cedant",
                //Cedantcategoryid = "90"
            };

            #endregion

            #region Assert

            if (cedantsController.Get(criteria) is NegotiatedContentResult<ResponseCollection<Cedant>> contentResult)
            {
                Assertions.AssertOkResponse(contentResult);
                for (int i = 0; i <= contentResult.Content.results.Count - 1; i++)
                {
                    var actualCedants = contentResult.Content.results[i].data;
					Assertions.AssertData(expectedCedants, actualCedants);
                    Assert.IsEmpty(contentResult.Content.results[i].messages);
                }
            }

            #endregion
        }

        [TestCaseSource(typeof(CedantsSearchData), "TestCasesRoot")]
        public void CedantsController_GetParentCedent_OKResponse(CedantsSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupCedantRepository(out Mock<ICedantRepository> cedantRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.CedantsRoutePrefix}");
			CedantsController cedantsController = CreateCedantController(httpRequest, cedantRepository.Object);
			#endregion

			#region Act

            #endregion

            #region Expected Data

            var expectedCedants = new Cedant()
            {
                Cedantid = 56495,
                Name = "Starr Indemnity & Liability Company",
                Cedantgroupid = 1019169,
                Cedantgroupname = "Starr International Company, Inc.",
                Locationid = 244894,
                Locationaddress = "8401 N Central Expressway",
                Locationcity = "Dallas",
                Locationstate = "TX",
                Locationpostcode = null,
                Country = "United States"
            };

            #endregion

            #region Assert

            if (cedantsController.Get(criteria.CedantName) is NegotiatedContentResult<ResponseCollection<Cedant>> contentResult)
            {
                Assertions.AssertOkResponse(contentResult);
                for (int i = 0; i <= contentResult.Content.results.Count - 1; i++)
                {
                    Assertions.AssertData(expectedCedants, contentResult.Content.results[i].data);
                    Assert.IsEmpty(contentResult.Content.results[i].messages);
                }
            }

            #endregion
        }

        [TestCaseSource(typeof(CedantsSearchData), "NegativeTestCasesRoot")]
        public void CedantsController_GetCedent_NotFoundAPIException(CedantsSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupCedantRepository(out Mock<ICedantRepository> cedantRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.CedantsRoutePrefix}");
			CedantsController cedantsController = CreateCedantController(httpRequest, cedantRepository.Object);

			#endregion

			#region Act


            #endregion

            #region Assert

            IHttpActionResult actionResult = cedantsController.Get(criteria.CedantName);
            Assert.AreEqual(HttpStatusCode.BadRequest, ((StatusCodeResult)actionResult).StatusCode);

            #endregion
        }


        [TestCaseSource(typeof(CedantsSearchData), "NegativeTestCases")]
        public void CedantsController_GetCedent_ValidationAPIException(CedantsSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();
            SetupCedantRepository(out Mock<ICedantRepository> cedantRepository, criteria);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.CedantsRoutePrefix}");
			CedantsController cedantsController = CreateCedantController(httpRequest, cedantRepository.Object);

			#endregion

			#region Act


            #endregion

            #region Assert

            IHttpActionResult actionResult = cedantsController.Get(criteria);
            Assert.AreEqual(HttpStatusCode.BadRequest, ((StatusCodeResult)actionResult).StatusCode);


            #endregion
        }

        #endregion


        #region Private Methods

        private void SetupCedantRepository(out Mock<ICedantRepository> cedantRepository, CedantsSearchCriteria criteria)
        {
            cedantRepository = new Mock<ICedantRepository>();
            IList<Data.Models.grs_VGrsCedant> cedantDbData = new List<Data.Models.grs_VGrsCedant>
            {
                new Data.Models.grs_VGrsCedant
                {
                    Cedantid = 56495,
                    Cedant = "Starr Indemnity & Liability Company",
                    Parentgrouptypeid = 10011,
                    Parentgrouptype = "Reinsurance Cedant Group",
                    Cedantgroupid = 1019169,
                    Cedantgroupname = "Starr International Company, Inc.",
                    Locationid = 244894,
                    Locationname = "Starr Indemnity",
                    Locationaddress = "8401 N Central Expressway",
                    Locationcity = "Dallas",
                    Locationstate = "TX",
                    Locationpostcode = null,
                    Country = "United States",
                    Parentcompanyname = null,
                    Cedantcategories = "Global Re Cedant",
                    Cedantcategoryid = "90"
                }
            };
            cedantRepository.Setup(p => p.GetCedants(criteria.CedantName, criteria.ParentGroupName, criteria.CedantId, criteria.ParentGroupId, criteria.LocationId)).Returns(cedantDbData);
        }

        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on Groupings", false },
                { "Default view based on current user", false },
                { "Default view based on manager", true }
            };

            userIdentity = new UserIdentity("GRS", 1, 1, "", "", "ONEDEV", DateTime.Now,  true, "", false, permission, null, null);
            userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

		private CedantsController CreateCedantController(HttpRequestMessage httpRequest, ICedantRepository cedantRepository)
		{

			CedantManager cedantsManager = new CedantManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, cedantRepository, transformationManager);
			CompanyAPIManager companyReAPIManager = new CompanyAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, cedantsManager);
			CedantsController cedantsController = new CedantsController(userManager.Object, companyReAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};
			return cedantsController;
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


        #region Cedants Search Data

        public class CedantsSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantId = "56495" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { ParentGroupId = "1019169" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { LocationId = "2448947" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "Starr Indemnity & Liability Company " });
                    yield return new TestCaseData(new CedantsSearchCriteria() { ParentGroupName = "Starr International Company, Inc." });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "Starr Indemnity & Liability Company ", ParentGroupName = "Starr International Company, Inc." });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantId = "56495", ParentGroupId = "1019169", LocationId = "2448947", CedantName = "Starr Indemnity & Liability Company ", ParentGroupName = "Starr International Company, Inc." });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantId = null, ParentGroupId = null, LocationId = null, CedantName = null, ParentGroupName = null });
                }
            }

            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "S" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { ParentGroupName = "S" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "S", ParentGroupName = "Starr International Company, Inc." });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "Starr Indemnity & Liability Company ", ParentGroupName = "S" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantId = "", ParentGroupId = "", LocationId = "", CedantName = "", ParentGroupName = "" });
                    yield return new TestCaseData(new CedantsSearchCriteria() {  });
                }
            }

            public static IEnumerable TestCasesRoot
            {
                get
                {
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "Starr Indemnity & Liability Company " });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = null });
                }
            }

            public static IEnumerable NegativeTestCasesRoot
            {
                get
                {
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = "S" });
                    yield return new TestCaseData(new CedantsSearchCriteria() { CedantName = null });
                }
            }

        }

        #endregion

    }
}
