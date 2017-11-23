
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used for verb syntax structure.
    /// </summary>
    public sealed class ParticleNode : Node
    {
        /// <summary>
        /// Create a new <see cref="ParticleNode"/>.
        /// </summary>
        public ParticleNode(string id, string origToken) : base(id, origToken)
        {

        }
    }
}