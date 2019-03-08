using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Underwriting.Service.BLL.Interfaces;
using Markel.GlobalRe.Underwriting.Service.BLL.Managers;
using Markel.GlobalRe.Underwriting.Service.BLL.Managers.Transformations;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
    [Category("DealLockManager")]
    [Category("Managers")]
    [TestFixture]
    public class DealLockManagerTest
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private CacheStoreManager cacheStoreManager;
        private EntityLockTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new CacheStoreManager();
            transformationManager = new EntityLockTransformationManager();
            dealLockManager = new Mock<IEntityLockManager>();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCase(123, false)]
        public void DealLockManager_ShouldCall_UnlockDeal(int dealNumber, bool isNegative)
        {
            // Arrange
            Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
            SetupUserIdentity();
            SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isNegative);

            EntityLockManager dealLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object,
                dealLockRepository.Object, transformationManager);

            //Act
            var actual = dealLockManager.Lock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId);
            dealLockManager.Unlock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId);

            //Assert
            dealLockRepository.Verify(p => p.LockEntity((int)EntityLockType.Deals, It.IsAny<int>(), It.IsAny<int>()));
            dealLockRepository.Verify(p => p.UnlockEntity((int)EntityLockType.Deals, It.IsAny<int>(), It.IsAny<int>()));

            Assert.IsTrue(actual);
        }

        [Test]
        [TestCase(123, true)]
        public void DealLockManager_ShouldCall_LockDeal_Negative(int dealNumber, bool isNegative)
        {
            // Arrange
            Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
            SetupUserIdentity();
            SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isNegative);

            EntityLockManager dealLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object,
                dealLockRepository.Object, transformationManager);

            //Assert
            Assert.Throws<NotAllowedAPIException>(delegate { dealLockManager.Lock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId); });
        }

        [Test]
        public void DealLockManager_ShouldCall_ThrowNullReferenceException()
        {
            // Arrange
            Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
            //Assert
            Assert.Throws<NullReferenceException>(delegate { new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealLockRepository.Object, null); });
        }
        #endregion

        #region Private Methods
        private void SetupDealLockRepository(Mock<IEntityLockRepository> dealLockRepository, int dealNumber, int userId, bool isNegative = false)
        {

			List<grs_PrLckGetLockedItem> lockedItemList = new List<grs_PrLckGetLockedItem>()
			{
				new grs_PrLckGetLockedItem()
				{
					LockID = 12,
					CategoryID = 2,
					ItemID = 3,
					UserID = isNegative ? 555 : userIdentity.UserId,
					SessionID = 12,
					DataBaseName = "TestDb",
					IsEditLock = true,
					Notes = "Notes",
					EntryTime = DateTime.Now ,
					ExpirationTime = DateTime.Now.AddMinutes(5),
					LockingUser = isNegative ? "jane.doe@corporate.net" : userIdentity.UserName+'@'+userIdentity.DomainName
				}
            };
            dealLockRepository.Setup(p => p.LockEntity((int)EntityLockType.Deals, dealNumber, userId)).Returns(true);
            dealLockRepository.Setup(p => p.GetEntityLocks((int)EntityLockType.Deals, dealNumber, userId)).Returns(lockedItemList);
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
    }
}
