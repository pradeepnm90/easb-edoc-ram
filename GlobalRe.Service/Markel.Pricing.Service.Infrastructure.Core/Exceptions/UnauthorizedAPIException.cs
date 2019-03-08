namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The request has not been applied because it lacks valid authentication credentials for the target resource.
    /// </summary>
    public class UnauthorizedAPIException : APIException
    {
        #region Constructors

        public UnauthorizedAPIException() : base() { }
        public UnauthorizedAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 401; } }

        #endregion Methods
    }
}
