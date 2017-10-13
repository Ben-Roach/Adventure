
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used by the player for extradiegetic game control.
    /// </summary>
    class Command : Node
    {
        /// <summary>The method to call on behalf of the <see cref="Command"/>.</summary>
        public Action ActionDelegate { get; }

        /// <summary>
        /// Create a new <see cref="Command"/>.
        /// </summary>
        /// <param name="actionDelegate">The method to call on behalf of the <see cref="Command"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when origToken or actionDelegate is null.</exception>
        public Command(string origToken, Action actionDelegate) : base(origToken)
        {
            ActionDelegate = actionDelegate != null ? actionDelegate
                : throw new ArgumentNullException("Attempted to construct a " + nameof(Direction) + " where " + nameof(actionDelegate) + " was null.");
        }
    }
}
