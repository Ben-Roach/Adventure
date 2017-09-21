using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentence
{
    /// <summary>
    /// Used as the basis of specifying and finding diegetic game objects.
    /// </summary>
    class Noun : INode
    {
        public string OrigToken { get; }
        Adjective[] containedAdjectives;
        /// <summary>List of Adjectives associated with the Noun.</summary>
        public IList<Adjective> ContainedAdjectives { get { return Array.AsReadOnly(containedAdjectives); } }

        /// <summary>
        /// Create a new Noun INode with containing no Adjectives.
        /// </summary>
        public Noun(string origToken)
        {
            OrigToken = origToken;
            containedAdjectives = new Adjective[0];
        }

        /// <summary>Add adjectives to Noun's ContainedAdjectives.</summary>
        public void AddAdjectives(Adjective[] adjectives)
        {
            containedAdjectives = adjectives.ToArray();
        }
    }

}