using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using NUnit.Framework;
using System.Net;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace Markel.GlobalRe.Service.Underwriting.Test.Helpers
{
    static class Assertions
    {
        public static void AssertOkResponse<TExpected>(NegotiatedContentResult<TExpected> contentResult)
        {
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.OK);
            Assert.IsInstanceOf(typeof(TExpected), contentResult.Content);
        }

        public static void AssertCreatedResponse<TExpected>(NegotiatedContentResult<TExpected> contentResult)
        {
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.Created);
            Assert.IsInstanceOf(typeof(TExpected), contentResult.Content);
        }

        public static void AssertLink(Link expected, Link actual)
        {
            Assert.AreEqual(expected.type, actual.type);
            Assert.AreEqual(expected.rel, actual.rel);
            Assert.AreEqual(expected.href, actual.href);
            Assert.AreEqual(expected.method, actual.method);
        }
		public static void AssertMessage(Message expected, Message actual)
		{
			Assert.AreEqual(expected.detail, actual.detail);
			Assert.AreEqual(expected.Severity, actual.Severity);
			Assert.AreEqual(expected.field, actual.field);
		}

		public static void AssertData(object expected, object actual)
		{
			AreEqualByJson(expected, actual);
		}

		public static void AreEqualByJson(object expected, object actual)
		{
			var serializer = new JavaScriptSerializer();
			var expectedJson = serializer.Serialize(expected);
			var actualJson = serializer.Serialize(actual);
			Assert.AreEqual(expectedJson, actualJson);
		}
	}
}
