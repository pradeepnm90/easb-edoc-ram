using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// A request method is not supported for the requested resource;
    /// for example, a GET request on a form that requires data to be presented via POST,
    /// or a PUT request on a read-only resource.
    /// </summary>
    public class NotAllowedAPIException : APIMessageException
    {
        #region Constructors

        public NotAllowedAPIException() : base() { }
        public NotAllowedAPIException(string message) : base(message) { }
        public NotAllowedAPIException(string message, APIMessage detail) : base(message, detail) { }
        public NotAllowedAPIException(string message, IList<APIMessage> details) : base(message, details) { }
        public NotAllowedAPIException(IList<APIMessage> details) : base(details) { }
        public NotAllowedAPIException(string field, string detail) : base(field, detail) { }
        public NotAllowedAPIException(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 405; } }

        #endregion Methods
    }
}
