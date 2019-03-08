using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Markel.Pricing.Service.Infrastructure.Config;

namespace Markel.Pricing.Service.Infrastructure.Logging.Configuration
{
    public sealed class LoggingConfigurationSection : ConfigurationSection
    {
        internal const string SectionName = "Logging";

        [ConfigurationProperty("loggingSettings", IsRequired = false)]
        public LoggingConfigurationSetting LoggingConfigurationSetting
        {
            get { return base["loggingSettings"] as LoggingConfigurationSetting; }
        }

        public static LoggingConfigurationSection GetCurrent()
        {
            return (LoggingConfigurationSection)ConfigurationManager.GetSection(MarkelConfiguration.MarkelSectionName(SectionName));
        }
    }
}
