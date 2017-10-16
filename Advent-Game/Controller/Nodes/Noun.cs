using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Used as the basis of specifying and finding diegetic game objects.
    /// </summary>
    class Noun : Node
    {
        List<Adjective> containedAdjectives;
        /// <summary><see cref="Adjective"/> objects associated with the <see cref="Noun"/>.</summary>
        public IReadOnlyCollection<Adjective> ContainedAdjectives { get { return containedAdjectives.AsReadOnly(); } }

        /// <summary>
        /// Create a new Noun INode containing no Adjectives.
        /// </summary>
        public Noun(string origToken) : base(origToken)
        {
            containedAdjectives = new List<Adjective>();
        }

        /// <summary>Adds an <see cref="Adjective"/> to <see cref="containedAdjectives"/>.</summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="adj"/> is null.</exception>
        public void AddAdjective(Adjective adj)
        {
            if (adj == null)
                throw new ArgumentNullException("Attempted to construct a " + nameof(Adjective) + " where " + nameof(adj) + " was null.");
            containedAdjectives.Add(adj);
        }
    }

}