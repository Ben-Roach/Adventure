
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Adventure.Controller
{
    /// <summary>
    /// An enumerable collection of word groups with extra associated data, with an associated <see cref="Node"/> type.
    /// </summary>
    /// <remarks>
    /// <see cref="GlossarySection{T}"/> objects act as lookup tables during input processing to determine the validity of each word
    /// input by the player. The <typeparamref name="T"/> parameter is directly related to the resulting <see cref="Node"/> object's
    /// type, and is intended to fill the unique property of the <see cref="Node"/>.
    /// </remarks>
    /// <typeparam name="T">The Type of the data associated with each word group.</typeparam>
    public class GlossarySection<T> : IEnumerable<Tuple<string[], T>>
    {
        /// <summary>The base <see cref="List{T}"/> of <see cref="Tuple{T1, T2}"/> objects, where T1 is a word group, and T2 is extra data.</summary>
        private List<Tuple<string[], T>> baseList;

        /// <summary>
        /// Create a new <see cref="GlossarySection{T}"/>.
        /// </summary>
        public GlossarySection()
        {
            baseList = new List<Tuple<string[], T>>();
        }

        /// <summary>
        /// Adds a 2-Tuple entry to the <see cref="GlossarySection{T}"/>. Should only be used during declaration.
        /// </summary>
        /// <param name="wordGroup">The word group of the entry.</param>
        /// <param name="nodeData"></param>
        public void Add(string[] wordGroup, T nodeData)
        {
            baseList.Add(new Tuple<string[], T>(wordGroup, nodeData));
        }

        /// <summary>
        /// Enumerates the <see cref="GlossarySection{T}"/>, yielding a 2-Tuple of a word group and its associated data object.
        /// </summary>
        /// <returns>A 2-Tuple, where Item1 is a word group, and Item2 will fill the unique property of the intended <see cref="Node"/>.</returns>
        public IEnumerator<Tuple<string[], T>> GetEnumerator()
        {
            foreach (Tuple<string[], T> t in baseList)
            { yield return t; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
