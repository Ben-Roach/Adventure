
namespace Adventure.Language
{
    /// <summary>
    /// Represents a word used for verb phrase structure.
    /// </summary>
    public sealed class PrepositionNode : Node
    {
        /// <summary>
        /// Create a new <see cref="PrepositionNode"/>.
        /// </summary>
        public PrepositionNode(string origWord, string id) : base(origWord, id)
        { }
    }
}