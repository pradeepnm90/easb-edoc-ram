using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static List<string> GetPropertyNames(this Type type, bool ignoreIEnumerable = true)
        {
            var properties = new List<string>();
            properties.AddRange(type.GetProperties()
                .Where(p => ignoreIEnumerable == false || p.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                .Select(p => p.Name));
            properties.AddRange(type.GetFields()
                .Where(f => ignoreIEnumerable == false || f.FieldType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(f.FieldType))
                .Select(f => f.Name));
            return properties.Distinct().ToList();
        }

        public static List<string> GetPublicProperties<T>(this T entity, bool ignoreIEnumerable = true)
        {
            return entity.GetType().GetPropertyNames(ignoreIEnumerable);
        }

        public static string PropertyName(this Expression<Func<object>> propertyExpression)
        {
            PropertyInfo propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.Name;

            MemberInfo memberInfo = ((propertyExpression.Body as UnaryExpression)?.Operand as MemberExpression)?.Member;
            if (memberInfo != null)
                return memberInfo.Name;

            throw new ArgumentException("Invalid Property Expression!");
        }

        public static List<T> Attributes<T>(this Type t) where T : Attribute
        {
            object[] attributes = t.GetCustomAttributes(typeof(T), true);
            if (attributes == null || attributes.Length == 0) return new List<T>();

            return attributes.Select(a => (T)a).ToList();
        }

        /// <summary>
        /// Returns default instance of specified Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// Returns PropertyInfo of property name for specified Type. The property name 
        /// can be specified as the full path to a property of a subclass.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>PropertyInfo for specified property; otherwise, null.</returns>
        public static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            PropertyInfo pInfo = null;

            foreach (var property in propertyName.ToString().Split(".".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray())
            {
                pInfo = type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (pInfo == null) throw new IllegalArgumentAPIException($"Invalid property name '{propertyName}'");
                type = pInfo.PropertyType;
            }

            return pInfo;
        }

        public static object GetValueAsType(this Type type, object value)
        {
            Type uType = Nullable.GetUnderlyingType(type);
            //object convertedValue = (value == null) ? type.GetDefault() : Convert.ChangeType(value, uType ?? type);
            object convertedValue = (uType != null) ? ((value == null) ? type.GetDefault() : Convert.ChangeType(value, uType)) : Convert.ChangeType(value, type);
            return convertedValue;
        }

        /// <summary>
        /// Extension method to make static generic method calls.
        ///    Example: 
        ///         Type elementType = collection.GetElementTypeOfEnumerable();
        ///         typeof(Queryable).MakeStaticGenericMethodCall("AsQueryable", elementType, enumCollection);
        /// </summary>
        /// <returns>Type of object being enumerated.</returns>
        public static object MakeStaticGenericMethodCall(this Type typeObj, string method, Type typeArg, params object[] values)
        {
            return MakeStaticGenericMethodCall(typeObj, null, method, new[] { typeArg }, values);
        }
        public static object MakeStaticGenericMethodCall(this Type typeObj, string method, Type typeArg1, Type typeArg2, params object[] values)
        {
            return MakeStaticGenericMethodCall(typeObj, null, method, new[] { typeArg1, typeArg2 }, values);
        }
        public static object MakeStaticGenericMethodCall(this Type typeObj, object obj, string method, Type typeArg, params object[] values)
        {
            return MakeStaticGenericMethodCall(typeObj, obj, method, new[] { typeArg }, values);
        }
        public static object MakeStaticGenericMethodCall(this Type typeObj, object obj, string method, Type typeArg1, Type typeArg2, params object[] values)
        {
            return MakeStaticGenericMethodCall(typeObj, obj, method, new[] { typeArg1, typeArg2 }, values);
        }
        private static object MakeStaticGenericMethodCall(this Type typeObj, object obj, string method, Type[] argTypes, params object[] values)
        {
            Type[] types = new Type[(values == null) ? 0 : values.Length];
            if (values != null)
            {
                for (int i = 0; i < values.Count(); i++)
                    types[i] = values[i].GetType();
            }
            MethodInfo[] methodInfo = typeObj.GetMethods();
            foreach (MethodInfo m in methodInfo)
            {
                if (m.Name == method && m.IsGenericMethod)
                {
                    // TODO: Verify that parameter types match
                    Type[] args = m.GetGenericArguments();
                    MethodInfo genericMethod = m.MakeGenericMethod(argTypes);
                    return genericMethod.Invoke(obj, values);
                }
            }
            return null;
        }
        //public static object MakeStaticGenericMethodCall(this Type typeObj, string method, Type typeArg, params object[] values)
        //{
        //    Type[] types = new System.Type[(values == null) ? 0 : values.Length];
        //    if (values != null)
        //    {
        //        for (var i = 0; i < values.Count(); i++)
        //            types[i] = values[i].GetType();
        //    }
        //    //	var mi = typeObj.GetMethod(method, types);
        //    //	var genericMethod = mi.MakeGenericMethod(new[] { typeArg });
        //    //	return genericMethod.Invoke(null, values);

        //    var methodInfo = typeObj.GetMethods();
        //    foreach (var m in methodInfo)
        //    {
        //        if (m.Name == method && m.IsGenericMethod)
        //        {
        //            // TODO: Verify that parameter types match
        //            var args = m.GetGenericArguments();
        //            var genericMethod = m.MakeGenericMethod(new[] { typeArg });
        //            return genericMethod.Invoke(null, values);
        //        }
        //    }
        //    return null;
        //}
    }
}
