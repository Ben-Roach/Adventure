
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="CommandNode"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class CommandDef : Definition
    {
        /// <summary>The method to call on behalf of the <see cref="CommandNode"/>.</summary>
        Action commandDelegate;

        /// <summary>
        /// Create a new <see cref="CommandDef"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="CommandNode"/>.</param>
        /// <param name="commandDelegate">The method to call on behalf of the words in <paramref name="wordGroup"/>.</param>
        public CommandDef(ICollection<string> wordGroup, Action commandDelegate) : base(wordGroup)
        {
            this.commandDelegate = commandDelegate ?? throw new ArgumentNullException(nameof(commandDelegate));
        }

        /// <summary>
        /// Create a new <see cref="CommandNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="CommandNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new CommandNode(origToken, commandDelegate);
        }
    }
}
