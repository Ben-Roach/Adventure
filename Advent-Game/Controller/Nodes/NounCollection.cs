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
        /// <summary>The contained <see cref="Noun"/> objects, in order.</summary>
        public IReadOnlyCollection<Noun> ContainedNouns { get => containedNouns.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="NounCollection"/> containing <paramref name="nouns"/>.
        /// </summary>
        /// <param name="nouns">The <see cref="Noun"/> objects contained in the <see cref="NounCollection"/>.</param>
        /// <exception cref="ArgumentException">Thrown when nouns is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="nouns"/> is null.</exception>
        public NounCollection(IList<Noun> nouns) : base(nouns[0].OrigToken)
        {
            if (nouns == null) throw new ArgumentNullException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " was null.");
            else if (nouns.Count == 0) throw new ArgumentException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " was zero-length.");
            foreach (Noun n in nouns)
            { if (n == null) throw new ArgumentException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " containined a null item."); }
            containedNouns = nouns.ToList();
        }
    }
}