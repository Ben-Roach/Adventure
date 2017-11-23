
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="NounNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class NounDef : Definition
    {
        /// <summary>
        /// Create a new <see cref="NounDef"/>.
        /// </summary>
        public NounDef(string id) : base(id)
        { }

        /// <summary>
        /// Create a new <see cref="NounNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="NounNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new NounNode(id, origToken);
        }
    }
}
