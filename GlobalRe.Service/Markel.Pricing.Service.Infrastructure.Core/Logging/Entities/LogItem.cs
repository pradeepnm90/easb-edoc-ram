using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Logging.Entities
{
    [Serializable]
    [DataContract]
    public class LogItem : ILogItem
    {
        #region Constructors

        public LogItem()
        {
            Timestamp = DateTimeOffset.Now;
            ExtensionElements = new Dictionary<string, object>();
        }

        public LogItem(LogLevel logLevel, string message, Exception exception = null, string stackTrace = null)
            : this()
        {
            LogLevel = logLevel;
            Message = message;
            Exception = exception;
            StackTrace = stackTrace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The logging level, which defaults to <see cref="Markel.Pricing.Service.Infrastructure.Logging.Enums.LogLevel.Info"/>.
        /// </summary>
        [DataMember]
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Date and time of the log entry. If no explicitly
        /// set, this property provides the timestamp of
        /// the object's creation.
        /// </summary>
        [DataMember]
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// A summarizing title for the logged entry. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// The logged message body. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// The name of the logger used to log this item.
        /// </summary>
        [DataMember]
        public string LoggerName { get; set; }

        /// <summary>
        /// Allows to attach an exception to the message.
        /// Defaults to <c>null</c>.
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public Exception Exception { get; set; }

        /// <summary>
        /// Event number or identifier. Defaults to null.
        /// </summary>
        [DataMember]
        public int? EventId { get; set; }

        /// <summary>
        /// Additional properties that extend the functionality of what can be logged
        /// for use by custom loggers
        /// </summary>
        [DataMember]
        public UserIdentity UserIdentity { get; set; }

        /// <summary>
        /// Key Value based Dictionary object to pass on arbitrary values to Loggers
        /// </summary>
        [DataMember]
        public Dictionary<string, object> ExtensionElements { get; set; }

        /// <summary>
        /// Exception stack trace 
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        #endregion
    }
}