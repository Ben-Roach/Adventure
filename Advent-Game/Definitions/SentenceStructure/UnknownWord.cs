
using System;

namespace SentenceStructure
{
    /// <summary>
    /// Represents a word input by the player that is not known by the game.
    /// </summary>
    class UnknownWord : Node
    {
        /// <summary>
        /// Create a new <see cref="UnknownWord"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public UnknownWord(string origToken) : base(origToken)
        { }
    }
}