
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Conjunction"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ConjunctionEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="ConjunctionEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Conjunction"/>.</param>
        public ConjunctionEntry(string word) : base(word)
        { }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void Validate(Glossary glossary)
        {
            foreach (string word in WordGroup)
            {
                if (glossary.TryGetEntryType(word, out Type t) == true)
                    throw new GlossaryValidationException(word);
            }
        }

        /// <summary>
        /// Create a new <see cref="Conjunction"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Conjunction"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Conjunction(origToken);
        }
    }
}
