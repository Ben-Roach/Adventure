
using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents the definition of a headword in the <see cref="Glossary"/>.
    /// </summary>
    public abstract class Definition
    {
        /// <summary>The unique name of this <see cref="Definition"/>, used to
        /// identify any <see cref="Node"/> created by it.</summary>
        public string ID { get; }

        /// <summary>
        /// Create a new <see cref="Definition"/>.
        /// </summary>
        /// <param name="id">The unique name of this <see cref="Definition"/>, used to
        /// identify any <see cref="Node"/> created by it.</param>
        public Definition(string id)
        {
            ID = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Create a new <see cref="Node"/> from a <see cref="Token"/> and the data specified
        /// in this <see cref="Definition"/>.
        /// </summary>
        /// <param name="token">The <see cref="Token"/> representing the word used by the player.</param>
        /// <returns>The new <see cref="Node"/> created from <paramref name="token"/> and this <see cref="Definition"/>.</returns>
        public abstract Node CreateNode(Token token);
    }
}
