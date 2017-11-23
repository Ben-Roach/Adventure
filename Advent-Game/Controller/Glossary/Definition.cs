
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
        protected string id;

        /// <summary>
        /// Create a new <see cref="Definition"/>.
        /// </summary>
        /// <param name="id">The unique name of this <see cref="Definition"/>, used to
        /// identify any <see cref="Node"/> created by it.</param>
        public Definition(string id)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Create a new <see cref="Node"/> using the data specified in this <see cref="Definition"/>.
        /// </summary>
        /// <param name="origToken">The token originally used by the player.</param>
        /// <returns>The new <see cref="Node"/> that represents <paramref name="origToken"/>.</returns>
        public abstract Node CreateNode(string origToken);
    }
}
