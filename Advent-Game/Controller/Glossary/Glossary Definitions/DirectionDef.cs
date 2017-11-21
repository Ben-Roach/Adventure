
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="DirectionNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class DirectionDef : Definition
    {
        /// <summary>Represents the actual direction to use.</summary>
        DirCode directionCode;

        /// <summary>
        /// Create a new <see cref="DirectionDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="DirectionNode"/>.</param>
        /// <param name="directionCode">Signifies the direction represented by the words in <paramref name="wordGroup"/>.</param>
        public DirectionDef(ICollection<string> wordGroup, DirCode directionCode) : base(wordGroup)
        {
            this.directionCode = directionCode;
        }

        /// <summary>
        /// Create a new <see cref="DirectionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="DirectionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new DirectionNode(origToken, directionCode);
        }
    }
}
