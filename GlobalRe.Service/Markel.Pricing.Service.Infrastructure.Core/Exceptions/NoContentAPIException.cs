namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// The server successfully processed the request and is not returning any content.
    /// </summary>
    public class NoContentAPIException : APIMessageException
    {
        #region Constructors

        public NoContentAPIException() : base("No content! Bad request!") { }
        public NoContentAPIException(string message) : base(message) { }

        #endregion Constructors

        #region Methods

        public override int HttpStatusCode { get { return 422; } }

        #endregion Methods
    }
}
