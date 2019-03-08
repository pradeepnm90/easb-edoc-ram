using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Logging.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Markel.Pricing.Service.Infrastructure.Controllers
{
    /// <summary>
    /// Single Entry point to control CoreVelocity wide logging
    /// </summary>
    public class LogController : ILogController
    {
        #region Private Members

        private int loggerSequence = 0;
        private bool isInitialized = false;
        private static Task processQueueTask;
        private static ConcurrentQueue<ILogItem> _logItemsQueue = new ConcurrentQueue<ILogItem>();
        private static Dictionary<int, ILogger> _targetLoggers = new Dictionary<int, ILogger>();

        private ILogger LoggerInstance { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the LogController to target multiple configured loggers
        /// </summary>
        public LogController(ILogger loggerInstance)
        {
            LoggerInstance = loggerInstance;
        }

        #endregion

        #region Internal Members

        internal List<LoggerConfigurationSetting> LoggerSettings = new List<LoggerConfigurationSetting>();

        internal static Dictionary<int, ILogger> TargetLoggers
        {
            get { return _targetLoggers; }
            set { if (value != _targetLoggers) { _targetLoggers = value; } }
        }

        internal static ConcurrentQueue<ILogItem> LogItems
        {
            get { return _logItemsQueue; }
            set { if (value != _logItemsQueue) { _logItemsQueue = value; } }
        }

        #endregion

        #region Internal Properties

        internal int MaxQueueSize { get; set; }
        internal int SleepInterval { get; set; }
        internal string LoggingMode { get; set; }
        internal LoggersConfigurationSection CurrentLoggersConfiguration { get { return LoggersConfigurationSection.GetCurrent(); } }
        internal LoggingConfigurationSection CurrentLoggingConfiguration { get { return LoggingConfigurationSection.GetCurrent(); } }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Initializes target loggers in mode determined by configuration
        /// </summary>
        internal void Initialize()
        {
            // LogController Level settings
            LoggingConfigurationSetting loggingSetting = CurrentLoggingConfiguration.LoggingConfigurationSetting;
            if (loggingSetting != null)
            {
                this.LoggingMode = loggingSetting.Mode;
                this.SleepInterval = loggingSetting.SleepInterval;
                this.MaxQueueSize = loggingSetting.MaxQueueSize;
            }

            // Logger Level settings
            LoggerSettings = CurrentLoggersConfiguration.LoggerConfigurationSettings.Cast<LoggerConfigurationSetting>().ToList();

            try
            {
                // Setup Target loggers from configuration ordered by PriorityOrder
                foreach (LoggerConfigurationSetting configuredLogger in LoggerSettings.OrderBy(o => Convert.ToInt32(o.PriorityOrder)))
                {
                    loggerSequence++;
                    LoggerInstance.LoggerParameters = configuredLogger.Parameters;
                    LoggerInstance.LoggerPriority = Convert.ToInt32(configuredLogger.PriorityOrder);
                    TargetLoggers.Add(loggerSequence, LoggerInstance);
                }
            }
            catch (Exception)
            {
                // Do Nothing if logger setup fails
            }

            // If using Asynchronous processing, use another thread for Async Queue processing
            if (LoggingMode.Trim().ToUpperInvariant() == LogMode.Asynchronous.ToString().Trim().ToUpperInvariant())
            {
                processQueueTask = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        ProcessFromQueue();
                        Thread.Sleep(SleepInterval);
                    }
                }, new CancellationToken(), TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            isInitialized = true;
        }

        /// <summary>
        /// If configuration is set to Async, adds log items to the processing queue
        /// </summary>
        /// <param name="item">LogItem to log</param>
        internal void AddToQueue(ILogItem item)
        {
            try
            {
                if (LogItems.Count < MaxQueueSize)
                {
                    LogItems.Enqueue(item);
                }
            }
            catch (Exception)
            {
                // Do nothing if add to Queue failed
            }
        }

        /// <summary>
        /// Processes LogItems asyncronously from the queue
        /// </summary>
        internal void ProcessFromQueue()
        {
            ILogItem logItem = null;
            while (LogItems.TryDequeue(out logItem))
            {
                if (logItem != null)
                {
                    // Target loggers are already ordered by PriorityOrder
                    foreach (KeyValuePair<int, ILogger> keyValuePair in TargetLoggers.OrderBy(o => o.Key))
                    {
                        try
                        {
                            keyValuePair.Value.Log(logItem);
                            break; // If no exception stop targetting the remaining loggers
                        }
                        catch (Exception)
                        {
                            continue; // If exception occured in the higher priority logger, process next logger
                        }
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Passes the LogItem to all configured loggers for logging
        /// </summary>
        /// <param name="item">LogItem to log</param>
        [HttpPost]
        public void Log(ILogItem item)
        {
            try
            {
                if (!isInitialized)
                {
                    Initialize();
                }

                if (LoggingMode.Trim().ToUpperInvariant() == LogMode.Asynchronous.ToString().Trim().ToUpperInvariant())
                {
                    AddToQueue(item);
                }
                else
                {
                    foreach (KeyValuePair<int, ILogger> keyValuePair in TargetLoggers)
                    {
                        try
                        {
                            keyValuePair.Value.Log(item);
                            break; // If no exception stop targetting the remaining loggers
                        }
                        catch (Exception)
                        {
                            continue; // If exception occured in the higher priority logger, process next logger
                        }
                    }
                }
            }
            catch (Exception)
            {
                // The exception throw is suppressed so that applications dont get runtime errors 
                // if logging fails for any reason
            }
        }

        #endregion
    }
}