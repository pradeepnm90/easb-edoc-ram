using Markel.Pricing.Service.Infrastructure.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class PropertyAccessorUtility
    {
        #region Public Extensions

        public static bool CopyPropertyValueEx<F, T>(this F fromEntity, T toEntity, string property, string toProperty = null)
        {
            if (string.IsNullOrWhiteSpace(toProperty)) toProperty = property;

            Func<F, object> sourceGetter = GetPropertyGetter<F>(property);
            Func<T, object> destGetter = GetPropertyGetter<T>(toProperty);
            object sourceValue = sourceGetter(fromEntity);
            object destValue = destGetter(toEntity);
            //if (!sourceValue.Equals(destValue))//Old Condition 
            if ((sourceValue == null && destValue != null) || (sourceValue != null && !sourceValue.Equals(destValue)))// Add condition to set Property value blank, because sourceValue become Null and throws exception if Value is blank
            {
                var destSetter = GetPropertySetter<T>(toProperty);
                destSetter(toEntity, sourceValue);
                return true;
            }
            return false;
        }

        public static void SetPropertyValue<T>(this T obj, string property, object value) where T : class
        {
            Type objectType = typeof(T);
            PropertyInfo propertyDetail = objectType.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyDetail != null)
            {
                if (IsGenericNullableType(propertyDetail.PropertyType))
                {
                    value = (value.IsNullOrEmpty()) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(propertyDetail.PropertyType));
                }
                else
                {
                    if (value.IsNullOrEmpty() && propertyDetail.PropertyType != typeof(string))
                        throw new Exception("Mandatoy field can not be set to null value");
                    //var targetType = IsNullableType(propertyDetail.PropertyType) ? Nullable.GetUnderlyingType(propertyDetail.PropertyType) : propertyDetail.PropertyType;
                    //var targetType = propertyDetail.PropertyType;
                    value = Convert.ChangeType(value, propertyDetail.PropertyType);
                    //Set the value of the property
                }
                propertyDetail.SetValue(obj, value, null);
            }
        }

        #endregion Public Extensions

        #region Helper Methods

        private static Func<T, object> GetPropertyGetter<T>(string property)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "target");
            var propertyExp = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(propertyExp, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, instance).Compile();
        }

        private static Action<T, object> GetPropertySetter<T>(string property)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            ParameterExpression targetExp = Expression.Parameter(typeof(T), "target");
            ParameterExpression valueExp = Expression.Parameter(typeof(object), "value");

            // Expression.Property can be used here as well
            MemberExpression fieldExp = Expression.Property(targetExp, property);
            BinaryExpression assignExp = Expression.Assign(fieldExp, Expression.Convert(valueExp, propInfo.PropertyType));

            return Expression.Lambda<Action<T, object>>(assignExp, targetExp, valueExp).Compile();
        }

        private static bool IsGenericNullableType(Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        #endregion Helper Methods
    }
}
