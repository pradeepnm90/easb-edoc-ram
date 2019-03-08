using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class DictionaryExtension
    {
        public static TOut GetValue<TKey, TValue, TOut>(this Dictionary<TKey, TValue> context, TKey key, TOut defaultValue = default(TOut)) where TOut : TValue
        {
            return (context.ContainsKey(key) == true) ? (TOut)context[key] : defaultValue;    
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> context, TKey key, TValue defaultValue = default(TValue))
        {
            if (context == null) return defaultValue;
            return (context.ContainsKey(key) == true) ? context[key] : defaultValue;
        }

        public static void SetValue<TKey, TValue>(this Dictionary<TKey, TValue> context, TKey key, TValue value)
        {
            if (context.ContainsKey(key) == false) context.Add(key, value);
            else context[key] = value;
        }
    }
}
