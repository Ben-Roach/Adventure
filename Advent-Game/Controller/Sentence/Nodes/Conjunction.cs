﻿
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used for sentence structure.
    /// </summary>
    public sealed class Conjunction : Node
    {
        /// <summary>
        /// Create a new <see cref="Conjunction"/>.
        /// </summary>
        public Conjunction(string origToken) : base(origToken)
        { }
    }
}