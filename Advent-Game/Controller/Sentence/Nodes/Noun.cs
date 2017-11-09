
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Used as the basis of specifying and finding diegetic game objects.
    /// </summary>
    public sealed class Noun : Node
    {
        List<Adjective> containedAdjectives;
        /// <summary><see cref="Adjective"/> objects associated with the <see cref="Noun"/>.</summary>
        public IReadOnlyCollection<Adjective> ContainedAdjectives { get => containedAdjectives.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="Noun"/>.
        /// </summary>
        public Noun(string origToken) : base(origToken)
        {
            containedAdjectives = new List<Adjective>();
        }

        /// <summary>
        /// Adds an <see cref="Adjective"/> to <see cref="containedAdjectives"/>.
        /// </summary>
        /// <param name="adj">The <see cref="Adjective"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="adj"/> is null.</exception>
        public void AddAdjective(Adjective adj)
        {
            if (adj.IsNull()) throw new ArgumentNullException(nameof(adj));
            containedAdjectives.Add(adj);
        }
    }

}