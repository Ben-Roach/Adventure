
namespace Adventure.Language
{
    /// <summary>
    /// Represents a word used to specify a location or direction.
    /// </summary>
    public sealed class PrepositionNode : Node
    {
        /// <summary>Signifies the actual direction/location represented.</summary>
        public PrepFlag PrepType { get; }

        /// <summary>
        /// Create a new <see cref="PrepositionNode"/>.
        /// </summary>
        /// <param name="prepType">Signifies the direction/location represented.</param>
        public PrepositionNode(string origWord, string id, PrepFlag prepType) : base(origWord, id)
        {
            PrepType = prepType;
        }
    }
}