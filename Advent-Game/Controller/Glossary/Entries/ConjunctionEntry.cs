
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Conjunction"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ConjunctionEntry : Entry
    {
        /// <summary>
        /// Create a new <see cref="ConjunctionEntry"/>.
        /// </summary>
        /// <param name="word">The word that represents the new known <see cref="Conjunction"/>.</param>
        public ConjunctionEntry(string word) : base(word)
        { }

        /// <summary>
        /// Create a new <see cref="Conjunction"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Conjunction"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Conjunction(origToken);
        }
    }
}
