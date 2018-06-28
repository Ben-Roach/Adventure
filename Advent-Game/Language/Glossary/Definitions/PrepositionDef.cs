
namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="PrepositionNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class PrepositionDef : Definition
    {
        /// <summary>Signifies the direction/location represented.</summary>
        PrepFlag prepType;

        /// <summary>
        /// Create a new <see cref="PrepositionDef"/>.
        /// </summary>
        /// <param name="prepType">Signifies the direction/location represented.</param>
        public PrepositionDef(string id, PrepFlag prepType) : base(id)
        {
            this.prepType = prepType;
        }

        /// <summary>
        /// Create a new <see cref="PrepositionNode"/> from this definition.
        /// </summary>
        /// <returns>The new <see cref="PrepositionNode"/>, created from this definition.</returns>
        public override Node CreateNode(string origWord)
        {
            return new PrepositionNode(origWord, ID, prepType);
        }
    }
}
