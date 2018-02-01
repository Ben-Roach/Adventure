
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="DirectionNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class DirectionDef : Definition
    {
        /// <summary>Signifies the direction represented by the defined <see cref="DirectionNode"/>.</summary>
        DirFlag directionFlag;

        /// <summary>
        /// Create a new <see cref="DirectionDef"/>.
        /// </summary>
        /// <param name="directionFlag">Signifies the direction represented by the defined <see cref="DirectionNode"/>.</param>
        public DirectionDef(string id, DirFlag directionFlag) : base(id)
        {
            this.directionFlag = directionFlag;
        }

        /// <summary>
        /// Create a new <see cref="DirectionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="DirectionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new DirectionNode(origWord, ID, directionFlag);
        }
    }
}
