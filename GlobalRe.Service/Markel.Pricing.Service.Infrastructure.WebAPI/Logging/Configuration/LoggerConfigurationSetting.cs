using System;
using System.Linq;
using System.Configuration;

namespace Markel.Pricing.Service.Infrastructure.Logging.Configuration
{
    public sealed class LoggerConfigurationSetting : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, DefaultValue = "", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("typename", DefaultValue = "", IsRequired = true)]
        public string TypeName
        {
            get { return (string)base["typename"]; }
        }

        [ConfigurationProperty("parameters", DefaultValue = "", IsRequired = true)]
        public string Parameters
        {
            get { return (string)base["parameters"]; }
        }

        [ConfigurationProperty("priorityorder", DefaultValue = "", IsRequired = true)]
        public string PriorityOrder
        {
            get { return (string)base["priorityorder"]; }
        }
    }
}
