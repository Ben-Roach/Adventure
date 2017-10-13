
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Adventure.Controller
{
    /// <summary>
    /// An enumerable collection of word groups with extra associated data, with an associated <see cref="SentenceStructure.Node"/> type.
    /// </summary>
    /// <remarks>
    /// <see cref="GlossarySection{T}"/> objects act as lookup tables during input processing to determine the validity of each word
    /// input by the player, while also specifying the resulting INode's Type. The <typeparamref name="T"/> parameter of the Glossary is directly related
    /// to its <see cref="PartOfSpeech"/>, and is intended to fill the unique property of the resulting <see cref="SentenceStructure.Node"/> object.
    /// </remarks>
    /// <typeparam name="T">The Type of the data associated with each word group.</typeparam>
    public class GlossarySection<T> : IEnumerable<Tuple<string[], T>>
    {
        /// <summary>The base <see cref="List{T}"/> of <see cref="Tuple{T1, T2}"/> objects, where T1 is a word group, and T2 is extra data.</summary>
        private List<Tuple<string[], T>> baseList;
        /// <summary>The part of speech of the <see cref="GlossarySection{T}"/>.</summary>
        public Type PartOfSpeech { get; private set; }

        /// <summary>
        /// Create a new <see cref="GlossarySection{T}"/>.
        /// </summary>
        /// <param name="partOfSpeech">The part of speech of the <see cref="GlossarySection{T}"/>. Must be an <see cref="SentenceStructure.Node"/> type.</param>
        /// <param name="entries">An enumerable 2-Tuple collection where Item1 is a word group, and Item2 will fill the unique property
        /// of the intended <see cref="SentenceStructure.Node"/>.</param>
        public GlossarySection(Type partOfSpeech)
        {
            Debug.Assert(typeof(Controller.Node).IsAssignableFrom(partOfSpeech));
            PartOfSpeech = partOfSpeech;
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
        /// <returns>A 2-Tuple, where Item1 is a word group, and Item2 will fill the unique property of the intended <see cref="SentenceStructure.Node"/>.</returns>
        public IEnumerator<Tuple<string[], T>> GetEnumerator()
        {
            foreach (Tuple<string[], T> t in baseList)
            { yield return t; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
