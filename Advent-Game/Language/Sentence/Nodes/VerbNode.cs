
using System;
using System.Collections.Generic;

namespace Adventure.Language
{
    /// <summary>
    /// Used as the basis of performing actions on behalf of the player.
    /// </summary>
    public sealed class VerbNode : Node
    {
        private List<VerbUsage> usages;
        /// <summary>Syntaxes that are valid for the <see cref="VerbNode"/>.</summary>
        public IReadOnlyCollection<VerbUsage> Usages { get => usages.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="VerbNode"/> that contains <see cref="VerbUsage"/> objects.
        /// </summary>
        /// <param name="usages">Represent syntaxes that are valid for the <see cref="VerbNode"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="usages"/> is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="usages"/> is null.</exception>
        public VerbNode(string origWord, string id, ICollection<VerbUsage> usages) : base(origWord, id)
        {
            if (usages == null) throw new ArgumentNullException(nameof(usages));
            else if (usages.Count == 0) throw new ArgumentException(nameof(usages), "Cannot be zero-length.");
            else if (usages.ContainsNull()) throw new ArgumentException(nameof(usages), "Cannot contain null items.");
            else this.usages = new List<VerbUsage>(usages);
        }
    }
}