using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Generic
{
    /// <summary>
    /// Represents a equality comparer which could be used for any object T being passed to it for equality comparison.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _FuncEquals;
        private Func<T, int> _FuncHashcode;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEqualityComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="funcEquals">The func equals.</param>
        /// <param name="funcHashcode">The func hashcode.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.
        ///   </exception>
        public GenericEqualityComparer(Func<T, T, bool> funcEquals, Func<T, int> funcHashcode)
        {
            _FuncEquals = funcEquals;
            _FuncHashcode = funcHashcode;

            if (_FuncEquals == null) throw new NullReferenceException("_FuncEquals is Null.");
            if (_FuncHashcode == null) throw new NullReferenceException("_FuncHashcode is Null.");
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T"/> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return _FuncEquals(x, y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj)
        {
            return _FuncHashcode(obj);
        }
    }
}
