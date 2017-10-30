
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Noun"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class NounEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="NounEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Noun"/>.</param>
        public NounEntry(string word) : base(new string[] { word })
        { }

        /// <summary>
        /// Create a new <see cref="Noun"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Noun"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Noun(origToken.ToLower());
        }
    }
}
