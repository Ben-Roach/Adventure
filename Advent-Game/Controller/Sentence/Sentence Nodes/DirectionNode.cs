
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    public sealed class DirectionNode : Node
    {
        /// <summary>Signifies the actual direction to use.</summary>
        public DirCode DirectionCode { get; }

        /// <summary>
        /// Create a new <see cref="DirectionNode"/>.
        /// </summary>
        /// <param name="directionCode">Signifies the direction represented by the <see cref="DirectionNode"/>.</param>
        public DirectionNode(string id, string origToken, DirCode directionCode) : base(id, origToken)
        {
            DirectionCode = directionCode;
        }
    }
}