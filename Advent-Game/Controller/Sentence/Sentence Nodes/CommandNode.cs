
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player for extradiegetic game control.
    /// </summary>
    public sealed class CommandNode : Node
    {
        /// <summary>The method to call on behalf of the <see cref="CommandNode"/>.</summary>
        public Action Delegate { get; }

        /// <summary>
        /// Create a new <see cref="CommandNode"/>.
        /// </summary>
        /// <param name="commandDelegate">The method to call on behalf of the <see cref="CommandNode"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="commandDelegate"/> is null.</exception>
        public CommandNode(Token token, Action commandDelegate) : base(token)
        {
            Delegate = commandDelegate ?? throw new ArgumentNullException(nameof(commandDelegate));
        }
    }
}
