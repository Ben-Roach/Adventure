
namespace Adventure.Controller
{
    /// <summary>
    /// Used with <see cref="NounNode"/> objects to make them more specific.
    /// </summary>
    public sealed class AdjectiveNode : Node
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveNode"/>.
        /// </summary>
        public AdjectiveNode(string origToken) : base(origToken)
        { }
    }
}