
using System.Collections.Generic;

namespace Adventure
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Reports if the enumerable is null or contains null items.
        /// </summary>
        /// <returns>True if the enumerable is null or contains null items, else false.</returns>
        public static bool ContainsNull<T>(this IEnumerable<T> c)
        {
            if (c == null) return true;
            foreach (T t in c) { if (t == null) return true; }
            return false;
        }

        /// <summary>
        /// Reports if the enumerable is null or contains null, empty or whitespace strings.
        /// </summary>
        /// <returns>True if the enumerable is null or contains null, empty or whitespace strings, else false.</returns>
        public static bool ContainsNullOrWhiteSpace(this IEnumerable<string> c)
        {
            if (c == null) return true;
            foreach (string s in c) { if (string.IsNullOrWhiteSpace(s)) return true; }
            return false;
        }
    }
}
