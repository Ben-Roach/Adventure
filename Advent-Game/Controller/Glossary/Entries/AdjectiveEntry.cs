
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Adjective"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class AdjectiveEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Adjective"/>.</param>
        public AdjectiveEntry(string word) : base(new string[] { word })
        { }

        /// <summary>
        /// Create a new <see cref="Adjective"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Adjective"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Adjective(origToken.ToLower());
        }
    }
}
