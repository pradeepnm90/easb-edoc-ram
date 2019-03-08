using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Markel.Pricing.Service.Infrastructure.Logging.Configuration
{
    public sealed class LoggingConfigurationSetting : ConfigurationElement
    {
        [ConfigurationProperty("mode", DefaultValue = "Synchronous", IsRequired = true)]
        public string Mode
        {
            get { return (string)base["mode"]; }
        }

        [ConfigurationProperty("sleepInterval", DefaultValue = 1000, IsRequired = true)]
        public int SleepInterval
        {
            get { return (int)base["sleepInterval"]; }
        }

        [ConfigurationProperty("maxQueueSize", DefaultValue = 100, IsRequired = true)]
        public int MaxQueueSize
        {
            get { return (int)base["maxQueueSize"]; }
        }

        [ConfigurationProperty("filePath", DefaultValue = @"c:\logs\", IsRequired = false)]
        public string filePath
        {
            get { var path = (string)base["filePath"]; return path.EndsWith(@"\") ? path : path + @"\"; }
        }

        [ConfigurationProperty("fileName", DefaultValue = @"Markel.Runtime.log", IsRequired = false)]
        public string fileName
        {
            get { return (string)base["fileName"]; }
        }
    }
}
