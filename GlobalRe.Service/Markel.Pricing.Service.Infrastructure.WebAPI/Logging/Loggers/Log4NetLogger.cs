using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace Markel.Pricing.Service.Infrastructure.Logging.Loggers
{
    /// <summary>
    /// Wraps log4net logging functionality to CoreVelocity standard interface ILogger
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        #region Private Members

        private ILog logger = null;
        private bool isInitalized = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Default parameterless constructor
        /// Initializes Log4NetLogger
        /// </summary>
        public Log4NetLogger() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the logger name
        /// </summary>
        public string LoggerName { get { return GetType().Name; } }

        /// <summary>
        /// Logger specific configuration parameters
        /// </summary>
        public string LoggerParameters { get; set; }

        /// <summary>
        /// Logger execution priority, logger with higher priority is tried first
        /// </summary>
        public int LoggerPriority { get; set; }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Checks if logging is enabled for the loglevel
        /// </summary>
        internal bool IsLogLevelEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return logger.IsDebugEnabled;
                case LogLevel.Error:
                    return logger.IsErrorEnabled;
                case LogLevel.Fatal:
                    return logger.IsFatalEnabled;
                case LogLevel.Info:
                    return logger.IsInfoEnabled;
                case LogLevel.Warn:
                    return logger.IsWarnEnabled;
                default:
                    return true;
            }
        }

        #endregion

        #region ILogger Implementation

        /// <summary>
        /// Initializes the Log4Net Logger
        /// </summary>
        public void Initialize(string connectionString)
        {
            if (!String.IsNullOrEmpty(LoggerParameters))
            {
                string filePath = HostingEnvironment.MapPath(LoggerParameters);
                if (string.IsNullOrEmpty(filePath))
                    filePath = LoggerParameters;
                FileInfo configFileInfo = new FileInfo(filePath);
                XmlConfigurator.Configure(configFileInfo); // uses external config
            }
            else
            {
                XmlConfigurator.Configure(); // uses app.config
            }

            logger = log4net.LogManager.GetLogger(LoggerName);
            if (logger != null)
            {
                // Set AdoNetAppender ConnectionStrings
                var hier = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
                if (hier != null)
                {
                    var appenders = hier.GetAppenders().OfType<log4net.Appender.AdoNetAppender>();
                    foreach (var appender in appenders)
                    {
                        appender.ConnectionString = connectionString;
                        appender.ActivateOptions();
                    }
                }

                isInitalized = true;
            }
        }

        /// <summary>
        /// Logs the given message. Output depends on the associated
        /// log4net configuration.
        /// </summary>
        /// <param name="item">A <see cref="ILogItem"/> which encapsulates
        /// information to be logged.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/>
        /// is a null reference.</exception>
        public void Log(ILogItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (!isInitalized)
            {
                Initialize((item.UserIdentity != null)? item.UserIdentity.GetConnectionString():string.Empty);
            }

            item.LoggerName = this.LoggerName;

            switch (item.LogLevel)
            {
                case LogLevel.Fatal:
                    if (IsLogLevelEnabled(item.LogLevel))
                    {
                        logger.Fatal(item.Message, item.Exception);
                    }
                    break;

                case LogLevel.Error:
                    if (IsLogLevelEnabled(item.LogLevel))
                    {
                        logger.Error(item.Message, item.Exception);
                    }
                    break;

                case LogLevel.Warn:
                    if (IsLogLevelEnabled(item.LogLevel))
                    {
                        logger.Warn(item.Message, item.Exception);
                    }
                    break;

                case LogLevel.Info:
                    if (IsLogLevelEnabled(item.LogLevel))
                    {
                        logger.Info(item.Message, item.Exception);
                    }
                    break;

                case LogLevel.Debug:
                    if (IsLogLevelEnabled(item.LogLevel))
                    {
                        logger.Debug(item.Message, item.Exception);
                    }
                    break;

                default:
                    logger.Info(item.Message, item.Exception);
                    break;
            }
        }

        #endregion
    }
}