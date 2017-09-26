
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lexicon
{
    /// <summary>
    /// An enumerable collection of word groups with extra associated data, with a part of speech associated with the whole Glossary.
    /// </summary>
    /// <remarks>
    /// Glossaries act as lookup tables during input processing to determine the validity of each word input by the player,
    /// while also specifying the resulting INode's Type. The Type parameter of the Glossary is directly related to its
    /// PartOfSpeech, and is intended to fill the unique property(ies) of the resulting INode object.
    /// </remarks>
    /// <typeparam name="T">The Type of the data associated with each word group.</typeparam>
    public class GlossarySection<T> : IEnumerable<Tuple<string[], T>>
    {
        private List<Tuple<string[], T>> baseCollection;
        /// <summary>The part of speech of the Glossary.</summary>
        public Type PartOfSpeech { get; private set; }

        /// <summary>
        /// Create a new Glossary.
        /// </summary>
        /// <param name="partOfSpeech">The part of speech of the Glossary. Must be an INode type.</param>
        /// <param name="entries">An enumerable KeyValuePair collection where each Key (word group) has an associated object of type T as its Value.</param>
        public GlossarySection(Type partOfSpeech)
        {
            Debug.Assert(typeof(Sentence.INode).IsAssignableFrom(partOfSpeech));
            PartOfSpeech = partOfSpeech;
            baseCollection = new List<Tuple<string[], T>>();
        }

        /// <summary>
        /// Adds a 2-Tuple to the Glossary. Should only be used during declaration.
        /// </summary>
        /// <param name="words"></param>
        /// <param name="nodeData"></param>
        public void Add(string[] words, T nodeData)
        {
            baseCollection.Add(new Tuple<string[], T>(words, nodeData));
        }

        /// <summary>
        /// Enumerates the Glossary, yielding a KeyValuePair of a word group and its associated data object.
        /// </summary>
        /// <returns>A KeyValuePair, where the Key is a word group, and the Value is its associated data.</returns>
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
