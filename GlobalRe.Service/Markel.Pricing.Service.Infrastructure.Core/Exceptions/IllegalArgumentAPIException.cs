using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The request was well-formed but was unable to be followed due to semantic errors.
    /// </summary>
    public class IllegalArgumentAPIException : APIMessageException
    {
        #region Constructors

        public IllegalArgumentAPIException() : base() { }
        public IllegalArgumentAPIException(string message) : base(message) { }
        public IllegalArgumentAPIException(string message, APIMessage detail) : base(message, detail) { }
        public IllegalArgumentAPIException(string message, IList<APIMessage> details) : base(message, details) { }
        public IllegalArgumentAPIException(IList<APIMessage> details) : base(details) { }
        public IllegalArgumentAPIException(string field, string detail) : base(field, detail) { }
        public IllegalArgumentAPIException(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 422; } }

        #endregion Methods
    }
}
