using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("ContractTypes")]
    [Category("Controllers")]
    [TestFixture]
    public class ContractTypesLookupControllerTest
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
       // private Mock<IDealLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private ContractTypesTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
            transformationManager = new ContractTypesTransformationManager(userManager.Object);
           // dealLockManager = new Mock<IDealLockManager>();

        }

        #endregion

         [Test]
       // [TestCaseSource(typeof(ContractTypeSearchData), "TestCases")]
        public void ContractTypeController_Get_Returns_OKResponseCode()
        {
            #region Arrange

            SetupUserIdentity();

            //string assumedOrceded = "assumed";

            //Create mock repository
            var contractTypeRepository = new Mock<IContractTypesLookupRepository>();

            //Build response
            IList<Data.Models.grs_VGrsContractType> contractTypeDbData = new List<Data.Models.grs_VGrsContractType>
            {
                new Data.Models.grs_VGrsContractType
                {
                    Code = 11,
                    Exposurename = "Reinsurance - Treaty",
                    InsuranceName ="test",
                    AssumedFlag = 1,
                    AssumedName = "Treaty",
                    CededFlag = null,
                    Exposuretype=1,
                    Active=true,
                    Catorder=1,
                    CededName="ceded",
                    RowId=1
                },
            };
            #endregion
            // Search method

            contractTypeRepository.Setup(p => p.GetContractTypes())
                        .Returns(contractTypeDbData);

            //Manager
            //ContractTypesManager contractTypeManager = new ContractTypesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, contractTypeRepository.Object, transformationManager, new Mock<IPersonProfileManager>().Object);
            ContractTypesManager contractTypeManager = new ContractTypesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, contractTypeRepository.Object, transformationManager);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.LookupsContractTypesRoutePrefix}");

            DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealManager>().Object, new Mock<IEntityLockManager>().Object);
          

            ContractTypesLookupController contractTypeController = new ContractTypesLookupController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };



            

            #region Act

            //var response = contractTypeController.Get(criteria);

            // CoverageBasisSearchCriteria dsc = new CoverageBasisSearchCriteria { assumedOrCededFlag = "assumed" };
            var response = contractTypeController.Get();

            //var response = contractTypeController.Get(It.IsAny<DealNotesSearchCriteria>());
            var contentResult = response as NegotiatedContentResult<ResponseCollection<ContractTypes>>;

            #endregion

            #region Assert

          

        var expectedContractType = new ContractTypes
            {
               
                name = "Treaty",
                value = "11",
                group="1",
                isActive = true

                
        };
         
        
            if (contentResult != null)
            {
                Assertions.AssertOkResponse(contentResult);

                var contractTypeData = contentResult.Content.results;

                for (int i = 0; i <= contractTypeData.Count - 1; i++)
                {
                    var actualcontractTypes = contractTypeData[i].data;

                    Assertions.AssertData(expectedContractType, actualcontractTypes);
                    Assert.IsEmpty(contractTypeData[i].messages);
                }
            }

            #endregion
          

           
        }

        [Test]
        public void ContractTypesController_IsNullorEmpty_NotFoundAPIException()
        {

            SetupUserIdentity();
            var contractTypeRepository = new Mock<IContractTypesLookupRepository>();

            //Build response
            IList<Data.Models.grs_VGrsContractType> contractTypeDbData = new List<Data.Models.grs_VGrsContractType>
            {
                new Data.Models.grs_VGrsContractType
                {
                   
                },
            };

            // Search method
            
            contractTypeRepository.Setup(p => p.GetContractTypes())
                        .Returns(contractTypeDbData);

            //Manager
            //ContractTypesManager contractTypeManager = new ContractTypesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, contractTypeRepository.Object, transformationManager, new Mock<IPersonProfileManager>().Object);
            ContractTypesManager contractTypeManager = new ContractTypesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, contractTypeRepository.Object, transformationManager);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.LookupsContractTypesRoutePrefix}");

            DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, new Mock<IDealManager>().Object, new Mock<IEntityLockManager>().Object);


            ContractTypesLookupController contractTypeController = new ContractTypesLookupController(userManager.Object, dealAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration()
            };



            #region Assert

            IHttpActionResult actionResult = contractTypeController.Get();
            Assert.AreEqual(HttpStatusCode.InternalServerError, ((StatusCodeResult)actionResult).StatusCode);

            #endregion

        }
        private void SetupUserIdentity()
        {
            var permission = new Dictionary<string, bool>
            {
                { "Default view based on Groupings", false },
                { "Default view based on current user", false },
                { "Default view based on manager", true }
            };

            userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, permission, null, null);
            userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            userManager = null;
            cacheStoreManager = null;
            mockLogManager = null;
            transformationManager = null;
            userIdentity = null;
        }

    }
}


