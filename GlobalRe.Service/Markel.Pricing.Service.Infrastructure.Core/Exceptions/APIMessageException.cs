using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    public abstract class APIMessageException : APIException
    {
        #region Variables

        private List<APIMessage> _messageDetails = new List<APIMessage>();

        #endregion Variables

        #region Constructors

        public APIMessageException() { }

        public APIMessageException(string message) : base(message) { }
        
        public APIMessageException(IList<APIMessage> details) : base()
        {
            _messageDetails.AddRange(details);
        }

        public APIMessageException(string field, string detail) : base()
        {
            _messageDetails.Add(new APIMessage(field, detail));
        }

        public APIMessageException(Expression<Func<object>> fieldExpression, string detail) : base()
        {
            _messageDetails.Add(new APIMessage(fieldExpression, detail));
        }

        public APIMessageException(string message, APIMessage detail) : base(message)
        {
            _messageDetails.Add(detail);
        }

        public APIMessageException(string message, IList<APIMessage> details) : base(message)
        {
            _messageDetails.AddRange(details);
        }

        #endregion Constructors

        #region Properties

        public virtual ReadOnlyCollection<APIMessage> Details { get { return _messageDetails.AsReadOnly(); } }

        #endregion Properties

        #region Methods

        public void Add(string field, string detail)
        {
            _messageDetails.Add(new APIMessage(field, detail));
        }

        #endregion Methods
    }
}
