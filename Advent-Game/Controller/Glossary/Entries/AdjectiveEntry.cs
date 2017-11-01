
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Adjective"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class AdjectiveEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Adjective"/>.</param>
        public AdjectiveEntry(string word) : base(word)
        { }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>,
        /// unless it already exists in another <see cref="AdjectiveEntry"/>.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void Validate(Glossary glossary)
        {
            foreach (string word in WordGroup)
            {
                if (glossary.TryGetEntryType(word, out Type t) == true && t != typeof(AdjectiveEntry))
                {
                    throw new GlossaryValidationException(word);
                }
            }
        }

        /// <summary>
        /// Create a new <see cref="Adjective"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Adjective"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Adjective(origToken);
        }
    }
}
