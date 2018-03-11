
namespace Adventure.Language
{
    /// <summary>
    /// Used with <see cref="NounNode"/> objects to make them more specific.
    /// </summary>
    public sealed class AdjectiveNode : Node
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveNode"/>.
        /// </summary>
        public AdjectiveNode(string origWord, string id) : base(origWord, id)
        { }
    }
}