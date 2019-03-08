using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoreVelocity.Core.Security
{
    /// <summary>
    /// Represents Erms Permissions for the currently athenticated security token from ERMS
    /// </summary>
    [Serializable]
    [DataContract]
    public class PermissionSet
    {

        #region Constructors

        /// <summary>
        /// Default constructor for initializing PermissionSet with Permissions and ConfigurableValues
        /// </summary>
        public PermissionSet()
        {
            _permissions = new List<Permission>();
            _configurableValues = new List<ConfigurableValue>();

        }

        public PermissionSet(IList<Permission> permissions, IList<ConfigurableValue> configurableValues)
        {
            _permissions = new List<Permission>();
            _configurableValues = new List<ConfigurableValue>();

            if (permissions != null)
            {
                _permissions = permissions;
            }
            if (configurableValues != null)
            {
                _configurableValues = configurableValues;
            }
        }


        #endregion

        #region Private Members

        /// <summary>
        /// Backing field for Permissions
        /// </summary>
        private IList<Permission> _permissions;

        /// <summary>
        /// Backing field for Configurable values
        /// </summary>
        private IList<ConfigurableValue> _configurableValues;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets whether the permissions have been loaded or not.
        /// </summary>
        [DataMember]
        public bool ArePermissionsLoaded { get; private set; }

        /// <summary>
        /// Adds a permission to the Permissions list
        /// </summary>
        /// <param name="permissionFact">Permission name in ERMS</param>
        public void AddPermission(string permissionFact)
        {
            Permission permission = new Permission();
            permission.FactDescription = permissionFact;
            _permissions.Add(permission);
        }

        /// <summary>
        /// Adds a permission row as object
        /// </summary>
        /// <param name="permission">Permission object row to add to the list of permissions</param>
        public void AddPermission(Permission permission)
        {
            _permissions.Add(permission);
        }

        /// <summary>
        /// Adds a configurable value to the ConfigurableValues list
        /// </summary>
        /// <param name="configurationFact">Configuration fact in ERMS</param>
        public void AddConfigurationValue(string configurationFact)
        {
            ConfigurableValue configurableValue = new ConfigurableValue();
            configurableValue.ConfigurationKey = configurationFact;
            _configurableValues.Add(configurableValue);
        }

        /// <summary>
        /// Adds a configurable value to the ConfigurableValues list as object
        /// </summary>
        /// <param name="configurableValue">Configuration fact in ERMS</param>
        public void AddConfigurationValue(ConfigurableValue configurableValue)
        {
            _configurableValues.Add(configurableValue);
        }

        /// <summary>
        /// List of Permissions for the authenticated user, applications to supply the list to load
        /// </summary>
        [DataMember]
        public IList<Permission> Permissions
        {
            get
            {
                return _permissions;
            }
            private set
            {
                _permissions = value;
            }
        }

        /// <summary>
        /// List of configurable values in ERMS, applications to supply the list to load
        /// </summary>
        [DataMember]
        public IList<ConfigurableValue> ConfigurableValues
        {
            get
            {
                return _configurableValues;
            }
            private set
            {
                _configurableValues = value;
            }
        }

        #endregion
    }
}
