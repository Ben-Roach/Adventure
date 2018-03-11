
namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="ConjunctionNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ConjunctionDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="ConjunctionDef"/>.
        /// </summary>
        public ConjunctionDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="ConjunctionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="ConjunctionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new ConjunctionNode(origWord, ID);
        }
    }
}
