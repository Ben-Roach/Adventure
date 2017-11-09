
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
        protected Entry(ICollection<string> wordGroup)
        {
            if (wordGroup.IsNull()) throw new ArgumentNullException(nameof(wordGroup));
            else if (wordGroup.IsNullOrEmpty()) throw new ArgumentException(nameof(wordGroup), "Cannot be zero-length.");
            else if (wordGroup.ContainsNullOrWhiteSpace()) throw new ArgumentException(nameof(wordGroup), "Cannot contain a null, empty, or whitespace string.");
            else this.wordGroup = new List<string>(wordGroup);
        }

        /// <summary>
        /// Create a new <see cref="Entry"/> containing only one word.
        /// </summary>
        /// <param name="word">The word to include in this <see cref="Entry"/>.</param>
        protected Entry(string word)
        {
            if (word.IsNull()) throw new ArgumentNullException(nameof(word));
            else if (string.IsNullOrWhiteSpace(word)) throw new ArgumentException(nameof(word), "Cannot be empty or whitespace.");
            else wordGroup = new List<string>() { word };
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
        /// Calls <see cref="BaseValidateAndNormalize(Glossary, Type)"/>.
        /// <para>Can be overridden to set different validation rules, but always call
        /// <see cref="BaseValidateAndNormalize(Glossary, Type)"/> in the override first.</para>
        /// </summary>
        /// <seealso cref="BaseValidateAndNormalize(Glossary, Type)"/>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public virtual void ValidateAndNormalize(Glossary glossary)
        {
            BaseValidateAndNormalize(glossary);
        }

        /// <summary>
        /// Validates that this <see cref="Entry"/> can be added to <paramref name="glossary"/>, and normalizes the
        /// words in <see cref="wordGroup"/> if so.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        /// <param name="exceptedEntryType">No exception is thrown if a word in this <see cref="Entry"/>
        /// is found in another <see cref="Entry"/> of this type in <paramref name="glossary"/>.</param>
        protected void BaseValidateAndNormalize(Glossary glossary, Type exceptedEntryType = null)
        {
            for (int i = 0; i < wordGroup.Count; i++)
            {
                // normalize before validating
                string word = glossary.Normalize(wordGroup[i]);
                // check if word contains invalid characters
                if (glossary.ContainsInvalidChar(word))
                    throw new GlossaryValidationException(word, "Word contains an invalid character.");
                // check if word fails glossary validation
                if (glossary.IsInvalidWord(word))
                    throw new GlossaryValidationException(word, "Word is considered invalid by the glossary.");
                // check for duplicates
                if (glossary.TryGetEntryType(word, out Type t) == true && (exceptedEntryType.IsNull() || !t.Equals(exceptedEntryType)))
                    throw new GlossaryValidationException(word, "Attempted to add a duplicate word.");
                // validation passed, apply normalization
                wordGroup[i] = word;
            }
        }

        /// <summary>
        /// Create a new <see cref="Node"/> using the data specified in this <see cref="Entry"/>.
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
