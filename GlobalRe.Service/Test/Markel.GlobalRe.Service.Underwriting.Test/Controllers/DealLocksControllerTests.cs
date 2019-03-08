
using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("Deal")]
    [Category("Controllers")]
    [TestFixture]
    public class DealLocksControllerTests
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
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
        }

		#endregion

		#region Test Methods

		[Test]
		[TestCase(123, false)]
		[TestCase(123, true)]
		public void DealLocksController_GetLocks_Returns_OKResponseCode(int dealNumber, bool isLocked)
		{
			#region Arrange

			Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
			SetupUserIdentity();
			SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isLocked);

			string url = $"{AppSettings.BASEURL}{ RouteHelper.DealsRoutePrefix }/{dealNumber}/locks";
			//string url = $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}{RouteHelper.DealLocksPrefix}";
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), url);

			DealLocksController dealLocksController = CreateDealLocksController(httpRequest, dealLockRepository.Object);

			#endregion

			#region Act

			var response = dealLocksController.GetLocks(dealNumber);
			var contentResult = response as NegotiatedContentResult<ResponseItem<EntityLock>>;

			#endregion

			#region Assert

			#region Expected Data

			var expectedDealLock = new  EntityLock()
			{
				entityId = dealNumber,
				entityTypeName = EntityType.Deals.ToString(),
				lockedByDisplayName = "John Doe",
				lockedTimestamp = DateTime.MinValue
			};

			string expectedURL = ($"/{ RouteHelper.DealsRoutePrefix }/{dealNumber }/locks");
			var expectedMessages = new List<Message>();
			Message expectedMessage = new Warning("entityID", $"{ expectedDealLock.lockedByDisplayName} has locked this deal for edit");
			expectedMessages.Add(expectedMessage);
			IList<Link> links = null;
			ResponseItem<EntityLock> expectedContent = new ResponseItem<EntityLock>(expectedURL, expectedDealLock, links, expectedMessages);

			#endregion

			Assertions.AssertOkResponse(contentResult);

			var actualDealLock = contentResult.Content.data;
			if (isLocked)
			{
				Assertions.AreEqualByJson(expectedContent, contentResult.Content);
			}
			else
			{
				Assert.IsNull(actualDealLock);
				Assert.IsNull(contentResult.Content.messages);
				Assert.IsNull(links);
			}



			#endregion
		}

		[Test]
		[TestCase(123, true, false)]
		[TestCase(123, false, false)]
		[TestCase(123, false, true)]
		public void DealLockManager_ShouldCall_LockDeal(int dealNumber, bool isLocked, bool isNegative)
		{
			#region Arrange

			Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
			SetupUserIdentity();
			SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isLocked, isNegative);

			string url = $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}{RouteHelper.DealLocksPrefix}";
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), url);

			DealLocksController dealLocksController = CreateDealLocksController(httpRequest, dealLockRepository.Object);

			#endregion

			#region Act

			var response = dealLocksController.Lock(dealNumber);
			var result = response as StatusCodeResult;
			//var contentResult = response as OkNegotiatedContentResult<ResponseItem<EntityLock>>;

			#endregion

			#region Assert
			HttpStatusCode expectedStatusCode = (!isNegative) ? HttpStatusCode.OK : HttpStatusCode.Conflict;
			Assert.AreEqual(expectedStatusCode, result.StatusCode);
			#endregion
		}

		[Test]
		[TestCase(123, true, true)]
		public void DealLockManager_ShouldCall_LockDeal_Throws_NotAllowedAPIException(int dealNumber, bool isLocked, bool isNegative)
		{
			#region Arrange

			Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
			SetupUserIdentity();
			SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isLocked, isNegative);

			string url = $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}{RouteHelper.DealLocksPrefix}";
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), url);

			DealLocksController dealLocksController = CreateDealLocksController(httpRequest, dealLockRepository.Object);

			#endregion

			#region Assert
			Assert.Throws(typeof(NotAllowedAPIException), delegate { dealLocksController.Lock(dealNumber); });
			#endregion
		}

		[Test]
		[TestCase(123, true)]
		[TestCase(123, false)]
		public void DealLockManager_ShouldCall_UnlockDeal(int dealNumber, bool isLocked)
		{
			#region Arrange

			Mock<IEntityLockRepository> dealLockRepository = new Mock<IEntityLockRepository>();
			SetupUserIdentity();
			SetupDealLockRepository(dealLockRepository, dealNumber, userIdentity.UserId, isLocked);

			string url = $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}{RouteHelper.DealLocksPrefix}";
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), url);

			DealLocksController dealLocksController = CreateDealLocksController(httpRequest, dealLockRepository.Object);

			#endregion

			#region Act
			var response = dealLocksController.Unlock(dealNumber);
			var result = response as StatusCodeResult;
			//var contentResult = response as OkNegotiatedContentResult<ResponseItem<EntityLock>>;

			#endregion

			#region Assert
			HttpStatusCode expectedStatusCode = isLocked ? HttpStatusCode.ExpectationFailed : HttpStatusCode.OK;
			Assert.AreEqual(expectedStatusCode, result.StatusCode);
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

        private DealLocksController CreateDealLocksController(HttpRequestMessage httpRequest, IEntityLockRepository dealLockRepository)
        {

			EntityLockManager dealLockManager = new EntityLockManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealLockRepository, transformationManager);
			DealManager dealManager = new DealManager(userManager.Object, cacheStoreManager, mockLogManager.Object, 
				new Mock<IDealRepository>().Object, new Mock<IWorkbenchDealsRepository>().Object, new Mock<ITblDealRepository>().Object,
				new Mock<IDealStatusesLookupManager>().Object, dealLockManager, new Mock<IDealTransformationManager>().Object, 
				new Mock<ICedantManager>().Object, new Mock<IBrokerManager>().Object, new Mock<ITblClausesDealRepository>().Object);

			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, dealManager, dealLockManager);

			DealLocksController dealLocksController = new DealLocksController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };
            return dealLocksController;
        }

		private void SetupDealLockRepository(Mock<IEntityLockRepository> dealLockRepository, int dealNumber, int userId, bool isLocked = true, bool isNegative = false)
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
						ItemID = 123,
						UserID = isNegative ? 125 : userIdentity.UserId,
						SessionID = 12,
						DataBaseName = "TestDb",
						IsEditLock = true,
						Notes = "Notes",
						EntryTime = DateTime.MinValue,
						ExpirationTime = DateTime.MaxValue,
						LockingUserName = isNegative ? "Jane Doe" : "John Doe",
						LockingUser = isNegative ? "jane.doe@corporate.net" : userIdentity.UserName + '@' + userIdentity.DomainName
					});
			}
			dealLockRepository.Setup(p => p.GetEntityLocks((int)EntityLockType.Deals, dealNumber, userId)).Returns(lockedItemList);
			dealLockRepository.Setup(p => p.LockEntity((int)EntityLockType.Deals, dealNumber, userId, out errorMessage)).Returns(!isNegative);
			dealLockRepository.Setup(p => p.UnlockEntity((int)EntityLockType.Deals, dealNumber, userId));
		}

		private void SetupUserIdentity()
        {
			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, null, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        #endregion

    }
}
