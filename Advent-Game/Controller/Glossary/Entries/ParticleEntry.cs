
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Particle"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ParticleEntry : Entry
    {
        /// <summary>The commonly understood form of the words in <see cref="Entry.WordGroup"/>, used to test if
        /// one of those words is used by the player.</summary>
        string lemma;

        /// <summary>
        /// Create a new <see cref="ParticleEntry"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="Particle"/>.</param>
        /// <param name="lemma">The commonly understood form or synonym of the words in <paramref name="wordGroup"/>.</param>
        public ParticleEntry(ICollection<string> wordGroup, string lemma) : base(wordGroup)
        {
            this.lemma = lemma ?? throw new ArgumentNullException(nameof(lemma));
        }

        /// <summary>
        /// Create a new <see cref="Particle"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Particle"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Particle(origToken, lemma);
        }
    }
}
