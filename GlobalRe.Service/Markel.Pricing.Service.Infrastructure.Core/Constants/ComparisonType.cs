using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    /// <summary>
    /// Enum that sets the type of comparison to perform the mapped field to a Filter Parameter and its value.
    /// </summary>
    [DataContract]
    public enum ComparisonType
    {
        /// <summary>
        /// Comparison Type Not Set
        /// </summary>
        [EnumMember]
        None,
        /// <summary>
        /// Performs an EQUALS TO comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField = @Parameter1 </code>
        /// </summary>
        [EnumMember]
        Equals,
        /// <summary>
        /// Performs an GREATER THAN comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField &gt; @Parameter1 </code>
        /// </summary>
        [EnumMember]
        GreaterThan,
        /// <summary>
        /// Performs a LESS THAN comparison between the mapped field and the value:
        /// <code>
        /// Example Output Where Clause  (Using SQL Server):
        /// 
        /// WHERE MappedField &lt; @Parameter1
        /// 
        /// </code>
        /// </summary>
        [EnumMember]
        LessThan,
        /// <summary>
        /// Performs a LIKE surrounded by %% comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField LIKE @Parameter1 
        ///        -- Where the FormatString of @Parameter1 is '%{0}%'
        /// </code>
        /// </summary>
        [EnumMember]
        Contains,
        /// <summary>
        /// Performs a LIKE surrounded by % comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField LIKE @Parameter1 
        ///        -- Where the FormatString of @Parameter1 is '{0}%'
        /// </code>
        /// </summary>
        [EnumMember]
        StartsWith,
        /// <summary>
        /// Performs a LIKE surrounded by % comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField LIKE @Parameter1 
        ///        -- Where the FormatString of @Parameter1 is '%{0}'
        /// </code>
        /// </summary>
        [EnumMember]
        EndsWith,
        /// <summary>
        /// Performs an IN comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField IN (@Param0,@Param1,@Param2,@Param3) 
        ///        -- Where the @Param(n) are built from the Value of the CriteriaParameter.
        /// </code>
        /// </summary>
        [EnumMember]
        In,
        /// <summary>
        /// NOT RECOMMENDED, NOT IN is not recommended in SQL Server, NOT EXIST is preferred over NOT IN.
        /// Performs a NOT IN comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField NOT IN (@Param0,@Param1,@Param2,@Param3) 
        ///        -- Where the @Param(n) are built from the Value of the CriteriaParameter.
        /// </code>
        /// </summary>
        [EnumMember]
        NotIn,
        //[EnumMember]
        //Exists = 8,
        //[EnumMember]
        //NotExists = 9,
        /// <summary>
        /// Performs an LESS THAN/EQUAL TO comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField &lt;= @Parameter0 </code>
        /// </summary>
        [EnumMember]
        LessThanEqualTo,
        /// <summary>
        /// Performs an GREATER THAN/EQUAL TO comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField &gt;= @Parameter0 </code>
        /// </summary>
        [EnumMember]
        GreaterThanEqualTo,
        /// <summary>
        /// Performs a LIKE using any % provided by the developer (*'s are converted to % as well) comparison between the mapped field and the value:
        /// <code> Example Output Where Clause  (Using SQL Server): WHERE MappedField LIKE @Parameter1 
        ///        -- Where the FormatString of @Parameter1 is {0} 
        /// </code>
        /// </summary>
        [EnumMember]
        CustomLike,
        /// <summary>
        /// Performs a NOT EQUALS TO comparison between the mapped field and the value:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE MappedField != @Parameter1 </code>
        /// </summary>
        [EnumMember]
        NotEqual,
        /// <summary>
        /// Checks if mapped field IS NULL:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE MappedField IS NULL </code>
        /// </summary>
        [EnumMember]
        IsNull,
        /// <summary>
        /// Checks if mapped field IS NOT NULL:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE MappedField IS NOT NULL </code>
        /// </summary>
        [EnumMember]
        IsNotNull,
        /// <summary>
        /// Performs a BETWEEN comparison to the mapped field and the value:
        /// <code> Example Output Where Clause (Using SQL Server): WHERE MappedField BETWEEN @Parameter1 AND @Parameter2 </code>
        /// </summary>
        [EnumMember]
        Between,
        /// <summary>
        /// Checks if mapped field IS NULL OR EMPTY:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE ISNULL(MappedField, '') = '' </code>
        /// </summary>
        [EnumMember]
        IsNullOrEmpty,
        /// <summary>
        /// Checks if mapped field IS NOT NULL OR EMPTY:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE ISNULL(MappedField, '') != '' </code>
        /// </summary>
        [EnumMember]
        IsNotNullOrEmpty,
        /// <summary>
        /// Checks if mapped field does not contain value:
        /// <code>Example Output Where Clause (Using SQL Server): WHERE MappedField NOT LIKE @Parameter1
        ///        -- Where the FormatString of @Parameter1 is {%0%} </code>
        ///</code>
        /// </summary>
        [EnumMember]
        NotContain,
        /// <summary>
        /// Performs a BETWEEN comparison to the mapped field and the value:
        /// <code> Example Output Where Clause (Using SQL Server): WHERE MappedField BETWEEN @Parameter1 AND @Parameter2 </code>
        /// </summary>
        [EnumMember]
        InRange
    }
}
