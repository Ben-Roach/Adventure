
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Verb"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class VerbEntry : Entry
    {
        /// <summary>Syntaxes that are valid for a <see cref="Verb"/>.</summary>
        List<VerbSyntax> syntaxes;

        /// <summary>
        /// Create a new <see cref="VerbEntry"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="Verb"/>.</param>
        /// <param name="syntaxes">Represent syntaxes that are valid for the words in <paramref name="wordGroup"/>.</param>
        public VerbEntry(ICollection<string> wordGroup, ICollection<VerbSyntax> syntaxes) : base(wordGroup)
        {
            if (syntaxes == null) throw new ArgumentNullException(nameof(syntaxes));
            else if (syntaxes.Count == 0) throw new ArgumentException(nameof(syntaxes) + " is zero-length.");
            foreach (VerbSyntax s in syntaxes) { if (s == null) throw new ArgumentException(nameof(syntaxes) + " contains a null item."); }
            this.syntaxes = new List<VerbSyntax>(syntaxes);
        }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>,
        /// and that all contained <see cref="VerbSyntax"/> objects are valid.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void Validate(Glossary glossary)
        {
            foreach (string word in WordGroup)
            {
                if (glossary.TryGetEntryType(word, out Type t) == true)
                    throw new GlossaryValidationException(word);
            }
            foreach (VerbSyntax syntax in syntaxes)
                syntax.Validate(glossary);
        }

        /// <summary>
        /// Create a new <see cref="Verb"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Verb"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Verb(origToken, syntaxes);
        }
    }
}
