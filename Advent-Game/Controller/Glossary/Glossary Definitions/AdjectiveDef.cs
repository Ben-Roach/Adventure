
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="AdjectiveNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class AdjectiveDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveDef"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="AdjectiveNode"/>.</param>
        public AdjectiveDef(string word) : base(word)
        { }

        /// <summary>
        /// Create a new <see cref="AdjectiveNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="AdjectiveNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new AdjectiveNode(origToken);
        }
    }
}
