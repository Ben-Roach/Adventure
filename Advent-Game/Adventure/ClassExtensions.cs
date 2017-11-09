
using System.Collections.Generic;

namespace Adventure
{
    public static class ClassExtensions
    {
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
        /// Reports if the collection is null or contains null items.
        /// </summary>
        /// <returns>True if the collection is null or contains null items, else false.</returns>
        public static bool IsNullOrContainsNull<T>(this ICollection<T> c)
        {
            if (ReferenceEquals(c, null)) return true;
            foreach (T t in c) { if (ReferenceEquals(t, null)) return true; }
            return false;
        }

        /// <summary>
        /// Reports if the collection is null or contains null, empty or whitespace strings.
        /// </summary>
        /// <returns>True if the collection is null or contains null, empty or whitespace strings, else false.</returns>
        public static bool ContainsNullOrWhiteSpace(this ICollection<string> c)
        {
            if (ReferenceEquals(c, null)) return true;
            foreach (string s in c) { if (string.IsNullOrWhiteSpace(s)) return true; }
            return false;
        }
    }
}
