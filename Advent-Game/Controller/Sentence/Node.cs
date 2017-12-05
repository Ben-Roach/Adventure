
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents an interpretable lexical unit in a <see cref="Sentence"/>.
    /// </summary>
    public abstract class Node
    {
        /// <summary>The original word entered by the player. Used primarily for error messages.</summary>
        public string OrigWord { get; }
        /// <summary>The ID of the <see cref="Definition"/> that created this <see cref="Node"/>.
        /// Used to uniquely identify this <see cref="Node"/> in a <see cref="Sentence"/>,
        /// according to the <see cref="Definition"/> that created it.</summary>
        public string DefID { get; }

        /// <summary>
        /// Create a new <see cref="Node"/>.
        /// </summary>
        /// <param name="origWord">The original word entered by the player.</param>
        /// <param name="defID">The ID of the <see cref="Definition"/> that created this <see cref="Node"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token"/> is null.</exception>
        protected Node(string origWord, string defID)
        {
            OrigWord = origWord ?? throw new ArgumentNullException(nameof(origWord));
            DefID = defID ?? throw new ArgumentNullException(nameof(defID));
        }
    }
}