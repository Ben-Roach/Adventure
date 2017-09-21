
namespace Sentence
{
    /// <summary>
    /// Represents a word input by the player that could not be found in a glossary.
    /// </summary>
    class UnknownWord : INode
    {
        public string OrigToken { get; }

        /// <summary>
        /// Create a new UnknownWord INode.
        /// </summary>
        public UnknownWord(string origToken)
        {
            OrigToken = origToken;
        }
    }
}