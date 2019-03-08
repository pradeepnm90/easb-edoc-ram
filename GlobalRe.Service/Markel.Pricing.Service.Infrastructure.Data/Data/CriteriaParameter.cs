using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a parameter for fetching data from a database.
    /// </summary>
    public sealed class CriteriaParameter : ICriteriaParameter
    {
        #region Constants

        private const string ParameterFormatString = "@{0}";
        private const int DefaultParameterLength = 8;

        #endregion
       
        #region Constructor
        /// <summary>
        /// Creates an instance of CriteriaParameter
        /// </summary>
        public CriteriaParameter() { }
        /// <summary>
        /// Creates an instance of CriteriaParameter
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sourceColumn">Column name</param>
        /// <param name="value">Parameter value.</param>
        public CriteriaParameter(string parameterName, string sourceColumn, object value)
        {
            ParameterName = parameterName;
            SourceColumn = sourceColumn;
            Value = value;
        }
     
        public CriteriaParameter(string parameterName, object value) : this(parameterName, value, null){}
        public CriteriaParameter(string parameterName, object value, string propertyName) : this(parameterName, value, propertyName, ParameterDirection.Input) { }
        public CriteriaParameter(string parameterName, object value, string propertyName, ParameterDirection direction) : this(parameterName, value, propertyName, direction, ComparisonType.Equals) { }
        public CriteriaParameter(string parameterName, object value, string propertyName, ParameterDirection direction, ComparisonType comparisonType) : this(parameterName, value, propertyName, direction, comparisonType, ConditionType.And) { }
        public CriteriaParameter(string parameterName, object value, string propertyName, ParameterDirection direction, ComparisonType comparisonType, ConditionType conditionType) : this(parameterName, value, propertyName, direction, comparisonType, conditionType, 0) { }
        public CriteriaParameter(string parameterName, object value, string propertyName, ParameterDirection direction, ComparisonType comparisonType, ConditionType conditionType, int groupIndex) : this(parameterName, value, propertyName, direction, comparisonType, conditionType, groupIndex, ConditionType.And) { }
        public CriteriaParameter(string parameterName, object value, string propertyName, ParameterDirection direction, ComparisonType comparisonType, ConditionType conditionType, int groupIndex, ConditionType groupConditionType)
        {

            PropertyName = propertyName;
            ParameterName = parameterName;
            Value = value;
            Direction = direction;
            ComparisonType = comparisonType;
            ConditionType = conditionType;
            GroupIndex = groupIndex;
            GroupConditionType = groupConditionType;
        }

        #endregion

        #region Private Members

        private string _parameterName;
        private string _propertyName;

        #endregion

        #region ICriteriaParameter Members

        /// <summary>
        /// Gets whether the parameter can be added to the IDbCommand's
        /// parameter collection. This allows a set of rules to be applied
        /// to the parameter based on types and values if you want to ignore
        /// adding specific types that may be handled internally.
        /// </summary>
        public bool CanAdd
        {
            get
            {
                if (Direction == ParameterDirection.Output || Direction == ParameterDirection.InputOutput || Direction == ParameterDirection.ReturnValue)
                {
                    return true;
                }

                bool canAdd;
                if (Value is string)
                {
                    canAdd = !string.IsNullOrEmpty(Value as string);
                }
                else
                {
                    canAdd = Value != null;
                }

                return (ComparisonType != ComparisonType.In) && canAdd;
            }
        }

        /// <summary>
        /// Gets whether or not to add the paramter to the query builder's WHERE clause. The getter
        /// contains rules that apply to the parameter that may exclude it from the WHERE clause.
        /// </summary>
        public bool CanAddToWhereClause
        {
            get
            {
                  bool canAdd = true;
                if(DbType.HasValue)
                {
                    canAdd &= DbType.Value != SqlDbType.Structured;
                }

                return canAdd && HasValue() && !IsUpdateOrInsertValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AddIfEmpty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUpdateOrInsertValue { get; set; }

        /// <summary>
        /// Gets or sets whether the parameter is a generated parameter or not.
        /// Generated parameters are removed after the query is executed, they are generated for
        /// special situations like "IN" clauses.
        /// </summary>
        public bool IsGenerated { get; set; }

        /// <summary>
        /// Gets or sets the condition type to use between the next group of 
        /// statements. Group 0's condition type has no effect on the where
        /// clause at all. Default condition type is 'ConditionType.And'
        /// </summary>
        [DefaultValue(ConditionType.And)]
        public ConditionType GroupConditionType { get; set; }

        /// <summary>
        /// Gets or sets the condition to use if generating the WHERE clause.
        /// Default is <see cref="T:CoreVelocity.DataAccess.ConditionType.And"/>.
        /// </summary>
        [DefaultValue(ConditionType.And)]
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// Gets or sets the comparison type to use if generating the WHERE clause.
        /// Default is <see cref="T:CoreVelocity.DataAccess.ComparisonType.Equals"/>.
        /// </summary>
        [DefaultValue(ComparisonType.Equals)]
        public ComparisonType ComparisonType { get; set; }


        /// <summary>
        /// Gets or sets the size of the parameter's value.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the data type of the parameter if the mapped type is different.
        /// </summary>
        /// <value>The type of the db.</value>
        public SqlDbType? DbType { get; set; }

        /// <summary>
        /// Gets or sets the order in which to process this criteria. Zeros (0) will be grouped together first and so on.
        /// </summary>
        [DefaultValue("0")]
        public int GroupIndex { get; set; }

                
        /// <summary>
        /// Gets the formatted name of the ParameterName.
        /// </summary>
        public string FormattedName { get { return string.Format(ParameterFormatString, ParameterName); } }
               
        /// <summary>
        /// Returns true or false based on if the CriteriaParamter has a value or not.
        /// </summary>
        /// <returns>true or false based on if the parameter has a value or not.</returns>
        public bool HasValue()
        {
            // If it's a string, check for Null or String.Empty.
            if (Value is string)
            {
                return !string.IsNullOrEmpty(Value as string);
            }
            // If it's an IEnumerable, check for Null or 0 items in the collection (uses .Count() extension for IEnumerable)
            else if(Value is ICollection)
            {
                ICollection collection = Value as ICollection;
                if (collection == null)
                {
                    return false;
                }
                else
                {
                    return collection.Count > 0;
                }
            }
            // An object, just return whether it's null or not.
            else
            {
                return Value != null;

            }
        }

        /// <summary>
        /// Gets or sets the direction of the parameter.
        /// </summary>
        /// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The
        /// default is Input.</returns>
        /// <exception cref="T:System.ArgumentException">The property was not set to one
        /// of the valid <see cref="T:System.Data.ParameterDirection" /> values. </exception>
        /// <value></value>
        [DefaultValue(ParameterDirection.Input)]
        public System.Data.ParameterDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets whether the parameter is NULLABLE or not.
        /// </summary>
        /// <returns>true if null values are accepted; otherwise, false. The default is false.</returns>
        /// <value></value>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter to use in the
        /// WHERE clause or stored procedure, ie: @Name = 'value'
        /// </summary>
        /// <returns>An <see cref="T:System.Object" /> that is the value of the parameter.
        /// The default value is null.</returns>
        /// <value></value>
        public object Value { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the parameter to map to.
        /// </summary>
        /// <returns>The name of the <see cref="T:System.Data.IDataParameter" />. The default
        /// is an empty string.</returns>
        /// <value></value>
        public string ParameterName 
        {
            get
            {
                return _parameterName;
            }
            set
            {
                if (_parameterName != value)
                {
                    _parameterName = value;

                    // If PropertyName is empty, set it to the parameterName right away.
                    // This will allow the developer to use the ParameterName as the PropertyName
                    // and keep the ParameterName auto-generator later on from setting the PropertyName
                    // to an invalid name if one is not set. This fixes a bug that would allow the developer
                    // to set a ParameterName without a PropertyName, add 2 parameters of the same name and cause
                    // an exception during query generation because it will use the generated ParameterName instead of the
                    // original ParameterName. (ie: "UserId0" instead of "UserId")
                    if(string.IsNullOrEmpty(_propertyName))
                    {
                        PropertyName = _parameterName;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the property name to use when the VALUE is a collection.
        /// This will typically be the case when the ComparisonType is "IN"
        /// </summary>
        public string CollectionPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the PropertyName to map the parameter to in the database. 
        /// If String.IsNullOrEmpty then the Name property will be used
        /// as the PropertyName instead.
        /// </summary>
        /// <returns>
        /// The name of the source column that is mapped to the parameter.
        /// The default is an empty string
        /// .</returns>
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                if (_propertyName != value)
                {
                    _propertyName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the source column to map to in the database if the column 
        /// isn't mapped in the <see cref="T:CoreVelocity.Core.Data.FieldMetaData"/>
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// Gets or sets the type name the parameter should be mapped to
        /// within the database.
        /// </summary>
        /// <value></value>
        public string TypeName { get;set; }

        #endregion

        #region Private Methods
      
        #endregion
    }
}
