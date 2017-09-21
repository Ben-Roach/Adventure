
namespace Sentence
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    class Direction : INode
    {
        public string OrigToken { get; }

        public string DirCode { get; }

        /// <summary>
        /// Create a new Direction INode with an associated dirCode.
        /// </summary>
        /// <param name="dirCode">A short string representing the actual direction to consider.</param>
        public Direction(string origToken, string dirCode)
        {
            OrigToken = origToken;
            DirCode = dirCode;
        }
    }
}