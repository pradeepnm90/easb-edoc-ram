using System;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    public abstract class APIException : Exception
    {
        #region Constructors

        public APIException() : base("Error") { }

        public APIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public abstract int HttpStatusCode { get; }

        #endregion Methods
    }
}
