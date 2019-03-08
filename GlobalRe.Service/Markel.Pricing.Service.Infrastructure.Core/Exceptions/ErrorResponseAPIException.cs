using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// Third party service returned error. Subsequent requests by the client are permissible.
    /// 
    /// 424 Failed Dependency (WebDAV):
    /// The 424 (Failed Dependency) status code means that the method could not be performed on the resource
    /// because the requested action depended on another action and that action failed.
    /// For example, if a command in a PROPPATCH method fails, then, at minimum,
    /// the rest of the commands will also fail with 424 (Failed Dependency).
    /// </summary>
    public class ErrorResponseAPIException : APIMessageException
    {
        #region Constructors

        public ErrorResponseAPIException() : base() { }
        public ErrorResponseAPIException(string message) : base(message) { }
        public ErrorResponseAPIException(string message, APIMessage detail) : base(message, detail) { }
        public ErrorResponseAPIException(string message, IList<APIMessage> details) : base(message, details) { }
        public ErrorResponseAPIException(IList<APIMessage> details) : base(details) { }
        public ErrorResponseAPIException(string field, string detail) : base(field, detail) { }
        public ErrorResponseAPIException(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 424; } }

        #endregion Methods
    }
}
