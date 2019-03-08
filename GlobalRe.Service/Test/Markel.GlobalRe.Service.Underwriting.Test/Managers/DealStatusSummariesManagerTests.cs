using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Markel.GlobalRe.Service.Underwriting.Test.Controllers.DealStatusSummariesControllerTests;

namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
    [Category("Deal Status Summaries")]
    [Category("Managers")]
    [TestFixture]
    public class DealStatusSummariesManagerTests
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private CacheStoreManager cacheStoreManager;
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
            transformationManager = new DealStatusSummariesTransformationManager(userManager.Object);
        }

        #endregion

        #region Test Methods


        [TestCaseSource(typeof(DealSummarySearchData), "TestCases")]
        public void DealStatusSummariesManager_GetAllDealStatusSummaries_Returns_ExpectedCount(DealSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();

            //Create mock repository
            var dealStatusSummariesRepository = new Mock<IDealStatusSummariesRepository>();

            //Build response
            IList<Data.Models.grs_PrGetGrsDealCountByStatus> dealDbData = GetDealDbData();


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

            #endregion

            #region Act

            EntityResult<IEnumerable<BLL_DealStatusSummary>> actual = dealStatusSummariesManager.GetAllDealStatusSummaries(criteria.ToSearchCriteria());

            #endregion

            #region Assert

            int expectedCount = 2;
            Assert.AreEqual(expectedCount, actual.Data.Count());

            #endregion

        }

        [TestCaseSource(typeof(BLL_DealStatusSummaryData), "TestCases")]
        public void DealStatusSummariesManager_GetEntityActions_Returns_EntityActionWithExpectedKey(BLL_DealStatusSummary entity)
        {
            DealStatusSummariesManager dealStatusSummariesManager = new DealStatusSummariesManager(userManager.Object, cacheStoreManager, mockLogManager.Object, new Mock<IDealStatusSummariesRepository>().Object, transformationManager);

            var res = dealStatusSummariesManager.GetEntityActions(entity);

            var expectedKey = entity?.StatusCode;
            if (expectedKey.HasValue) { Assert.AreEqual(Convert.ToString(expectedKey), res.EntityKey); }
            else { Assert.AreEqual(expectedKey, res.EntityKey); }
        }

        //[Test]
        //public void DealStatusSummariesManager_GetAllDealStatusSummaries_Returns_ExpectedCount_If_Subdivision_IS_Empty()
        //{
        //    #region Arrange
        //    SetupUserIdentity();

        //    //Create mock repository
        //    var dealStatusSummariesRepository = new Mock<IDealStatusSummariesRepository>();

        //    Mock<IPersonProfileRepository> personProfileRepository = new Mock<IPersonProfileRepository>();
        //    Mock<IPersonProfileManager> personProfileManager = new Mock<IPersonProfileManager>();

        //    SetupPersonProfileRepository(personProfileRepository, personProfileManager);

        //    //Build response
        //    IList<Data.Models.grs_PrGetGrsDealCountByStatus> dealDbData = GetDealDbData();

        //    SetupUserIdentity();
        //    // Search method
        //    dealStatusSummariesRepository.Setup(p => p.GetAllDealStatusSummaries(string.Empty, userIdentity.NameId))
        //            .Returns(dealDbData);

        //    //Manager
        //    DealStatusSummariesManager dealStatusSummariesManager = new DealStatusSummariesManager(userManager.Object, cacheStoreManager.Object,
        //        mockLogManager.Object, dealStatusSummariesRepository.Object, transformationManager, personProfileManager.Object);

        //    #endregion

        //    #region Act

        //    EntityResult<IEnumerable<BLL_DealStatusSummary>> actual = dealStatusSummariesManager.GetAllDealStatusSummaries(string.Empty);

        //    #endregion

        //    #region Assert

        //    int expectedCount = 2;
        //    Assert.AreEqual(expectedCount, actual.Data.Count());

        //    #endregion

        //}

        #endregion

        #region CleanUp
        [OneTimeTearDown]
        public void TearDown()
        {
            userManager = null;
            cacheStoreManager = null;
            mockLogManager = null;
            userIdentity = null;
        }

        #endregion

        #region Test Case Data

        public class BLL_DealStatusSummaryData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                         new BLL_DealStatusSummary
                         {
                             WorkflowId = 1,
                             WorkflowName = "Default",
                             StatusCode = 1,
                             StatusName = "In Progress",
                             Count = 415,
                             SortOrder = 1,
                             DealStatusSummary = new List<BLL_DealStatusSummary>()
                              {
                                 new BLL_DealStatusSummary
                                    {
                                        WorkflowId = 1,
                                        WorkflowName = "Default",
                                        StatusCode = 3,
                                        StatusName = "Under Review",
                                        Count = 415,
                                        SortOrder = 1,
                                        DealStatusSummary = null
                                    }
                             }
                         }
                        );
                    yield return new TestCaseData(null);
                }
            }

            public class DealSummarySearchData
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        yield return new TestCaseData(new DealSummarySearchCriteria() { SubDivisions = "16" });
                        yield return new TestCaseData(null);
                    }
                }

            }

        }



        #endregion

        #region private methods
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


        private List<Data.Models.grs_PrGetGrsDealCountByStatus> GetDealDbData()
        {
            return new List<Data.Models.grs_PrGetGrsDealCountByStatus>
            {
                new Data.Models.grs_PrGetGrsDealCountByStatus
                {
                    StatusGroup =1,
                    StatusGroupName ="In Progress",
                    StatusGroupSortOrder =1,
                    StatusCode =3,
                    StatusName ="Under Review",
                    StatusSortOrder=1,
                    Count=415,
                    WorkflowID=1,
                    WorkflowName= "Default"
                },
                 new Data.Models.grs_PrGetGrsDealCountByStatus
                {
                    StatusGroup =1,
                    StatusGroupName ="In Progress",
                    StatusGroupSortOrder =1,
                    StatusCode =80,
                    StatusName ="Authorize",
                    StatusSortOrder=2,
                    Count=403,
                    WorkflowID=1,
                    WorkflowName= "Default"
                },
                new Data.Models.grs_PrGetGrsDealCountByStatus
                {
                    StatusGroup =3,
                    StatusGroupName ="Bound",
                    StatusGroupSortOrder =3,
                    StatusCode =1000,
                    StatusName ="Bound",
                    StatusSortOrder=1,
                    Count=1578,
                    WorkflowID=1,
                    WorkflowName= "Default"
                }
            };
        }

        #endregion

    }
}
