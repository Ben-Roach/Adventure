
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Used for sentence and syntax structure.
    /// </summary>
    class Particle : Node
    {
        /// <summary>The commonly understood form of <see cref="OrigToken"/>, used to test if a specific word or synonym was used by the player.</summary>
        public string Lemma { get; }

        /// <summary>
        /// Create a new <see cref="Particle"/>.
        /// </summary>
        /// <param name="lemma">The commonly understood form or synonym of the word.</param>
        /// <exception cref="ArgumentNullException">Thrown when origToken or lemma is null.</exception>
        public Particle(string origToken, string lemma) : base(origToken)
        {
            Lemma = lemma != null ? lemma : throw new ArgumentNullException("Attempted to create a " + nameof(Particle) + " where " + nameof(lemma) + " was null.");
        }
    }
}