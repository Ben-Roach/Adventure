
using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an entry in the <see cref="Glossary"/>, a group of words that the player
    /// can use that should be treated as synonyms.
    /// </summary>
    public abstract class Definition : IEnumerable<string>
    {
        /// <summary>A group of words that should be treated as synonyms.</summary>
        protected List<string> wordGroup;

        /// <summary>
        /// Create a new <see cref="Definition"/>.
        /// </summary>
        /// <param name="wordGroup">The words to include in this <see cref="Definition"/>.</param>
        protected Definition(ICollection<string> wordGroup)
        {
            if (wordGroup == null) throw new ArgumentNullException(nameof(wordGroup));
            else if (wordGroup.Count == 0) throw new ArgumentException(nameof(wordGroup), "Cannot be zero-length.");
            else if (wordGroup.ContainsNullOrWhiteSpace()) throw new ArgumentException(nameof(wordGroup), "Cannot contain a null, empty, or whitespace string.");
            else this.wordGroup = new List<string>(wordGroup);
        }

        /// <summary>
        /// Create a new <see cref="Definition"/> containing only one word.
        /// </summary>
        /// <param name="word">The word to include in this <see cref="Definition"/>.</param>
        protected Definition(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            else if (string.IsNullOrWhiteSpace(word)) throw new ArgumentException(nameof(word), "Cannot be empty or whitespace.");
            else wordGroup = new List<string>() { word };
        }

        /// <summary>
        /// Reports whether the <see cref="Definition"/> includes the specified word.
        /// </summary>
        /// <param name="word">The word to test.</param>
        /// <returns>True if the <see cref="Definition"/> includes <paramref name="word"/>, else false.</returns>
        public bool Contains(string word)
        {
            if (wordGroup.Contains(word))
                return true;
            return false;
        }

        /// <summary>
        /// Create a new <see cref="Node"/> using the data specified in this <see cref="Definition"/>.
        /// </summary>
        /// <param name="origToken">The token originally used by the player.</param>
        /// <returns>The new <see cref="Node"/> that represents <paramref name="origToken"/>.</returns>
        public abstract Node CreateNode(string origToken);

        public IEnumerator<string> GetEnumerator()
        {
            foreach (string s in wordGroup)
            {
                yield return s;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
