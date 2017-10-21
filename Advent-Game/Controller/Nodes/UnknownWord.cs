﻿
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word input by the player that is not in the <see cref="Glossary"/>.
    /// </summary>
    class UnknownWord : Node
    {
        /// <summary>
        /// Create a new <see cref="UnknownWord"/>.
        /// </summary>
        public UnknownWord(string origToken) : base(origToken)
        { }
    }
}