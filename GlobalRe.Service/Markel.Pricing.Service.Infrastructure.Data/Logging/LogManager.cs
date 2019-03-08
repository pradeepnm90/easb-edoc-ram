using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging.Entities;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Globalization;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class LogManager : ILogManager
    {
        #region Private Members

        private ILogController LogController { get; set; }
        private IUserManager UserManager { get; set; }
        private UserIdentity UserIdentity
        {
            get
            {
                return UserManager?.UserIdentity;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prevents a default instance of the <see cref="LogManager"/> class from being created.
        /// </summary>
        public LogManager(IUserManager userManager, ILogController logController)
        {
            UserManager = userManager;
            LogController = logController;
        }

        /// <summary>
        /// Logs the message and exception.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="ex">The exception</param>
        /// <param name="logLevel">The log level.</param>
        public void LogMessage(Exception ex, LogLevel logLevel = LogLevel.Fatal, object requestObject = null)
        {
            LogMessage(new LogModel(ex.Message), ex, logLevel, requestObject?.ConvertToXml());
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="requestObject"></param>
        /// <param name="logLevel">The log level.</param>
        public void LogMessage(string message, LogLevel logLevel = LogLevel.Info, object requestObject = null)
        {
            LogMessage(new LogModel(message), null, logLevel, requestObject?.ConvertToXml());
        }

        /// <summary>
        /// Logs the message long with an exception object.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception.</param>
        /// <param name="logLevel">The log level.</param>
        public void LogMessage(string message, Exception ex, LogLevel logLevel = LogLevel.Fatal)
        {
            LogMessage(new LogModel(message), ex, logLevel);
        }

        public void LogMessage(LogModel log, LogLevel logLevel = LogLevel.Info, string requestObject = "", string eventName = "")
        {
            LogMessage(log, null, logLevel, requestObject, eventName);
        }

        public void LogMessage(LogModel log, Exception ex, LogLevel logLevel = LogLevel.Error, string requestObject = "", string eventName = "")
        {
            try
            {
                // Adjust user and application information (if needed)
                var userName = log.UserName.GetValueOrDefault((UserIdentity != null) ? UserIdentity.UserName : string.Empty);
                var evironment = log.Environment.GetValueOrDefault((UserIdentity != null) ? UserIdentity.EnvironmentName : "NA");
                var applicationIdentifier = log.ApplicationIdentifier.GetValueOrDefault((UserIdentity != null) ? UserIdentity.ApplicationName : null);
                var stackTrace = log.StackTrace.GetValueOrDefault((ex != null) ? ex.StackTrace : string.Empty);

                log4net.LogicalThreadContext.Properties["User"] = userName;
                log4net.LogicalThreadContext.Properties["Environment"] = evironment;
                log4net.LogicalThreadContext.Properties["UrlReferrer"] = log.UrlReferrer.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["ClientBrowser"] = log.ClientBrowser.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["ClientIP"] = log.ClientIP.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["URL"] = log.Url.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["ApplicationIdentifier"] = applicationIdentifier;
                log4net.LogicalThreadContext.Properties["Source"] = log.Source.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["RequestObject"] = log.RequestObject.GetValueOrDefault(requestObject);
                log4net.LogicalThreadContext.Properties["EventName"] = log.EventName.GetValueOrDefault(string.Empty);
                log4net.LogicalThreadContext.Properties["StackTrace"] = stackTrace;

                string exceptionMessage = string.Empty;
                if (ex != null)
                {
                    switch (ex.GetType().FullName)
                    {
                        case "System.Data.Entity.Validation.DbEntityValidationException":
                            System.Data.Entity.Validation.DbEntityValidationException validation = ex as System.Data.Entity.Validation.DbEntityValidationException;
                            exceptionMessage = "ExceptionMessage-";
                            if (validation != null && validation.EntityValidationErrors != null)
                            {
                                foreach (var validationErrors in validation.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        exceptionMessage += string.Format(" Property: {0} Error: {1} ", validationError.PropertyName, validationError.ErrorMessage);
                                    }
                                }
                            }
                            break;
                    }
                }

                LogController.Log(new LogItem(logLevel, String.Format(CultureInfo.CurrentCulture, "{0} {1}", log.Message, exceptionMessage), ex, stackTrace) { UserIdentity = UserIdentity });
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}