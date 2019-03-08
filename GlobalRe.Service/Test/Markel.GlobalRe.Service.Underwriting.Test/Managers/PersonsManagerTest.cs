using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Test.Managers
{
    [Category("Persons")]
    [Category("Managers")]
    [TestFixture]
    public class PersonsManagerTest
    {

        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private CacheStoreManager cacheStoreManager;
        private PersonTransformationManager transformationManager;
        UserIdentity userIdentity;

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

        [TestCaseSource(typeof(PersonSearchData), "TestCases")]
        public void PersonManager_Search_ShouldCall_RepositorySearch(PersonSearchCriteria personSearchCriteria)
        {
            //Arrange
            Mock<IPersonRepository> personRepository = new Mock<IPersonRepository>();


            SetupUserIdentity();
            SetupPersonRepository(personRepository);

            var criteria = new SearchCriteria();
            criteria.Add(new FilterParameter("PersonId", 123));

            PersonsManager personsManager = new PersonsManager(userManager.Object, cacheStoreManager, mockLogManager.Object, personRepository.Object, transformationManager);

            //Act
            IPaginatedList<BLL_Person> actual = personsManager.Search(criteria);

            int expectedTotalRecordCount = 1;

            //Assert
            personRepository.Verify(p => p.Search(It.IsAny<SearchCriteria>()));

            Assert.AreEqual(expectedTotalRecordCount, actual.TotalRecordCount);

        }


		[Test]
		public void PersonManager_Constructor_Throws_NullReferenceException()
		{
			Mock<IPersonRepository> personRepository = new Mock<IPersonRepository>();
			Assert.Throws(typeof(NullReferenceException), delegate { new PersonsManager(userManager.Object, cacheStoreManager, mockLogManager.Object, personRepository.Object, null); });
		}

        [Test]
        public void PersonManager_ConstructorWithNullRepository_Throws_NullReferenceException()
        {
			Assert.Throws(typeof(NullReferenceException), delegate { new PersonsManager(userManager.Object, cacheStoreManager, mockLogManager.Object, null, null); });
        }

        #endregion

        #region Private Methods

        private void SetupPersonRepository(Mock<IPersonRepository> personRepository)
        {
            List<TbPerson> personDbData = new List<TbPerson> {
                new TbPerson()
                {
                    PersonId = 123,
                    FirstName = "John",
                    LastName = "Smith",
                    CommonName = "John Smith"
                }
            };

            IPaginatedList<TbPerson> expected = new PaginatedList<TbPerson>()
            {
                PageCount = 1,
                PageIndex = 0,
                PageSize = 1,
                TotalRecordCount = 1,
                Items = personDbData
            };


            // Search method
            personRepository.Setup(p => p.Search(It.IsAny<SearchCriteria>()))
                    .Returns(expected);

            //Filter & Sort Parameters
            personRepository
             .Setup(x => x.GetFilterParameters())
                 .Returns(new List<string> { "PersonId" });

            personRepository
            .Setup(x => x.GetSortParameters())
            .Returns(new List<string> { "PersonId", "FirstName", "LastName", "CommonName" });
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

        #endregion

    }
}
