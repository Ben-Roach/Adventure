
using System;
using System.Collections.Generic;

namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="VerbNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class VerbDef : Definition
    {
        /// <summary>Syntaxes that are valid for a <see cref="VerbNode"/>.</summary>
        List<VerbPhrase> syntaxes;

        /// <summary>
        /// Create a new <see cref="VerbDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="VerbNode"/>.</param>
        /// <param name="syntaxes">Represent syntaxes that are valid for the words in <paramref name="wordGroup"/>.</param>
        public VerbDef(string id, ICollection<VerbPhrase> syntaxes) : base(id)
        {
            if (syntaxes == null) throw new ArgumentNullException(nameof(syntaxes));
            else if (syntaxes.Count == 0) throw new ArgumentException(nameof(syntaxes), "Cannot be zero-length.");
            else if (syntaxes.ContainsNull()) throw new ArgumentException(nameof(syntaxes), "Cannot contain null items.");
            else this.syntaxes = new List<VerbPhrase>(syntaxes);
        }

        /// <summary>
        /// Create a new <see cref="VerbNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="VerbNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new VerbNode(origWord, ID, syntaxes);
        }
    }
}
