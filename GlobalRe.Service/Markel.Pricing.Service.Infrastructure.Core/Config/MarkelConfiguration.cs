using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Markel.Pricing.Service.Infrastructure.Config
{
    public class MarkelConfiguration
    {
        #region Constants

        private static readonly string ACCESS_TOKEN_LIFETIME_KEY = "AccessTokenLifetime";
        private static readonly string API_EXCEPTION_LOG_FILTER_KEY = "ApiExceptionLogFilter";
        private static readonly string APPLICATION_NAME_KEY = "AppName";
        private static readonly string AUTO_PROCESS_CRON_INTERVAL_KEY = "AutoProcessCronIntervalMinutes";
        private static readonly string AUTO_PROCESS_POLL_INTERVAL_KEY = "AutoProcessPollingIntervalSeconds";
        private static readonly string AUTO_PROCESS_RETRY_COUNT_KEY = "AutoProcessRetryCount";
        private static readonly string AUTO_PROCESS_WORKER_COUNT_KEY = "AutoProcessWorkerCount";
        private static readonly string COUNTRY_SORT_ORDER_KEY = "CountrySortOrder";
        private static readonly string DB_COMMAND_TIMEOUT_KEY = "DBCommandTimeout";
        private static readonly string DEBUG_FLAG_KEY = "Debug";
        private static readonly string DOMAIN_ENVIRONMENT_SETTING = "USERDNSDOMAIN";
        private static readonly string DOMAIN_MAPPINGS_KEY = "DomainMappings";
        private static readonly string DOMAIN_NAME_KEY = "DomainName";
        private static readonly string ENVIRONMENT_NAME_KEY = "EnvironmentName";
        private static readonly string LOAD_MODULE_CONFIG_FILES_KEY = "LoadModuleConfigFiles";
        private static readonly string LOCK_LEASE_TIMEOUT_SECONDS_KEY = "LockLeaseTimeoutSeconds";
        private static readonly string MARKEL_GROUP_NAME_KEY = "MarkelGroupName";
        private static readonly string METADATA_PATH_KEY = "MetadataPath";
        private static readonly string MODULE_INCLUDE_FILTER_KEY = "ModuleIncludeFilter";
        private static readonly string MODULE_TYPE_FILTER_KEY = "ModuleTypeFilter";
        private static readonly string MODULE_UNITY_CONFIG_EXTENSION_KEY = "ModuleUnityConfigExtension";
        private static readonly string PRE_CACHE_PRICING_MODEL_NAMES_KEY = "PreCachePricingModelNames";
        private static readonly string REGISTER_TYPE_FILTER_KEY = "RegisterTypeFilter";
        private static readonly string SERVICE_ACCOUNT_KEY = "ServiceAccount";
        private static readonly string SUPPORTED_PRICING_MODEL_NAMES_KEY = "SupportedPricingModelNames";
        private static readonly string TEMP_PATH_KEY = "TempFilePath";
        private static readonly string TRUSTED_SERVERS_KEY = "TrustedServers";
        private static readonly string UNIT_OF_WORK_TIMEOUT_MINUTES_KEY = "UnitOfWorkTimeoutMinutes";
        private static readonly string UNITY_CONFIG_SECTION_NAME_KEY = "UnityConfigSectionName";
        private static readonly string UNITY_CONTAINER_NAME_KEY = "UnityContainerName";
        private static readonly string UW_SERVICE_ENVRONMENT_NAME_KEY = "UnderwritingServiceEnvironmentName";


        private static readonly string DEFAULT_CONTAINER_NAME = "default";
        private static readonly string DEFAULT_METADATA_PATH = "Metadata";
        private static readonly string DEFAULT_MODULE_FILTER = "^*.dll$";
        private static readonly string DEFAULT_MODULE_UNITY_CONFIG_EXTENSION = "unity.config";
        private static readonly string DEFAULT_UNITY_CONFIG_SECTION_NAME = "unity";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Access Token Lifetime (timeout)
        /// String Format: D.HH:MM:SS.MS
        /// </summary>
        public static TimeSpan AccessTokenLifetime
        {
            get
            {
                string accessTokenLifetime = ConfigurationManager.AppSettings[ACCESS_TOKEN_LIFETIME_KEY];
                TimeSpan lifetime;
                if (!TimeSpan.TryParse(accessTokenLifetime, out lifetime))
                {
                    // Default to one day
                    lifetime = TimeSpan.FromDays(1);
                }
                return lifetime;
            }
        }

        public static string ApiExceptionLogFilter
        {
            get
            {
                string logNotFoundExceptionsValue = ConfigurationManager.AppSettings[API_EXCEPTION_LOG_FILTER_KEY];
                if (string.IsNullOrEmpty(logNotFoundExceptionsValue))
                    return null;

                return logNotFoundExceptionsValue;
            }
        }

        /// <summary>
        /// Returns the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public static string ApplicationName
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get(APPLICATION_NAME_KEY);
                if (string.IsNullOrEmpty(name)) throw new Exception($"Application Config is missing required Key '{APPLICATION_NAME_KEY}'!");
                return name;
            }
        }

        public static int AutoProcessCronIntervalMinutes
        {
            get
            {
                int cronInterval = 0;
                string cronIntervalValue = ConfigurationManager.AppSettings[AUTO_PROCESS_CRON_INTERVAL_KEY];
                if (int.TryParse(cronIntervalValue, out cronInterval))
                    return cronInterval;

                return 1; // Default Value
            }
        }

        public static int AutoProcessPollingIntervalSeconds
        {
            get
            {
                int pollInterval = 0;
                string pollIntervalValue = ConfigurationManager.AppSettings[AUTO_PROCESS_POLL_INTERVAL_KEY];
                if (int.TryParse(pollIntervalValue, out pollInterval))
                    return pollInterval;

                return 15; // Default Value
            }
        }

        public static int AutoProcessRetryCount
        {
            get
            {
                int serviceRetryCount = 0;
                string serviceRetryCountValue = ConfigurationManager.AppSettings[AUTO_PROCESS_RETRY_COUNT_KEY];
                if (int.TryParse(serviceRetryCountValue, out serviceRetryCount))
                    return serviceRetryCount;

                return 0; // Default Value
            }
        }

        public static int AutoProcessWorkerCount
        {
            get
            {
                int workerCount = 0;
                string workerCountValue = ConfigurationManager.AppSettings[AUTO_PROCESS_WORKER_COUNT_KEY];
                if (int.TryParse(workerCountValue, out workerCount))
                    return workerCount;

                return 20; // Default Value
            }
        }

        public static string[] CountrySortOrder
        {
            get
            {
                string countrySortOrder = ConfigurationManager.AppSettings[COUNTRY_SORT_ORDER_KEY]; ;
                if (!string.IsNullOrEmpty(countrySortOrder))
                {
                    return countrySortOrder.Split(';');
                }

                // Default
                return new string[] { "United States", "Canada" };
            }
        }

        public static int? DBCommandTimeout
        {
            get
            {
                int dbCommandTimeout;
                if (int.TryParse(ConfigurationManager.AppSettings[DB_COMMAND_TIMEOUT_KEY], out dbCommandTimeout))
                {
                    return dbCommandTimeout;
                }

                return null;
            }
        }

        public static bool IsDebugMode
        {
            get
            {
                bool isDebug = false;
                string debugFlag = ConfigurationManager.AppSettings.Get(DEBUG_FLAG_KEY);
                if (!string.IsNullOrEmpty(debugFlag))
                {
                    bool.TryParse(debugFlag, out isDebug);
                }
                return isDebug;
            }
        }

        /// <summary>
        /// Returns the configured domain mappings.
        /// </summary>
        /// <value>
        /// The domain mappings.
        /// </value>
        public static Dictionary<string, string> DomainMappings
        {
            get
            {
                string mappings = ConfigurationManager.AppSettings.Get(DOMAIN_MAPPINGS_KEY);
                return (string.IsNullOrEmpty(mappings)) ? new Dictionary<string, string>() : mappings.SplitToDictionary();
            }
        }

        /// <summary>
        /// Gets the name of the domain.
        /// </summary>
        /// <value>
        /// The name of the domain.
        /// </value>
        public static string DomainName
        {
            get
            {
                string domainName = ConfigurationManager.AppSettings.Get(DOMAIN_NAME_KEY);
                return (string.IsNullOrEmpty(domainName)) ? Environment.GetEnvironmentVariable(DOMAIN_ENVIRONMENT_SETTING) : domainName;
            }
        }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <value>
        /// The name of the environment.
        /// </value>
        public static string EnvironmentName
        {
            get
            {
                string defaultEnvironmentName = ConfigurationManager.AppSettings.Get(ENVIRONMENT_NAME_KEY);
                if (string.IsNullOrEmpty(defaultEnvironmentName))
                    throw new Exception(string.Format("{0} Key is not set in web.config!", ENVIRONMENT_NAME_KEY));

                return defaultEnvironmentName;
            }
        }

        /// <summary>
        /// Return true if loading of unity module configuration files are configured; otherwise, false.
        /// </summary>
        public static bool LoadModuleConfigFiles
        {
            get
            {
                bool loadFiles = false;
                string name = ConfigurationManager.AppSettings.Get(LOAD_MODULE_CONFIG_FILES_KEY);
                if (!string.IsNullOrEmpty(name))
                {
                    bool.TryParse(name, out loadFiles);
                }
                return loadFiles;
            }
        }

        public static int LockLeaseTimeoutSeconds
        {
            get
            {
                int seconds = 0;
                string lockLeaseTimeoutSeconds = ConfigurationManager.AppSettings[LOCK_LEASE_TIMEOUT_SECONDS_KEY];
                if (int.TryParse(lockLeaseTimeoutSeconds, out seconds))
                    return seconds;

                return 1200; // Default Value
            }
        }

        /// <summary>
        /// Gets formatting string for markel configuration groups.
        /// </summary>
        /// <value>
        /// The markel group format string.
        /// </value>
        public static string MarkelGroupFormatString
        {
            get
            {
                string markelGroupName = ConfigurationManager.AppSettings.Get(MARKEL_GROUP_NAME_KEY);
                markelGroupName = string.IsNullOrEmpty(markelGroupName) ? "markel" : markelGroupName;

                return markelGroupName + "/{0}";
            }
        }

        /// <summary>
        /// Return the metadata path if configured; otherwise, default path <see cref="MarkelConfiguration.DEFAULT_METADATA_PATH"/>.
        /// </summary>
        public static string MetadataPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings.Get(METADATA_PATH_KEY);
                return (string.IsNullOrEmpty(path)) ? DEFAULT_METADATA_PATH : path;
            }
        }

        /// <summary>
        /// Return module filter if configured; otherwise, default filter of "^*.dll".
        /// </summary>
        public static string ModuleFilter
        {
            get
            {
                string filter = ConfigurationManager.AppSettings.Get(MODULE_INCLUDE_FILTER_KEY);
                return (string.IsNullOrEmpty(filter)) ? DEFAULT_MODULE_FILTER : filter;
            }
        }

        /// <summary>
        /// Return type filter if configured; otherwise, null.
        /// </summary>
        public static string TypeFilter
        {
            get
            {
                string filter = ConfigurationManager.AppSettings.Get(MODULE_TYPE_FILTER_KEY);
                return (string.IsNullOrEmpty(filter)) ? null : filter;
            }
        }

        /// <summary>
        /// Return unity module configuration file extension if configured; otherwise, <see cref="MarkelConfiguration.DEFAULT_MODULE_UNITY_CONFIG_EXTENSION"/>.
        /// </summary>
        public static string ModuleUnityConfigExtension
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get(MODULE_UNITY_CONFIG_EXTENSION_KEY);
                return (string.IsNullOrEmpty(name)) ? DEFAULT_MODULE_UNITY_CONFIG_EXTENSION : name;
            }
        }

        public static string[] PreCachePricingModelNames
        {
            get
            {
                string preCachePricingModelNames = ConfigurationManager.AppSettings[PRE_CACHE_PRICING_MODEL_NAMES_KEY];
                if (string.IsNullOrEmpty(preCachePricingModelNames))
                    return new string[0];

                return preCachePricingModelNames.Split('|');
            }
        }

        /// <summary>
        /// Return register type filter if configured; otherwise, null.
        /// </summary>
        public static string RegisterTypeFilter
        {
            get
            {
                string filter = ConfigurationManager.AppSettings.Get(REGISTER_TYPE_FILTER_KEY);
                return (string.IsNullOrEmpty(filter)) ? null : filter;
            }
        }

        /// <summary>
        /// Gets Pricing Service Account.
        /// </summary>
        /// <value>
        /// Pricing Service Account.
        /// </value>
        public static string ServiceAccountName
        {
            get
            {
                string serviceAccountName = ConfigurationManager.AppSettings[SERVICE_ACCOUNT_KEY];
                if (string.IsNullOrEmpty(serviceAccountName) || !serviceAccountName.Contains("@"))
                    throw new NullReferenceException(string.Format("Missing or invalid appSettings key '{0}'", SERVICE_ACCOUNT_KEY));

                return serviceAccountName;
            }
        }

        public static string[] SupportedPricingModelNames
        {
            get
            {
                string supportedPricingModelNames = ConfigurationManager.AppSettings[SUPPORTED_PRICING_MODEL_NAMES_KEY];
                if (string.IsNullOrEmpty(supportedPricingModelNames))
                    throw new NullReferenceException(string.Format("Missing or invalid appSettings key '{0}'", SUPPORTED_PRICING_MODEL_NAMES_KEY));

                return supportedPricingModelNames.Split('|');
            }
        }

        /// <summary>
        /// Return the temp file path if configured; otherwise, default system temp path.
        /// </summary>
        public static string TempFilePath
        {
            get
            {
                string tempFilePath = ConfigurationManager.AppSettings.Get(TEMP_PATH_KEY);
                if (!string.IsNullOrEmpty(tempFilePath) && !Directory.Exists(tempFilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(tempFilePath);
                    }
                    catch
                    {
                        // Failed to create temp directory
                        tempFilePath = null;
                    }
                }
                return (string.IsNullOrEmpty(tempFilePath)) ? Path.GetTempPath() : tempFilePath;
            }
        }

        /// <summary>
        /// List of Trusted Servers.
        /// </summary>
        /// <value>
        /// Regular Expression of Trusted Server Names.
        /// </value>
        public static string TrustedServers
        {
            get
            {
                string trustedServers = ConfigurationManager.AppSettings.Get(TRUSTED_SERVERS_KEY);
                if (string.IsNullOrEmpty(trustedServers))
                    return $"^{Environment.MachineName}$";

                return trustedServers;
            }
        }

        public static TimeSpan UnitOfWorkTimeout
        {
            get
            {
                int unitOfWorkTimeoutMinutes = 15;
                string unitOfWorkTimeoutMinutesValue = ConfigurationManager.AppSettings.Get(UNIT_OF_WORK_TIMEOUT_MINUTES_KEY);
                if (!string.IsNullOrEmpty(unitOfWorkTimeoutMinutesValue))
                {
                    int.TryParse(unitOfWorkTimeoutMinutesValue, out unitOfWorkTimeoutMinutes);
                }

                return new TimeSpan(0, unitOfWorkTimeoutMinutes, 0);
            }
        }

        /// <summary>
        /// Return unity configuration section name if configured; otherwise, <see cref="MarkelConfiguration.DEFAULT_UNITY_CONFIG_SECTION_NAME"/>.
        /// </summary>
        public static string UnityConfigSectionName
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get(UNITY_CONFIG_SECTION_NAME_KEY);
                return (string.IsNullOrEmpty(name)) ? DEFAULT_UNITY_CONFIG_SECTION_NAME : name;
            }
        }

        /// <summary>
        /// Return unity container name if configured; otherwise, <see cref="DEFAULT_CONTAINER_NAME"/>.
        /// </summary>
        public static string UnityContainerName
        {
            get
            {
                string name = ConfigurationManager.AppSettings.Get(UNITY_CONTAINER_NAME_KEY);
                return (string.IsNullOrEmpty(name)) ? DEFAULT_CONTAINER_NAME : name;
            }
        }

        public static string UnderwritingServiceEnvironmentName
        {
            get
            {
                string uwServiceEnvironmentName = ConfigurationManager.AppSettings[UW_SERVICE_ENVRONMENT_NAME_KEY];
                if (string.IsNullOrEmpty(uwServiceEnvironmentName))
                    throw new NullReferenceException(string.Format("Missing or invalid appSettings key '{0}'", UW_SERVICE_ENVRONMENT_NAME_KEY));

                return uwServiceEnvironmentName;
            }
        }

        #endregion Properties

        #region Additional Properties

        /// <summary>
        /// HangFire Default Queue Name is controlled by the App Setting Debug Flag:
        /// * True: Run on current server and Application Version
        /// * False: Run on all servers for the configured environment
        /// Reference: http://docs.hangfire.io/en/latest/background-processing/configuring-queues.html
        /// The Queue name argument must consist of lowercase letters, digits and underscore characters only.
        /// </summary>
        public static string HangfireQueueName
        {
            get
            {
                string stateName = EnvironmentName;

                if (IsDebugMode)
                {
                    Version version = IOHelper.GetAssemblyVersion();
                    string serverVersion = version.ToString();

                    if (serverVersion.Equals("1.0.0.0")) serverVersion = "[DEVELOPMENT]";
                    else if (version.Major == 0 && version.Minor == 0) serverVersion = "[DEVELOPMENT]";

                    stateName = $"{stateName}_{Environment.MachineName.Replace('-', '_')}_{serverVersion.Replace('.', '_')}";
                }

                return stateName.RemoveSpecialCharacters('_').ToLower();
            }
        }

        #endregion Additional Properties

        #region Public Methods

        public static DateValueConfiguration GetDateValueConfiguration(string key)
        {
            string dateValueSettings = ConfigurationManager.AppSettings[key];
            return JsonConvert.DeserializeObject<DateValueConfiguration>(dateValueSettings);
        }

        /// <summary>
        /// Return the fully qualified section name within Markels configuration group.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        public static string MarkelSectionName(string sectionName)
        {
            return string.Format(MarkelGroupFormatString, sectionName);
        }

        #endregion Public Methods
    }
}