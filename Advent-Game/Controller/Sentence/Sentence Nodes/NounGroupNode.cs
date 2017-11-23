using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an ordered collection of nouns used collectively by the player.
    /// </summary>
    public sealed class NounGroupNode : Node
    {
        List<NounNode> containedNouns;
        /// <summary>The contained <see cref="NounNode"/> objects, in the order used by the player.</summary>
        public IReadOnlyCollection<NounNode> ContainedNouns { get => containedNouns.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="NounGroupNode"/> containing <paramref name="nouns"/>.
        /// </summary>
        /// <param name="nouns">The <see cref="NounNode"/> objects contained in the <see cref="NounGroupNode"/>.</param>
        /// <exception cref="ArgumentException">Thrown when nouns is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="nouns"/> is null.</exception>
        public NounGroupNode(ICollection<NounNode> nouns) : base(nouns.ToList()[0].ID, nouns.ToList()[0].OrigToken)
        {
            if (nouns == null) throw new ArgumentNullException(nameof(nouns));
            else if (nouns.Count == 0) throw new ArgumentException(nameof(nouns), "Cannot be zero-length.");
            else if (nouns.ContainsNull()) throw new ArgumentException(nameof(nouns), "Cannot contain null items.");
            else containedNouns = nouns.ToList();
        }
    }
}