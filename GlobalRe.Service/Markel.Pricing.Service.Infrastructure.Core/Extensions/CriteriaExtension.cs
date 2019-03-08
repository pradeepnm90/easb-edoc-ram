using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class CriteriaExtension
    {
        #region ToExpressionString

        public const string PARAMETER_SEPARATOR = "|";

        public static string ToFilterExpressionString<T>(this IReadOnlyCollection<FilterParameter> parameters, ref List<object> expressionParams, string distinctField = null)
        {
            string dynamicFilter = string.Empty;
            expressionParams = new List<object>();

            // Remove In & Not-In filters for the column we're trying to get distinct values on
            var usedParameters = parameters.Where(p => !(string.Equals(p.Name, distinctField) && p.FilterOperator.In(ComparisonType.In, ComparisonType.NotIn))).ToList();
            var paramGroups = usedParameters.GroupBy(p => p.Group).OrderBy(g => g.Key);
            foreach (var paramGroup in paramGroups)
            {
                dynamicFilter += paramGroup.ToList().AddFilterExpressionGroup<T>(ref expressionParams);
            }

            // Remove trailing connector
            int indexOfLastConnector = dynamicFilter.Trim().LastIndexOf(" ");
            dynamicFilter = dynamicFilter.Substring(0, (indexOfLastConnector < 0) ? 0 : indexOfLastConnector);

            return dynamicFilter;
        }

        public static string ToSortExpressionString<T>(this IReadOnlyCollection<SortField> sortByFields, string defaultSortColumn)
        {
            string orderByClause = string.Empty;

            if (sortByFields == null || !sortByFields.Any(f => f.FieldName.HasValue()))
                sortByFields = new List<SortField>() { new SortField() { FieldName = defaultSortColumn, SortOrder = SortOrderType.Descending } };

            foreach (var field in sortByFields.Where(f => f.FieldName.HasValue()))
            {
                Type subType = typeof(T);
                System.Reflection.PropertyInfo pInfo = subType.GetPropertyInfo(field.FieldName);
                if (pInfo != null)
                {
                    orderByClause += string.Format("{0} {1},", field.FieldName, field.SortOrder);
                }
            }

            // Remove trailing sort connector
            int indexOfLastConnector = orderByClause.Trim().LastIndexOf(",");
            orderByClause = orderByClause.Substring(0, (indexOfLastConnector < 0) ? 0 : indexOfLastConnector);

            return orderByClause;
        }

        private static string AddFilterExpressionGroup<T>(this List<FilterParameter> parameters, ref List<object> expressionParams)
        {
            string dynamicFilter = string.Empty;
            foreach (FilterParameter filterParam in parameters)
            {
                System.Reflection.PropertyInfo pInfo = typeof(T).GetPropertyInfo(filterParam.Name);

                if (pInfo != null)
                {
                    object convertedValue = null;
                    Type uType = Nullable.GetUnderlyingType(pInfo.PropertyType);

                    // We can pre-convert value if it does not contain a seperated list
                    if (filterParam.FilterOperator != ComparisonType.Between && filterParam.FilterOperator != ComparisonType.In && filterParam.FilterOperator != ComparisonType.NotIn)
                    {
                        try
                        {
                            convertedValue = pInfo.PropertyType.GetValueAsType(filterParam.Value);
                        }
                        catch (Exception)
                        {
                            throw new IllegalArgumentAPIException($"Invalid value '{filterParam.Value}' for '{filterParam.Name}'!");
                        }
                    }

                    switch (filterParam.FilterOperator)
                    {
                        case ComparisonType.Equals:
                            dynamicFilter += string.Format("{0} = {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.GreaterThan:
                            dynamicFilter += string.Format("{0} > {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.LessThan:
                            dynamicFilter += string.Format("{0} < {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.Contains:
                        case ComparisonType.NotContain:
                            string prefix = (filterParam.FilterOperator == ComparisonType.Contains) ? "" : "!";
                            dynamicFilter += string.Format("{0}{1}.Contains({2}) {3} ", prefix, filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.StartsWith:
                            dynamicFilter += string.Format("{0}.StartsWith({1}) {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.EndsWith:
                            dynamicFilter += string.Format("{0}.EndsWith({1}) {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.LessThanEqualTo:
                            dynamicFilter += string.Format("{0} <= {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.GreaterThanEqualTo:
                            dynamicFilter += string.Format("{0} >= {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.NotEqual:
                            dynamicFilter += string.Format("{0} <> {1} {2} ", filterParam.Name, string.Format("@{0}", expressionParams.Count), filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.IsNull:
                            dynamicFilter += string.Format("{0} = NULL {1} ", filterParam.Name, filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.IsNotNull:
                            dynamicFilter += string.Format("{0} != NULL {1} ", filterParam.Name, filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.InRange:
                        case ComparisonType.Between:
                            // Make sure Value is a valid seperated list
                            if (filterParam.Value != null && !string.IsNullOrWhiteSpace(filterParam.Value.ToString()))
                            {
                                string[] values = filterParam.Value.ToString().Split(PARAMETER_SEPARATOR.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                                if (values.Length > 1)
                                {
                                    expressionParams.Add(pInfo.PropertyType.GetValueAsType(values[0]));
                                    expressionParams.Add(pInfo.PropertyType.GetValueAsType(values[1]));

                                    dynamicFilter += string.Format("({0} >= {1} && {0} <= {2}) {3} ", filterParam.Name,
                                        string.Format("@{0}", expressionParams.Count - 2), string.Format("@{0}", expressionParams.Count - 1), filterParam.GroupOperator);
                                }
                            }
                            break;
                        case ComparisonType.In:
                        case ComparisonType.NotIn:
                            {
                                // Make sure Value is a valid seperated list
                                string paramsListFormat = string.Empty;
                                string[] values = filterParam.Value.ToString().Split(PARAMETER_SEPARATOR.ToArray()).Select(sValue => sValue.Trim()).ToArray();

                                if (values.Length != 0)
                                {
                                    dynamicFilter += string.Format("{0}(@{1}.Contains(outerIt.{2})) {3} ", (filterParam.FilterOperator == ComparisonType.In) ? "" : "!", expressionParams.Count, filterParam.Name, filterParam.GroupOperator);

                                    //TODO: REplace foreach with --  	var lst3 = Array.ConvertAll(lst, long.Parse);
                                    var lst = (IList)Activator.CreateInstance((typeof(List<>).MakeGenericType(pInfo.PropertyType)));
                                    foreach (var s in values)
                                        lst.Add(s.ConvertToType(pInfo.PropertyType));

                                    // Strings always check for null and string.Empty (if either exists in list of values)
                                    if (pInfo.PropertyType == typeof(string))
                                    {
                                        bool hasEmptyString = lst.Contains("");
                                        bool hasNullValue = lst.Contains(null);

                                        if (hasEmptyString && !hasNullValue)
                                            lst.Add(null);

                                        else if (hasNullValue && !hasEmptyString)
                                            lst.Add("");
                                    }

                                    expressionParams.Add(lst);
                                }
                            }
                            break;
                        case ComparisonType.IsNullOrEmpty:
                            dynamicFilter += string.Format("({0} = NULL || {0} = \"\") {1} ", filterParam.Name, filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.IsNotNullOrEmpty:
                            dynamicFilter += string.Format("({0} != NULL && {0} != \"\") {1} ", filterParam.Name, filterParam.GroupOperator);
                            expressionParams.Add(convertedValue);
                            break;
                        case ComparisonType.CustomLike:
                        default:
                            break;
                    }
                }
            }

            // Add grouping 
            dynamicFilter = dynamicFilter.Insert(dynamicFilter.Trim().LastIndexOf(" "), ")").Insert(0, "(");

            return dynamicFilter;
        }

        public static object ConvertToType(this string value, Type toType)
        {
            Type uType = Nullable.GetUnderlyingType(toType);
            if (uType != null)
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
            }

            // Converts nulls to default(T) if T is not nullable. 
            //return (value == null) ? ((toType.IsValueType) ? Activator.CreateInstance(toType) : null) : Convert.ChangeType(value, uType ?? toType);

            // Throws an exception if value is null and T is not nullable
            return (uType != null) ? ((value == null) ? Activator.CreateInstance(toType) : Convert.ChangeType(value, uType)) : Convert.ChangeType(value, toType);
        }
        
        #endregion ToExpressionString

        #region Add

        public static void Add(this List<FilterParameter> parameters, string parameterName, object value, ComparisonType filter = ComparisonType.Equals, ConditionType condition = ConditionType.And, int group = 0)
        {
            parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        public static void AddIf(this List<FilterParameter> parameters, string parameterName, int? value, ComparisonType filter = ComparisonType.Equals, ConditionType condition = ConditionType.And, int group = 0)
        {
            if (value.HasValue)
                parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        public static void AddIf(this List<FilterParameter> parameters, string parameterName, long? value, ComparisonType filter = ComparisonType.Equals, ConditionType condition = ConditionType.And, int group = 0)
        {
            if (value.HasValue)
                parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        public static void AddIf(this List<FilterParameter> parameters, string parameterName, string value, ComparisonType filter = ComparisonType.Contains, ConditionType condition = ConditionType.And, int group = 0)
        {
            if (!string.IsNullOrEmpty(value))
                parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        public static void AddIf(this List<FilterParameter> parameters, string parameterName, bool? value, ComparisonType filter = ComparisonType.Equals, ConditionType condition = ConditionType.And, int group = 0)
        {
            if (value.HasValue)
                parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        public static void AddIf(this List<FilterParameter> parameters, string parameterName, DateTime? value, ComparisonType filter = ComparisonType.Equals, ConditionType condition = ConditionType.And, int group = 0)
        {
            if (value.HasValue)
                parameters.Add(new FilterParameter(parameterName, filter, value, condition, group));
        }

        #endregion Add

        #region Get

        public static FilterParameter[] Get(this IEnumerable<FilterParameter> parameters, string parameterName, params ComparisonType[] comparisonTypes)
        {
            if (parameters == null) return new FilterParameter[0];
            return parameters.Where(item =>
                string.Equals(item.Name, parameterName, StringComparison.CurrentCultureIgnoreCase) &&
                (comparisonTypes.Length == 0 || comparisonTypes.Contains(item.FilterOperator))
            ).ToArray();
        }

        public static T FirstOrDefaultValue<T>(this IEnumerable<FilterParameter> parameters, Enum parameterNameEnum)
        {
            return FirstOrDefaultValue<T>(parameters, parameterNameEnum.ToString());
        }

        public static FilterParameter FirstOrDefault(this IEnumerable<FilterParameter> parameters, string parameterName)
        {
            if (parameters == null) return null;
            return parameters.FirstOrDefault(item => string.Equals(item.Name, parameterName, StringComparison.CurrentCultureIgnoreCase));
        }

        public static T FirstOrDefaultValue<T>(this IEnumerable<FilterParameter> parameters, string parameterName)
        {
            Asserter.AssertIsNotNullOrEmptyString("parameterName", parameterName);
            return parameters.FirstOrDefault(parameterName).Value<T>();
        }

        public static T Value<T>(this FilterParameter parameter)
        {
            if (parameter != null && parameter.Value != null && !string.IsNullOrEmpty(parameter.Value.ToString()))
            {
                if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                {
                    int result = 0;
                    if (Int32.TryParse(parameter.Value.ToString(), out result))
                        return (T)(object)result;
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)parameter.Value;
                }
                else if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
                {
                    bool result = false;
                    if (bool.TryParse(parameter.Value.ToString(), out result))
                        return (T)(object)result;
                }
                else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                {
                    DateTime result;
                    if (DateTime.TryParse(parameter.Value.ToString(), out result))
                        return (T)(object)result;
                    return default(T);
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Unhandled type: {0}", typeof(T)));
                }
            }

            return default(T);
        }

        /// <summary>
        /// Dynamically add group id as the largest group # + 1
        /// </summary>
        /// <param name="parameters">List of Filter Parameters</param>
        /// <returns>Group (int)</returns>
        public static int GetNextGroup(this List<FilterParameter> parameters)
        {
            if (parameters == null || parameters.Count == 0) return 1;

            return (parameters.Max(f => f.Group) ?? 0) + 1;
        }

        #endregion Get
    }
}
