
using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an entry in the <see cref="Glossary"/>, a group of words that the player
    /// can use that should be treated as synonyms.
    /// </summary>
    public abstract class Entry : IEnumerable<string>
    {
        /// <summary>A group of words that should be treated as synonyms.</summary>
        protected List<string> wordGroup;

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
        /// Create a new <see cref="Entry"/> containing only one word.
        /// </summary>
        /// <param name="word">The word to include in this <see cref="Entry"/>.</param>
        public Entry(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            else if (word == "") throw new ArgumentException(nameof(word) + " is an empty string.");
            wordGroup = new List<string>() { word };
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
        /// Calls <see cref="BaseValidation(Glossary, Type)"/>.
        /// <para>Can be overridden to set different validation rules, but always call
        /// <see cref="BaseValidation(Glossary, Type)"/> in the override first.</para>
        /// </summary>
        /// <seealso cref="BaseValidation(Glossary, Type)"/>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public virtual void Validate(Glossary glossary)
        {
            BaseValidation(glossary);
        }

        /// <summary>
        /// Throws exception if the words in this <see cref="Entry"/> are already contained in <paramref name="glossary"/>,
        /// Unless <paramref name="glossary"/> contains it in an <see cref="Entry"/> of type <paramref name="exceptedEntryType"/>.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        /// <param name="exceptedEntryType">No exception is thrown if a word in the <see cref="Entry"/>
        /// is found in another <see cref="Entry"/> of this type in <paramref name="glossary"/></param>
        protected void BaseValidation(Glossary glossary, Type exceptedEntryType = null)
        {
            foreach (string word in wordGroup)
            {
                if (glossary.TryGetEntryType(word, out Type t) == true)
                    throw new GlossaryValidationException(word);
            }
        }

        /// <summary>
        /// Create a new <see cref="Node"/> using the data specified in this <see cref="Entry"/>.
        /// </summary>
        /// <param name="origToken">The token originally used by the player.</param>
        /// <returns>The new <see cref="Node"/> that represents <paramref name="origToken"/>.</returns>
        public abstract Node CreateNode(string origToken);

        /// <summary>
        /// Enumerates the words in the <see cref="Entry"/>.
        /// </summary>
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
