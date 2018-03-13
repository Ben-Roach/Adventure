
namespace Adventure.Language
{
    /// <summary>
    /// Used with <see cref="NounNode"/> objects to make them more specific.
    /// May be formed from Adjectives (<see cref="AdjectiveDef"/>) or Noun Adjuncts (<see cref="NounNode"/>).
    /// </summary>
    public sealed class NounModifierNode : Node
    {
        /// <summary>
        /// Create a new <see cref="NounModifierNode"/>.
        /// </summary>
        public NounModifierNode(string origWord, string id) : base(origWord, id)
        { }

        /// <summary>
        /// Create a new <see cref="NounModifierNode"/> from a <see cref="NounNode"/>.
        /// Used when a <see cref="NounNode"/> is acting as a noun adjunct.
        /// </summary>
        /// <param name="noun">The noun node to create a new <see cref="NounModifierNode"/> from.</param>
        /// <returns>A new <see cref="NounModifierNode"/> based on the given <see cref="NounNode"/>.</returns>
        public static NounModifierNode FromNoun(NounNode noun)
        {
            return new NounModifierNode(noun.OrigWord, noun.ID);
        }
    }
}