
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="NounNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class NounDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="NounDef"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="NounNode"/>.</param>
        public NounDef(string word) : base(word)
        { }

        /// <summary>
        /// Create a new <see cref="NounNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="NounNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new NounNode(origToken);
        }
    }
}
