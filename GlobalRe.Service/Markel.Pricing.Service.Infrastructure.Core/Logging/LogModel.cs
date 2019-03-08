namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public class LogModel
    {
        #region Public Properties

        public string ApplicationIdentifier { get; private set; }
        public string Message { get; private set; }
        public string Environment { get; private set; }
        public string UserName { get; private set; }
        public string WebServer { get; private set; }
        public string ClientBrowser { get; private set; }
        public string ClientIP { get; private set; }
        public string Url { get; private set; }
        public string UrlReferrer { get; private set; }
        public string Source { get; private set; }
        public string EventName { get; private set; }
        public string RequestObject { get; private set; }
        public string StackTrace { get; private set; }

        #endregion

        #region Constructors

        public LogModel(string applicationIdentifier, string message,
                        string clientBrowser, string clientIp, string webServer,
                        string url, string urlReferrer)
        {
            ApplicationIdentifier = applicationIdentifier;
            Message = message;

            ClientBrowser = clientBrowser;
            ClientIP = clientIp;
            WebServer = webServer;
            Url = url;
            UrlReferrer = urlReferrer;
        }

        public LogModel(string message)
        {
            Message = message;
        }

        #endregion Constructors
    }
}
