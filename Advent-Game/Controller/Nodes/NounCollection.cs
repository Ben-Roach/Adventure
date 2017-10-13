using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a collection of nouns used collectively by the player.
    /// </summary>
    class NounCollection : Node
    {
        Noun[] containedNouns;
        /// <summary>The contained <see cref="Noun"/> objects.</summary>
        public IList<Noun> ContainedNouns { get { return Array.AsReadOnly(containedNouns); } }

        /// <summary>
        /// Create a new <see cref="NounCollection"/> containing <see cref="Noun"/> objects.
        /// </summary>
        /// <param name="nouns">The <see cref="Noun"/> objects contained in the <see cref="NounCollection"/>.</param>
        /// <exception cref="ArgumentException">Thrown when nouns is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when nouns is null.</exception>
        public NounCollection(Noun[] nouns) : base(nouns[0].OrigToken)
        {
            if (nouns == null) throw new ArgumentNullException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " was null.");
            else if (nouns.Length == 0) throw new ArgumentException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " was zero-length.");
            foreach (Noun n in nouns)
            { if (n == null) throw new ArgumentException("Attempted to create a " + nameof(NounCollection) + " where " + nameof(nouns) + " containined a null item."); }
            containedNouns = nouns.ToArray();
        }
    }
}