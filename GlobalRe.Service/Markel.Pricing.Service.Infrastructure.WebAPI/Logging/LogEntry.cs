using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>	Log entry. 
    /// Holds all the information which needs to be logged into the DB. Used by ActivityLogManager
    /// </summary>
    ///
    /// <remarks>	Ram.srinivasan, 2/23/2010. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    [DataContract]
    public class LogEntry 
    {
        private readonly string message;
        private readonly string hostName;
        private readonly string userAction;
        private readonly string originator;
        private readonly string sessionIdentifier;
        private readonly string userName;
        private readonly string clientIPAddress;
        private readonly string clientOperatingSystem;
        private readonly string clientBrowser;
        private readonly string screenSize;
        private readonly string dealNumber;
        private readonly string dealComponentID;
        private readonly Guid jobIdentifier;
        private readonly System.Exception exceptionToLog;
        private LogLevel logLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> struct.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="userAction">The user action.</param>
        /// <param name="originator">The originator.</param>
        /// <param name="sessionIdentifier">The session identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="clientIPAddress">The client IP address.</param>
        /// <param name="clientOperatingSystem">The client operating system.</param>
        /// <param name="clientBrowser">The client browser.</param>
        /// <param name="screenSize">Size of the screen.</param>
        /// <param name="exceptionToLog">The exception to log.</param>
        /// <param name="dealNumber">The deal number.</param>
        /// <param name="dealComponentID">The deal component ID.</param>
        /// <param name="jobIdentifier">The job identifier for OCR and Bing. </param>
        public LogEntry(string message, string hostName, string userAction, string originator, string sessionIdentifier, string userName, string clientIPAddress, string clientOperatingSystem, string clientBrowser, string screenSize, System.Exception exceptionToLog, string dealNumber, string dealComponentID, Guid jobIdentifier)
        {
            this.message = message;
            this.hostName = hostName;
            this.userAction = userAction;
            this.originator = originator;
            this.sessionIdentifier = sessionIdentifier;
            this.userName = userName;
            this.clientIPAddress = clientIPAddress;
            this.clientOperatingSystem = clientOperatingSystem;
            this.clientBrowser = clientBrowser;
            this.screenSize = screenSize;
            this.exceptionToLog = exceptionToLog;
            this.dealNumber = dealNumber;
            this.dealComponentID = dealComponentID;
            this.jobIdentifier = jobIdentifier;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        [DataMember]
        public string HostName
        {
            get { return hostName; }
        }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        [DataMember]
        public string UserAction
        {
            get { return userAction; }
        }

        /// <summary>
        /// Gets the originator.
        /// </summary>
        /// <value>The originator.</value>
        [DataMember]
        public string Originator
        {
            get { return originator; }
        }

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        [DataMember]
        public string SessionIdentifier
        {
            get { return sessionIdentifier; }
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember]
        public string UserName
        {
            get { return userName; }
        }

        /// <summary>
        /// Gets the client IP address.
        /// </summary>
        /// <value>The client IP address.</value>
        [DataMember]
        public string ClientIPAddress
        {
            get { return clientIPAddress; }
        }

        /// <summary>
        /// Gets the client operating system.
        /// </summary>
        /// <value>The client operating system.</value>
        [DataMember]
        public string ClientOperatingSystem
        {
            get { return clientOperatingSystem; }
        }

        /// <summary>
        /// Gets the client browser.
        /// </summary>
        /// <value>The client browser.</value>
        [DataMember]
        public string ClientBrowser
        {
            get { return clientBrowser; }
        }

        /// <summary>
        /// Gets the size of the screen.
        /// </summary>
        /// <value>The size of the screen.</value>
        [DataMember]
        public string ScreenSize
        {
            get { return screenSize; }
        }

        /// <summary>
        /// Gets the exception to log.
        /// </summary>
        /// <value>The exception to log.</value>
        [DataMember]
        public Exception ExceptionToLog
        {
            get { return this.exceptionToLog; }
        }

        /// <summary>
        /// Gets the deal component ID.
        /// </summary>
        /// <value>The deal component ID.</value>
        [DataMember]
        public string DealComponentID
        {
            get { return this.dealComponentID; }
        }

        /// <summary>
        /// Gets the deal number.
        /// </summary>
        /// <value>The deal number.</value>
        [DataMember]
        public string DealNumber
        {
            get { return this.dealNumber; }
        }

        /// <summary>
        /// Gets the log level.
        /// </summary>
        /// <value>The log level.</value>
        [DataMember]
        public LogLevel LogLevel
        {
            get { return logLevel; }
            set { logLevel = value; }
        }

        /// <summary>
        /// Gets the jobIdentifier.
        /// </summary>
        /// <value>The deal number.</value>
        [DataMember]
        public Guid JobIdentifier
        {
            get { return this.jobIdentifier; }
        }
                
    }
}
