using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.API.v1.Traits;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
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
using Newtonsoft.Json.Linq;
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
	[Category("UserView")]
	[Category("Controllers")]
	[TestFixture]
	public class UserViewControllerTests
	{
		#region Private Variables

		private Mock<ILogManager> mockLogManager;
		private Mock<IUserManager> userManager;
		private Mock<ICacheStoreManager> cacheStoreManager;
		private UserViewTransformationManager transformationManager;
		private UserIdentity userIdentity;
		private Mock<IUserViewRepository> userViewRepository;
		private Mock<IUserViewScreenRepository> userViewScreenRepository;

		#endregion

		#region Initial Setup

		[OneTimeSetUp]
		public void Init()
		{
			mockLogManager = new Mock<ILogManager>();
			userManager = new Mock<IUserManager>();
			cacheStoreManager = new Mock<ICacheStoreManager>();
			transformationManager = new UserViewTransformationManager(userManager.Object);
			userViewRepository = new Mock<IUserViewRepository>();
			userViewScreenRepository = new Mock<IUserViewScreenRepository>();

		}

		#endregion

		[TestCaseSource(typeof(UserViewSearchData), "TestCases")]
		public void UserViewScreenController_Get_Returns_OKResponseCode(UserViewSearchCriteria criteria)
		{
			#region Arrange

			SetupUserIdentity();

			//Build response
			IList<grs_VGrsUserView> userViewScreenDbData = new List<grs_VGrsUserView>
			{
				new grs_VGrsUserView
				{
					ViewId = 101,
				Userid = 100,
				Default = false,
				Screenname = "GRS.UW_Workbench",
				Viewname = "mysubmissions",
				Layout = "SomeJason",
				Customview = false,
				Sortorder = 1
				}
			};

			// Search method
			userViewScreenRepository.Setup(p => p.GetMany(It.IsAny<Expression<Func<grs_VGrsUserView, bool>>>())).Returns(userViewScreenDbData);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");
			UserViewController userviewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userviewController.Get(criteria);
			var contentResult = response as NegotiatedContentResult<ResponseCollection<UserView>>;

			#endregion

			#region Assert

			#region Expected


			var expectedUserViewScreen = new UserView
			{
				ViewId = 101,
				UserId = 100,
				Default = false,
				ScreenName = "GRS.UW_Workbench",
				ViewName = "mysubmissions",
				Layout = null,
				CustomView = false,
				SortOrder = 1
			};

			//var expectedGetLink = new Link(LinkType.RelatedEntity, EntityType.Notes, $"v1/userviews?screenname={expectedUserViewScreen.ScreenName}", HttpMethodType.GET);

			#endregion

			Assertions.AssertOkResponse(contentResult);

			var summaryData = contentResult.Content.results;

			for (int i = 0; i <= summaryData.Count - 1; i++)
			{
				//Data
				var actualUserViewScreen = summaryData[i].data;
				Assertions.AssertData(expectedUserViewScreen, actualUserViewScreen);

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

		[TestCaseSource(typeof(UserViewSearchData), "NegativeTestCases")]
		public void UserViewScreenController_Get_Returns_BadResponse(UserViewSearchCriteria criteria)
		{
			#region Arrange

			SetupUserIdentity();

			//Build response
			IList<grs_VGrsUserView> userViewScreenDbData = new List<grs_VGrsUserView>
			{
				new grs_VGrsUserView
				{
					ViewId = 101,
				Userid = 100,
				Default = false,
				Screenname = "GRS.UW_Workbench",
				Viewname = "mysubmissions",
				Layout = "SomeJason",
				Customview = false,
				Sortorder = 1
				}
			};

			// Search method
			userViewScreenRepository.Setup(p => p.GetMany(It.IsAny<Expression<Func<grs_VGrsUserView, bool>>>())).Returns(userViewScreenDbData);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");

			UserViewController userviewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userviewController.Get(criteria) as BadRequestErrorMessageResult;

			#endregion

			#region Assert

			string expectedMessage = "No Parameter found";
			Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
			Assert.AreEqual(expectedMessage, response.Message);
		}

		#endregion

		[Test]
        [TestCaseSource(typeof(UserViewPutData), "TestCases")]
		public void UserViewController_Put_Returns_OkCode(UserView userveiws)
		{
			#region Arrange
			SetupUserIdentity();

			SetupUserViewRepository(out Mock<IUserViewRepository> userViewRepository);

			var usrViewRepository = new Mock<IUserViewRepository>();

			#endregion
			#region Act

			var usrViewDbData = new grs_TblUserview()
			{
				ViewId = 46,
				Userid = 4896,
				Default = false,
				Screenname = "GRS.UW_Workbench",
				Viewname = "mysubmissions",
				Layout = "SomeJason",
				Customview = false,
				Sortorder = 1
			};

			usrViewRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_TblUserview, bool>>>())).Returns(usrViewDbData);
			usrViewRepository.Setup(p => p.Get(46)).Returns(usrViewDbData);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");
			UserViewManager UserViewManager = new UserViewManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, usrViewRepository.Object, transformationManager);

			UserPreferencesAPIManager userViewAPIManager = new UserPreferencesAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, UserViewManager);

			UserViewController userviewController = new UserViewController(userManager.Object, userViewAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};


			#endregion

			#region Act

			var response = userviewController.Put(userveiws.ViewId, userveiws);

			#endregion

			#region Expected Data

			var expectedUserViewdata = new UserView()
			{
				ViewId = 46,
				UserId = 4896,
				Default = false,
				ScreenName = "GRS.UW_Workbench",
				ViewName = "mysubmissions",
				Layout = "SomeJason",
				CustomView = false,
				SortOrder = 1
			};

			#region Assert
			var contentResult = response as NegotiatedContentResult<ResponseItem<UserView>>;

			Assertions.AssertOkResponse(contentResult);
			var usrViewData = contentResult.Content.data;

			//Data
			var actualUserViewdata = usrViewData;
			Assertions.AssertData(expectedUserViewdata, actualUserViewdata);

			//Links & Messages
			//  Assert.IsEmpty(contentResult.Content.links);
			Assert.IsEmpty(contentResult.Content.messages);
			#endregion

		}


		[Test]
		[TestCaseSource(typeof(UserViewPutData), "NegativeTestCases")]
		public void UserViewController_Put_Returns_BadRequest(UserView userviews)
		{
			#region Arrange
			SetupUserIdentity();
			SetupUserViewRepository(out Mock<IUserViewRepository> userviewRepository);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");

			UserViewController userviewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userviewController.Put(userviews.ViewId, userviews);

			#endregion

			#region Assert
			var expectedStatusCode = (userviews == null) ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest;
			var result = response as StatusCodeResult;
			Assert.AreEqual(expectedStatusCode, result.StatusCode);

			#endregion

		}
		#endregion Put Userview

		#region Post Userview

		[Test]
        [TestCaseSource(typeof(UserViewPostData), "TestCases")]
        public void UserViewController_Post_Returns_Created(UserView userViews)
        {
            #region Arrange

            SetupUserIdentity();
			userViewRepository = new Mock<IUserViewRepository>();
			//SetupUserViewRepository(out Mock<IUserViewRepository> userviewsRepository);

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");
			UserViewController userviewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			var userviewdata = new grs_TblUserview()
			{
				ViewId = 101,
				Userid = 100,
				Default = false,
				Screenname = "GRS.UW_Workbench",
				Viewname = "mysubmissions",
				Layout = "SomeJason",
				Customview = false,
				Sortorder = 1
			};

			grs_TblUserview nullData = null;
			userViewRepository.SetupSequence(p => p.Get(It.IsAny<Expression<Func<grs_TblUserview, bool>>>()))
				.Returns(nullData)
				.Returns(userviewdata);

			#endregion

			#region Act

			var response = userviewController.Post(userViews);

			#endregion

			#region Expected Data

			var expectedUserViewdata = 
				new UserView()
				{
					ViewId = 101,
					UserId = 100,
					Default = false,
					ScreenName = "GRS.UW_Workbench",
					ViewName = "mysubmissions",
					Layout = "SomeJason",
					CustomView = false,
					SortOrder = 1
				};

            #endregion

            #region Assert
            var contentResult = response as NegotiatedContentResult<ResponseItem<UserView>>;
            Assertions.AssertCreatedResponse(contentResult);
            var usrViewData = contentResult.Content.data;

            //Data
            var actualUserViewdata = usrViewData;
            Assertions.AssertData(expectedUserViewdata, actualUserViewdata);


            //Links & Messages
            //  Assert.IsEmpty(contentResult.Content.links);
            Assert.IsEmpty(contentResult.Content.messages);
            #endregion

        }

		[Test]
		[TestCaseSource(typeof(UserViewPostData), "NegativeTestCases")]
		public void UserViewController_Post_Returns_BadRequest(UserView userViews)
		{
			#region Arrange

			SetupUserIdentity();
			SetupUserViewRepository(out Mock<IUserViewRepository> userviewsRepository);
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPPOST), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}");
			UserViewController userviewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);
			#endregion

			#region Act
			var response = userviewController.Post(userViews);
			#endregion

			#region Assert
			var expectedStatusCode = (userViews == null) ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest;
			var result = response as StatusCodeResult;
			Assert.AreEqual(expectedStatusCode, result.StatusCode);

			#endregion

		}
		#endregion Post Userview

		#region Private Methods 
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
        private void SetupUserViewRepository()
        {

            var userviewdata = new grs_TblUserview()
            {
                ViewId = 101,
                Userid = 100,
                Default = false,
                Screenname = "GRS.UW_Workbench",
                Viewname = "mysubmissions",
                Layout = "SomeJason",
                Customview=false,
                Sortorder=1
            };
            userViewRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_TblUserview, bool>>>())).Returns(userviewdata);
        }
        #endregion

        public class UserViewSearchData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new UserViewSearchCriteria() { ScreenName = "GRS.UW_Workbench" });
					yield return new TestCaseData(new UserViewSearchCriteria() { ScreenName = "-1" });
				}
			}

			public static IEnumerable NegativeTestCases
			{
				get
				{
					yield return new TestCaseData(new UserViewSearchCriteria() { ScreenName = null });
					yield return new TestCaseData(null);
				}
			}
		}


		public class UserViewIDSearch
        {
            public int viewId { set; get; }
        }
        public class UserViewIdSearchData
        {

            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new UserViewIDSearch { viewId = 101 });
                    yield return new TestCaseData(new UserViewIDSearch { viewId = -1 });
                }
            }

        }

        [Test]
        [TestCaseSource(typeof(UserViewIdSearchData), "TestCases")]
        public void UserViewController_DefaultGetWithViewID_Returns_OKResponseCode(UserViewIDSearch vwId)
        {
            #region Arrange

            var ViewID = vwId.viewId;
            SetupUserViewRepository();
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}/{ViewID}");

			UserViewController userViewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userViewController.Get(ViewID);
            var contentResult = response as NegotiatedContentResult<ResponseItem<UserView>>;

            #endregion

            #region Assert

            #region Expected Data

            var expectedUserView = new UserView()
            {
                ViewId = 101,
                UserId = 100,
                Default = false,
                ScreenName = "GRS.UW_Workbench",
                ViewName = "mysubmissions",
                Layout = "SomeJason",
                CustomView=false,
                SortOrder=1
            };

            #endregion

            Assertions.AssertOkResponse(contentResult);
            var UserViewsData = contentResult.Content.data;

            //Data
            var actualUserView = UserViewsData;
            Assertions.AssertData(expectedUserView, actualUserView);

            //Links & Messages
            // Assert.IsEmpty(contentResult.Content.links);
            Assert.IsEmpty(contentResult.Content.messages);
            #endregion

        }

		#region Delete uservview Method

		[Test]
		[TestCaseSource(typeof(UserViewIdDeleteData), "TestCases")]
		public void UserViewController_DeleteUserView_Returns_OKResponseCode(UserViewDelete vwID)
		{
			#region Arrange
			SetupUserViewRepository();

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPDELETE), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}/{vwID.viewId}");

			UserViewController userViewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userViewController.Delete(vwID.viewId, vwID.userviewdelete);

			#endregion

			#region Assert

			var contentResult = response as NegotiatedContentResult<Response>;
			Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
			Assert.IsNotEmpty(contentResult.Content.messages);
			
			#endregion
		}

		[Test]
		[TestCaseSource(typeof(UserViewIdDeleteData), "NegativeTestCases")]
		public void UserViewController_DeleteUserView_Returns_BadResponse(UserViewDelete vwID)
		{
			#region Arrange
			SetupUserViewRepository();

			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPDELETE), $"{AppSettings.BASEURL}{RouteHelper.UserviewRoutePrefix}/{vwID.viewId}");

			UserViewController userViewController = CreateUserViewController(httpRequest, userViewRepository.Object, userViewScreenRepository.Object);

			#endregion

			#region Act

			var response = userViewController.Delete(vwID.viewId, vwID.userviewdelete) as BadRequestErrorMessageResult;

			#endregion

			#region Assert

			string expectedMessage = (vwID.userviewdelete == null) ? "Json Input not found" : "Screen name is not equal to GRS.UW_Workbench";
			Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
			Assert.AreEqual(expectedMessage, response.Message);

			#endregion
		}
		#endregion Delete userview Method

		#region Delete uservview TestData
		public class UserViewDelete
        {
            public int viewId { set; get; }
            public dynamic userviewdelete { set; get; }

        }
        public class UserViewIdDeleteData
        {
            public static IEnumerable TestCases
            {
				get
				{
					yield return new TestCaseData(new UserViewDelete
					{
						viewId = 101,
						userviewdelete = new JObject(JObject.Parse("{\"screenname\": " + "\"GRS.UW_Workbench\"" + ",  \"default\": true,  \"keymember\": true}"))
					});
				}
            }
			public static IEnumerable NegativeTestCases
			{
				get
				{
					yield return new TestCaseData(new UserViewDelete
					{
						viewId = 101,
						userviewdelete = new JObject(JObject.Parse("{\"screenname\": " + "\"UW_Workbench\"" + ",  \"default\": true,  \"keymember\": true}"))
					});
					yield return new TestCaseData(new UserViewDelete
					{
						viewId = 202,
						userviewdelete = null
					});
				}
			}

		}

		#endregion Delete uservview TestData

		#region Put Userview TestData
		public class UserViewPutData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new UserView()
                    {
                        ViewId = 46,
                        UserId = 4896,
                        Default = false,
                        ScreenName = "GRS.UW_Workbench",
                        ViewName = "mysubmissions",
                        Layout = "SomeJason",
                        CustomView = false,
                        SortOrder = 1
                    });
                    yield return new TestCaseData(new UserView()
                    {
                        ViewId = 44,
                        UserId = 4896,
                        Default = false,
                        ScreenName = "GRS.UW_Workbench",
                        ViewName = "mysubmissions",
                        Layout = "SomeJason",
                        CustomView = false,
                        SortOrder = 1
                    });
                    yield return new TestCaseData(new UserView()
                    {
                        ViewId = 6,
                        UserId = 3627,
                        Default = false,
                        ScreenName = "GRS.UW_Workbench",
                        ViewName = "From API 1",
                        Layout = "SomeJason",
                        CustomView = false,
                        SortOrder = 1
                    });
                }
            }
			public static IEnumerable NegativeTestCases
			{
				get
				{
					yield return new TestCaseData(new UserView());
				}
			}
		}

		public class UserViewPostData
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(new UserView()
					{
						ViewId = 46,
						UserId = 4896,
						Default = false,
						ScreenName = "GRS.UW_Workbench",
						ViewName = "mysubmiss90880980ions",
						Layout = "SomeJa88238742son",
						CustomView = false,
						SortOrder = 1
					});
					yield return new TestCaseData(new UserView()
					{
						ViewId = 8,
						UserId = 5,
						Default = true,
						ScreenName = "UW_Workbench",
						ViewName = "mysubmissions",
						Layout = "SomeJason",
						CustomView = false,
						SortOrder = 1
					});
				}
			}

			public static IEnumerable NegativeTestCases
			{
				get
				{
					yield return new TestCaseData(new UserView());
					yield return new TestCaseData(null);
					yield return new TestCaseData(new UserView()
					{
						ViewId = 998,
						UserId = 5,
						Default = false,
						ScreenName = "",
						ViewName = "",
						Layout = "SomeJason",
						CustomView = false,
						SortOrder = 1
					});

				}

			}
		}
		#endregion Put Userview TestData

		private UserViewController CreateUserViewController(HttpRequestMessage httpRequest, IUserViewRepository userViewRepository, IUserViewScreenRepository userViewScreenRepository)
		{
			UserViewManager UserViewManager = new UserViewManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, userViewRepository, transformationManager, userViewScreenRepository);
			UserPreferencesAPIManager userViewAPIManager = new UserPreferencesAPIManager(userManager.Object, cacheStoreManager.Object, mockLogManager.Object, UserViewManager);
			UserViewController userViewController = new UserViewController(userManager.Object, userViewAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration()
			};
			return userViewController;
		}



		private void SetupUserViewRepository(out Mock<IUserViewRepository> userViewRepository)
        {
            //Create mock repository
            userViewRepository = new Mock<IUserViewRepository>();

            //Build response
            var userViewDataList = new List<grs_TblUserview> {new grs_TblUserview()
            {
                 ViewId = 101,
                        Userid = 100,
                        Default = false,
                        Screenname = "GRS.UW_Workbench",
                        Viewname = "mysubmissions",
                        Layout = "SomeJason",
                        Customview = false,
                Sortorder = 1
            } };

            var userViewData = new grs_TblUserview()
            {
                ViewId = 101,
                Userid = 100,
                Default = false,
                Screenname = "GRS.UW_Workbench",
                Viewname = "mysubmissions",
                Layout = "SomeJason",
                Customview = false,
                Sortorder = 1
            };
            //            userviewRepository.Setup(p => p.ExecuteQuery(It.IsAny<string>())).Returns(userViewDataList);
            userViewRepository.Setup(p => p.Get(It.IsAny<Expression<Func<grs_TblUserview, bool>>>())).Returns(userViewData);
			userViewRepository.Setup(p => p.Save(It.IsAny<grs_TblUserview>(), false));
			userViewRepository.Setup(p => p.Add(It.IsAny<grs_TblUserview>()));
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


