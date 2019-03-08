using System;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public interface ILogManager
    {
        //LogManager GetLogManager();
        void LogMessage(LogModel log, LogLevel logLevel = LogLevel.Info, string requestObject = "", string eventName = "");
        void LogMessage(LogModel log, Exception ex, LogLevel logLevel = LogLevel.Error, string requestObject = "", string eventName = "");
        void LogMessage(Exception ex, LogLevel logLevel = LogLevel.Error, object requestObject = null);
        void LogMessage(string message, LogLevel logLevel = LogLevel.Info, object requestObject = null);
        void LogMessage(string message, Exception ex, LogLevel logLevel = LogLevel.Error);
    }
}
