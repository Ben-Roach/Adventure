
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lexicon
{
    /// <summary>
    /// An enumerable collection of word groups with extra associated data, with an associated <see cref="SentenceStructure.INode"/> type.
    /// </summary>
    /// <remarks>
    /// <see cref="GlossarySection{T}"/> objects act as lookup tables during input processing to determine the validity of each word
    /// input by the player, while also specifying the resulting INode's Type. The <typeparamref name="T"/> parameter of the Glossary is directly related
    /// to its <see cref="PartOfSpeech"/>, and is intended to fill the unique property of the resulting <see cref="SentenceStructure.INode"/> object.
    /// </remarks>
    /// <typeparam name="T">The Type of the data associated with each word group.</typeparam>
    public class GlossarySection<T> : IEnumerable<Tuple<string[], T>>
    {
        private List<Tuple<string[], T>> baseCollection;
        /// <summary>The part of speech of the Glossary.</summary>
        public Type PartOfSpeech { get; private set; }

        /// <summary>
        /// Create a new <see cref="GlossarySection{T}"/>.
        /// </summary>
        /// <param name="partOfSpeech">The part of speech of the <see cref="GlossarySection{T}"/>. Must be an <see cref="SentenceStructure.INode"/> type.</param>
        /// <param name="entries">An enumerable 2-Tuple collection where Item1 is a word group, and Item2 will fill the unique property
        /// of the intended <see cref="SentenceStructure.INode"/>.</param>
        public GlossarySection(Type partOfSpeech)
        {
            Debug.Assert(typeof(SentenceStructure.INode).IsAssignableFrom(partOfSpeech));
            PartOfSpeech = partOfSpeech;
            baseCollection = new List<Tuple<string[], T>>();
        }

        /// <summary>
        /// Adds a 2-Tuple to the <see cref="GlossarySection{T}"/>. Should only be used during declaration.
        /// </summary>
        /// <param name="words"></param>
        /// <param name="nodeData"></param>
        public void Add(string[] words, T nodeData)
        {
            baseCollection.Add(new Tuple<string[], T>(words, nodeData));
        }

        /// <summary>
        /// Enumerates the <see cref="GlossarySection{T}"/>, yielding a 2-Tuple of a word group and its associated data object.
        /// </summary>
        /// <returns>A 2-Tuple, where Item1 is a word group, and Item2 will fill the unique property of the intended <see cref="SentenceStructure.INode"/>.</returns>
        public IEnumerator<Tuple<string[], T>> GetEnumerator()
        {
            foreach (Tuple<string[], T> pair in baseCollection)
            {
                yield return pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
