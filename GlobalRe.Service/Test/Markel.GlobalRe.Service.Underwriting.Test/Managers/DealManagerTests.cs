using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
	[Category("Deal")]
    [Category("Managers")]
    [TestFixture]
    public class DealManagerTests
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

        [TestCaseSource(typeof(DealSearchData), "TestCases")]
        public void DealManager_Search_ShouldCall_RepositorySearch(DealSearchCriteria dealSearchCriteria)
        {
            // Arrange

            Mock<IDealRepository> dealRepository = new Mock<IDealRepository>();
            Mock<IWorkbenchDealsRepository> workbenchDealsRepository = new Mock<IWorkbenchDealsRepository>();
            Mock<ITblDealRepository> tblDealRepository = new Mock<ITblDealRepository>();
			Mock<ICedantManager> cedantManager = SetupCedant();

            if (dealSearchCriteria.GlobalReData) { SetupDealRepository(dealRepository); }
            else { SetupWorkbenchDealsRepository(workbenchDealsRepository); }

            //SetupDealStatusesLookupRepository(out Mock<IDealStatusesLookupRepository> dealStatusesLookupRepository);
            SetupDealStatusesLookupManager(out Mock<IDealStatusesLookupManager> dealStatusesLookupManager);

            DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository.Object, workbenchDealsRepository.Object, tblDealRepository.Object, dealStatusesLookupManager.Object, dealLockManager.Object, transformationManager,cedantManager.Object, new Mock<IBrokerManager>().Object, new Mock<ITblClausesDealRepository>().Object);

            //Act
            IPaginatedList<BLL_Deal> actual = dealManager.Search(dealSearchCriteria.ToSearchCriteria());

            int expectedTotalRecordCount = 1;

            //Assert
            if (dealSearchCriteria.GlobalReData) { dealRepository.Verify(p => p.Search(It.IsAny<SearchCriteria>())); }
            else { workbenchDealsRepository.Verify(p => p.Search(It.IsAny<SearchCriteria>())); }

            Assert.AreEqual(expectedTotalRecordCount, actual.TotalRecordCount);

        }


        [Test]
        public void DealManager_Get_ShouldCall_RepositoryGet()
        {
            // Arrange
            Mock<IDealRepository> dealRepository = new Mock<IDealRepository>();
            SetupDealRepository(dealRepository, false);

            DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository.Object, new Mock<IWorkbenchDealsRepository>().Object, new Mock<ITblDealRepository>().Object, new Mock<IDealStatusesLookupManager>().Object, new Mock<IEntityLockManager>().Object, transformationManager, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object, new Mock<ITblClausesDealRepository>().Object);

            //Act
            var dealNumber = 123;
            EntityResult<BLL_Deal> actual = dealManager.GetDeal(dealNumber);

            //Assert
            dealRepository.Verify(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDeal, bool>>>()));
            Assert.IsNotNull(actual.Data);
            Assert.AreEqual(dealNumber, actual.Data.Dealnum);

        }

        [Test]
        public void DealManager_Get_ShouldCall_RepositoryGet_Negative()
        {
			// Arrange
			Mock<IDealRepository> dealRepository = new Mock<IDealRepository>();
			Mock<ITblDealRepository> tblDealRepository = new Mock<ITblDealRepository>();
			SetupDealRepository(dealRepository, true);

			DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository.Object, new Mock<IWorkbenchDealsRepository>().Object, new Mock<ITblDealRepository>().Object, new Mock<IDealStatusesLookupManager>().Object, new Mock<IEntityLockManager>().Object, transformationManager, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object, new Mock<ITblClausesDealRepository>().Object);

			//Act
			Assert.That(() => dealManager.GetDeal(123), Throws.Exception);
        }

		[Test]
		[TestCaseSource(typeof(DealUpdateData), "UpdateData")]
		public void DealManager_Put_ShouldCall_RepositorySave(BLL_Deal entity, string bypassCedantBrokerValidation)
		{
			#region Arrange
			Mock<IDealRepository> dealRepository = new Mock<IDealRepository>();
			Mock<IWorkbenchDealsRepository> workbenchDealsRepository = new Mock<IWorkbenchDealsRepository>();
			Mock<IDealStatusesLookupManager> dealStatusesLookupManager = new Mock<IDealStatusesLookupManager>();
			Mock<ITblDealRepository> tblDealRepository = SetupTblDealRepository(false);
			Mock<ICedantManager> cedantManager = SetupCedant();
			Mock<IBrokerManager> brokerManager = SetupBroker();

			SetupUserIdentity();
			SetupDealRepository(dealRepository, false, bypassCedantBrokerValidation);

			DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository.Object, workbenchDealsRepository.Object, tblDealRepository.Object, dealStatusesLookupManager.Object, dealLockManager.Object, transformationManager, cedantManager.Object, brokerManager.Object, new Mock<ITblClausesDealRepository>().Object);
			#endregion

			#region Act
			EntityResult<BLL_Deal> actual = dealManager.UpdateDeal(entity);
			#endregion

			#region  Assert
			dealRepository.Verify(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDeal, bool>>>()));
			tblDealRepository.Verify(p => p.Save(It.IsAny<TblDeal>(), false));

			Assert.IsNotNull(actual.Data);
			Assert.AreEqual(entity.Dealnum, actual.Data.Dealnum);
			Assert.AreEqual(entity.Dealname, actual.Data.Dealname);

			#endregion
		}

		[Test]
		[TestCaseSource(typeof(DealValidateData), "ValidateData")]
		public void DealManager_Put_ShouldCall_Validate(BLL_Deal entity, string bypassCedantBrokerValidation, bool hasValidCompanyGroup)
		{
			#region Arrange
			Mock<IDealRepository> dealRepository = new Mock<IDealRepository>();
			Mock<IWorkbenchDealsRepository> workbenchDealsRepository = new Mock<IWorkbenchDealsRepository>();
			Mock<IDealStatusesLookupManager> dealStatusesLookupManager = new Mock<IDealStatusesLookupManager>();
			Mock<ITblDealRepository> tblDealRepository = SetupTblDealRepository(false);
			Mock<ICedantManager> cedantManager = SetupCedant(hasValidCompanyGroup);
			Mock<IBrokerManager> brokerManager = SetupBroker(hasValidCompanyGroup);

			SetupUserIdentity();
			SetupDealRepository(dealRepository, false, bypassCedantBrokerValidation);

			DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealRepository.Object, workbenchDealsRepository.Object, tblDealRepository.Object, dealStatusesLookupManager.Object, dealLockManager.Object, transformationManager, cedantManager.Object, brokerManager.Object, new Mock<ITblClausesDealRepository>().Object);
			#endregion

			#region Act
			#endregion

			#region  Assert
			Assert.Throws<IllegalArgumentAPIException>(delegate { dealManager.UpdateDeal(entity); });
			#endregion
		}
		[Test]
        [TestCaseSource(typeof(DealUpdateData), "UpdateData")]
        public void DealManager_Put_ShouldCall_RepositoryPut_Negative(BLL_Deal entity, string bypassCedantBrokerValidation)
        {
			#region Arrange
			var globalRedDealRepository = new Mock<IWorkbenchDealsRepository>();
            var tblDealRepository = SetupTblDealRepository(true);

            SetupWorkbenchDealsRepository(globalRedDealRepository);

            DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, new Mock<IDealRepository>().Object, globalRedDealRepository.Object, tblDealRepository.Object, new Mock<IDealStatusesLookupManager>().Object, new Mock<IEntityLockManager>().Object, transformationManager, new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object, new Mock<ITblClausesDealRepository>().Object);
			#endregion

			#region  Assert
			Assert.Throws<NotFoundAPIException>(delegate { dealManager.SaveDeal(entity); });
			//Assert.That(() => dealManager.SaveDeal(entity), Throws.Exception);
			#endregion
		}

		#endregion

		#region Private Methods
		private void SetupWorkbenchDealsRepository(Mock<IWorkbenchDealsRepository> workbenchDealsRepository)
        {
            List<grs_VGrsDealsByStatu> dealDbData = new List<grs_VGrsDealsByStatu> { new grs_VGrsDealsByStatu() { } };
            IPaginatedList<grs_VGrsDealsByStatu> expected = new PaginatedList<grs_VGrsDealsByStatu>()
            {
                PageCount = 1,
                PageIndex = 0,
                PageSize = 1,
                TotalRecordCount = 1,
                Items = dealDbData
            };


            // Search method
            workbenchDealsRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>()))
                    .Returns(expected);

            //Filter & Sort Parameters
            workbenchDealsRepository
                     .Setup(x => x.GetFilterParameters())
                         .Returns(new List<string> { "StatusCodes", "GlobalReData" });

            workbenchDealsRepository
            .Setup(x => x.GetSortParameters())
            .Returns(new List<string> { "StatusName" });

            workbenchDealsRepository
            .Setup(x => x.GetStatusCodes())
                .Returns(new List<int> { 3, 80, 2, 14, 29, 16, 1000, 1001, 23, 41, 1, 22 });
        }

		private void SetupDealRepository(Mock<IDealRepository> dealRepository, bool isNegative = false, string bypassCedantBrokerValidation = "1")
		{

			grs_VGrsDeal dealDbData = new grs_VGrsDeal
			{
				Dealnum = 123,
				Dealname = "DealTest 2",
			};
			List<grs_VGrsDeal> dealsDbData = new List<grs_VGrsDeal> { dealDbData };
			IPaginatedList<grs_VGrsDeal> expected = new PaginatedList<grs_VGrsDeal>()
			{
				PageCount = 1,
				PageIndex = 0,
				PageSize = 1,
				TotalRecordCount = 1,
				Items = dealsDbData
			};

			List<TbComGrpReq> validationStatuses = new List<TbComGrpReq>
			{
				new TbComGrpReq
				{
					StatusId = 65,
					Active = true
				}
			};

			// Search method
			dealRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>())).Returns(expected);
			dealRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDeal, bool>>>())).Returns(dealDbData);
			dealRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDeal, bool>>>())).Returns(isNegative ? null : dealDbData);
			dealRepository.Setup(p => p.GetDealStatusesRequiredInCompanyGroupValidation()).Returns(validationStatuses);
			dealRepository.Setup(p => p.EvaluateConfigSetting(It.IsAny<int>(), It.IsAny<string>())).Returns(bypassCedantBrokerValidation);
			//Filter & Sort Parameters
			dealRepository.Setup(x => x.GetFilterParameters()).Returns(new List<string> { "Status", "GlobalReData" });
			dealRepository.Setup(x => x.GetSortParameters()).Returns(new List<string> { "StatusName" });
			
		}

		private void SetupDealStatusesLookupRepository(out Mock<IDealStatusesLookupRepository> dealStatusesLookupRepository)
        {
            dealStatusesLookupRepository = new Mock<IDealStatusesLookupRepository>();

            IList<LookupEntity> sampledata = new List<LookupEntity>
            {
                new LookupEntity(id: 1, code: "0", description: "Bound", isActive: true),
                new LookupEntity(id: 2, code: "16", description: "On Hold", isActive: true)
            };

            List<LookupEntity> lookupData = new List<LookupEntity>();
            dealStatusesLookupRepository.Setup(d => d.GetLookupData())
                .Returns(sampledata);

        }

        private void SetupDealStatusesLookupManager(out Mock<IDealStatusesLookupManager> dealStatusesLookupManager)
        {
            dealStatusesLookupManager = new Mock<IDealStatusesLookupManager>();
            dealStatusesLookupManager.Setup(d => d.GetGlobalReDealStatusCodes()).Returns(new List<int> { 0, 16 });
            dealStatusesLookupManager.Setup(d => d.GetGlobalReDealStatusNames()).Returns(new List<string> { "bound", "on hold" });

        }

        private static Mock<ITblDealRepository> SetupTblDealRepository(bool isNegative)
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

            tblDealRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TblDeal, bool>>>(), It.IsAny<Expression<Func<TblDeal, object>>>())).Returns(isNegative ? null : expectedTblDeal);
			tblDealRepository.Setup(p => p.Save(It.IsAny<TblDeal>(), false));

			return tblDealRepository;
        }

		private static Mock<ICedantManager> SetupCedant(bool hasValidCompanyGroup = true)
		{
			Mock<ICedantManager> cedantManager = new Mock<ICedantManager>();
			cedantManager.Setup(p => p.CedantHasReinsuranceCedantGroup(It.IsAny<int?>())).Returns(hasValidCompanyGroup);
			return cedantManager;
		}
		private static Mock<IBrokerManager> SetupBroker(bool hasValidCompanyGroup = true)
		{
			Mock<IBrokerManager> brokerManager = new Mock<IBrokerManager>();
			brokerManager.Setup(p => p.BrokerHasReinsuranceBrokerGroup(It.IsAny<int?>())).Returns(hasValidCompanyGroup);
			return brokerManager;
		}


		private void SetupUserIdentity()
        {
			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, null, null, null);
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
            userIdentity = null;
        }

        #endregion

        #region Test Case Data

        public class DealSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new DealSearchCriteria() { Status = 0,  GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { Status = 16, GlobalReData = true });
					yield return new TestCaseData(new DealSearchCriteria() { StatusCodes = "3,80,2,14,29",  GlobalReData = false });
                }
            }
        }

        public class DealUpdateData
        {
            public static IEnumerable UpdateData
            {
                get
                {
					yield return new TestCaseData(new BLL_Deal() { Dealnum = 123, Dealname = "DealTest 2", Status = 65 }, "1");
					yield return new TestCaseData(new BLL_Deal() { Dealnum = 123, Dealname = "DealTest 2", Status = 0 }, "1");
					yield return new TestCaseData(new BLL_Deal() { Dealnum = 123, Dealname = "DealTest 2", Status = 65 }, "0");
					yield return new TestCaseData(new BLL_Deal() { Dealnum = 123, Dealname = "DealTest 2", Status = 0 }, "0");
				}
			}
        }

		public class DealValidateData
		{
			public static IEnumerable ValidateData
			{
				get
				{
					yield return new TestCaseData(new BLL_Deal() { Dealnum = 123, Dealname = "DealTest 2", Status = 65 }, "0", false);
				}
			}
		}

		#endregion
	}
}
