using System;

namespace Sentence
{
    /// <summary>
    /// Represents a word used by the player for extradiegetic game control.
    /// </summary>
    class Command : INode
    {
        public string OrigToken { get; }
        /// <summary>Contains the method associated with the Command.</summary>
        public Action ActionDelegate { get; }

        /// <summary>
        /// Create a new Command INode with an associated actionDelegate.
        /// </summary>
        /// <param name="actionDelegate">The method associated with the Command.</param>
        public Command(string origToken, Action actionDelegate)
        {
            OrigToken = origToken;
            ActionDelegate = actionDelegate;
        }
    }
}
