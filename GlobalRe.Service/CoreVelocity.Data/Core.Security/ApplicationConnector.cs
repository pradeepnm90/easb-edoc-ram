using System;

namespace CoreVelocity.Core.Security
{
    /// <summary>
    /// Provides a hook for applications to pass on multiple connectionstrings for multiple databases.
    /// </summary>
    [Serializable]
    public class ApplicationConnector
    {

        #region Constructor

        /// <summary>
        /// Default parameterless constructor sets IsServersideOnly to true by default
        /// </summary>
        public ApplicationConnector()
        {
            this.IsServersideOnly = true;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the ConnectionKey required for caching the connectionstring.
        /// </summary>
        public string ConnectionKey { get; set; }

        /// <summary>
        /// Gets or sets Connectionstring corresponding to the ConnectionKey property.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Boolean to indicate if the connectionstring will be used only on server side
        /// This will require any code to not expose connectionstrings to client code when true
        /// </summary>
        public bool IsServersideOnly { get; set; }

        #endregion

    }
}
