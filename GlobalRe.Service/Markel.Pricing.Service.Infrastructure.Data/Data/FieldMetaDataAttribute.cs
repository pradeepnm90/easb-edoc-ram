using System;
using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Provides an attribute that lets you specify which columns in a 
    /// database table the property or field is mapped to. The parent class
    /// should always have a <see cref="T:CoreVelocity.Core.Data.TableMappingAttribute"/>
    /// decorated on it for which 'Table' or 'View' to map to in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FieldMetaDataAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the source column name within the database to map 
        /// the property to.
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// Gets or sets the parameter name to use when generating the parameters if 
        /// a specified parameter name is required, otherwise the paramter name will be 
        /// auto-generated based on the column name unless specified in the <see cref="T:CoreVelocity.DataAccess.ICriteria"/>
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets whether the column is read only. This will
        /// prevent auto-generated updates from mapping this field
        /// back to the database when performing an 'Update.'
        /// </summary>
        [DefaultValue(false)]
        public bool IsReadOnly { get; set; }

        #endregion
    }
}
