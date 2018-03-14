
namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="PrepositionNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class PrepositionDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="PrepositionDef"/>.
        /// </summary>
        public PrepositionDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="PrepositionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="PrepositionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new PrepositionNode(origWord, ID);
        }
    }
}
