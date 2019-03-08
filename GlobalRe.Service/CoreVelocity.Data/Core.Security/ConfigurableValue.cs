using System;
using System.Runtime.Serialization;

namespace CoreVelocity.Core.Security
{
    /// <summary>
    /// Represents a configurable value within the ERMS system
    /// </summary>
    [Serializable]
    [DataContract]
    public class ConfigurableValue
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the configuration identifier in ERMS
        /// </summary>
        [DataMember]
        public int ConfigurationPK { get; set; }

        /// <summary>
        /// Gets or sets the configuration value for the identifier in ERMS
        /// </summary>
        [DataMember]
        public string ConfigurationValue { get; set; }

        /// <summary>
        /// Gets or sets the description of the configuration Fact - used as lookup key in ERMS
        /// </summary>
        [DataMember]
        public string ConfigurationKey { get; set; }

        #endregion
    }
}
