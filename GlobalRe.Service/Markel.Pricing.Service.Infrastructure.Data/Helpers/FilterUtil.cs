using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Data.Helpers
{
    public enum FilterOperatorExpression
    {
        //
        // Summary:
        //     A bitwise or logical AND operation, such as (a & b) in C# and (a And b) in Visual
        //     Basic.
        [Description("&")]
        And = 1,
        //
        // Summary:
        //     A conditional AND operation that evaluates the second operand only if the first
        //     operand evaluates to true. It corresponds to (a && b) in C# and (a AndAlso b)
        //     in Visual Basic.
        [Description("&&")]
        AndAlso = 2,
        //
        // Summary:
        //     A node that represents an equality comparison, such as (a == b) in C# or (a =
        //     b) in Visual Basic.
        [Description("=")]
        Equal=3,
        [Description(">")]
        GreaterThan = 4,
        //
        // Summary:
        //     A "greater than or equal to" comparison, such as (a >= b).
        [Description(">=")]
        GreaterThanOrEqual = 5,
        [Description("IN")]
        In = 0,       
        [Description("<")]
        LessThan = 7,
        //
        // Summary:
        //     A "less than or equal to" comparison, such as (a <= b).
        [Description("<=")]
        LessThanOrEqual = 8,
        [Description("LIKE")]
        Like = 9,
        [Description("<>")]
        NotEqual = 10,
        [Description("NOT IN")]
        NotIn = 11,
        [Description("NOT LIKE")]
        NotLike = 12,
        //
        // Summary:
        //     A bitwise or logical OR operation, such as (a | b) in C# or (a Or b) in Visual
        //     Basic.
        [Description("|")]
        Or = 13,
        //
        // Summary:
        //     A short-circuiting conditional OR operation, such as (a || b) in C# or (a OrElse
        //     b) in Visual Basic.
        [Description("||")]
        OrElse = 14,
        [Description("BETWEEN")]
        BETWEEN = 15,

    }
    public static class FilterUtil
    {
        public static bool IsNullableType(Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Expression In(Expression leftExpression, IEnumerable<string> valueItems)
        {
            Expression binaryExpressionTree = BuildBinaryOrTree(valueItems.GetEnumerator(), leftExpression, null);
            return binaryExpressionTree;
        }

        public static Expression NotIn(Expression leftExpression, IEnumerable<string> valueItems)
        {
            Expression binaryExpressionTree = BuildBinaryAndTree(valueItems.GetEnumerator(), leftExpression, null);
            return binaryExpressionTree;
        }

        private static Expression BuildBinaryOrTree(IEnumerator<string> itemEnumerator, Expression leftExpression, Expression expression)
        {
            if (itemEnumerator.MoveNext() == false)
                return expression;

            MemberExpression member = leftExpression as MemberExpression;
            if (member == null)
                return null;
            Expression constant = GetRightConstantExpression(itemEnumerator, member);;
            BinaryExpression comparison = Expression.Equal(leftExpression, constant);

            BinaryExpression newExpression;

            if (expression == null)
                newExpression = comparison;
            else
                newExpression = Expression.OrElse(expression, comparison);

            return BuildBinaryOrTree(itemEnumerator, leftExpression, newExpression);
        }

        private static Expression GetRightConstantExpression(IEnumerator<string> itemEnumerator, MemberExpression member)
        {
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            Type propType = propertyType;
            if (IsNullableType(propertyType))
            {
                propType = propertyType.GetGenericArguments()[0];
            }
            Expression constant = Expression.Constant(Convert.ChangeType(itemEnumerator.Current.Trim(), propType));
            if (IsNullableType(propertyType))
                constant = Expression.Convert(constant, propertyType);
            return constant;
        }

        private static Expression BuildBinaryAndTree(IEnumerator<string> itemEnumerator, Expression leftExpression, Expression expression)
        {
            if (itemEnumerator.MoveNext() == false)
                return expression;

            MemberExpression member = leftExpression as MemberExpression;
            if (member == null)
                return null;
            Expression constant = GetRightConstantExpression(itemEnumerator, member);
            BinaryExpression comparison = Expression.NotEqual(leftExpression, constant);

            BinaryExpression newExpression;

            if (expression == null)
                newExpression = comparison;
            else
                newExpression = Expression.AndAlso(expression, comparison);

            return BuildBinaryAndTree(itemEnumerator, leftExpression, newExpression);
        }

        public static Expression Like(Expression leftExpression, string rightValue)
        {
            if (leftExpression.Type != typeof(string))
                throw new ArgumentException("Property must be a string");
            //string.Contains with string parameter.
            var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(leftExpression, stringContainsMethod, Expression.Constant(rightValue, typeof(string)));
        }

        public static Expression NotLike(Expression leftExpression, string rightValue)
        {
            Expression likeExpression = Like(leftExpression, rightValue);
            return Expression.Not(likeExpression);
        }

        public static Expression Between(Expression leftExpression, IEnumerable<string> valueItems)
        {
            if (valueItems.Count() > 2)
                throw new ArgumentException("Between operator expect only two values");
            PropertyInfo property = GetPropertyInfo(leftExpression);
            Expression lowerValue = Expression.GreaterThanOrEqual(leftExpression, GetRightExpression(property, valueItems.ToArray()[0]));
            Expression upperValue = Expression.LessThanOrEqual(leftExpression, GetRightExpression(property, valueItems.ToArray()[1]));
            return Expression.AndAlso(lowerValue, upperValue);
         }

        private static Expression GetRightExpression(PropertyInfo property, string value)
        {           
            Type propertyType = property.PropertyType;
            Type propType = propertyType;
            if (FilterUtil.IsNullableType(propertyType))
            {
                propType = propertyType.GetGenericArguments()[0];
            }
            Expression right = Expression.Constant(Convert.ChangeType(value, propType));
            if (FilterUtil.IsNullableType(propertyType))
                right = Expression.Convert(right, propertyType);

            return right;
        }

        public static PropertyInfo GetPropertyInfo(Expression expression)
        {
            var body = expression as MemberExpression;
            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");            

            return (PropertyInfo)body.Member;           
        }

        //public static Expression<Func<TEntity, bool>> Like<TEntity>(string propertyName, string queryText)
        //{
        //    var parameter = Expression.Parameter(typeof(TEntity), "entity");
        //    var getter = Expression.Property(parameter, propertyName);
        //    //ToString is not supported in Linq-To-Entities, throw an exception if the property is not a string.
        //    if (getter.Type != typeof(string))
        //        throw new ArgumentException("Property must be a string");
        //    //string.Contains with string parameter.
        //    var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //    var containsCall = Expression.Call(getter, stringContainsMethod,
        //        Expression.Constant(queryText, typeof(string)));

        //    return Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);
        //}

        public static Expression Sum(this IQueryable source, string member)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (member == null) throw new ArgumentNullException(nameof(member));

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "s");
            PropertyInfo property = source.ElementType.GetProperty(member);
            MemberExpression getter = Expression.MakeMemberAccess(parameter, property);
            Expression selector = Expression.Lambda(getter, parameter);

            MethodInfo sumMethod = typeof(Queryable).GetMethods().First(
                m => m.Name == "Sum"
                     && m.ReturnType == property.PropertyType
                     && m.IsGenericMethod);
                       
            var genericSumMethod = sumMethod.MakeGenericMethod(new[] { source.ElementType });

            return Expression.Call(
                null,
                genericSumMethod,
                new[] { source.Expression, Expression.Quote(selector) });
        }

    }

}
