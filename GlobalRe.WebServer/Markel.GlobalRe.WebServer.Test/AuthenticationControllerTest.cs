using Markel.GlobalRe.WebServer.Controllers;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace Markel.GlobalRe.WebServer.Test
{
    [TestFixture]
	[Category("Controllers")]
	public class AuthenticationControllerTest
	{

		#region Initial Setup

		[SetUp]
		public void Setup()
		{
		}

		#endregion

		//[Test]
		public void WhoAmIControllerTest_GetValues_Returns_WindowsUserName()
		{

            #region Arrange

            WhoAmIController controller = new WhoAmIController();
			string expectedUserName = Environment.UserName;

			#endregion

			#region Act

			HttpResponseMessage userNameValue = controller.Get();
			string actualUserName = userNameValue.Content.ToString();

			#endregion

			#region Assert
			Assert.AreEqual(expectedUserName, actualUserName);
			#endregion
		}

		//[Test]
		public void WhoAmIControllerTest_CreateNewController_Success()
		{

            #region Arrange

            WhoAmIController controller = new WhoAmIController();

			#endregion

			#region Act


			#endregion

			#region Assert
			Assert.IsNotNull(controller);
			#endregion
		}

		#region TearDown

		[TearDown]
		public void TearDown()
		{
		}

		#endregion
	}
}

