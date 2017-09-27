using System;
using System.Collections.Generic;
using System.Linq;

namespace SentenceStructure
{
    /// <summary>
    /// Represents a collection of nouns used collectively by the player.
    /// </summary>
    class NounCollection : INode
    {
        /// <summary><see cref="Noun.OrigToken"/> of the first <see cref="Noun"/> in <see cref="ContainedNouns"/>.</summary>
        public string OrigToken { get { return containedNouns[0].OrigToken; } }

        Noun[] containedNouns;
        /// <summary>The contained <see cref="Noun"/> objects.</summary>
        public IList<Noun> ContainedNouns { get { return Array.AsReadOnly(containedNouns); } }

        /// <summary>
        /// Create a new <see cref="NounCollection"/> containing <see cref="Noun"/> objects.
        /// </summary>
        /// <param name="nouns">The <see cref="Noun"/> objects contained in the <see cref="NounCollection"/>.</param>
        /// <exception cref="ArgumentException">Thrown when nouns is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when nouns is null.</exception>
        public NounCollection(Noun[] nouns)
        {
            if (nouns == null) throw new ArgumentNullException("Attempted to create a NounCollection with a null array.");
            else if (nouns.Length == 0) throw new ArgumentException("Attempted to create a NounCollection with a zero-length array.");
            foreach (Noun n in nouns)
            { if (n == null) throw new ArgumentException("Attempted to create a NounCollection with an array containing a null item."); }
            containedNouns = nouns.ToArray();
        }
    }
}