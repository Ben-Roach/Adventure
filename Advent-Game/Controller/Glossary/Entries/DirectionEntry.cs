
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Direction"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class DirectionEntry : Entry
    {
        /// <summary>Represents the actual direction to use.</summary>
        DirCode directionCode;

        /// <summary>
        /// Create a new <see cref="DirectionEntry"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="Direction"/>.</param>
        /// <param name="directionCode">Signifies the direction represented by the words in <paramref name="wordGroup"/>.</param>
        public DirectionEntry(ICollection<string> wordGroup, DirCode directionCode) : base(wordGroup)
        {
            this.directionCode = directionCode;
        }

        /// <summary>
        /// Create a new <see cref="Direction"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Direction"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Direction(origToken, directionCode);
        }
    }
}
