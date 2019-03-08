namespace Markel.Pricing.Service.Infrastructure.Logging
{
    /// <summary>
    /// ILogController interface definition contract for all LogController implementations
    /// </summary>
    public interface ILogController
    {
        /// <summary>
        /// Adds a log item to be passed to confiigured loggers for logging
        /// </summary>
        /// <param name="item">LogItem containing data to log</param>
        void Log(ILogItem item);
    }
}
