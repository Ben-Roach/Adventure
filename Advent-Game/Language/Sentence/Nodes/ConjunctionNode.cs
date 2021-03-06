﻿
namespace Adventure.Language
{
    /// <summary>
    /// Represents a word used for sentence structure.
    /// </summary>
    public sealed class ConjunctionNode : Node
    {
        /// <summary>
        /// Create a new <see cref="ConjunctionNode"/>.
        /// </summary>
        public ConjunctionNode(string origWord, string id) : base(origWord, id)
        { }
    }
}