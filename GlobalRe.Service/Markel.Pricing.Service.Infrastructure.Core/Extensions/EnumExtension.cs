using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static partial class EnumExtension
    {
        public static string EnumDescription(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            if (field == null) throw new NullReferenceException($"Enum value of {enumValue.ToString()} was not found!");
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0 ? enumValue.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        #region LookupEntity

        public static List<LookupEntity> ToLookupEntityList<T>(this IList<T> list)
            where T : struct
        {
            return list.Select(e => e.ToLookupEntity<T>()).ToList();
        }

        public static List<LookupEntity> ToLookupEntity<T>()
            where T : struct
        {
            var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
            var array2 = Enum.GetNames(typeof(T)).ToArray<string>();
            List<LookupEntity> lst = new List<LookupEntity>();
            for (int i = 0; i < array.Length; i++)
            {
                T value = array[i];
                lst.Add(value.ToLookupEntity<T>());
            }
            return lst;
        }

        public static LookupEntity ToLookupEntity<T>(this T value)
            where T : struct
        {
            return new LookupEntity(value as Enum);
        }

        #endregion LookupEntity

        /// <summary>
        /// Converts enumValue to specified Enum type, and if it cannot be converted, defaultValue will be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The enum value or name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this object value, T defaultValue = default(T)) where T : struct
        {
            if (value == null) return defaultValue;

            bool isConverted = false;
            T returnEnum = defaultValue;

            if (!(isConverted = Enum.TryParse<T>(value.ToString(), out returnEnum)))
            {
                Int32 enumIntValue;
                if ((value.GetType().IsEnum || value.IsNumeric()) && Int32.TryParse(((Int32)value).ToString(), out enumIntValue))
                {
                    if (Enum.IsDefined(typeof(T), enumIntValue))
                    {
                        isConverted = Enum.TryParse<T>(enumIntValue.ToString(), out returnEnum);
                    }
                }
            }
            if (isConverted && Enum.IsDefined(typeof(T), returnEnum.ToString()) == false)
            {
                returnEnum = defaultValue;
            }
            return returnEnum;
        }

        public static bool NameEquals(this Enum value, string name, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            return value.ToString().Equals(name, comparisonType);
        }

        public static T ToEnumFromDescription<T>(this string description) where T : struct
        {
            if (string.IsNullOrEmpty(description))
                throw new NullReferenceException(string.Format("{0} can not be null", description));
            var enumObj = ToLookupEntity<T>().FirstOrDefault(d => description.Trim().ToLower().Equals(d.Description.Trim().ToLower()));
            if (enumObj == null)
                return default(T);
            //throw new NullReferenceException(string.Format("{0} can not defined", description));

            return enumObj.Code.ToEnum<T>();
        }
    }
}
