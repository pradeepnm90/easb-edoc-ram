namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The user has sent too many requests in a given amount of time ("rate limiting").
    /// Note: Responses with the 429 status code MUST NOT be stored by a cache.
    /// </summary>
    public class TooManyRequestsAPIException : APIException
    {
        #region Constructors

        public TooManyRequestsAPIException() : base() { }
        public TooManyRequestsAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 429; } }

        #endregion Methods
    }
}
