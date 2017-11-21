
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="ParticleNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ParticleDef : Definition
    {
        /// <summary>The commonly understood form of the words in <see cref="Definition.WordGroup"/>, used to test if
        /// one of those words is used by the player.</summary>
        string lemma;

        /// <summary>
        /// Create a new <see cref="ParticleDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="ParticleNode"/>.</param>
        /// <param name="lemma">The commonly understood form or synonym of the words in <paramref name="wordGroup"/>.</param>
        public ParticleDef(ICollection<string> wordGroup, string lemma) : base(wordGroup)
        {
            this.lemma = lemma ?? throw new ArgumentNullException(nameof(lemma));
        }

        /// <summary>
        /// Create a new <see cref="ParticleNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="ParticleNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new ParticleNode(origToken, lemma);
        }
    }
}
