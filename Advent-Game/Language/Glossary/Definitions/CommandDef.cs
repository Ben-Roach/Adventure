
using System;

namespace Adventure.Language
{
    /// <summary>
    /// Represents a known <see cref="CommandNode"/> definition in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class CommandDef : Definition
    {
        /// <summary>The method to call on behalf of the <see cref="CommandNode"/>.</summary>
        public Action CommandDelegate { get; }

        /// <summary>
        /// Create a new <see cref="CommandDef"/>.
        /// </summary>
        /// <param name="commandDelegate">The method to call on behalf of the <see cref="CommandNode"/>.</param>
        public CommandDef(string id, Action commandDelegate) : base(id)
        {
            CommandDelegate = commandDelegate ?? throw new ArgumentNullException(nameof(commandDelegate));
        }

        /// <summary>
        /// Create a new <see cref="CommandNode"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="CommandNode"/>, created from this entry.</returns>
        public override Node CreateNode(string origWord)
        {
            return new CommandNode(origWord, ID, CommandDelegate);
        }
    }
}
