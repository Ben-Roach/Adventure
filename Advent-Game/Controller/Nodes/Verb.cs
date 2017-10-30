
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Used as the basis of performing actions on behalf of the player.
    /// </summary>
    class Verb : Node
    {
        List<VerbSyntax> syntaxes;
        /// <summary>Syntaxes that are valid for the <see cref="Verb"/>.</summary>
        public IReadOnlyCollection<VerbSyntax> Syntaxes { get => syntaxes.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="Verb"/> that contains <see cref="VerbSyntax"/> objects.
        /// </summary>
        /// <param name="syntaxes"><see cref="VerbSyntax"/> objects that are valid for the <see cref="Verb"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="syntaxes"/> is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="syntaxes"/> is null.</exception>
        public Verb(string origToken, ICollection<VerbSyntax> syntaxes) : base(origToken)
        {
            if (syntaxes == null) throw new ArgumentNullException(nameof(syntaxes));
            else if (syntaxes.Count == 0) throw new ArgumentException(nameof(syntaxes) + " is zero-length.");
            foreach (VerbSyntax s in syntaxes) { if (s == null) throw new ArgumentException(nameof(syntaxes) + " contains a null item."); }
            this.syntaxes = new List<VerbSyntax>(syntaxes);
        }
    }
}