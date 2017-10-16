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
        Adjective[] containedAdjectives;
        /// <summary><see cref="Adjective"/> objects associated with the <see cref="Noun"/>.</summary>
        public IList<Adjective> ContainedAdjectives { get { return Array.AsReadOnly(containedAdjectives); } }

        /// <summary>
        /// Create a new Noun INode containing no Adjectives.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public Noun(string origToken) : base(origToken)
        {
            containedAdjectives = new Adjective[0];
        }

        /// <summary>Add adjectives to Noun's ContainedAdjectives.</summary>
        public void AddAdjectives(Adjective[] adjectives)
        {
            containedAdjectives = adjectives.ToArray();
        }
    }

}