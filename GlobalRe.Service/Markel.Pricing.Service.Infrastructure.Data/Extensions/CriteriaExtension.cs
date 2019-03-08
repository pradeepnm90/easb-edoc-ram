using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Data.Extensions
{
    public static class CriteriaExtension
    {
        public static ICriteria ToFilterCriteria(this SearchCriteria searchCriteria, string defaultSortColumn)
        {
            SortedFieldCollection sortedFields = new SortedFieldCollection();
            if (searchCriteria.SortedFields == null || searchCriteria.SortedFields.Count == 0)
            {
                sortedFields.Add(new SortedField(defaultSortColumn, SortOrderType.Descending));
            }
            else
            {
                foreach (SortField field in searchCriteria.SortedFields)
                {
                    sortedFields.Add(new SortedField(field.FieldName, field.SortOrder));
                }
            }

            return new Criteria()
            {
                Pagination = new Pagination(searchCriteria),
                SortedFields = sortedFields
            };
        }

        #region Distinct

        private static Dictionary<string, Tuple<Type, object>> DistinctTypeSelectors = new Dictionary<string, Tuple<Type, object>>()
        {
            { "System.Object",   new Tuple<Type, object>(typeof(object), (Func<object, object>)(i => i)) },
            { "System.Boolean",  new Tuple<Type, object>(typeof(bool), (Func<bool, bool>)(i => i)) },
            { "System.Byte",     new Tuple<Type, object>(typeof(byte), (Func<byte, byte>)(i => i)) },
            { "System.Int32",    new Tuple<Type, object>(typeof(int), (Func<int, int>)(i => i)) },
            { "System.Int16",    new Tuple<Type, object>(typeof(Int16), (Func<Int16, Int16>)(i => i)) },
            { "System.Long",     new Tuple<Type, object>(typeof(long), (Func<long, long>)(i => i)) },
            { "System.Double",   new Tuple<Type, object>(typeof(double), (Func<double, double>)(i => i)) },
            { "System.Decimal",  new Tuple<Type, object>(typeof(decimal), (Func<decimal, decimal>)(i => i)) },
            { "System.Float",    new Tuple<Type, object>(typeof(float), (Func<float, float>)(i => i)) },
            { "System.String",   new Tuple<Type, object>(typeof(string), (Func<string, string>)(i => i)) },
            { "System.DateTime", new Tuple<Type, object>(typeof(DateTime), (Func<DateTime, DateTime>)(i => i)) },
            { "System.Nullable`1[System.Boolean]",  new Tuple<Type, object>(typeof(bool?), (Func<bool?, bool?>)(i => i)) },
            { "System.Nullable`1[System.Byte]",     new Tuple<Type, object>(typeof(byte?), (Func<byte?, byte?>)(i => i)) },
            { "System.Nullable`1[System.Int32]",    new Tuple<Type, object>(typeof(int?), (Func<int?, int?>)(i => i)) },
            { "System.Nullable`1[System.Int16]",    new Tuple<Type, object>(typeof(Int16?), (Func<Int16?, Int16?>)(i => i)) },
            { "System.Nullable`1[System.Long]",     new Tuple<Type, object>(typeof(long?), (Func<long?, long?>)(i => i)) },
            { "System.Nullable`1[System.Double]",   new Tuple<Type, object>(typeof(double?), (Func<double?, double?>)(i => i)) },
            { "System.Nullable`1[System.Decimal]",  new Tuple<Type, object>(typeof(decimal?), (Func<decimal?, decimal?>)(i => i)) },
            { "System.Nullable`1[System.Float]",    new Tuple<Type, object>(typeof(float?), (Func<float?, float?>)(i => i)) },
            { "System.Nullable`1[System.String]",   new Tuple<Type, object>(typeof(string), (Func<string, string>)(i => i)) },
            { "System.Nullable`1[System.DateTime]", new Tuple<Type, object>(typeof(DateTime?), (Func<DateTime?, DateTime?>)(i => i)) },
        };

        public static IEnumerable GetDistinct<T>(this IEnumerable<T> list, SearchCriteria criteria, string distinctColumn)
        {
            return list.AsQueryable().GetDistinct(criteria, distinctColumn);
        }

        public static IEnumerable GetDistinct<T>(this IQueryable<T> query, SearchCriteria criteria, string distinctColumn)
        {
            if (string.IsNullOrWhiteSpace(distinctColumn)) return new List<object>();

            List<object> expressionParams = null;
            string dynamicFilter = null;
            string dynamicOrderBy = null;

            if (criteria != null)
            {
                // Initialize filter query filter and sort order
                dynamicFilter = criteria.Parameters.ToFilterExpressionString<T>(ref expressionParams, distinctColumn);
                dynamicOrderBy = criteria.SortedFields.ToSortExpressionString<T>(distinctColumn);
            }
            else
            {
                dynamicOrderBy = ((IReadOnlyCollection<SortField>)null).ToSortExpressionString<T>(distinctColumn);
            }

            IQueryable iQuery = query;
            if (!string.IsNullOrWhiteSpace(dynamicFilter))
                iQuery = query.Where(dynamicFilter, expressionParams.ToArray());

            iQuery = iQuery.Select(distinctColumn).Distinct();
            //iQuery = iQuery.OrderBy(string.Format("{0} Ascending", dynamicOrderBy)); //ETM_FIX: OrderBy not working on when sent to database (not added to SQL statement).
            var distinctOfType = typeof(Enumerable).MakeStaticGenericMethodCall("ToList", iQuery.ElementType, iQuery);

            //TODO: Remove this once database order by is fixed.
            //distinctResults = distinctOfType.OrderBy(s => s); //Switched to make generic call because value types were failing
            var underlyingType = iQuery.ElementType.ToString();
            var selector = DistinctTypeSelectors.ContainsKey(underlyingType) ? DistinctTypeSelectors[underlyingType] : DistinctTypeSelectors["System.Object"];
            return typeof(Enumerable).MakeStaticGenericMethodCall("OrderBy", selector.Item1, selector.Item1, new object[] { distinctOfType, selector.Item2 }) as IEnumerable;
        }

        #endregion Distinct
    }
}
