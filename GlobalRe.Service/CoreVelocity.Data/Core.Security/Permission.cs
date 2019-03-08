using System;
using System.Runtime.Serialization;

namespace CoreVelocity.Core.Security
{
    /// <summary>
    /// Object representation for an ERMS permission
    /// </summary>
    [Serializable]
    [DataContract]
    public class Permission
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the FactPK rom ERMS
        /// </summary>
        [DataMember]
        public int FactPK { get; set; }

        /// <summary>
        /// Gets or sets description of the ERMS Fact
        /// </summary>
        [DataMember]
        public string FactDescription { get; set; }

        /// <summary>
        /// Gets or sets value of the ERMS Fact
        /// </summary>
        [DataMember]
        public bool FactValue { get; set; }

        #endregion
    }

}
