using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalReUnderwriting.Service.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups;
using Moq;
using NUnit.Framework;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;

namespace Markel.GlobalReUnderwriting.Service.Test.Lookups.Controllers
{
	[TestFixture]
	[Category("Lookups")]
	[Category("Controllers")]
	class AttachmentBasisLookupTest
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
		public void AttachmentBasisLookupController_Get_StatusOK()
		{

			#region Arrange

			var mockRepository = new Mock<IAttachmentBasisLookupRepository>();
			AttachmentBasisLookupManager lookupManager = new AttachmentBasisLookupManager(userManager.Object, cacheStoreManager.Object, mockRepository.Object);

			#endregion

			#region Act
			AttachmentBasisLookupController controller = new AttachmentBasisLookupController(userManager.Object, lookupManager);
			#endregion

			#region Assert
			Assert.IsNotNull(controller);
			#endregion
		}

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
