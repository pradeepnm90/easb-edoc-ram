using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The requested resource could not be found but may be available in the future.
    /// Subsequent requests by the client are permissible.
    /// </summary>
    public class NotFoundAPIException : APIMessageException
    {
        #region Constructors

        public NotFoundAPIException() : base() { }
        public NotFoundAPIException(string message) : base(message) { }
        public NotFoundAPIException(string message, APIMessage detail) : base(message, detail) { }
        public NotFoundAPIException(string message, IList<APIMessage> details) : base(message, details) { }
        public NotFoundAPIException(IList<APIMessage> details) : base(details) { }
        public NotFoundAPIException(string field, string detail) : base(field, detail) { }
        public NotFoundAPIException(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 404; } }

        #endregion Methods
    }
}
