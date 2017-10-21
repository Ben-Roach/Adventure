
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
        /// Adds a 2-<see cref="Tuple"/> to <paramref name="set"/>.
        /// </summary>
        /// <remarks>
        /// Used to make instantiation of a set of 2-tuples cleaner.
        /// </remarks>
        /// <param name="set">The <see cref="ISet"/> to add to.</param>
        /// <param name="item1">Item1 in the tuple to add to <paramref name="set"/>.</param>
        /// <param name="item2">Item2 item in the tuple to add to <paramref name="set"/>.</param>
        public static void Add<T1, T2>(this ISet<Tuple<T1, T2>> set, T1 item1, T2 item2)
        {
            set.Add(Tuple.Create(item1, item2));
        }
    }
}
