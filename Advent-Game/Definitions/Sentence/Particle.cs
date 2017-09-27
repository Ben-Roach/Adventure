
using System;

namespace SentenceStructure
{
    /// <summary>
    /// Used for sentence and syntax structure.
    /// </summary>
    class Particle : INode
    {
        public string OrigToken { get; }
        /// <summary>The commonly understood form of <see cref="OrigToken"/>, used to test if a specific word or synonym was used by the player.</summary>
        public string Lemma { get; }

        /// <summary>
        /// Create a new <see cref="Particle"/>.
        /// </summary>
        /// <param name="lemma">The commonly understood form or synonym of the word.</param>
        /// <exception cref="ArgumentNullException">Thrown when origToken or lemma is null.</exception>
        public Particle(string origToken, string lemma)
        {
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create a Particle with a null origToken.");
            Lemma = lemma != null ? lemma : throw new ArgumentNullException("Attempted to create a Particle with a null lemma.");
        }
    }
}