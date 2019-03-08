using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.GlobalReUnderwriting.Service.Test.Lookups.Controllers
{
	[TestFixture]
    [Category("Lookups")]
    [Category("Controllers")]
    class ExposureTreeControllerTest
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private CacheStoreManager cacheStoreManager;
        private ExposureTreeTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new CacheStoreManager();
            transformationManager = new ExposureTreeTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();

        }

        #endregion
        [Test]
        public void ExposureTreeController_Get_Returns_OKResponseCode()
        {
            #region Arrange

            SetupUserIdentity();

            //Create mock repository
            var exposureTreeRepository = new Mock<IExposureTreeRepository>();

            //Build response
            IList<grs_VExposureTreeExt> exposureTreeDbData = new List<grs_VExposureTreeExt>
            {
                new grs_VExposureTreeExt
                {
                 SubdivisionCode =  300,
                 SubdivisionName = "Casualty",
                 ProductLineCode = 1001,
                 ProductLineName = "Auto Reins",
                 ExposureGroupCode  = 59,
                 ExposureGroupName =  "CA - Auto - Commercial",
                 ExposureTypeCode =  5909,
                 ExposureTypeName = "CA - Auto - Commercial - Non Trucking"
                }
            };

           exposureTreeRepository.Setup(p => p.GetGlobalReExposureTree())
                        .Returns(exposureTreeDbData);
      
            //Manager
            ExposureTreeManager exposureTreeManager = new ExposureTreeManager(userManager.Object, cacheStoreManager,
                mockLogManager.Object, exposureTreeRepository.Object, transformationManager);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.LookupsExposureTreeRoutePrefix}");

            DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, exposureTreeManager);

            ExposureTreeController exposureTreeController = new ExposureTreeController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };



            #endregion

            #region Act

            var response = exposureTreeController.GetGlobalReExposureTree();

            var contentResult = response as NegotiatedContentResult<ResponseCollection<ExposureTree>>;

            #endregion

            #region Assert

            #region Expected

            ExposureTree expectedExposureTree = new ExposureTree
            {
                 SubdivisionCode =  300,
                 SubdivisionName = "Casualty",
                 ProductLineCode = 1001,
                 ProductLineName = "Auto Reins",
                 ExposureGroupCode  = 59,
                 ExposureGroupName =  "CA - Auto - Commercial",
                 ExposureTypeCode =  5909,
                 ExposureTypeName = "CA - Auto - Commercial - Non Trucking"
            };


            #endregion
          
            Assertions.AssertOkResponse(contentResult);
           

            var summaryData = contentResult.Content.results;

            for (int i = 0; i <= summaryData.Count - 1; i++)
            {
                //Data
                var actualExposureTree = summaryData[i].data;
				Assertions.AssertData(expectedExposureTree, actualExposureTree);
                
                Assert.IsEmpty(summaryData[i].messages);
            }

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

        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on Subdivision/Exposure Groupings", false },
                { "Default view based on current user", false },
                { "Default view based on manager", true }
            };

			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, permission, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }
    }
}

