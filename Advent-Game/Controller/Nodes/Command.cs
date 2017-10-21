
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player for extradiegetic game control.
    /// </summary>
    class Command : Node
    {
        /// <summary>The method to call on behalf of the <see cref="Command"/>.</summary>
        public Action Delegate { get; }

        /// <summary>
        /// Create a new <see cref="Command"/>.
        /// </summary>
        /// <param name="commandDelegate">The method to call on behalf of the <see cref="Command"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="commandDelegate"/> is null.</exception>
        public Command(string origToken, Action commandDelegate) : base(origToken)
        {
            Delegate = commandDelegate ?? throw new ArgumentNullException("Attempted to construct a " + nameof(Direction) + " where " + nameof(commandDelegate) + " was null.");
        }
    }
}
