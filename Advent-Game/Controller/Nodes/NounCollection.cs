using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an ordered collection of nouns used collectively by the player.
    /// </summary>
    class NounCollection : Node
    {
        List<Noun> containedNouns;
        /// <summary>The contained <see cref="Noun"/> objects, in the order used by the player.</summary>
        public IReadOnlyCollection<Noun> ContainedNouns { get => containedNouns.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="NounCollection"/> containing <paramref name="nouns"/>.
        /// </summary>
        /// <param name="nouns">The <see cref="Noun"/> objects contained in the <see cref="NounCollection"/>.</param>
        /// <exception cref="ArgumentException">Thrown when nouns is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="nouns"/> is null.</exception>
        public NounCollection(ICollection<Noun> nouns) : base(nouns.ToList()[0].OrigToken)
        {
            if (nouns == null) throw new ArgumentNullException(nameof(nouns));
            else if (nouns.Count == 0) throw new ArgumentException(nameof(nouns) + "is zero-length.");
            foreach (Noun n in nouns) { if (n == null) throw new ArgumentException(nameof(nouns) + "contains a null item."); }
            containedNouns = nouns.ToList();
        }
    }
}