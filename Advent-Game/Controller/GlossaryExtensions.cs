
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlossaryExtensions
    {
        /// <summary>
        /// Used to make instantiation of each section of the <see cref="Glossary"/> cleaner.
        /// </summary>
        public static void Add<T>(this ICollection<Tuple<string[], T>> collection, string[] item1, T item2)
        {
            collection.Add(Tuple.Create(item1, item2));
        }
    }
}
