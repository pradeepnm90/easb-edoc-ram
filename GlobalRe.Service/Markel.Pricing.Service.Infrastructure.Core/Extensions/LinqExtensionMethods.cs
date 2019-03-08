using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class LinqExtensionMethods
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, string sortColumnName, string orderByMethodName)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortColumnName);
            var exp = Expression.Lambda(prop, param);
            //string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { query.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), orderByMethodName, types, query.Expression, exp);
            return query.Provider.CreateQuery<T>(mce);
        }

        public static IEnumerable<T> FlattenTree<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f)
        {
            return e.SelectMany(c => f(c).FlattenTree(f)).Concat(e);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}
