
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an entry in the <see cref="Glossary"/>, a group of words that the player
    /// can use that should be treated as synonyms.
    /// </summary>
    public abstract class Entry
    {
        
        List<string> wordGroup;
        /// <summary>A group of words that should be treated as synonyms.</summary>
        public IReadOnlyCollection<string> WordGroup { get => wordGroup.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="Entry"/>.
        /// </summary>
        /// <param name="wordGroup">The words to include in this <see cref="Entry"/>.</param>
        public Entry(ICollection<string> wordGroup)
        {
            if (wordGroup == null) throw new ArgumentNullException(nameof(wordGroup));
            else if (wordGroup.Count == 0) throw new ArgumentException(nameof(wordGroup) + " is zero-length.");
            foreach (string s in wordGroup) { if (s == null) throw new ArgumentException(nameof(wordGroup) + " contains a null item."); }
            this.wordGroup = new List<string>(wordGroup);
        }

        /// <summary>
        /// Reports whether the <see cref="Entry"/> includes the specified word.
        /// </summary>
        /// <param name="word">The word to test.</param>
        /// <returns>True if the <see cref="Entry"/> includes <paramref name="word"/>, else false.</returns>
        public bool Contains(string word)
        {
            if (wordGroup.Contains(word))
                return true;
            return false;
        }

        /// <summary>
        /// Create a new <see cref="Node"/> using the data specified in this <see cref="Entry"/>.
        /// </summary>
        /// <param name="origToken">The token originally used by the player.</param>
        /// <returns>The new <see cref="Node"/> that represents <paramref name="origToken"/>.</returns>
        public abstract Node CreateNode(string origToken);
    }
}
