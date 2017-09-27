
using System;

namespace SentenceStructure
{
    /// <summary>
    /// Represents a word input by the player that is not known by the game.
    /// </summary>
    class UnknownWord : INode
    {
        public string OrigToken { get; }

        /// <summary>
        /// Create a new <see cref="UnknownWord"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public UnknownWord(string origToken)
        {
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create an UnknownWord with a null origToken.");
        }
    }
}