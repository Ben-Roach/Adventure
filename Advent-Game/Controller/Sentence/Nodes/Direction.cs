
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    public sealed class Direction : Node
    {
        /// <summary>Signifies the actual direction to use.</summary>
        public DirCode DirectionCode { get; }

        /// <summary>
        /// Create a new <see cref="Direction"/>.
        /// </summary>
        /// <param name="directionCode">Signifies the direction represented by the <see cref="Direction"/>.</param>
        public Direction(string origToken, DirCode directionCode) : base(origToken)
        {
            DirectionCode = directionCode;
        }
    }
}