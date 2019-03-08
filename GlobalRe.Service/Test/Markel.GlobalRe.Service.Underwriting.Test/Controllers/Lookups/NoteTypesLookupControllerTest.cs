using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;


namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("Notetypes")]
    [Category("Controllers")]
    [TestFixture]
    public class NoteTypesLookupControllerTest
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
		public void NoteTypesLookupController_Get_StatusOK()
		{

			#region Arrange

			var mockRepository = new Mock<INoteTypesLookupRepository>();
			NoteTypesLookupManager lookupManager = new NoteTypesLookupManager(userManager.Object, cacheStoreManager.Object, mockRepository.Object);

			#endregion

			#region Act
			NoteTypesLookupController controller = new NoteTypesLookupController(userManager.Object, lookupManager);
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


