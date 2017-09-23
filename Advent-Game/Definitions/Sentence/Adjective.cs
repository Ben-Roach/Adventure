
using System;

namespace Sentence
{
    /// <summary>
    /// Added to <see cref="Noun.ContainedAdjectives"/> to make the <see cref="Noun"/> more specific.
    /// </summary>
    class Adjective : INode
    {
        public string OrigToken { get; }

        /// <summary>
        /// Create a new <see cref="Adjective"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public Adjective(string origToken)
        {
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create an Adjective with a null origToken.");
        }
    }
}