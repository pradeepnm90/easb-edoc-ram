namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The method could not be performed on the resource because the server is unable
    /// to store the representation needed to successfully complete the request.
    /// </summary>
    public class InsufficientStorageAPIException : APIException
    {
        #region Constructors

        public InsufficientStorageAPIException() : base() { }
        public InsufficientStorageAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 507; } }

        #endregion Methods
    }
}
