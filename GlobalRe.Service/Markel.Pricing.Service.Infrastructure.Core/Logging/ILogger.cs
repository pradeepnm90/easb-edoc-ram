namespace Markel.Pricing.Service.Infrastructure.Logging
{
    /// <summary>
    /// Interface that maps all CoreVelocity loggers
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets this logger's name
        /// </summary>
        string LoggerName { get; }

        /// <summary>
        /// Logger specific configuration parameters
        /// </summary>
        string LoggerParameters { get; set; }

        /// <summary>
        /// Gets or sets the Logger priority for order of target execution
        /// </summary>
        int LoggerPriority { get; set; }

        /// <summary>
        /// Initialization routine for each logger
        /// </summary>
        void Initialize(string connectionString);

        /// <summary>
        /// Creates a new log entry based on a given log item.
        /// </summary>
        /// <param name="item">Encaspulates logging information.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/>
        /// is a null reference.</exception>
        void Log(ILogItem item);
    }
}
