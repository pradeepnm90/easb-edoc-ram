using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
    [Category("Lookups")]
    [Category("Managers")]
    [TestFixture]
    public class DealStatusesLookupManagerTests
    {
        #region Private Variables

        Mock<IUserManager> userManager;
        CacheStoreManager cacheStoreManager;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            userManager = new Mock<IUserManager>();
            SetupUserIdentity();
            cacheStoreManager = new CacheStoreManager();
        }

        #endregion

        #region Test Methods

        [Test]
        public void DealStatusesLookupManager_GetGlobalReDealStatusCodes_Returns_ExpectedStatusCodes()
        {
            Mock<IDealStatusesLookupRepository> mockRepository = SetupDealStatusesLookupRepository();

            DealStatusesLookupManager lookupManager = new DealStatusesLookupManager(userManager.Object, cacheStoreManager, mockRepository.Object);

            var statusCodes = lookupManager.GetGlobalReDealStatusCodes();

            Assert.Multiple(() =>
            {
                Assert.That(statusCodes, Is.All.Not.Null);
                Assert.That(statusCodes, Is.All.InstanceOf<int>());
                //Assert.That(statusCodes, Is.SubsetOf(new List<int>() { 0, 16 }));
                Assert.That(new List<int>() { 0, 16 }, Is.EquivalentTo(statusCodes));
            });

        }

        [Test]
        public void DealStatusesLookupManager_GetGlobalReDealStatusNames_Returns_ExpectedStatusNames()
        {
            Mock<IDealStatusesLookupRepository> mockRepository = SetupDealStatusesLookupRepository();

            DealStatusesLookupManager lookupManager = new DealStatusesLookupManager(userManager.Object, cacheStoreManager, mockRepository.Object);

            var statusNames = lookupManager.GetGlobalReDealStatusNames();

            Assert.Multiple(() =>
            {
                Assert.That(statusNames, Is.All.Not.Null);
                Assert.That(statusNames, Is.All.InstanceOf<string>());
                // Assert.That(statusNames, Is.SubsetOf(new List<string>() { "Bound","On Hold" }));
                Assert.That(new List<string>() { "bound", "on hold" }, Is.EquivalentTo(statusNames));
            });

        }

        #endregion

        #region Private Methods

        private void SetupUserIdentity()
        {
			var userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, null, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        private Mock<IDealStatusesLookupRepository> SetupDealStatusesLookupRepository()
        {
            var mockRepository = new Mock<IDealStatusesLookupRepository>();

            IList<LookupEntity> sampledata = new List<LookupEntity>
            {
                new LookupEntity(id: 1, code: "0", description: "Bound", isActive: true),
                new LookupEntity(id: 2, code: "16", description: "On Hold", isActive: true)
            };

            List<LookupEntity> lookupData = new List<LookupEntity>();
            mockRepository.Setup(d => d.GetLookupData())
                .Returns(sampledata);
            return mockRepository;
        }

        #endregion

        #region TearDown

        [OneTimeTearDown]
        public void Cleanup()
        {
            userManager = null;
            cacheStoreManager = null;
        }

        #endregion

    }
}
