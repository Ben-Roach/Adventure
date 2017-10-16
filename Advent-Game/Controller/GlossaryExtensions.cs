
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    public static class GlossaryExtensions
    {
        public static void Add<T>(this ICollection<Tuple<string[], T>> collection, string[] item1, T item2)
        {
            collection.Add(Tuple.Create(item1, item2));
        }
    }
}
