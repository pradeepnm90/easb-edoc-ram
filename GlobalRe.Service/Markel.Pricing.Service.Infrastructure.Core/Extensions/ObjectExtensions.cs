using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Extentions for Object property values
    /// </summary>
    public static class ObjectExtensions
    {
        #region Property Extensions

        /// <summary>
        /// Copies the property value.
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromEntity">From entity.</param>
        /// <param name="toEntity">To entity.</param>
        /// <param name="property">The property.</param>
        /// <param name="toProperty">To property.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        public static bool CopyPropertyValue<F, T>(this F fromEntity, T toEntity, string property, string toProperty = null)
        {
            bool valueCopied = false;
            if (string.IsNullOrWhiteSpace(toProperty))
                toProperty = property;

            object sourceValue = fromEntity.GetPropertyValue(property);
            object destValue = toEntity.GetPropertyValue(toProperty);

            // Add condition to set Property value blank, because sourceValue become Null and throws exception if Value is blank
            if ((sourceValue == null && destValue != null) || (sourceValue != null && !sourceValue.Equals(destValue)))
            {
                toEntity.SetPropertyValue(toProperty, sourceValue);
                valueCopied = true;
            }

            return valueCopied;
        }

        /// <summary>
        /// Returns value of specified property. Path to property can be specified using "." as property seperator.
        /// </summary>
        /// <param name="source">Source object to fetch the property from.</param>
        /// <param name="propertyName">The name of the property to fetch from the source.</param>
        /// <returns>
        /// Null if target or property not specified; otherwise, property value. 
        ///	Exception if property with specified name does not exist.
        ///</returns>
        public static object GetPropertyValue(this object source, string propertyName, bool ignoreCase = true)
        {
            if (source == null || propertyName.IsNullOrEmpty()) return null;

            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | ((ignoreCase) ? BindingFlags.IgnoreCase : BindingFlags.Default);

            string[] properties = propertyName.Split(".".ToArray());
            object value = source;
            foreach (string property in properties)
            {
                PropertyInfo pInfo = value.GetType().GetProperty(property, bindingFlags);
                value = pInfo.GetValue(value, null);
            }
            return value;
        }

        /// <summary>
        /// Sets value of specified property.
        /// </summary>
        /// <returns>Exception if property with specified name does not exist or if error converting value type.</returns>
        public static void SetPropertyValue(this object target, string propertyName, object value, bool ignoreCase = true)
        {
            PropertyInfo pInfo = null;
            bool deepCopySource = false;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | ((ignoreCase) ? BindingFlags.IgnoreCase : BindingFlags.Default);
            string[] properties = propertyName.Split(".".ToArray());
            object currentTarget = target;
            for (int i = 0; i < properties.Count(); i++)
            {
                pInfo = currentTarget.GetType().GetProperty(properties[i], bindingFlags);
                if (i < (properties.Count() - 1))
                {
                    object newTarget = pInfo.GetValue(currentTarget, null);
                    // If we are setting a sub-property, then initialize newTarget
                    if (newTarget == null && !pInfo.PropertyType.IsValueType && pInfo.PropertyType != typeof(String) && (i + 1) < properties.Count())
                        pInfo.SetValue(currentTarget, (newTarget = Activator.CreateInstance(pInfo.PropertyType)), null);
                    currentTarget = newTarget;
                }
                else if (!pInfo.PropertyType.IsValueType && pInfo.PropertyType != typeof(String) && pInfo.GetValue(currentTarget, null) == null)
                {
                    deepCopySource = true;  // If not a sub-property, then set deep copy source to target
                }
            }

            // Using JsonExtensions.IsNullOrEmpty to check for NULL because it also test object as JValue (object can be json object)
            Type uType = Nullable.GetUnderlyingType(pInfo.PropertyType);
            object convertedValue;
            if (deepCopySource == true)
                convertedValue = (value.IsNullOrEmpty()) ? value : value.DeepCopy(); // Uses DeepCopyByExpressionTreeObj
            else
                convertedValue = (value.IsNullOrEmpty()) ? pInfo.PropertyType.GetDefault() : Convert.ChangeType(value, uType ?? pInfo.PropertyType);

            pInfo.SetValue(currentTarget, convertedValue, null);
        }

        public static Type GetPropertyType<T>(this string propertyName, bool ignoreCase = true)
        {
            Type propertyType = typeof(T);
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | ((ignoreCase) ? BindingFlags.IgnoreCase : BindingFlags.Default);
            string[] properties = propertyName.Split(".".ToArray());

            for (int i = 0; i < properties.Count(); i++)
            {
                propertyType = propertyType?.GetProperty(properties[i], bindingFlags)?.PropertyType;
            }

            if (propertyType == null)
                throw new IllegalArgumentAPIException($"Invalid Property Name '{propertyName}'!");

            return propertyType;
        }

        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="fieldAccessExpression">The field access expression.</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this object target, Func<T> fieldAccessExpression)
        {
            return (target == null) ? default(T) : fieldAccessExpression();
        }

        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="fieldAccessExpression">The field access expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this object target, Func<T> fieldAccessExpression, T defaultValue)
        {
            if (target == null)
                return defaultValue;
            var result = fieldAccessExpression();
            return (result == null) ? defaultValue : result;
        }

        #endregion Property Extensions

        /// <summary>
        /// Extension method to create a deep copy of any type.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="objectToCopy">The object to copy.</param>
        /// <returns>The copied object of the type T.</returns>
        public static T DeepCopy<T>(this T objectToCopy)
        {
            return objectToCopy.DeepCopyByExpressionTree();
        }

        public static string ToCsvData<T>(this T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "Value can not be null or Nothing!");
            }

            StringBuilder sb = new StringBuilder();
            Type t = obj.GetType();
            PropertyInfo[] pi = t.GetProperties();

            for (int index = 0; index < pi.Length; index++)
            {
                sb.Append(pi[index].GetValue(obj, null));

                if (index < pi.Length - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public static T ChangeType<T>(this object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }
        
        public static object ChangeType(this object value, Type conversion)
        {
            Type t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }
            else if (value == null && t.IsClass == false)
            {
                return Activator.CreateInstance(t);
            }

            return Convert.ChangeType(value, t);
        }

        public static bool IsNumeric(this object obj)
        {
            if (!Equals(obj, null))
            {
                return IsNumeric(obj.GetType());
            }
            return false;
        }

        public static bool IsNumeric(this Type objType)
        {
            if (objType.IsPrimitive)
            {
                return (objType != typeof(bool) && objType != typeof(char) && objType != typeof(IntPtr) && objType != typeof(UIntPtr)) || objType == typeof(decimal);
            }
            return false;
        }

        public static bool HasValue(this object obj)
        {
            if (obj == null) return false;

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                object value = propertyInfo.GetValue(obj, null);
                if (propertyInfo.PropertyType == typeof(string))
                {
                    if (!string.IsNullOrEmpty(value as string)) return true;
                }
                else if (value != null)
                {
                    ICollection collection = value as ICollection;
                    if (collection != null)
                    {
                        if (collection.Count > 0) return true;
                    }
                    else return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates object is not null and returns object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="instance">Instance of Object</param>
        /// <returns>Original instance of object</returns>
        public static T ValidateObject<T>(this T instance)
        {
            if (instance == null) throw new ArgumentNullException(typeof(T).ToString());
            return instance;
        }

        public static void Use<T>(this T item, Action<T> action)
        {
            action(item);
        }

        #region CompareEquals

        public static List<string> ChangedProperties<T>(this T baseObject, T compareObject)
        {
            List<string> changedProperties = new List<string>();

            if (baseObject != null || compareObject != null)
            {
                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                changedProperties.AddRange(props.Select(p => p.Name));

                if (baseObject != null && compareObject != null)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        var propInfo = baseObject.GetType().GetProperty(prop.Name);
                        object dataFromCompare = propInfo.GetValue(baseObject, null);
                        object dataToCompare = propInfo.GetValue(compareObject, null);

                        if (dataFromCompare.CompareEquals(dataToCompare))
                            changedProperties.Remove(prop.Name);
                    }
                }
            }

            return changedProperties;
        }

        public static bool CompareEquals<T>(this T objectFromCompare, T objectToCompare)
        {
            bool result = (objectFromCompare == null && objectToCompare == null);

            if (!result)
            {
                if (objectFromCompare == null || objectToCompare == null)
                    result = false;
                else
                {
                    try
                    {
                        Type fromType = objectFromCompare.GetType();
                        if (fromType.IsPrimitive)
                        {
                            result = objectFromCompare.Equals(objectToCompare);
                        }

                        else if (fromType.FullName.Contains("System.String"))
                        {
                            result = ((objectFromCompare as string) == (objectToCompare as string));
                        }

                        else if (fromType.FullName.Contains("DateTime"))
                        {
                            result = (DateTime.Parse(objectFromCompare.ToString()).Ticks == DateTime.Parse(objectToCompare.ToString()).Ticks);
                        }

                        // stringbuilder handling here is optional, but doing it this way cuts down
                        // on reursive calls to this method
                        else if (fromType.FullName.Contains("System.Text.StringBuilder"))
                        {
                            result = ((objectFromCompare as StringBuilder).ToString() == (objectToCompare as StringBuilder).ToString());
                        }

                        else if (fromType.FullName.Contains("System.Collections.Generic.Dictionary"))
                        {

                            PropertyInfo countProp = fromType.GetProperty("Count");
                            PropertyInfo keysProp = fromType.GetProperty("Keys");
                            PropertyInfo valuesProp = fromType.GetProperty("Values");
                            int fromCount = (int)countProp.GetValue(objectFromCompare, null);
                            int toCount = (int)countProp.GetValue(objectToCompare, null);

                            result = (fromCount == toCount);
                            if (result && fromCount > 0)
                            {
                                var fromKeys = keysProp.GetValue(objectFromCompare, null);
                                var toKeys = keysProp.GetValue(objectToCompare, null);
                                result = CompareEquals(fromKeys, toKeys);
                                if (result)
                                {
                                    var fromValues = valuesProp.GetValue(objectFromCompare, null);
                                    var toValues = valuesProp.GetValue(objectToCompare, null);
                                    result = CompareEquals(fromValues, toValues);
                                }
                            }
                        }

                        // collections presented a unique problem in that the original code always returned
                        // false when they're encountered. The following code was tested with generic
                        // lists (of both primitive types and complex classes). I see no reason why an
                        // ObservableCollection shouldn't also work here (unless the properties or
                        // methods already used are not appropriate).
                        else if (fromType.IsGenericType || fromType.IsArray)
                        {
                            string propName = (fromType.IsGenericType) ? "Count" : "Length";
                            string methName = (fromType.IsGenericType) ? "get_Item" : "Get";
                            PropertyInfo propInfo = fromType.GetProperty(propName);
                            MethodInfo methInfo = fromType.GetMethod(methName);
                            if (propInfo != null && methInfo != null)
                            {
                                int fromCount = (int)propInfo.GetValue(objectFromCompare, null);
                                int toCount = (int)propInfo.GetValue(objectToCompare, null);
                                result = (fromCount == toCount);
                                if (result && fromCount > 0)
                                {
                                    for (int index = 0; index < fromCount; index++)
                                    {
                                        // Get an instance of the item in the list object 
                                        object fromItem = methInfo.Invoke(objectFromCompare, new object[] { index });
                                        object toItem = methInfo.Invoke(objectToCompare, new object[] { index });
                                        result = CompareEquals(fromItem, toItem);
                                        if (!result)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            PropertyInfo[] props = fromType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (PropertyInfo prop in props)
                            {
                                object dataFromCompare = fromType.GetProperty(prop.Name).GetValue(objectFromCompare, null);
                                object dataToCompare = fromType.GetProperty(prop.Name).GetValue(objectToCompare, null);
                                Type type = dataToCompare?.GetType();
                                result = (type == null) ? CompareEquals(dataFromCompare, dataToCompare) : CompareEquals(Convert.ChangeType(dataFromCompare, type), Convert.ChangeType(dataToCompare, type));

                                // no point in continuing beyond the first property that isn't equal.
                                if (!result)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception /* ex */)
                    {
                    }
                }
            }
            return result;

        }

        public static bool CompareEquals<T>(this T objectFromCompare, T objectToCompare, string propertyName)
        {
            bool result = (objectFromCompare == null && objectToCompare == null);
            if (!result)
            {
                try
                {
                    Type fromType = objectFromCompare.GetType();
                    PropertyInfo prop = fromType.GetProperty(propertyName);
                    if (prop != null)
                    {
                        object dataFromCompare = prop.GetValue(objectFromCompare, null);
                        object dataToCompare = prop.GetValue(objectToCompare, null);
                        Type type = prop.GetValue(objectToCompare)?.GetType();
                        result = (type == null) ? CompareEquals(dataFromCompare, dataToCompare) : CompareEquals(Convert.ChangeType(dataFromCompare, type), Convert.ChangeType(dataToCompare, type));
                    }
                }
                catch (Exception /* ex */)
                {
                }
            }
            return result;
        }

        public static bool CompareEquals<T>(this T objectFromCompare, T objectToCompare, string[] propertyNames)
        {
            bool result = (objectFromCompare == null && objectToCompare == null);
            if (!result)
            {
                try
                {
                    foreach (string propertyName in propertyNames)
                    {
                        result = CompareEquals(objectFromCompare, objectToCompare, propertyNames);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
                catch (Exception /* ex */)
                {
                }
            }
            return result;
        }

        #endregion CompareEquals
    }
}
