
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
        List<VerbUsage> Usages;

        /// <summary>
        /// Create a new <see cref="VerbDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="VerbNode"/>.</param>
        /// <param name="usages">Represents valid verb phrases for the verbs in <paramref name="wordGroup"/>.</param>
        public VerbDef(string id, ICollection<VerbUsage> usages) : base(id)
        {
            if (usages == null) throw new ArgumentNullException(nameof(usages));
            else if (usages.Count == 0) throw new ArgumentException(nameof(usages), "Cannot be zero-length.");
            else if (usages.ContainsNull()) throw new ArgumentException(nameof(usages), "Cannot contain null items.");
            else Usages = new List<VerbUsage>(usages);
        }

        /// <summary>
        /// Create a new <see cref="VerbNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="VerbNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new VerbNode(origWord, ID, Usages);
        }
    }
}
