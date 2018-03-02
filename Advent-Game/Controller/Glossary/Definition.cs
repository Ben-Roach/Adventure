
using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents the definition of a word.
    /// </summary>
    public abstract class Definition
    {
        /// <summary>The unique name of this <see cref="Definition"/>, used to
        /// identify any <see cref="Node"/> using it.</summary>
        public string ID { get; }

        /// <summary>
        /// Create a new <see cref="Definition"/>.
        /// </summary>
        /// <param name="id">The unique name of this <see cref="Definition"/>, used to
        /// identify any <see cref="Node"/> using it.</param>
        public Definition(string id)
        {
            ID = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Create a new <see cref="Node"/> from a string (the originally input word) and the data specified
        /// in this <see cref="Definition"/>.
        /// </summary>
        /// <param name="origWord">The original word used by the player.</param>
        /// <returns>The new <see cref="Node"/> created from <paramref name="token"/> and this <see cref="Definition"/>.</returns>
        public abstract Node CreateNode(string origWord);
    }
}
