namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The server is currently unable to handle the request due to a temporary overload
    /// or scheduled maintenance, which will likely be alleviated after some delay.
    /// </summary>
    public class ServiceUnavailableAPIException : APIException
    {
        #region Constructors

        public ServiceUnavailableAPIException() : base() { }
        public ServiceUnavailableAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 503; } }

        #endregion Methods
    }
}
