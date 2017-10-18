
using System;
using System.Collections.Generic;

namespace Adventure
{
    /// <summary>
    /// Extension methods for classes.
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Adds a <see cref="Tuple"/> of (<paramref name="item1"/>, <paramref name="item2"/>) to <paramref name="collection"/>.
        /// Used to make instantiation of a collection of tuples cleaner.
        /// </summary>
        public static void Add<T>(this ICollection<Tuple<string[], T>> collection, string[] item1, T item2)
        {
            collection.Add(Tuple.Create(item1, item2));
        }
    }
}
