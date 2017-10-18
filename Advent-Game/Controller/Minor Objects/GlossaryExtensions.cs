
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Extension methods for the <see cref="Glossary"/>.
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Adds a <see cref="Tuple"/> of (<paramref name="item1"/>, <paramref name="item2"/>) to <paramref name="collection"/>.
        /// Used to make instantiation of each section of the <see cref="Glossary"/> cleaner.
        /// </summary>
        /// <typeparam name="T">The type of the property associated with the word group and <see cref="Node"/>.</typeparam>
        /// <param name="item1">The word group of the entry.</param>
        /// <param name="item2">The property associated with the word group and the resulting <see cref="Node"/>.</param>
        public static void Add<T>(this ICollection<Tuple<string[], T>> collection, string[] item1, T item2)
        {
            collection.Add(Tuple.Create(item1, item2));
        }
    }
}
