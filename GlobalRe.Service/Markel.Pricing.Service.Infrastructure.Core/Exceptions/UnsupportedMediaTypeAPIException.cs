namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// UNSUPPORTED MEDIA TYPE
    /// The origin server is refusing to service the request because the payload is in a format not supported by this method on the target resource.
    /// </summary>
    public class UnsupportedMediaTypeAPIException : APIException
    {
        #region Constructors

        public UnsupportedMediaTypeAPIException() : base() { }
        public UnsupportedMediaTypeAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 415; } }

        #endregion Methods
    }
}
