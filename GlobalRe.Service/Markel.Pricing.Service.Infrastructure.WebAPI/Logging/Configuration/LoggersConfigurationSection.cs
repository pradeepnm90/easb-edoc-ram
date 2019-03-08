using System;
using System.Linq;
using System.Configuration;
using Markel.Pricing.Service.Infrastructure.Config;

namespace Markel.Pricing.Service.Infrastructure.Logging.Configuration
{
    public class LoggersConfigurationSection : ConfigurationSection
    {
        internal const string SectionName = "Loggers";

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public LoggerSettingCollection LoggerConfigurationSettings
        {
            get { return (LoggerSettingCollection)this[""]; }
            set { this[""] = value; }
        }

        public static LoggersConfigurationSection GetCurrent()
        {
            return (LoggersConfigurationSection)ConfigurationManager.GetSection(MarkelConfiguration.MarkelSectionName(SectionName));
        }
    }
}
