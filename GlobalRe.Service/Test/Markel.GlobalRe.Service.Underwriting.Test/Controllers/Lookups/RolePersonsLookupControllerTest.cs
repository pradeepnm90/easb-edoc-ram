using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers.Lookups
{
    [Category("Lookups")]
    [Category("Controllers")]
    [TestFixture]
    public class RolePersonsLookupControllerTest
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

        #region Test Method

        [TestCaseSource(typeof(RolePersonsSearchData), "TestCases")]
        public void RolePersonsLookupController_Get_Returns_OkResponse(RolePersonSearchCriteria rolePersonSearchCriteria)
        {
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.LookupsRolePersonsRoutePrefix}/{rolePersonSearchCriteria}");
            Mock<IRolePersonsManager> rolePersonsManager = new Mock<IRolePersonsManager>();
            IList<LookupEntity> sampledata = new List<LookupEntity>
            {
                new LookupEntity(id: 52517, code: "Alan Dowling", description: "GlobalRe.Underwriter", isActive: true),
            };

            if (rolePersonSearchCriteria?.Roles == null) { rolePersonsManager.Setup(p => p.GetAll()).Returns(sampledata); }
            else { rolePersonsManager.Setup(p => p.GetByGroupName(rolePersonSearchCriteria.Roles)).Returns(sampledata); }


            #region Repository Mocking (Need to fix)

            //SetupUserIdentity();

            //Mock<IRolePersonsLookupRepository> rolePersonsLookupRepository = new Mock<IRolePersonsLookupRepository>();
            //rolePersonsLookupRepository.Setup(p => p.GetLookupData()).Returns(sampledata);

            //cacheStoreManager.Setup(p => p.BuildKey("Lookup", "RolePersons")).Returns("LookupRolePersons");

            //RolePersonsManager rPersonsManager = new RolePersonsManager(userManager.Object, cacheStoreManager.Object, rolePersonsLookupRepository.Object);

            #endregion

            RolePersonsLookupController rolePersonsLookupController = new RolePersonsLookupController(userManager.Object, rolePersonsManager.Object)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };

            var response = rolePersonsLookupController.Get(rolePersonSearchCriteria);

            var contentResult = response as NegotiatedContentResult<ResponseLookup<GroupNameValuePair>>;

            Assertions.AssertOkResponse(contentResult);


        }

        [Test]
        public void RolePersonsLookupController_Constructor_Throws_NullReferenceException() =>
            Assert.Throws(typeof(NullReferenceException), delegate { RolePersonsLookupController rolePersonsLookupController = new RolePersonsLookupController(userManager.Object, null); });


        #endregion

        //private void SetupUserIdentity()
        //{
        //    var permission = new Dictionary<string, bool>
        //    {
        //        { "Default view based on Subdivision/Exposure Groupings", false },
        //        { "Default view based on current user", false },
        //        { "Default view based on manager", true }
        //    };

        //    UserIdentity userIdentity = new UserIdentity(123, 1, "", "", "", "ONEDEV", "", DateTime.Now, DateTime.Now.AddHours(8), true, "", false, permission, null, null);
        //    userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        //}

        #region TearDown

        [TearDown]
        public void TearDown()
        {
            userManager = null;
            cacheStoreManager = null;
        }

        #endregion

        public class RolePersonsSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new RolePersonSearchCriteria() { Roles = "GlobalRe.Underwriter" });
                    yield return new TestCaseData(null);
                }
            }
        }

    }
}
