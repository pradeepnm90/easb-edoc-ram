using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
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

namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
    [Category("Persons")]
    [Category("Controllers")]
    [TestFixture]
    public class PersonsControllerTests
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private CacheStoreManager cacheStoreManager;
        private PersonTransformationManager transformationManager;
        private UserIdentity userIdentity;
        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new CacheStoreManager();
            transformationManager = new PersonTransformationManager();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCaseSource(typeof(PersonSearchData), "TestCases")]
        public void PersonsController_Get_Returns_OKResponseCode(PersonSearchCriteria personSearchCriteria)
        {

			#region Arrange
            SetupUserIdentity();
            SetupPersonRepository(personSearchCriteria, out Mock<IPersonRepository> personRepository, out IPaginatedList<TbPerson> expected);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.PersonRoutePrefix}/{personSearchCriteria}");

            PersonsController personsController = CreatePersonsController(httpRequest, personRepository.Object);

            #endregion

            #region Act

            var response = personsController.Search(personSearchCriteria);
			//var response = personsController.Get(It.IsAny<int>());
			var contentResult = response as NegotiatedContentResult<ResponsePaginatedCollection<Person>>;


            #endregion

            #region Assert

            #region Expected Data

            var expectedPerson = new Person()
            {
				PersonId = (personSearchCriteria.PersonId) == 0 ? userIdentity.UserId : 123,
                FirstName = "John",
                LastName = "Smith",
                DisplayName = "John Smith"
            };

            #endregion

            Assertions.AssertOkResponse(contentResult);

            var personsData = contentResult.Content.results;
            for (int i = 0; i <= personsData.Count - 1; i++)
            {
                //Data
                var actualPerson = personsData[i].data;
				Assertions.AssertData(expectedPerson, actualPerson);

            }

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

        private PersonsController CreatePersonsController(HttpRequestMessage httpRequest, IPersonRepository personsRepository)
        {
            PersonsManager personsManager = new PersonsManager(userManager.Object, cacheStoreManager, mockLogManager.Object, personsRepository, transformationManager);

            PersonAPIManager personAPIManager = new PersonAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, personsManager);

            PersonsController personsController = new PersonsController(userManager.Object, personAPIManager)
            {
                Request = httpRequest,
                Configuration = new HttpConfiguration(),
            };

            return personsController;
        }

        private void SetupPersonRepository(PersonSearchCriteria personSearchCriteria, out Mock<IPersonRepository> personRepository, out IPaginatedList<TbPerson> expected)
        {
            //Create mock repository
            personRepository = new Mock<IPersonRepository>();

			//Build response
			List<TbPerson> personDbData = new List<TbPerson> {
				new TbPerson()
				{
					PersonId = 123,
					FirstName = "John",
					LastName = "Smith",
					CommonName = "John Smith"
				},
				new TbPerson()
				{
					PersonId = 124,
					FirstName = "John",
					LastName = "Smith",
					CommonName = "",
					PersonName ="John Smith"
				},
				new TbPerson()
				{
					PersonId = 125,
					FirstName = "John",
					LastName = "Smith",
					CommonName = "",
					PersonName = ""
				},
				new TbPerson()
				{
					PersonId = userIdentity.UserId,
					FirstName = "John",
					LastName = "Smith",
					CommonName = "",
					PersonName = ""
				}
			};

			expected = new PaginatedList<TbPerson>()
			{
				PageCount = 10,
				PageIndex = 0,
				PageSize = 10,
				TotalRecordCount = 1,
				Items = new List<TbPerson>() { personDbData.Find((p) => p.PersonId == (personSearchCriteria.PersonId == 0 ? userIdentity.NameId : personSearchCriteria.PersonId)) }
			};


			// Search method
			personRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>())).Returns(expected);

			//Filter & Sort Parameters
			personRepository.Setup(x => x.GetFilterParameters()).Returns(new List<string> { "PersonId" });

            personRepository.Setup(x => x.GetSortParameters()).Returns(new List<string> { "PersonId", "FirstName", "LastName", "CommonName" });
        }

        #endregion

        #region Test Case Data

        public class PersonSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
					yield return new TestCaseData(new PersonSearchCriteria() { PersonId = 123 });
					yield return new TestCaseData(new PersonSearchCriteria() { PersonId = 0 });
                }
            }

        }

        private void SetupUserIdentity()
        {
			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "ONEDEV", DateTime.Now, true, "", false, null, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
        }
        #endregion

    }
}
