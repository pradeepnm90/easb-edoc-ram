using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
	[Category("DealCoverages")]
    [Category("Managers")]
    [TestFixture]
    class DealCoveragesManagerTests
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IDealCoveragesManager> dealCoverageManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private DealCoveragesTransformationManager transformationManager;
        private UserIdentity userIdentity;
        private Mock<IDealCoveragesRepository> dealCoveragesRepository;

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
            dealCoverageManager = new Mock<IDealCoveragesManager>();
        }

		#endregion


		#region TestCase Methods
		[Test]
		[TestCaseSource(typeof(DealCoverageData), "TestCaseCoverageData")]
		public void DealCoveragesManager_Get_ShouldCall_GetCoverages(int dealNumber)
		{
			//Arrange
			SetupUserIdentity();

			// Arrange
			dealCoveragesRepository = new Mock<IDealCoveragesRepository>();

			//SetupDealCoveragesRepository(dealCoveragesRepository, dealNumber);
			IList<Data.Models.grs_VGrsDealCoverage> CoveragesDbData = new List<Data.Models.grs_VGrsDealCoverage>
			{
				new Data.Models.grs_VGrsDealCoverage
				{
					Dealnum = dealNumber,
					CoverId = 13,
					CoverName = "California Earthquake"
				}
			};

			// Get method
			dealCoveragesRepository.Setup(p => p.GetDealCoverages(dealNumber)).Returns(CoveragesDbData);

			DealCoveragesManager dealCovergaresManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealCoveragesRepository.Object, new Mock<IEntityLockManager>().Object, transformationManager);

			//Act
			EntityResult<IEnumerable<BLL_DealCoverages>> actual = dealCovergaresManager.GetDealCoverages(dealNumber);

			//Assert
			Assert.IsNotEmpty(actual.Data);
		}

		[Test]
		[TestCase (null)]
		public void DealCoveragesManager_Get_ShouldThrow_IllegalArgumentAPIException(int dealNumber)
		{
			//Arrange
			SetupUserIdentity();

			// Arrange
			dealCoveragesRepository = new Mock<IDealCoveragesRepository>();

			//SetupDealCoveragesRepository(dealCoveragesRepository, dealNumber);
			IList<Data.Models.grs_VGrsDealCoverage> CoveragesDbData = new List<Data.Models.grs_VGrsDealCoverage>
			{
				new Data.Models.grs_VGrsDealCoverage
				{
					Dealnum = dealNumber,
					CoverId = 13,
					CoverName = "California Earthquake"
				}
			};

			// Get method
			dealCoveragesRepository.Setup(p => p.GetDealCoverages(dealNumber)).Returns(CoveragesDbData);

			DealCoveragesManager dealCovergaresManager = new DealCoveragesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealCoveragesRepository.Object, new Mock<IEntityLockManager>().Object, transformationManager);

			//Assert
			Assert.Throws<IllegalArgumentAPIException>(delegate { dealCovergaresManager.GetDealCoverages(dealNumber); });
		}


		#endregion


		#region Private Methods

		private void SetupDealCoveragesRepository(Mock<IDealCoveragesRepository> dealCoveragesRepository, int dealNumber, bool isNegative = false)
        {

            IList<Data.Models.grs_VGrsDealCoverage> CoveragesDbData = new List<Data.Models.grs_VGrsDealCoverage>
            {
                new Data.Models.grs_VGrsDealCoverage
                {
                    Dealnum = dealNumber,
                    CoverId = 13,
                    CoverName = "California Earthquake"
                }
            };

            // Get method
            //dealCoveragesRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_VGrsDealCoverage, bool>>>())).Returns(isNegative ? null : CoveragesDbData);
            dealCoveragesRepository.Setup(p => p.GetDealCoverages(dealNumber)).Returns(CoveragesDbData);
        }

        #endregion


        #region Private Methods
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
            dealCoverageManager = null;
        }

        #endregion


        #region Initiate Test Mock Data
        public class DealCoverageData
        {
            public static IEnumerable TestCaseCoverageData
            {
                get
                {
                    yield return new TestCaseData(1321360);
                }
            }

        }
        #endregion

    }
}
