
using System.Collections.Generic;

namespace Adventure
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Reports if the object is a null reference.
        /// </summary>
        /// <returns>True if the object is a null reference, else false.</returns>
        public static bool IsNull(this object o)
        {
            if (ReferenceEquals(o, null)) return true;
            else return false;
        }

        /// <summary>
        /// Reports if the collection is null or contains no items.
        /// </summary>
        /// <returns>True if the collection is null or contains no items, else false.</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> c)
        {
            if (ReferenceEquals(c, null)) return true;
            else if (c.Count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Reports if the enumerable is null or contains null items.
        /// </summary>
        /// <returns>True if the enumerable is null or contains null items, else false.</returns>
        public static bool ContainsNull<T>(this IEnumerable<T> c)
        {
            if (ReferenceEquals(c, null)) return true;
            foreach (T t in c) { if (ReferenceEquals(t, null)) return true; }
            return false;
        }

        /// <summary>
        /// Reports if the enumerable is null or contains null, empty or whitespace strings.
        /// </summary>
        /// <returns>True if the enumerable is null or contains null, empty or whitespace strings, else false.</returns>
        public static bool ContainsNullOrWhiteSpace(this IEnumerable<string> c)
        {
            if (ReferenceEquals(c, null)) return true;
            foreach (string s in c) { if (string.IsNullOrWhiteSpace(s)) return true; }
            return false;
        }
    }
}
