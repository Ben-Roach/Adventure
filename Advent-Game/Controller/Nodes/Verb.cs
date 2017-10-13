
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Used as the basis of performing actions on behalf of the player.
    /// </summary>
    class Verb : Node
    {
        VerbSyntax[] syntaxes;
        /// <summary>Syntaxes that are valid for the <see cref="Verb"/>.</summary>
        public IList<VerbSyntax> Syntaxes { get { return Array.AsReadOnly(syntaxes); } }

        /// <summary>
        /// Create a new <see cref="Verb"/> that contains <see cref="VerbSyntax"/> objects.
        /// </summary>
        /// <param name="syntaxes"><see cref="VerbSyntax"/> objects that are valid for the <see cref="Verb"/>.</param>
        /// <exception cref="ArgumentException">Thrown when syntaxes is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when origToken or syntaxes is null.</exception>
        public Verb(string origToken, VerbSyntax[] syntaxes) : base(origToken)
        {
            if (syntaxes == null) throw new ArgumentNullException("Attempted to create a " + nameof(Verb) + " where " + nameof(syntaxes) + " was null.");
            else if (syntaxes.Length == 0) throw new ArgumentException("Attempted to create a " + nameof(Verb) + " where " + nameof(syntaxes) + " was zero-length.");
            foreach (VerbSyntax s in syntaxes)
            {
                if (s == null) throw new ArgumentException("Attempted to create a " + nameof(Verb) + " where " + nameof(syntaxes) + " contained a null item.");
            }
            this.syntaxes = syntaxes;
        }
    }
}