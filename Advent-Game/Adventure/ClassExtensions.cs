
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
        /// Adds a <see cref="Tuple"/> to <paramref name="set"/>.
        /// Used to make instantiation of a set of tuples cleaner.
        /// </summary>
        public static void Add<T1, T2>(this ISet<Tuple<T1, T2>> set, T1 item1, T2 item2)
        {
            set.Add(Tuple.Create(item1, item2));
        }
    }
}
