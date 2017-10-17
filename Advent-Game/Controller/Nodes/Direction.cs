
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    class Direction : Node
    {
        /// <summary>Represents the actual direction to use.</summary>
        public DirCode DirCode { get; }

        /// <summary>
        /// Create a new <see cref="Direction"/>.
        /// </summary>
        /// <param name="dirCode">Represents the represented direction.</param>
        public Direction(string origToken, DirCode dirCode) : base(origToken)
        {
            DirCode = dirCode;
        }
    }
}