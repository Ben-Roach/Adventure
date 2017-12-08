
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="ParticleNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class ParticleDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="ParticleDef"/>.
        /// </summary>
        public ParticleDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="ParticleNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="ParticleNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new ParticleNode(origWord, ID);
        }
    }
}
