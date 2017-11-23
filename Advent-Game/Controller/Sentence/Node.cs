
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an interpretable lexical unit in a <see cref="Sentence"/>.
    /// </summary>
    public abstract class Node
    {
        /// <summary>The ID of the <see cref="Definition"/> that created this <see cref="Node"/>.
        /// Used to uniquely identify this <see cref="Node"/> in a <see cref="Sentence"/>.</summary>
        public string ID { get; }
        /// <summary>The original word entered by the player. Used primarily for error messages.</summary>
        public string OrigToken { get; }

        /// <summary>
        /// Create a new <see cref="Node"/>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Definition"/> that created this <see cref="Node"/>.</param>
        /// <param name="origToken">The original word entered by the player.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="origToken"/> is null.</exception>
        protected Node(string id, string origToken)
        {
            ID = id ?? throw new ArgumentNullException(nameof(id));
            OrigToken = origToken ?? throw new ArgumentNullException(nameof(origToken));
        }
    }
}