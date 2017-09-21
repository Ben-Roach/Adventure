
namespace Sentence
{
    /// <summary>
    /// Represents an interpretable lexical unit in a sentence.
    /// </summary>
    public interface INode
    {
        /// <summary>The original word entered by the player, used primarily for error messages.</summary>
        string OrigToken { get; }
    }
}