
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Noun"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class NounEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="NounEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Noun"/>.</param>
        public NounEntry(string word) : base(word)
        { }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>,
        /// unless it already exists in another <see cref="NounEntry"/>.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void Validate(Glossary glossary)
        {
            BaseValidation(glossary, typeof(NounEntry));
        }

        /// <summary>
        /// Create a new <see cref="Noun"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Noun"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Noun(origToken);
        }
    }
}
