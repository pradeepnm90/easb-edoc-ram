using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
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

    [Category("Deal Status Summaries")]
    [Category("Controllers")]
    [TestFixture]
    public class DealStatusSummariesControllerTests
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private CacheStoreManager cacheStoreManager;
        private Mock<IEntityLockManager> dealLockManager;
        private DealStatusSummariesTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new CacheStoreManager();
            dealLockManager = new Mock<IEntityLockManager>();
            transformationManager = new DealStatusSummariesTransformationManager(userManager.Object);
        }

        #endregion

        #region Test Methods

        [TestCaseSource(typeof(DealSummarySearchData), "TestCases")]
        public void DealStatusSummariesController_Get_Returns_OKResponseCode(DealSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();

            //Create mock repository
            var dealStatusSummariesRepository = new Mock<IDealStatusSummariesRepository>();

            //Build response
            IList<Data.Models.grs_PrGetGrsDealCountByStatus> dealDbData = new List<Data.Models.grs_PrGetGrsDealCountByStatus>
            {
                new Data.Models.grs_PrGetGrsDealCountByStatus
                {
                    StatusGroup =3,
                    StatusGroupName ="Bound - Pending Actions",
                    StatusGroupSortOrder =3,
                    StatusCode =1000,
                    StatusName ="Bound - Pending Actions",
                    StatusSortOrder=1,
                    Count=1578,
                    WorkflowID=1,
                    WorkflowName="Default"
                }
            };


            // Search method
            dealStatusSummariesRepository.Setup(p => p.GetAllDealStatusSummaries(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(dealDbData);
            dealStatusSummariesRepository.Setup(p => p.ValidPerson(It.IsAny<int>()))
                    .Returns(true);
            dealStatusSummariesRepository.Setup(x => x.GetGlobalReExposureTree()).Returns(new List<grs_VExposureTreeExt> {
                new grs_VExposureTreeExt
                {
                 SubdivisionCode =  1,
                 SubdivisionName = "Casualty",
                 ProductLineCode = 1001,
                 ProductLineName = "Auto Reins",
                 ExposureGroupCode  = 59,
                 ExposureGroupName =  "CA - Auto - Commercial",
                 ExposureTypeCode =  5916,
                 ExposureTypeName = "CA - Auto - Commercial - Non Trucking"
                },
                new grs_VExposureTreeExt
                {
                 SubdivisionCode =  2,
                 SubdivisionName = "Casualty",
                 ProductLineCode = 1001,
                 ProductLineName = "Auto Reins",
                 ExposureGroupCode  = 59,
                 ExposureGroupName =  "CA - Auto - Commercial",
                 ExposureTypeCode =  5922,
                 ExposureTypeName = "CA - Auto - Commercial - Non Trucking"
                }

                });
            //Manager
            DealStatusSummariesManager dealStatusSummariesManager = new DealStatusSummariesManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealStatusSummariesRepository.Object, transformationManager);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealStatusSummaryRoutePrefix}");

            DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealStatusSummariesManager);

            DealStatusSummariesController dealStatusSummariesController = new DealStatusSummariesController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };



            #endregion

            #region Act

            var response = dealStatusSummariesController.GetAll(criteria);
            var contentResult = response as NegotiatedContentResult<ResponseCollection<DealStatusSummary>>;

            #endregion

            #region Assert

            #region Expected

            var expectedDealSummary = new DealStatusSummary
            {
                StatusCode = 1000,
                StatusName = "Bound - Pending Actions",
                SortOrder = 3,
                Count = 1578,
                WorkflowId = 1,
                WorkflowName = "Default"
            };

            var expectedGetLink = new Link(LinkType.RelatedEntity, EntityType.Deals, $"v1/deals?statuscodes={expectedDealSummary.StatusCode}", HttpMethodType.GET);

            #endregion

            Assertions.AssertOkResponse(contentResult);

            var summaryData = contentResult.Content.results;

            for (int i = 0; i <= summaryData.Count - 1; i++)
            {
                //Data
                var actualDealSummary = summaryData[i].data;
				Assertions.AssertData(expectedDealSummary, actualDealSummary);

                //Links & Messages
                Assert.Multiple(() =>
                {
                    Assert.IsNotEmpty(summaryData[i].links);
                    Assert.AreEqual(1, summaryData[i].links.Count);
                    Assertions.AssertLink(expectedGetLink, summaryData[i].links[0]);
                });

                Assert.IsEmpty(summaryData[i].messages);
            }

            #endregion

        }

        #endregion

        #region Private Methods
        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on Subdivision/Exposure Groupings", false },
                { "Default view based on current user", false },
                { "Default view based on manager", true }
            };

			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, permission, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        #endregion

        #region TearDown

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

        #region Test Case data

        public class DealSummarySearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1" ,PersonIds = "1"});
                    yield return new TestCaseData(new DealSearchCriteria());
                }
            }

        }

        #endregion
    }
}
