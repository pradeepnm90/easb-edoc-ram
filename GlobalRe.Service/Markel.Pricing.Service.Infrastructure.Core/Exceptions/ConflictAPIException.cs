using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// CONFLICT
    /// The request could not be completed due to a conflict with the current state of the target resource.
    /// This code is used in situations where the user might be able to resolve the conflict and resubmit the request.
    /// </summary>
    public class ConflictAPIException : APIMessageException
    {
        #region Constructors

        public ConflictAPIException() : base() { }
        public ConflictAPIException(string message) : base(message) { }
        public ConflictAPIException(string message, APIMessage detail) : base(message, detail) { }
        public ConflictAPIException(string message, IList<APIMessage> details) : base(message, details) { }
        public ConflictAPIException(IList<APIMessage> details) : base(details) { }
        public ConflictAPIException(string field, string detail) : base(field, detail) { }
        public ConflictAPIException(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 409; } }

        #endregion Methods
    }
}
