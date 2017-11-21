
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="ConjunctionNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ConjunctionDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="ConjunctionDef"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="ConjunctionNode"/>.</param>
        public ConjunctionDef(string word) : base(word)
        { }

        /// <summary>
        /// Create a new <see cref="ConjunctionNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="ConjunctionNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new ConjunctionNode(origToken);
        }
    }
}
