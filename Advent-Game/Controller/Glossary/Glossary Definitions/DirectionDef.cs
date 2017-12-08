
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="DirectionNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class DirectionDef : Definition
    {
        /// <summary>Signifies the direction represented by the defined <see cref="DirectionNode"/>.</summary>
        DirCode directionCode;

        /// <summary>
        /// Create a new <see cref="DirectionDef"/>.
        /// </summary>
        /// <param name="directionCode">Signifies the direction represented by the defined <see cref="DirectionNode"/>.</param>
        public DirectionDef(string id, DirCode directionCode) : base(id)
        {
            this.directionCode = directionCode;
        }

        /// <summary>
        /// Create a new <see cref="DirectionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="DirectionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new DirectionNode(origWord, ID, directionCode);
        }
    }
}
