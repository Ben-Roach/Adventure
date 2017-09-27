
namespace SentenceStructure
{
    /// <summary>
    /// Represents an interpretable lexical unit in a <see cref="Sentence"/>.
    /// </summary>
    public interface INode
    {
        /// <summary>The original word entered by the player, used primarily for error messages.</summary>
        string OrigToken { get; }
    }
}