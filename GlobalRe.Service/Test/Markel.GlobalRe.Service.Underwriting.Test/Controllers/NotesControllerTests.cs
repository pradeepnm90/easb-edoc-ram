using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
    [Category("Notes")]
    [Category("Controllers")]
    [TestFixture]
    public class NotesControllerTests
    {
        #region Private Variables

        private Mock<ILogManager> mockLogManager;
        private Mock<IUserManager> userManager;
        private Mock<IEntityLockManager> dealLockManager;
        private Mock<ICacheStoreManager> cacheStoreManager;
        private NotesTransformationManager transformationManager;
        private UserIdentity userIdentity;

        #endregion

        #region Initial Setup

        [OneTimeSetUp]
        public void Init()
        {
            mockLogManager = new Mock<ILogManager>();
            userManager = new Mock<IUserManager>();
            cacheStoreManager = new Mock<ICacheStoreManager>();
            transformationManager = new NotesTransformationManager(userManager.Object);
            dealLockManager = new Mock<IEntityLockManager>();

        }

        #endregion

        [TestCaseSource(typeof(NotesSearchData), "TestCases")]
        public void NotesController_Get_Returns_OKResponseCode(NotesSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();

            //Create mock repository
            var dealNotesRepository = new Mock<INotesRepository>();

            //Build response
            IList<Data.Models.grs_VGrsNote> dealDbData = new List<Data.Models.grs_VGrsNote>
            {
                new Data.Models.grs_VGrsNote
                {
                    Notenum = 698,
                    Dealnum = 233,
                    Notedate = Convert.ToDateTime("2001-06-19 00:00:00.000"),
                    Notes = "Left message for John Kenney to let him no that we would most likely not be able to quote on the submission due the relatively small size of the deal, and the amount of risk transfer that appeared that they were looking for (in the slip: winner take all w/ no agg limit)",
                    Notetype = 22,
                    Whoentered = 8069,
                      Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
                }
            };

            // Search method
            dealNotesRepository.Setup(p => p.GetNotes(criteria.DealNumber)).Returns(dealDbData);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, dealNotesRepository.Object);

            #endregion

            #region Act

            //var response = dealNotesController.Get(criteria);

            //DealNotesSearchCriteria dsc = new DealNotesSearchCriteria { DealNumber = 233 };
            var response = dealNotesController.Get(criteria);

            //var response = dealNotesController.Get(It.IsAny<DealNotesSearchCriteria>());
            var contentResult = response as NegotiatedContentResult<ResponseCollection<Notes>>;

            #endregion

            #region Assert

            #region Expected

            var expectedDealNotes = new Notes
            {
                Notenum = 698,
                DealNumber = 233,
                Notedate = Convert.ToDateTime("2001-06-19 00:00:00.000"),
                NoteText = "Left message for John Kenney to let him no that we would most likely not be able to quote on the submission due the relatively small size of the deal, and the amount of risk transfer that appeared that they were looking for (in the slip: winner take all w/ no agg limit)",
                Notetype = 22,
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
            };

            var expectedGetLink = new Link(LinkType.RelatedEntity, EntityType.Notes, $"v1/notes?dealnumber={expectedDealNotes.DealNumber}", HttpMethodType.GET);

            #endregion
            Assertions.AssertOkResponse(contentResult);

            var summaryData = contentResult.Content.results;

            for (int i = 0; i <= summaryData.Count - 1; i++)
            {
                //Data
                var actualDealNotes = summaryData[i].data;

				Assertions.AssertData(expectedDealNotes, actualDealNotes);

                // Links & Messages
                //Assert.Multiple(() =>
                //{
                //    Assert.IsNotEmpty(summaryData[i].links);
                //    Assert.AreEqual(1, summaryData[i].links.Count);
                //    Assertions.AssertLink(expectedGetLink, summaryData[i].links[0]);
                //});

                Assert.IsEmpty(summaryData[i].messages);
            }

            #endregion

        }

        [TestCaseSource(typeof(NotesSearchData), "NegativeTestCases")]
        public void NotesController_Get_Returns_BadResponse(NotesSearchCriteria criteria)
        {
            #region Arrange

            SetupUserIdentity();

            //Build response
            var dealNotesRepository = new Mock<INotesRepository>();

            //Build response
            IList<Data.Models.grs_VGrsNote> dealDbData = new List<Data.Models.grs_VGrsNote>
            {
                new Data.Models.grs_VGrsNote
                {
                    Notenum = 698,
                    Dealnum = 233,
                    Notedate = Convert.ToDateTime("2001-06-19 00:00:00.000"),
                    Notes = "Left message for John Kenney to let him no that we would most likely not be able to quote on the submission due the relatively small size of the deal, and the amount of risk transfer that appeared that they were looking for (in the slip: winner take all w/ no agg limit)",
                    Notetype = 22,
                    Whoentered = 8069,
                      Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
                }
            };


            // Search method
            dealNotesRepository.Setup(p => p.GetMany(It.IsAny<Expression<Func<grs_VGrsNote, bool>>>())).Returns(dealDbData);
            //Manager
            //DealNotesManager dealNotesManager = new DealNotesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealNotesRepository.Object,transformationManager, new Mock<IPersonProfileManager>().Object);
            NotesManager dealNotesManager = new NotesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealNotesRepository.Object, transformationManager, new Mock<ITbDealNotesRepository>().Object);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			#endregion

			#region Act

			var response = dealNotesController.Get(criteria) as BadRequestErrorMessageResult;
            #endregion

            #region Assert

            string expectedMessage = "No Parameter found";
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
            Assert.AreEqual(expectedMessage, response.Message);

            #endregion
        }

        [Test]
        public void NotesController_GetNoteNumber_Returns_OKResponseCode()
        {
            #region Arrange

            int notenumbertest = 698;

            SetupUserIdentity();

            //Build response
            IList<Data.Models.grs_VGrsNote> dealDbData = new List<Data.Models.grs_VGrsNote>
            {
                new Data.Models.grs_VGrsNote
                {
                    Notenum = 698,
                    Dealnum = 233,
                    Notedate = Convert.ToDateTime("2001-06-19 00:00:00.000"),
                    Notes = "Left message for John Kenney to let him no that we would most likely not be able to quote on the submission due the relatively small size of the deal, and the amount of risk transfer that appeared that they were looking for (in the slip: winner take all w/ no agg limit)",
                    Notetype = 22,
                    Whoentered = 8069,
                      Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
                }
            };

            var notesRepository = new Mock<INotesRepository>();
            notesRepository.Setup(p => p.GetNotebyNoteNumber(notenumbertest)).Returns(dealDbData);

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}/{notenumbertest}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, notesRepository.Object);



			#endregion

			#region Act
			NegotiatedContentResult<ResponseCollection<Notes>> response;

            response = dealNotesController.Get(notenumbertest) as NegotiatedContentResult<ResponseCollection<Notes>>;
            var contentResult = response as NegotiatedContentResult<ResponseCollection<Notes>>;

            #endregion

            #region Assert

            #region Expected

            var expectedDealNotes = new Notes
            {
                Notenum = 698,
                DealNumber = 233,
                Notedate = Convert.ToDateTime("2001-06-19 00:00:00.000"),
                NoteText = "Left message for John Kenney to let him no that we would most likely not be able to quote on the submission due the relatively small size of the deal, and the amount of risk transfer that appeared that they were looking for (in the slip: winner take all w/ no agg limit)",
                Notetype = 22,
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName = null,
                LastName = "Downey"
            };

            var expectedGetLink = new Link(LinkType.RelatedEntity, EntityType.Notes, $"v1/notes/{notenumbertest}", HttpMethodType.GET);

            #endregion
            Assertions.AssertOkResponse(contentResult);

            var summaryData = contentResult.Content.results;

            for (int i = 0; i <= summaryData.Count - 1; i++)
            {
                //Data
                var actualDealNotes = summaryData[i].data;

				Assertions.AssertData(expectedDealNotes, actualDealNotes);

                // Links & Messages
                //Assert.Multiple(() =>
                //{
                //    Assert.IsNotEmpty(summaryData[i].links);
                //    Assert.AreEqual(1, summaryData[i].links.Count);
                //    Assertions.AssertLink(expectedGetLink, summaryData[i].links[0]);
                //});

                Assert.IsEmpty(summaryData[i].messages);
            }

            #endregion

        }




        [Test]
        [TestCaseSource(typeof(DealNotesPostData), "DealNotesPostTestCases")]
        public void DealNotesController_Post_Returns_CreatedCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();

            SetupDealNotesRepo(out Mock<ITbDealNotesRepository> dealNotesRepository, dealNotes);
            #endregion

            var notesRepository = new Mock<INotesRepository>();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, notesRepository.Object);

			//Build response
			IList<Data.Models.grs_VGrsNote> dealDbData = new List<Data.Models.grs_VGrsNote>
            {
                new Data.Models.grs_VGrsNote
                {
                 Dealnum = 101,
                Notenum = 100,
                Notetype = 3,
                Notes = "Test Notes Desc",
                Notedate = Convert.ToDateTime("03/01/2019"),
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
                }
            };

			notesRepository.Setup(p => p.GetNotebyNoteNumber(0)).Returns(dealDbData);

			#region Act

			var response = dealNotesController.Post(dealNotes);
            #endregion

            #region Expected Data

            var expectedDealNote = new Notes()
            {
                DealNumber = 101,
                Notenum = 100,
                Notetype = 3,
                NoteText = "Test Notes Desc",
                Notedate = Convert.ToDateTime("03/01/2019"),
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName = null,
                LastName = "Downey"
            };

            #endregion

            #region Assert
            var contentResult = response as NegotiatedContentResult<ResponseItem<Notes>>;

            Assertions.AssertCreatedResponse(contentResult);
            var dealNoteData = contentResult.Content.data;

            //Data
            var actualDealNote = dealNoteData;

			Assertions.AssertData(expectedDealNote, actualDealNote);
            
            //Links & Messages
            //  Assert.IsEmpty(contentResult.Content.links);
            Assert.IsEmpty(contentResult.Content.messages);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealNotesPostData), "DealNotesPostNoContentTestCases")]
        public void DealNotesController_Post_Returns_NoContentCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
            #endregion

            #region Act
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			var response = dealNotesController.Post(dealNotes);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealNotesPostData), "DealNotesPostBadRequestTestCases")]
        public void DealNotesController_Post_Returns_BadRequestCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			#endregion

			#region Act
			var response = dealNotesController.Post(dealNotes);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.BadRequest);
            #endregion

        }
        
        [Test]
        [TestCaseSource(typeof(DealNotesPostData), "DealNotesPostInternalServerErrorTestCases")]
        public void DealNotesController_Post_Returns_InternalServerErrorCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			#endregion

			#region Act
			var response = dealNotesController.Post(dealNotes);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.InternalServerError, contentResult.StatusCode);
            #endregion

        }


        [Test]
        [TestCaseSource(typeof(DealNotesPutData), "DealNotesPutTestCases")]
        public void DealNotesController_Put_Returns_OkCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();

            SetupDealNotesRepo(out Mock<ITbDealNotesRepository> dealNotesRepository, dealNotes);

            var notesRepository = new Mock<INotesRepository>();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");

			NotesManager dealNotesManager = new NotesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, notesRepository.Object, transformationManager, dealNotesRepository.Object);
			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealNotesManager);
			NotesController dealNotesController = new NotesController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};
			#endregion


			#region Act

			//Build response
			IList<Data.Models.grs_VGrsNote> dealDbData = new List<Data.Models.grs_VGrsNote>
            {
                new Data.Models.grs_VGrsNote
                {
                   Dealnum = 101,
                Notenum = 100,
                Notetype = 3,
                Notes = "Test Notes Desc",
                Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy")),
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName= null,
                LastName="Downey"
                }
            };
            #endregion
            #region Act


            notesRepository.Setup(p => p.GetNotebyNoteNumber(100)).Returns(dealDbData);



            #endregion

            #region Act
			var response = dealNotesController.Put(dealNotes);
            #endregion

            #region Expected Data

            var expectedDealNote = new Notes()
            {
                DealNumber = 101,
                Notenum = 100,
                Notetype = 3,
                NoteText = "Test Notes Desc",
                Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy")),
                Whoentered = 8069,
                Name = "Chris Downey",
                FirstName = "Chris",
                MiddleName = null,
                LastName = "Downey"
            };

            #endregion

            #region Assert
            var contentResult = response as NegotiatedContentResult<ResponseItem<Notes>>;


            Assertions.AssertOkResponse(contentResult);
            var dealNoteData = contentResult.Content.data;

            //Data
            var actualDealNote = dealNoteData;
			Assertions.AssertData(expectedDealNote, actualDealNote);
            


            //Links & Messages
            //  Assert.IsEmpty(contentResult.Content.links);
            Assert.IsEmpty(contentResult.Content.messages);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealNotesPutData), "DealNotesPutNoContentTestCases")]
        public void DealNotesController_Put_Returns_NoContentCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			#endregion

			#region Act
			var response = dealNotesController.Put(dealNotes);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealNotesPutData), "DealNotesPutBadRequestTestCases")]
        public void DealNotesController_Put_Returns_BadRequestCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);
			#endregion

			#region Act
			var response = dealNotesController.Put(dealNotes);
            #endregion

            #region Assert
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.BadRequest);
            #endregion

        }

        [Test]
        [TestCaseSource(typeof(DealNotesPutData), "DealNotesPutNotFoundTestCases")]
        public void DealNotesController_Put_Returns_NotFoundErrorCode(Notes dealNotes)
        {
            #region Arrange
            SetupUserIdentity();
            Mock<ITbDealNotesRepository> dealNotesRepository  =  new Mock<ITbDealNotesRepository>();
			TbDealnote dealnotes = null;
            dealNotesRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TbDealnote, bool>>>())).Returns(dealnotes);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.NotesRoutePrefix}");
			NotesController dealNotesController = CreateDealNotesController(httpRequest, new Mock<INotesRepository>().Object);

			#endregion

			#region Act

			#endregion

			#region Assert
			Assert.Throws(typeof(NotFoundAPIException), delegate { dealNotesController.Put(dealNotes); });
            #endregion

        }


		#region Private methods
		private NotesController CreateDealNotesController(HttpRequestMessage httpRequest, INotesRepository dealNotesRepository)
		{
			NotesManager dealNotesManager = new NotesManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealNotesRepository, transformationManager, new Mock<ITbDealNotesRepository>().Object);
			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, dealNotesManager);

			NotesController dealNotesController = new NotesController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};

			return dealNotesController;
		}


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

        [OneTimeTearDown]
        public void Cleanup()
        {
            userManager = null;
            cacheStoreManager = null;
            mockLogManager = null;
            transformationManager = null;
            userIdentity = null;
        }

        public class NotesSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new NotesSearchCriteria() { DealNumber = 233 });
                    //yield return new TestCaseData(new NotesSearchCriteria() { DealNumber = 0 });
                    //    yield return new TestCaseData(new NotesSearchCriteria() { DealNumber = 24444 });
                   // yield return new TestCaseData(null);
                }
            }
            public static IEnumerable NegativeTestCases
            {
                get
                {
                   // yield return new TestCaseData(new NotesSearchCriteria() { DealNumber = 0 });
                    yield return new TestCaseData(null);
                }
            }

        }

        public class DealNotesPostData
        {
            public static IEnumerable DealNotesPostTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes()
                    {
                        DealNumber = 101,
                        Notenum = 100,
                        Notetype = 3,
                        NoteText = "Test Notes Desc",
                        Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy")),
                        Whoentered=8098,
                        Name="Jhon",
                        MiddleName=null,
                        LastName="test"
                    });
                }
            }

            public static IEnumerable DealNotesPostBadRequestTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes());
                }
            }
            
            public static IEnumerable DealNotesPostNoContentTestCases
            {
                get
                {
                    yield return new TestCaseData(null);
                }
            }
            public static IEnumerable DealNotesPostInternalServerErrorTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes()
                    {
                        DealNumber = 101,
                        Notenum = 100,
                        Notetype = 3,
                        NoteText = "Test Notes Desc",
                        Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"))
                    });
                }
            }
        }

        public class DealNotesPutData
        {
            public static IEnumerable DealNotesPutTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes()
                    {
                        Notenum = 100,
                        Notetype = 3,
                        NoteText = "Test Notes Desc"
                    });
                }
            }

            public static IEnumerable DealNotesPutBadRequestTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes());
                }
            }

            public static IEnumerable DealNotesPutNoContentTestCases
            {
                get
                {
                    yield return new TestCaseData(null);
                }
            }
            public static IEnumerable DealNotesPutNotFoundTestCases
            {
                get
                {
                    yield return new TestCaseData(new Notes()
                    {
                        Notenum = 100,
                        Notetype = 3,
                        NoteText = "Test Notes Desc"
                    });
                }
            }
        }

        private void SetupDealNotesRepo(out Mock<ITbDealNotesRepository> dealNotesRepository, Notes dealNotes)
        {
            //Create mock repository
            dealNotesRepository = new Mock<ITbDealNotesRepository>();

            //Build response
            var dealNoteDataList = new List<TbDealnote> {new TbDealnote()
            {
                Dealnum = 101,
                Notenum = 99,
                Notetype = 3,
                Notes = "Test Notes Desc",
                Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"))
            } };

            var dealNoteData = new TbDealnote()
            {
                Dealnum = 101,
                Notenum = 100,
                Notetype = 3,
                Notes = "Test Notes Desc",
                Notedate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"))
            };
            //dealNotesRepository.Setup(p => p.GetAll()).Returns(dealNoteDataList);
            //dealNotesRepository.Setup(p => p.ExecuteQuery(It.IsAny<string>())).Returns(dealNoteDataList);
            dealNotesRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TbDealnote, bool>>>())).Returns(dealNoteData);
        }
    }
	#endregion
}

