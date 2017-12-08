
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="AdjectiveNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class AdjectiveDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="AdjectiveDef"/>.
        /// </summary>
        public AdjectiveDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="AdjectiveNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="AdjectiveNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new AdjectiveNode(origWord, ID);
        }
    }
}
