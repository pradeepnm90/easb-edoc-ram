using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Represents a set of useful extension methods for collections.
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// Gets the changed item i.e the item which is either deleted or added to the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public static T GetChangedItem<T>(this System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            var item = default(T);
            switch (args.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (args.NewItems != null && args.NewItems.Count > 0) item = (T)args.NewItems[0];
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (args.OldItems != null && args.OldItems.Count > 0) item = (T)args.OldItems[0];
                    break;
                default:
                    break;
            }

            return item;
        }

        #region Arrays

        public static void Initialize<T>(this Array array, T defaultValue)
        {
            int[] indicies = new int[array.Rank];
            SetDimension<T>(array, indicies, 0, defaultValue);
        }

        private static void SetDimension<T>(Array array, int[] indicies, int dimension, T defaultValue)
        {
            for (int i = 0; i <= array.GetUpperBound(dimension); i++)
            {
                indicies[dimension] = i;

                if (dimension < array.Rank - 1)
                    SetDimension<T>(array, indicies, dimension + 1, defaultValue);
                else
                    array.SetValue(defaultValue, indicies);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this Array array)
        {
            foreach (var item in array)
                yield return (T)item;
        }

        public static string[] GetAsArray(this Dictionary<string, string> dictionary)
        {
            return dictionary.GetAsList().ToArray();
        }

        public static List<string> GetAsList(this Dictionary<string, string> dictionary)
        {
            List<string> listItems = new List<string>();
            if (dictionary != null)
            {
                foreach (var metaData in dictionary)
                {
                    listItems.Add(string.Format("{0}={1}", metaData.Key, metaData.Value));
                }
            }
            return listItems;
        }

        public static void Add<KEY, VALUE>(this IList<KeyValuePair<KEY, VALUE>> list, KEY key, VALUE value)
        {
            list.Add(new KeyValuePair<KEY, VALUE>(key, value));
        }

        #endregion Arrays

        public static T FindNextItem<T>(this IEnumerable<T> items, Predicate<T> matchFilling)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (matchFilling == null)
                throw new ArgumentNullException("matchFilling");

            using (var iter = items.GetEnumerator())
            {
                T previous = default(T);
                while (iter.MoveNext())
                {
                    if (matchFilling(iter.Current))
                    {
                        if (iter.MoveNext())
                            return iter.Current;
                        else
                            return default(T);
                    }
                    previous = iter.Current;
                }
            }

            return default(T);
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items,int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }

        #region Linq Except WithDifferent Types

        /// <summary>
        /// Returns items in list 1 that are not in list 2
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TCompared">The type of the compared.</typeparam>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <param name="select1">The select1.</param>
        /// <param name="select2">The select2.</param>
        /// <returns></returns>
        public static IEnumerable<T1> Except<T1, T2, TCompared>(this IEnumerable<T1> list1, IEnumerable<T2> list2,
            Func<T1, TCompared> select1, Func<T2, TCompared> select2)
        {
            return Except(list1, list2, select1, select2, EqualityComparer<TCompared>.Default);
        }

        /// <summary>
        /// Returns items in list 1 that are not in list 2 using specified camparer
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TCompared">The type of the compared.</typeparam>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <param name="select1">The select1.</param>
        /// <param name="select2">The select2.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        public static IEnumerable<T1> Except<T1, T2, TCompared>(this IEnumerable<T1> list1, IEnumerable<T2> list2,
            Func<T1, TCompared> select1, Func<T2, TCompared> select2, IEqualityComparer<TCompared> comparer)
        {
            if (list1 == null)
                return new List<T1>();
            if (list2 == null)
                return list1.ToList();
            return ExceptIterator<T1, T2, TCompared>(list1, list2, select1, select2, comparer);
        }

        private static IEnumerable<T1> ExceptIterator<T1, T2, TCompared>(IEnumerable<T1> list1, IEnumerable<T2> list2,
            Func<T1, TCompared> select1, Func<T2, TCompared> select2, IEqualityComparer<TCompared> comparer)
        {
            HashSet<TCompared> set = new HashSet<TCompared>(list2.Select(select2), comparer);
            foreach (T1 source1 in list1)
            {
                if (set.Add(select1(source1)))
                {
                    yield return source1;
                }
            }
        }

        #endregion Linq Except WithDifferent Types

        public static bool HasValue<T>(this IEnumerable<T> items)
        {
            if (items == null) return false;
            if (items.Count() == 0) return false;
            return true;
        }

        //
        // Summary:
        //     Determines whether the System.Collections.Generic.ICollection`1 contains a specific
        //     value.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.ICollection`1.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.ICollection`1; otherwise,
        //     false.
        public static bool Contains<T>(this ICollection<T> array, params T[] items)
        {
            foreach (T value in items)
            {
                if (array.Contains(value)) return true;
            }

            return false;
        }

        public static bool In<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        public static bool NotIn<T>(this T item, params T[] list)
        {
            return !list.Contains(item);
        }
    }
}
