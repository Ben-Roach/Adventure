
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
        public string DefID { get; }
        /// <summary>The original word entered by the player. Used primarily for error messages.</summary>
        public string OrigWord { get; }

        /// <summary>
        /// Create a new <see cref="Node"/>.
        /// </summary>
        /// <param name="token">Derived from the word initially entered by the player.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token"/> is null.</exception>
        protected Node(Token token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            DefID = token.LookupWord;
            OrigWord = token.OrigWord;
        }
    }
}