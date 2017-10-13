
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    class Direction : Node
    {
        /// <summary>Represents the actual direction to use.</summary>
        public DirCodes DirCode { get; }

        /// <summary>
        /// Create a new <see cref="Direction"/>.
        /// </summary>
        /// <param name="dirCode">Represents the actual direction to use.</param>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public Direction(string origToken, DirCodes dirCode) : base(origToken)
        {
            DirCode = dirCode;
        }
    }
}