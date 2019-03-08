using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalReUnderwriting.Service.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups;
using Moq;
using NUnit.Framework;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using System;

namespace Markel.GlobalReUnderwriting.Service.Test.Deals.Controllers
{
	[TestFixture]
	[Category("Lookups")]
	[Category("Controllers")]
	class DealStatusesLookupTest
	{

		#region Private Variables

		Mock<IUserManager> userManager;
		Mock<ICacheStoreManager> cacheStoreManager;

		#endregion

		#region Initial Setup

		[SetUp]
		public void Setup()
		{
			userManager = new Mock<IUserManager>();
			cacheStoreManager = new Mock<ICacheStoreManager>();
		}

		#endregion

		[Test]
		public void DealStatusesLookupController_Create_Success()
		{

			#region Arrange
			var mockRepository = new Mock<IDealStatusesLookupRepository>();
			DealStatusesLookupManager lookupManager = new DealStatusesLookupManager(userManager.Object, cacheStoreManager.Object, mockRepository.Object);
			#endregion

			#region Act
			DealStatusesLookupController controller = new DealStatusesLookupController(userManager.Object, lookupManager);
			#endregion

			#region Assert
			Assert.IsNotNull(controller);
			#endregion
		}

		[Test]
		public void DealStatusesLookupController_Constructor_Throws_NullReferenceException() =>
				Assert.Throws(typeof(NullReferenceException), delegate { DealStatusesLookupController dealStatusesLookupController = new DealStatusesLookupController(userManager.Object, null); });


		#region TearDown

		[TearDown]
		public void TearDown()
		{
			userManager = null;
			cacheStoreManager = null;
		}

		#endregion

	}
}
