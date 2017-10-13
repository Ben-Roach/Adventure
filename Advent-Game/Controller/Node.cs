
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an interpretable lexical unit in a <see cref="Sentence"/>.
    /// </summary>
    public abstract class Node
    {
        /// <summary>The original word entered by the player, used primarily for error messages.</summary>
        public string OrigToken { get; }

        /// <summary>
        /// Create a new <see cref="Node"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public Node(string origToken)
        {
            OrigToken = origToken != null ? origToken
                : throw new ArgumentNullException("Attempted to create a " + nameof(Node) + " where " + nameof(origToken) + " was null.");
        }
    }
}