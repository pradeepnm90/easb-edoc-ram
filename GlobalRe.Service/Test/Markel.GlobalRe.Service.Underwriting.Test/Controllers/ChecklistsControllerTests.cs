using Markel.GlobalRe.Service.Underwriting.API.v1;
using Markel.GlobalRe.Service.Underwriting.API.v1.Controllers;
using Markel.GlobalRe.Service.Underwriting.API.v1.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.Test.Helpers;
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
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Markel.GlobalRe.Service.Underwriting.Test.Controllers
{
	[Category("Deal")]
	[Category("Controllers")]
	[TestFixture]
	public class ChecklistsControllerTests
	{

		#region Private Variables

		private Mock<ILogManager> mockLogManager;
		private Mock<IUserManager> userManager;
		private CacheStoreManager cacheStoreManager;
		private CheckListsTransformationManager transformationManager;
		private UserIdentity userIdentity;
		#endregion

		#region Initial Setup

		[OneTimeSetUp]
		public void Init()
		{
			mockLogManager = new Mock<ILogManager>();
			userManager = new Mock<IUserManager>();
			cacheStoreManager = new CacheStoreManager();
			transformationManager = new CheckListsTransformationManager(userManager.Object);
		}

		#endregion

        #region GET Test Methods

		[Test]
		public void ChecklistsController_Get_Returns_OKResponseCode()
		{

			#region Arrange
			SetupUserIdentity();

			int dealNumber = 123;
			var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}/checklists");

			Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();

            IList<grs_VGrsChecklistsByDeal> data = new List<grs_VGrsChecklistsByDeal>
            {
                new grs_VGrsChecklistsByDeal
                {
                    Dealnum = 1392041,
                    Entitynum =  1,
                    Category = 17,
                    CategoryName = "Pre-Bind Processing",
                    Catorder = 14,
                    Chklistnum = 276,
                    ChecklistName = "Actuarial Analysis",
                    Sortorder = 3,
                    Readonly = false,
                    Checked = true,
                    PersonId = 714027,
                    PersonName = "Dhanraj Patil",
                    Note = "",
                    FirstName = "Dhanraj",
                    LastName = "Patil",
                    MiddleName = null
                }
            };
            checkListRepository.Setup(p => p.GetAllDealChecklists(123)).Returns(data);

			CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

			#endregion

			#region Act

			var response = checkListsController.Get(dealNumber);
            var contentResult = response as NegotiatedContentResult<ResponseCollection<ChkCategoryTree>>;


			#endregion

			#region Assert

			#region Expected Data

            var expectedchktree = new CheckListTree {
                ChkListNum = 276,
                ChkListName = "Actuarial Analysis",
                SortOrder = 3,
                ReadOnly = false,
                Checked = true,
                PersonId = 714027,
                PersonName = "Dhanraj Patil",
                Note = "",
                FirstName = "Dhanraj",
                LastName = "Patil",
                MiddleName = null
            };

            var expectedData = new List<ChkCategoryTree> {
                new ChkCategoryTree
                {
                    DealNumber = 1392041,
                    EntityNum = 1,
                    CategoryID = 17,
                    CategoryName = "Pre-Bind Processing",
                    CatOrder = 14,
                    Checklists = new List<CheckListTree>{ expectedchktree }
                }
            };
            //expectedData[0].Checklists.Add(expectedchktree);

            var chklistdata = contentResult.Content.results;

            for (int i = 0; i <= chklistdata.Count - 1; i++)
            {
                //Data
                var actualchklistdata = chklistdata[i].data;
                Assertions.AssertData(expectedData[0], actualchklistdata);
                Assert.IsEmpty(chklistdata[i].messages);
            }

			#endregion

			Assertions.AssertOkResponse(contentResult);

			#endregion

		}

        [Test]
        public void ChecklistsController_Get_Returns_NotFoundResponseCode()
        {

            #region Arrange
            SetupUserIdentity();

            int dealNumber = 123;
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}/checklists");

            Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();

            IList<grs_VGrsChecklistsByDeal> data = new List<grs_VGrsChecklistsByDeal>
            {
            };
            checkListRepository.Setup(p => p.GetAllDealChecklists(123)).Returns(data);

            CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

		#endregion

            #region Act & Assert

            Assert.Throws(typeof(NotFoundAPIException), delegate { checkListsController.Get(dealNumber); });

            #endregion

        }

        [Test]
        public void ChecklistsController_Get_Returns_BadRequestResponseCode()
        {

            #region Arrange
            SetupUserIdentity();

            int dealNumber = -1;
            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealNumber}/checklists");
            Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();
            CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

            #endregion

            #region Act & Assert

            var response = checkListsController.Get(dealNumber);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
            #endregion

        }


        #endregion

        #region PUT Test Methods

        [Test]
        [TestCaseSource(typeof(CheckListPutData), "TestCases")]
        public void ChecklistsController_Put_Returns_OKResponseCode(int dealnumber, int checklistnum, bool chkstatus, ChkCategoryTree checklist)
        {

            #region Arrange
            SetupUserIdentity();

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/checklists/{checklistnum}");

            Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();

            IList<grs_VGrsChecklistsByDeal> data = new List<grs_VGrsChecklistsByDeal>
            {
                new grs_VGrsChecklistsByDeal
                {
                    Dealnum = 1392041,
                    Entitynum =  1,
                    Category = 17,
                    CategoryName = "Pre-Bind Processing",
                    Catorder = 14,
                    Chklistnum = 276,
                    ChecklistName = "Actuarial Analysis",
                    Sortorder = 3,
                    Readonly = false,
                    Checked = true,
                    PersonId = 714027,
                    PersonName = "Dhanraj Patil",
                    Note = "Test DP",
                    FirstName = "Dhanraj",
                    LastName = "Patil",
                    MiddleName = null
                }
            };
            if (chkstatus)
                checkListRepository.Setup(p => p.IsValidDealCheckedStatus(data[0].Dealnum, data[0].Chklistnum)).Returns(1);
            else
                checkListRepository.Setup(p => p.IsValidDealCheckedStatus(data[0].Dealnum, data[0].Chklistnum)).Returns(0);

            checkListRepository.Setup(p => p.GetPersonByUserId(data[0].PersonId ?? 0)).Returns(data[0].PersonName);
            checkListRepository.Setup(p => p.GetCheckNumByDealChecklists(data[0].Dealnum, data[0].Chklistnum)).Returns(data);
            var chklistval = new TbChklistval { PersonId = 714027 };
            checkListRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TbChklistval, bool>>>())).Returns(chklistval);

            CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

            #endregion

            #region Act

            var response = checkListsController.Put(dealnumber,checklistnum,checklist);


            #endregion

            #region Assert

            #region Expected Data

            var expectedchktree = new CheckListTree
            {
                ChkListNum = 276,
                ChkListName = "Actuarial Analysis",
                SortOrder = 3,
                ReadOnly = false,
                Checked = true,
                PersonId = 714027,
                PersonName = "Dhanraj Patil",
                Note = "Test DP",
                FirstName = "Dhanraj",
                LastName = "Patil",
                MiddleName = null
            };

            var expectedData = new List<ChkCategoryTree> {
                new ChkCategoryTree
                {
                    DealNumber = 1392041,
                    EntityNum = 1,
                    CategoryID = 17,
                    CategoryName = "Pre-Bind Processing",
                    CatOrder = 14,
                    Checklists = new List<CheckListTree>{ expectedchktree }
                }
            };
            #endregion

            if (chkstatus && !(checklist.Checklists[0].Checked ?? false))
            {
                var contentResult = response as NegotiatedContentResult<Response>;
                Assertions.AssertOkResponse(contentResult);
            }
            else
            {
                var contentResult = response as NegotiatedContentResult<ResponseItem<ChkCategoryTree>>;
                var chklistdata = contentResult.Content.data;
                Assertions.AssertData(expectedData[0], chklistdata);
                Assertions.AssertOkResponse(contentResult);
            }



            #endregion

        }

        [Test]
        [TestCaseSource(typeof(CheckListPutData), "NegativeTestCases")]
        public void ChecklistsController_Put_Returns_NotFoundResponseCode(int dealnumber, int checklistnum, int chkstatus, ChkCategoryTree checklist)
        {

            #region Arrange
            SetupUserIdentity();

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/checklists/{checklistnum}");

            Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();

            IList<grs_VGrsChecklistsByDeal> data = new List<grs_VGrsChecklistsByDeal>
            {
                new grs_VGrsChecklistsByDeal
                {
                    Dealnum = 1392041,
                    Entitynum =  1,
                    Category = 17,
                    CategoryName = "Pre-Bind Processing",
                    Catorder = 14,
                    Chklistnum = 276,
                    ChecklistName = "Actuarial Analysis",
                    Sortorder = 3,
                    Readonly = false,
                    Checked = true,
                    PersonId = 714027,
                    PersonName = "Dhanraj Patil",
                    Note = "Test DP",
                    FirstName = "Dhanraj",
                    LastName = "Patil",
                    MiddleName = null
                }
            };

            checkListRepository.Setup(p => p.IsValidDealCheckedStatus(data[0].Dealnum, data[0].Chklistnum)).Returns(chkstatus);
            checkListRepository.Setup(p => p.GetPersonByUserId(data[0].PersonId ?? 0)).Returns(data[0].PersonName);
            if (checklist.Checklists[0].ChkListNum == 999)
            {
                data.Clear();
                checkListRepository.Setup(p => p.GetCheckNumByDealChecklists(dealnumber, checklistnum)).Returns(data);
            }
            else
                checkListRepository.Setup(p => p.GetCheckNumByDealChecklists(data[0].Dealnum, data[0].Chklistnum)).Returns(data);
            var chklistval = new TbChklistval { PersonId = 714027 };
            if (checklist.Checklists[0].PersonId > 0)
            checkListRepository.Setup(p => p.Get(It.IsAny<Expression<Func<TbChklistval, bool>>>())).Returns(chklistval);

            CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

            #endregion

            #region Act & Assert

            Assert.Throws(typeof(NotFoundAPIException), delegate { checkListsController.Put(dealnumber, checklistnum, checklist); });

            #endregion

        }

        [Test]
        [TestCaseSource(typeof(CheckListPutDataV2), "NegativeTestCases")]
        public void ChecklistsController_Put_Returns_BadRequestResponseCode(int dealnumber, int checklistnum, int chkstatus, ChkCategoryTree checklist)
        {

            #region Arrange
            SetupUserIdentity();

            var httpRequest = new HttpRequestMessage(new HttpMethod(AppSettings.HTTPGET), $"{AppSettings.BASEURL}{RouteHelper.DealsRoutePrefix}/{dealnumber}/checklists/{checklistnum}");
            Mock<ITblCheckListRepository> checkListRepository = new Mock<ITblCheckListRepository>();

            CheckListsController checkListsController = CreateChecklistsController(httpRequest, checkListRepository.Object);

			#endregion

			#region Act
			var response = checkListsController.Put(dealnumber, checklistnum, checklist);
			#endregion

			#region Assert
			Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
            #endregion

        }

        #endregion

        #region Test Data
        public class CheckListPutData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(1392041, 276, true, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        EntityNum = 1,
                        CategoryID = 17,
                        CategoryName = "Pre-Bind Processing",
                        CatOrder = 14,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                ChkListName = "Actuarial Analysis",
                                SortOrder = 3,
                                ReadOnly = false,
                                Checked = true,
                                PersonId = 714027,
                                PersonName = "Dhanraj Patil",
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                                FirstName = "Dhanraj",
                                LastName = "Patil",
                                MiddleName = null
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, false, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        EntityNum = 1,
                        CategoryID = 17,
                        CategoryName = "Pre-Bind Processing",
                        CatOrder = 14,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                ChkListName = "Actuarial Analysis",
                                SortOrder = 3,
                                ReadOnly = false,
                                Checked = true,
                                PersonId = 714027,
                                PersonName = "Dhanraj Patil",
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                                FirstName = "Dhanraj",
                                LastName = "Patil",
                                MiddleName = null
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276,true, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        EntityNum = 1,
                        CategoryID = 17,
                        CategoryName = "Pre-Bind Processing",
                        CatOrder = 14,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                ChkListName = "Actuarial Analysis",
                                SortOrder = 3,
                                ReadOnly = false,
                                Checked = false,
                                PersonId = 714027,
                                PersonName = "Dhanraj Patil",
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                                FirstName = "Dhanraj",
                                LastName = "Patil",
                                MiddleName = null
                            }
                        }
                    });
                }//get
            }

            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(1392041, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 0, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27 Wrong Format",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 0, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                               // PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 0, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 71402799,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 999, 0, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 999,
                                Checked = true,
                                PersonId = 714027,
                                CheckedDateTime = "03/05/2019 12:40:27"
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                CheckedDateTime = "03/05/2019 12:40:27"
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                CheckedDateTime = "03/05/2019 12:40:27",
                                Note = "morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200morethan200"
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = false,
                                PersonId = -1,
                                CheckedDateTime = "03/05/2019 12:40:27",
                                Note = "Test DP"
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 71402799,
                                CheckedDateTime = "03/05/2019 12:40:27",
                                Note = "Test DP"
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, 0, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        CategoryID = 17,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = false,
                                PersonId = 714027,
                                CheckedDateTime = "03/05/2019 12:40:27",
                                Note = "Test DP"
                            }
                        }
                    });

                }//get
            }
        }//class

        public class CheckListPutDataV2
        {
            public static IEnumerable NegativeTestCases
            {
                get
                {
                    yield return new TestCaseData(1392041, 276, -1, null);

                    yield return new TestCaseData(1392041, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = null
                    });
                    yield return new TestCaseData(-1, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(10000, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 0, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 111, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = null,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        EntityNum = -5,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });

                    yield return new TestCaseData(1392041, 276, -1, new ChkCategoryTree()
                    {
                        DealNumber = 1392041,
                        EntityNum = 5,
                        Checklists = new List<CheckListTree>  { new CheckListTree
                            {
                                ChkListNum = 276,
                                Checked = true,
                                PersonId = 714027,
                                Note = "Test DP",
                                CheckedDateTime = "03/05/2019 12:40:27",
                            }
                        }
                    });
                }//get
            }
        }//class
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

		private CheckListsController CreateChecklistsController(HttpRequestMessage httpRequest, ITblCheckListRepository checkListsRepository)
		{
			CheckListsManager checkListsManager = new CheckListsManager(userManager.Object, cacheStoreManager, mockLogManager.Object, checkListsRepository, transformationManager);

			DealAPIManager dealAPIManager = new DealAPIManager(userManager.Object, cacheStoreManager, mockLogManager.Object, checkListsManager);

			CheckListsController checklistsController = new CheckListsController(userManager.Object, dealAPIManager)
			{
				Request = httpRequest,
				Configuration = new HttpConfiguration(),
			};

			return checklistsController;
		}


		private void SetupUserIdentity()
		{
			userIdentity = new UserIdentity("GRS", 1, 1, "john.doe", "corporate.net", "GRS", DateTime.Now, true, "", false, null, null, null);
			userManager.Setup(p => p.UserIdentity).Returns(userIdentity);
		}
		#endregion
	}
}
