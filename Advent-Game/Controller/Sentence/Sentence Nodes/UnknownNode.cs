
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word input by the player that is not in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class UnknownNode : Node
    {
        /// <summary>
        /// Create a new <see cref="UnknownNode"/>.
        /// </summary>
        public UnknownNode(Token token) : base(token)
        { }
    }
}