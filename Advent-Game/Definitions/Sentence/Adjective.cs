
namespace Sentence
{
    /// <summary>
    /// Added to Nouns to make them more specific.
    /// </summary>
    class Adjective : INode
    {
        public string OrigToken { get; }

        /// <summary>
        /// Create a new Adjective INode.
        /// </summary>
        public Adjective(string origToken)
        {
            OrigToken = origToken;
        }
    }
}