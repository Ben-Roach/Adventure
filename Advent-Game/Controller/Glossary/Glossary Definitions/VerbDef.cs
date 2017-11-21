
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="VerbNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class VerbDef : Definition
    {
        /// <summary>Syntaxes that are valid for a <see cref="VerbNode"/>.</summary>
        List<VerbSyntax> syntaxes;

        /// <summary>
        /// Create a new <see cref="VerbDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="VerbNode"/>.</param>
        /// <param name="syntaxes">Represent syntaxes that are valid for the words in <paramref name="wordGroup"/>.</param>
        public VerbDef(ICollection<string> wordGroup, ICollection<VerbSyntax> syntaxes) : base(wordGroup)
        {
            if (syntaxes == null) throw new ArgumentNullException(nameof(syntaxes));
            else if (syntaxes.Count == 0) throw new ArgumentException(nameof(syntaxes), "Cannot be zero-length.");
            else if (syntaxes.ContainsNull()) throw new ArgumentException(nameof(syntaxes), "Cannot contain null items.");
            else this.syntaxes = new List<VerbSyntax>(syntaxes);
        }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>,
        /// and that all contained <see cref="VerbSyntax"/> objects are valid.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void ValidateAndNormalize(Glossary glossary)
        {
            BaseValidateAndNormalize(glossary);
            foreach (VerbSyntax syntax in syntaxes)
                syntax.ValidateAndNormalize(glossary);
        }

        /// <summary>
        /// Create a new <see cref="VerbNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="VerbNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new VerbNode(origToken, syntaxes);
        }
    }
}
