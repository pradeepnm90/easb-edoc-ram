
using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using Newtonsoft.Json.Linq;
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
	[Category("Deal")]
    [Category("Controllers")]
    [TestFixture]
    public class DealsControllerTests
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private CacheStoreManager cacheStoreManager;
        private DealTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new CacheStoreManager();
            transformationManager = new DealTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCaseSource(typeof(DealSearchData), "TestCases")]
        [TestCaseSource(typeof(WorkbenchDealSearchData), "TestCases")]
        public void DealsController_Get_Returns_OKResponseCode(DealSearchCriteria dealSearchCriteria)
        {
            #region Arrange
            SetupUserIdentity();

            SetupDealRepository(dealSearchCriteria, out Mock<IDealRepository> dealRepository, out IPaginatedList<grs_VGrsDeal> expected);

            SetupWorkbenchDealsRepository(dealSearchCriteria, out Mock<IWorkbenchDealsRepository> dealByStatusRepository, out List<grs_VGrsDealsByStatu> expectedDealsByStatusData);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealSearchCriteria}");

            DealsController dealsController = CreateDealsController(httpRequest, dealRepository.Object, dealByStatusRepository.Object, new Mock<ITblDealRepository>().Object, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object);

            #endregion

            #region Act

            var response = dealsController.Get(dealSearchCriteria);
            var contentResult = response as NegotiatedContentResult<ResponseCollection<Deal>>;

            #endregion

            #region Assert

            #region Expected Data

            var expectedDeal = new Deal()
            {
                DealNumber = 101,
                DealName = "Test Deal",
                ContractNumber = 100981,
                InceptionDate = DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy"),
                ExpiryDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                TargetDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                Priority = 2,
                SubmittedDate = DateTime.Now.ToString("MM-dd-yyyy"),
                StatusCode = 0,
                Status = "Bound",
                PrimaryUnderwriterCode = 52026,
                PrimaryUnderwriterName = "Tod Costikyan",
                SecondaryUnderwriterCode = 9798,
                SecondaryUnderwriterName = "Marlon Williams",
                TechnicalAssistantCode = 950996,
                TechnicalAssistantName = "Kate Trent",
                ModellerCode = 10645,
                ModellerName = "Rachael Gosling",
                ActuaryCode = 8069,
                ActuaryName = "Chris Downey",
                BrokerCode = 665,
                BrokerName = "Direct",
                BrokerContactCode = 8373,
                BrokerContactName = "Dan Malloy",
                CedantCode = 56495,
                CedantName = "Starr Indemnity & Liability Company",
                CedentLocation = 244894,
                CedentLocationname = "Starr Indemnity"
            };

            #endregion

            Assertions.AssertOkResponse(contentResult);

            var dealsData = contentResult.Content.results;
            for (int i = 0; i <= dealsData.Count - 1; i++)
            {
                //Data
                var actualDeal = dealsData[i].data;
                Assert.AreEqual(expectedDeal.DealNumber, actualDeal.DealNumber);
                Assert.AreEqual(expectedDeal.DealName, actualDeal.DealName);
                Assert.AreEqual(expectedDeal.ContractNumber, actualDeal.ContractNumber);
                Assert.AreEqual(expectedDeal.InceptionDate, actualDeal.InceptionDate);
                Assert.AreEqual(expectedDeal.ExpiryDate, actualDeal.ExpiryDate);
                Assert.AreEqual(expectedDeal.TargetDate, actualDeal.TargetDate);
                Assert.AreEqual(expectedDeal.Priority, actualDeal.Priority);
                Assert.AreEqual(expectedDeal.SubmittedDate, actualDeal.SubmittedDate);
                Assert.AreEqual(expectedDeal.StatusCode, actualDeal.StatusCode);
                Assert.AreEqual(expectedDeal.Status, actualDeal.Status);
                Assert.AreEqual(expectedDeal.PrimaryUnderwriterCode, actualDeal.PrimaryUnderwriterCode);
                Assert.AreEqual(expectedDeal.PrimaryUnderwriterName, actualDeal.PrimaryUnderwriterName);
                Assert.AreEqual(expectedDeal.SecondaryUnderwriterCode, actualDeal.SecondaryUnderwriterCode);
                Assert.AreEqual(expectedDeal.SecondaryUnderwriterName, actualDeal.SecondaryUnderwriterName);
                Assert.AreEqual(expectedDeal.TechnicalAssistantCode, actualDeal.TechnicalAssistantCode);
                Assert.AreEqual(expectedDeal.TechnicalAssistantName, actualDeal.TechnicalAssistantName);
                Assert.AreEqual(expectedDeal.ModellerCode, actualDeal.ModellerCode);
                Assert.AreEqual(expectedDeal.ModellerName, actualDeal.ModellerName);
                Assert.AreEqual(expectedDeal.ActuaryCode, actualDeal.ActuaryCode);
                Assert.AreEqual(expectedDeal.BrokerCode, actualDeal.BrokerCode);
                Assert.AreEqual(expectedDeal.BrokerName, actualDeal.BrokerName);
                Assert.AreEqual(expectedDeal.BrokerContactCode, actualDeal.BrokerContactCode);
                Assert.AreEqual(expectedDeal.BrokerContactName, actualDeal.BrokerContactName);

                //Links & Messages
                // Assert.IsEmpty(dealsData[i].links);
                Assert.IsEmpty(dealsData[i].messages);
            }

            #endregion

        }

        [Test]
        public void DealsController_GetWithDealNumber_Returns_OKResponseCode()
        {

            #region Arrange

            SetupDealRepo(out Mock<IDealRepository> dealRepository);

            var workBenchDeaslRepository = new Mock<IWorkbenchDealsRepository>();

            var dealNumber = 101;

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}");

            DealsController dealsController = CreateDealsController(httpRequest, dealRepository.Object, workBenchDeaslRepository.Object, new Mock<ITblDealRepository>().Object, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object);

            #endregion

            #region Act

            var response = dealsController.Get(dealNumber);
            var contentResult = response as NegotiatedContentResult<ResponseItem<Deal>>;

            #endregion

            #region Assert

            #region Expected Data

            var expectedDeal = new Deal()
            {
                DealNumber = 101,
                DealName = "Test Deal",
                ContractNumber = 100981,
                InceptionDate = DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy"),
                ExpiryDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                TargetDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                Priority = 2,
                SubmittedDate = DateTime.Now.ToString("MM-dd-yyyy"),
                StatusCode = 0,
                Status = "Bound",
                PrimaryUnderwriterCode = 52026,
                PrimaryUnderwriterName = "Tod Costikyan",
                SecondaryUnderwriterCode = 9798,
                SecondaryUnderwriterName = "Marlon Williams",
                TechnicalAssistantCode = 950996,
                TechnicalAssistantName = "Kate Trent",
                ModellerCode = 10645,
                ModellerName = "Rachael Gosling",
                ActuaryCode = 8069,
                ActuaryName = "Chris Downey",
                BrokerCode = 665,
                BrokerName = "Direct",
                BrokerContactCode = 8373,
                BrokerContactName = "Dan Malloy",
                CedantCode = 56495,
                CedantName = "Starr Indemnity & Liability Company",
                Continuous = true,
                Renewal = 0,
                Dealtype = 4,
                DealtypeName = "Reinsurance - Treaty",
                Coveragetype = 2,
                CoveragetypeName = "Per Occurrence Excess loss",
                Policybasis = 2,
                PolicybasisName = "Losses Occurring During",
                Currency = 1,
                CurrencyName = "U.S. DOLLARS",
                Domicile = 1,
                DomicileName = "United States",
                Region = 0,
                RegionName = "",
                CedentLocation = 244894,
                CedentLocationname = "Starr Indemnity"
            };

            #endregion

            Assertions.AssertOkResponse(contentResult);
            var dealsData = contentResult.Content.data;

            //Data
            var actualDeal = dealsData;
            Assert.AreEqual(expectedDeal.DealNumber, actualDeal.DealNumber);
            Assert.AreEqual(expectedDeal.DealName, actualDeal.DealName);
            Assert.AreEqual(expectedDeal.ContractNumber, actualDeal.ContractNumber);
            Assert.AreEqual(expectedDeal.InceptionDate, actualDeal.InceptionDate);
            Assert.AreEqual(expectedDeal.ExpiryDate, actualDeal.ExpiryDate);
            Assert.AreEqual(expectedDeal.TargetDate, actualDeal.TargetDate);
            Assert.AreEqual(expectedDeal.Priority, actualDeal.Priority);
            Assert.AreEqual(expectedDeal.SubmittedDate, actualDeal.SubmittedDate);
            Assert.AreEqual(expectedDeal.StatusCode, actualDeal.StatusCode);
            Assert.AreEqual(expectedDeal.Status, actualDeal.Status);
            Assert.AreEqual(expectedDeal.PrimaryUnderwriterCode, actualDeal.PrimaryUnderwriterCode);
            Assert.AreEqual(expectedDeal.PrimaryUnderwriterName, actualDeal.PrimaryUnderwriterName);
            Assert.AreEqual(expectedDeal.SecondaryUnderwriterCode, actualDeal.SecondaryUnderwriterCode);
            Assert.AreEqual(expectedDeal.SecondaryUnderwriterName, actualDeal.SecondaryUnderwriterName);
            Assert.AreEqual(expectedDeal.TechnicalAssistantCode, actualDeal.TechnicalAssistantCode);
            Assert.AreEqual(expectedDeal.TechnicalAssistantName, actualDeal.TechnicalAssistantName);
            Assert.AreEqual(expectedDeal.ModellerCode, actualDeal.ModellerCode);
            Assert.AreEqual(expectedDeal.ModellerName, actualDeal.ModellerName);
            Assert.AreEqual(expectedDeal.ActuaryCode, actualDeal.ActuaryCode);
            Assert.AreEqual(expectedDeal.BrokerCode, actualDeal.BrokerCode);
            Assert.AreEqual(expectedDeal.BrokerName, actualDeal.BrokerName);
            Assert.AreEqual(expectedDeal.BrokerContactCode, actualDeal.BrokerContactCode);
            Assert.AreEqual(expectedDeal.BrokerContactName, actualDeal.BrokerContactName);
			Assert.AreEqual(expectedDeal.CedantCode, actualDeal.CedantCode);
			Assert.AreEqual(expectedDeal.CedantName, actualDeal.CedantName);
			Assert.AreEqual(expectedDeal.Continuous, actualDeal.Continuous);
            Assert.AreEqual(expectedDeal.Renewal, actualDeal.Renewal);
            Assert.AreEqual(expectedDeal.Dealtype, actualDeal.Dealtype);
            Assert.AreEqual(expectedDeal.DealtypeName, actualDeal.DealtypeName);
            Assert.AreEqual(expectedDeal.Coveragetype, actualDeal.Coveragetype);
            Assert.AreEqual(expectedDeal.CoveragetypeName, actualDeal.CoveragetypeName);
            Assert.AreEqual(expectedDeal.Policybasis, actualDeal.Policybasis);
            Assert.AreEqual(expectedDeal.PolicybasisName, actualDeal.PolicybasisName);
            Assert.AreEqual(expectedDeal.Currency, actualDeal.Currency);
            Assert.AreEqual(expectedDeal.CurrencyName, actualDeal.CurrencyName);
            Assert.AreEqual(expectedDeal.Domicile, actualDeal.Domicile);
            Assert.AreEqual(expectedDeal.DomicileName, actualDeal.DomicileName);
            Assert.AreEqual(expectedDeal.Region, actualDeal.Region);
            Assert.AreEqual(expectedDeal.RegionName, actualDeal.RegionName);
            Assert.AreEqual(expectedDeal.CedentLocation, actualDeal.CedentLocation);
            Assert.AreEqual(expectedDeal.CedentLocationname, actualDeal.CedentLocationname);


            //Links & Messages
            // Assert.IsEmpty(contentResult.Content.links);
            Assert.IsEmpty(contentResult.Content.messages);
            #endregion
        }

        [Test]
        [TestCaseSource(typeof(DealSearchData), "NegativeTestCases")]
        [TestCaseSource(typeof(WorkbenchDealSearchData), "NegativeTestCases")]
        public void DealsController_Get_Throws_IllegalArgumentAPIException(DealSearchCriteria dealSearchCriteria)
        {
            #region Arrange

            SetupDealRepository(dealSearchCriteria, out Mock<IDealRepository> dealRepository, out IPaginatedList<grs_VGrsDeal> expected);

            SetupWorkbenchDealsRepository(dealSearchCriteria, out Mock<IWorkbenchDealsRepository> dealByStatusRepository, out List<grs_VGrsDealsByStatu> expectedDealsByStatusData);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealSearchCriteria}");

            DealsController dealsController = CreateDealsController(httpRequest, dealRepository.Object, dealByStatusRepository.Object, new Mock<ITblDealRepository>().Object, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object);

            #endregion

            #region Act & Assert

            Assert.Throws(typeof(IllegalArgumentAPIException), delegate { dealsController.Get(dealSearchCriteria); });

            #endregion

        }

        // Need to refactor the below code to use CedantRepository test data load through MOCK Framework

        //[Test]
        //[TestCaseSource(typeof(DealPutData), "DealPutTestCases")]
        //public void DealsController_Put_Returns_OKResponseCode(Deal deal, int dealNumber)
        //{
        //    #region Arrange
        //    SetupUserIdentity();

        //    Mock<ITblDealRepository> tblDealRepository = SetupTblDealRepository();
        //    SetupDealRepo(out Mock<IGlobalReDealsRepository> dealByStatusRepository);
        //    SetupCedantsRepository(out Mock<ICedantsRepository> cedantsRepository);

        //    #endregion

        //    #region Act
        //    var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPUT), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}");
        //    DealsController dealsController = CreateDealsController(httpRequest, new Mock<IDealRepository>().Object, dealByStatusRepository.Object, tblDealRepository.Object, cedantsRepository.Object);
        //    var response = dealsController.Put(dealNumber, deal);
        //    #endregion

        //    #region Expected Data

        //    var expectedDeal = new Deal()
        //    {
        //        DealNumber = 101,
        //        DealName = "Test Deal",
        //        ContractNumber = 100981,
        //        InceptionDate = DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy"),
        //        ExpiryDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
        //        TargetDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
        //        Priority = 2,
        //        SubmittedDate = DateTime.Now.ToString("MM-dd-yyyy"),
        //        StatusCode = 0,
        //        Status = "Bound",
        //        PrimaryUnderwriterCode = 52026,
        //        PrimaryUnderwriterName = "Tod Costikyan",
        //        SecondaryUnderwriterCode = 9798,
        //        SecondaryUnderwriterName = "Marlon Williams",
        //        TechnicalAssistantCode = 950996,
        //        TechnicalAssistantName = "Kate Trent",
        //        ModellerCode = 10645,
        //        ModellerName = "Rachael Gosling",
        //        ActuaryCode = 8069,
        //        ActuaryName = "Chris Downey",
        //        BrokerCode = 665,
        //        BrokerName = "Direct",
        //        BrokerContactCode = 8373,
        //        BrokerContactName = "Dan Malloy",
        //        CedantCode = 56495,
        //        CedantName = "Starr Indemnity & Liability Company",
        //        CedentLocation = 244894,
        //        CedentLocationname = "Starr Indemnity"
        //    };

        //    #endregion

        //    #region Assert
        //    var contentResult = response as NegotiatedContentResult<ResponseItem<Deal>>;

        //    Assertions.AssertOkResponse(contentResult);
        //    var dealsData = contentResult.Content.data;

        //    //Data
        //    var actualDeal = dealsData;
        //    Assert.AreEqual(expectedDeal.DealNumber, actualDeal.DealNumber);
        //    Assert.AreEqual(expectedDeal.DealName, actualDeal.DealName);
        //    Assert.AreEqual(expectedDeal.ContractNumber, actualDeal.ContractNumber);
        //    Assert.AreEqual(expectedDeal.InceptionDate, actualDeal.InceptionDate);
        //    Assert.AreEqual(expectedDeal.ExpiryDate, actualDeal.ExpiryDate);
        //    Assert.AreEqual(expectedDeal.TargetDate, actualDeal.TargetDate);
        //    Assert.AreEqual(expectedDeal.Priority, actualDeal.Priority);
        //    Assert.AreEqual(expectedDeal.SubmittedDate, actualDeal.SubmittedDate);
        //    Assert.AreEqual(expectedDeal.StatusCode, actualDeal.StatusCode);
        //    Assert.AreEqual(expectedDeal.Status, actualDeal.Status);
        //    Assert.AreEqual(expectedDeal.PrimaryUnderwriterCode, actualDeal.PrimaryUnderwriterCode);
        //    Assert.AreEqual(expectedDeal.PrimaryUnderwriterName, actualDeal.PrimaryUnderwriterName);
        //    Assert.AreEqual(expectedDeal.SecondaryUnderwriterCode, actualDeal.SecondaryUnderwriterCode);
        //    Assert.AreEqual(expectedDeal.SecondaryUnderwriterName, actualDeal.SecondaryUnderwriterName);
        //    Assert.AreEqual(expectedDeal.TechnicalAssistantCode, actualDeal.TechnicalAssistantCode);
        //    Assert.AreEqual(expectedDeal.TechnicalAssistantName, actualDeal.TechnicalAssistantName);
        //    Assert.AreEqual(expectedDeal.ModellerCode, actualDeal.ModellerCode);
        //    Assert.AreEqual(expectedDeal.ModellerName, actualDeal.ModellerName);
        //    Assert.AreEqual(expectedDeal.ActuaryCode, actualDeal.ActuaryCode);
        //    Assert.AreEqual(expectedDeal.BrokerCode, actualDeal.BrokerCode);
        //    Assert.AreEqual(expectedDeal.BrokerName, actualDeal.BrokerName);
        //    Assert.AreEqual(expectedDeal.BrokerContactCode, actualDeal.BrokerContactCode);
        //    Assert.AreEqual(expectedDeal.BrokerContactName, actualDeal.BrokerContactName);
        //    Assert.AreEqual(expectedDeal.CedantCode, actualDeal.CedantCode);
        //    Assert.AreEqual(expectedDeal.CedantName, actualDeal.CedantName);
        //    Assert.AreEqual(expectedDeal.CedentLocation, actualDeal.CedentLocation);
        //    Assert.AreEqual(expectedDeal.CedentLocationname, actualDeal.CedentLocationname);

        //    //Links & Messages
        //    //  Assert.IsEmpty(contentResult.Content.links);
        //    Assert.IsEmpty(contentResult.Content.messages);
        //    #endregion

        //}

        [Test]
        [TestCaseSource(typeof(DealPutData), "NullDealPutTestCases")]
        public void DealsController_NullDeal_Put_Returns_OKResponseCode(Deal deal, int dealNumber)
        {
            #region Act
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPUT), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}");
            DealsController dealsController = CreateDealsController(httpRequest, new Mock<IDealRepository>().Object, new Mock<IWorkbenchDealsRepository>().Object, new Mock<ITblDealRepository>().Object, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object);
            var response = dealsController.Put(dealNumber, deal);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;
            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealPutData), "DealMisMatchPutTestCases")]
        public void DealsController_DealMisMatch_Put_Returns_OKResponseCode(Deal deal, int dealNumber)
        {
            #region Act
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPUT), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}");
            DealsController dealsController = CreateDealsController(httpRequest, new Mock<IDealRepository>().Object, new Mock<IWorkbenchDealsRepository>().Object, new Mock<ITblDealRepository>().Object, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object);
            var response = dealsController.Put(dealNumber, deal);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;
            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.Ambiguous);
            #endregion
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

        #region Private Methods 

        private DealsController CreateDealsController(HttpRequestMessage httpRequest, IDealRepository dealRepository, IWorkbenchDealsRepository dealByStatusRepository, ITblDealRepository tblDealRepository, ICedantManager cedantManager, IBrokerManager brokerManager)
        {
            SetupDealStatusesLookupManager(out Mock<IDealStatusesLookupManager> dealStatusesLookupManager);

            DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository, dealByStatusRepository, tblDealRepository, 
                dealStatusesLookupManager.Object, dealLockManager.Object, transformationManager, cedantManager, brokerManager, new Mock<ITblClausesDealRepository>().Object);

            DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealManager,  dealLockManager.Object);

            DealsController dealsController = new DealsController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };
            return dealsController;
        }

        private void SetupDealStatusesLookupManager(out Mock<IDealStatusesLookupManager> dealStatusesLookupManager)
        {
            dealStatusesLookupManager = new Mock<IDealStatusesLookupManager>();
            dealStatusesLookupManager.Setup(d => d.GetGlobalReDealStatusCodes()).Returns(new List<int> { 0, 16 });
            dealStatusesLookupManager.Setup(d => d.GetGlobalReDealStatusNames()).Returns(new List<string> { "bound", "on hold" });

        }

        private void SetupDealRepository(DealSearchCriteria dealSearchCriteria, out Mock<IDealRepository> dealRepository, out IPaginatedList<grs_VGrsDeal> expected)
        {
            //Create mock repository
            dealRepository = new Mock<IDealRepository>();

            //Build response
            List<grs_VGrsDeal> dealDbData = new List<grs_VGrsDeal> {
                new grs_VGrsDeal()
                {
                    Dealnum=101,
                    Dealname="Test Deal",
                    Contractnum=100981,
                    Inceptdate=DateTime.Now.AddYears(-10),
                    Expirydate=DateTime.Now.AddYears(2),
                    Targetdt=DateTime.Now.AddYears(2),
                    ModelPriority=2,
                    Submissiondate=DateTime.Now,
                    Status=0,
                    StatusName="Bound",
                    DealpipelineComplete=1,
                    InForce=0,
                    Renewable=0,
                    Uw1=52026,
                    Uw1Name="Tod Costikyan",
                    Uw2=9798,
                    Uw2Name="Marlon Williams",
                    Ta=950996,
                    TaName="Kate Trent",
                    Modeller=10645,
                    ModellerName="Rachael Gosling",
                    Act1=8069,
                    Act1Name="Chris Downey",
                    Broker=665,
                    BrokerName="Direct",
                    BrokerContact=8373,
                    BrokerContactName="Dan Malloy",
                    Division="MGR",
                    Paper=1,
                    PaperName="Markel Bermuda Limited",
                    Team=1,
                    TeamName="P&C Re Specialty (BDA)"
                }
            };

            expected = new PaginatedList<grs_VGrsDeal>()
            {
                PageCount = 10,
                PageIndex = 0,
                PageSize = 10,
                TotalRecordCount = 1,
                Items = dealDbData
            };

            // Search method
            dealRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>()))
                    .Returns(expected);

            //Filter & Sort Parameters

            dealRepository
             .Setup(x => x.GetFilterParameters())
                 .Returns(new List<string> { "StatusName", "Status",  "GlobalReData" });

            dealRepository
            .Setup(x => x.GetSortParameters())
            .Returns(new List<string> { "StatusName", "Dealnum" });
        }

        private void SetupWorkbenchDealsRepository(DealSearchCriteria dealSearchCriteria, out Mock<IWorkbenchDealsRepository> dealByStatusRepository, out List<grs_VGrsDealsByStatu> dealDbData)
        {
            //Create mock repository
            dealByStatusRepository = new Mock<IWorkbenchDealsRepository>();

            //Build response
            dealDbData = new List<grs_VGrsDealsByStatu> { new grs_VGrsDealsByStatu()
                {
                    Dealnum=101,
                    Dealname="Test Deal",
                    Contractnum=100981,
                    Inceptdate=DateTime.Now.AddYears(-10),
                    Expirydate=DateTime.Now.AddYears(2),
                    Targetdt=DateTime.Now.AddYears(2),
                    ModelPriority=2,
                    Submissiondate=DateTime.Now,
                    Status=0,
                    StatusName1="Bound",
                    DealPipelineComplete=1,
                    InForce=0,
                    Renewable=0,
                    Uw1=52026,
                    Uw1Name="Tod Costikyan",
                    Uw2=9798,
                    Uw2Name="Marlon Williams",
                    Ta=950996,
                    TaName="Kate Trent",
                    Modeller=10645,
                    ModellerName="Rachael Gosling",
                    Act1=8069,
                    Act1Name="Chris Downey",
                    Broker=665,
                    BrokerName="Direct",
                    BrokerContact=8373,
                    BrokerContactName="Dan Malloy",
                    Division="MGR",
                    Paper=1,
                    PaperName="Markel Bermuda Limited",
                    Team=1,
                    TeamName="P&C Re Specialty (BDA)",

                } };

            //expectedDealsByStatusData = new PaginatedList<grs_VGrsDealsByStatu>()
            //{
            //    PageCount = 10,
            //    PageIndex = 0,
            //    PageSize = 10,
            //    TotalRecordCount = 1,
            //    Items = dealDbData
            //};

            // Search method
            //dealByStatusRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>()))
            //        .Returns(expectedDealsByStatusData);

            dealByStatusRepository.Setup(p => p.GetDeals(It.IsAny<SearchCriteria>()))
                    .Returns(dealDbData);

            dealByStatusRepository
                         .Setup(x => x.GetFilterParameters())
                             .Returns(new List<string> { "StatusCodes", "GlobalReData", "SubDivisions", "Exposuretypes", "ProductLines", "ExposureGroups", "PersonIds" });

            dealByStatusRepository
            .Setup(x => x.GetSortParameters())
            .Returns(new List<string> { "StatusName", "Dealnum" });

            dealByStatusRepository
            .Setup(x => x.GetStatusCodes())
                .Returns(new List<int> { 3, 80, 2, 14, 29, 16, 1000, 1001, 23, 41, 1, 22 });

            //dealByStatusRepository
            //    .Setup(x => x.GetSubDivisionsWithAssociatedExposuretype())
            //    .Returns(new Dictionary<int, List<int>>
            //{
            //    { 1, new List<int> { 5916, 1126, 1128 } },
            //    { 2, new List<int> { 5922 } }
            //});

            dealByStatusRepository
                .Setup(x => x.GetGlobalReExposureTree())
                .Returns(new List<grs_VExposureTreeExt> {
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

            dealByStatusRepository.Setup(x => x.ValidPerson(12)).Returns(true);

        }

        private void SetupDealRepo(out Mock<IDealRepository> dealRepository)
        {
            //Create mock repository
            dealRepository = new Mock<IDealRepository>();

            //Build response
            grs_VGrsDeal dealData = new grs_VGrsDeal()
            {
                Dealnum = 101,
                Dealname = "Test Deal",
                Contractnum = 100981,
                Inceptdate = DateTime.Now.AddYears(-10),
                Expirydate = DateTime.Now.AddYears(2),
                Targetdt = DateTime.Now.AddYears(2),
                ModelPriority = 2,
                Submissiondate = DateTime.Now,
                Status = 0,
                StatusName = "Bound",
                InForce = 0,
                Renewable = 0,
                Uw1 = 52026,
                Uw1Name = "Tod Costikyan",
                Uw2 = 9798,
                Uw2Name = "Marlon Williams",
                Ta = 950996,
                TaName = "Kate Trent",
                Modeller = 10645,
                ModellerName = "Rachael Gosling",
                Act1 = 8069,
                Act1Name = "Chris Downey",
                Broker = 665,
                BrokerName = "Direct",
                BrokerContact = 8373,
                BrokerContactName = "Dan Malloy",
                Division = "MGR",
                Paper = 1,
                PaperName = "Markel Bermuda Limited",
                Team = 1,
                TeamName = "P&C Re Specialty (BDA)",
                Cedant = 56495,
                CedantName = "Starr Indemnity & Liability Company",
                Continuous = true,
                Renewal = 0,
                Dealtype = 4,
                DealtypeName = "Reinsurance - Treaty",
                Coveragetype = 2,
                CoveragetypeName = "Per Occurrence Excess loss",
                Policybasis = 2,
                PolicybasisName = "Losses Occurring During",
                Currency = 1,
                CurrencyName = "U.S. DOLLARS",
                Domicile = 1,
                DomicileName = "United States",
                Region = 0,
                RegionName = "",
                CedentLocation = 244894,
                CedentLocationname = "Starr Indemnity"
            };

            dealRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDeal, bool>>>())).Returns(dealData);
        }

        private void SetupCedantRepository(out Mock<ICedantRepository> cedantRepository)
        {
            cedantRepository = new Mock<ICedantRepository>();
            CedantsSearchCriteria criteria = new CedantsSearchCriteria
            {
                CedantName = "Starr Indemnity & Liability Company",
                ParentGroupName = "Starr International Company, Inc.",
                CedantId = "56495",
                ParentGroupId = "1019169",
                LocationId = "244894"
            };

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

        private static Mock<ITblDealRepository> SetupTblDealRepository()
        {
            Mock<ITblDealRepository> tblDealRepository = new Mock<ITblDealRepository>();

            var expectedTbDealPipeline = new TbDealPipeline()
            {
                DealNum = 101,
                ModelPriority = 2,
                Ta = 2
            };

            var expectedTblDeal = new TblDeal()
            {
                Dealnum = 101,
                Dealname = "Test Deal",
                Status = 3,
                Contractnum = 100981,
                Inceptdate = Convert.ToDateTime(DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy")),
                TbDealPipeline = expectedTbDealPipeline
            };

            tblDealRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TblDeal, bool>>>(), It.IsAny<Expression<Func<TblDeal, object>>>())).Returns(expectedTblDeal);

            return tblDealRepository;
        }

        private void SetupUserIdentity()
        {
			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, null, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        private Mock<IEntityLockManager> SetupDealLockManager(int dealNumber, int userId)
        {
            Mock<IEntityLockManager> dealLockManager = new Mock<IEntityLockManager>();
            dealLockManager.Setup(p => p.Lock((int)EntityLockType.Deals, dealNumber, userId)).Returns(true);
            return dealLockManager;
        }

        #endregion

        #region Test Case Data

        public class DealSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    //yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "16",  GlobalReData = true });
                    //yield return new TestCaseData(new DealSearchCriteria() { Status = "Bound",  GlobalReData = true });
                    //yield return new TestCaseData(new DealSearchCriteria() { StatusCode = 0,  GlobalReData = true });
                    yield return new TestCaseData(null);
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59", Exposuretypes = "5916",  GlobalReData = true });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59",  GlobalReData = true });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001",  GlobalReData = true });
                    yield return new TestCaseData(new DealSearchCriteria() { PersonIds = "12",  GlobalReData = true });

                }
            }
            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(new DealSearchCriteria() { Status = -99,  GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { Status = 9999, GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "99", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "99", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1,ABC", ProductLines = "99", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "99", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001,ABC", ExposureGroups = "99", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59", Exposuretypes = "12", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59,ABC", Exposuretypes = "12", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59", Exposuretypes = "12", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1", ProductLines = "1001", ExposureGroups = "59", Exposuretypes = "12,ABC", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { Exposuretypes = "5922", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { ProductLines = "1001", ExposureGroups = "59", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { ExposureGroups = "59", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { ProductLines = "1001", ExposureGroups = "59", Exposuretypes = "5922", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { ExposureGroups = "59", Exposuretypes = "5922", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { ProductLines = "1001", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { PersonIds = "1001,abc", GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { PersonIds = "1212", GlobalReData = true });
				}
            }
        }

        public class WorkbenchDealSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "16",  GlobalReData = false });
                    //yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "16",  GlobalReData = false, sort = "dealNumber" });
                    yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "3,80,2,14,29",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1,2",  GlobalReData = false });
                }
            }
            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "3,bound",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "1002",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "99",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "invalid",  GlobalReData = false });
                    yield return new TestCaseData(new DealSearchCriteria() { SubDivisions = "1,invalid",  GlobalReData = false });
                }
            }

        }

        public class DealPutData
        {
            public static IEnumerable DealPutTestCases
            {
                get
                {
                    yield return new TestCaseData(new Deal()
                    {
                        DealNumber = 101,
                        DealName = "Test Deal",
                        ContractNumber = 100981,
                        InceptionDate = DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy"),
                        ExpiryDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                        TargetDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                        Priority = 2,
                        SubmittedDate = DateTime.Now.ToString("MM-dd-yyyy"),
                        StatusCode = 0,
                        Status = "Bound",
                        PrimaryUnderwriterCode = 52026,
                        PrimaryUnderwriterName = "Tod Costikyan",
                        SecondaryUnderwriterCode = 9798,
                        SecondaryUnderwriterName = "Marlon Williams",
                        TechnicalAssistantCode = 950996,
                        TechnicalAssistantName = "Kate Trent",
                        ModellerCode = 10645,
                        ModellerName = "Rachael Gosling",
                        ActuaryCode = 8069,
                        ActuaryName = "Chris Downey",
                        BrokerCode = 665,
                        BrokerName = "Direct",
                        BrokerContactCode = 8373,
                        BrokerContactName = "Dan Malloy",
                        CedantCode = 56495,
                        CedantName = "Starr Indemnity & Liability Company",
                        CedentLocation = 244894,
                        CedentLocationname = "Starr Indemnity",
                    }, 101);
                }
            }
            public static IEnumerable NullDealPutTestCases
            {
                get
                {
                    yield return new TestCaseData(null, 101);
                }
            }
            public static IEnumerable DealMisMatchPutTestCases
            {
                get
                {
                    yield return new TestCaseData(new Deal()
                    {
                        DealNumber = 101,
                        DealName = "Test Deal",
                        ContractNumber = 100981,
                        InceptionDate = DateTime.Now.AddYears(-10).ToString("MM-dd-yyyy"),
                        ExpiryDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                        TargetDate = DateTime.Now.AddYears(2).ToString("MM-dd-yyyy"),
                        Priority = 2,
                        SubmittedDate = DateTime.Now.ToString("MM-dd-yyyy"),
                        StatusCode = 0,
                        Status = "Bound",
                        PrimaryUnderwriterCode = 52026,
                        PrimaryUnderwriterName = "Tod Costikyan",
                        SecondaryUnderwriterCode = 9798,
                        SecondaryUnderwriterName = "Marlon Williams",
                        TechnicalAssistantCode = 950996,
                        TechnicalAssistantName = "Kate Trent",
                        ModellerCode = 10645,
                        ModellerName = "Rachael Gosling",
                        ActuaryCode = 8069,
                        ActuaryName = "Chris Downey",
                        BrokerCode = 665,
                        BrokerName = "Direct",
                        BrokerContactCode = 8373,
                        BrokerContactName = "Dan Malloy",
                        CedantCode = 56495,
                        CedantName = "Starr Indemnity & Liability Company",
                        CedentLocation = 244894,
                        CedentLocationname = "Starr Indemnity",
                    }, 102);
                }
            }
        }

        public class DealPatchData
        {
            public static IEnumerable DealPatchObject
            {
                get
                {
                    yield return new TestCaseData(new JObject(JObject.Parse("{\"dealNumber\": 123,  \"LockDeal\": true,  \"DealNumber\": 123}")));
                    yield return new TestCaseData(new JObject(JObject.Parse("{\"dealNumber\": 123,  \"LockDeal\": false,  \"DealNumber\": 123}")));
                }
            }
        }

        #endregion

    }
}
