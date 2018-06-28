
namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="NounModifierNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class AdjectiveDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveDef"/>.
        /// </summary>
        public AdjectiveDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="NounModifierNode"/> from this definition.
        /// </summary>
        /// <returns>The new <see cref="NounModifierNode"/>, created from this definition.</returns>
        public override Node CreateNode(string origWord)
        {
            return new NounModifierNode(origWord, ID);
        }
    }
}
