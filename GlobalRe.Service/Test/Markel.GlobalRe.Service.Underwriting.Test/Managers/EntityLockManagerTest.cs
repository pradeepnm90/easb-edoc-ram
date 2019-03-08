using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
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
    [Category("EntityLockManager")]
    [Category("Managers")]
    [TestFixture]
    public class EntityLockManagerTest
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> entityLockManager;
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
            entityLockManager = new Mock<IEntityLockManager>();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCase(123, false, false)]
        public void EntityLockManager_ShouldCall_UnlockDeal(int dealNumber, bool isLocked, bool isNegative)
        {
            // Arrange
            Mock<IEntityLockRepository> entityLockRepository = new Mock<IEntityLockRepository>();
            SetupUserIdentity();
            SetupEntityLockRepository(entityLockRepository, dealNumber, userIdentity.UserId, isLocked, isNegative);

            EntityLockManager entityLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object,
                entityLockRepository.Object, transformationManager);

            //Act
			var actual = entityLockManager.Unlock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId);

			//Assert
			entityLockRepository.Verify(p => p.GetEntityLocks((int)EntityLockType.Deals, It.IsAny<int>(), It.IsAny<int>()));
			entityLockRepository.Verify(p => p.UnlockEntity((int)EntityLockType.Deals, It.IsAny<int>(), It.IsAny<int>()));

            Assert.IsTrue(actual);
        }

        [Test]
        [TestCase(123, true, true)]
        public void EntityLockManager_ShouldCall_LockDeal_Negative(int dealNumber, bool isLocked, bool isNegative)
        {
            // Arrange
            Mock<IEntityLockRepository> entityLockRepository = new Mock<IEntityLockRepository>();
            SetupUserIdentity();
            SetupEntityLockRepository(entityLockRepository, dealNumber, userIdentity.UserId, isLocked, isNegative);

            EntityLockManager entityLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object,
                entityLockRepository.Object, transformationManager);

            //Assert
            Assert.Throws<NotAllowedAPIException>(delegate { entityLockManager.Lock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId); });
        }

		[Test]
		[TestCase(123, true, true)]
		public void EntityLockManager_ShouldCall_LockDeal_Throws_NotAllowedAPIException(int dealNumber, bool isLocked, bool isNegative)
		{
			#region Arrange

			Mock<IEntityLockRepository> entityLockRepository = new Mock<IEntityLockRepository>();
			SetupUserIdentity();
			SetupEntityLockRepository(entityLockRepository, dealNumber, userIdentity.UserId, isLocked, isNegative);

			EntityLockManager entityLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object,
				entityLockRepository.Object, transformationManager);

			#endregion

			#region Assert
			Assert.Throws<NotAllowedAPIException>(delegate { entityLockManager.Lock((int)EntityLockType.Deals, dealNumber, userIdentity.UserId); });
			#endregion
		}

		[Test]
        public void EntityLockManager_ShouldCall_ThrowNullReferenceException()
        {
            // Arrange
            Mock<IEntityLockRepository> entityLockRepository = new Mock<IEntityLockRepository>();
            //Assert
            Assert.Throws<NullReferenceException>(delegate { new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object, entityLockRepository.Object, null); });
        }
        #endregion

        #region Private Methods
        private void SetupEntityLockRepository(Mock<IEntityLockRepository> entityLockRepository, int dealNumber, int userId, bool isLocked, bool isNegative = false)
        {
			string errorMessage;
			List<grs_PrLckGetLockedItem> lockedItemList = new List<grs_PrLckGetLockedItem>();
			if (isLocked)
			{
				lockedItemList.Add(
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
						EntryTime = DateTime.Now,
						ExpirationTime = DateTime.Now.AddMinutes(5),
						LockingUser = isNegative ? "jane.doe@corporate.net" : userIdentity.UserName + '@' + userIdentity.DomainName
					}
				);
			}
            entityLockRepository.Setup(p => p.LockEntity((int)EntityLockType.Deals, dealNumber, userId, out errorMessage)).Returns(!isNegative);
			entityLockRepository.Setup(p => p.UnlockEntity((int)EntityLockType.Deals, dealNumber, userId));
			entityLockRepository.Setup(p => p.GetEntityLocks((int)EntityLockType.Deals, dealNumber, userId)).Returns(lockedItemList);
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
