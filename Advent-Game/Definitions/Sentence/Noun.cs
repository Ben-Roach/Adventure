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
        /// <summary><see cref="Adjective"/> objects associated with the <see cref="Noun"/>.</summary>
        public IList<Adjective> ContainedAdjectives { get { return Array.AsReadOnly(containedAdjectives); } }

        /// <summary>
        /// Create a new Noun INode containing no Adjectives.
        /// </summary>
        public Noun(string origToken)
        {
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create a Verb object with a null origToken.");
            containedAdjectives = new Adjective[0];
        }

        /// <summary>
        /// Create a new <see cref="Noun"/> <see cref="INode"/> containing <see cref="Adjective"/> objects.
        /// </summary>
        /// <param name="adjectives">The <see cref="Adjective"/> objects contained in and related to the <see cref="Noun"/>.</param>
        /// <exception cref="ArgumentException">Thrown when adjectives is zero length or contains null items.</exception>
        /// <exception cref="ArgumentNullException">Thrown when origToken or adjectives is null.</exception>
        public Noun(string origToken, Adjective[] adjectives)
        {
            if (adjectives == null) throw new ArgumentNullException("Attempted to create a Noun with a null array.");
            else if (adjectives.Length == 0) throw new ArgumentException("Attempted to create a Noun with a zero-length array.");
            foreach (Adjective a in adjectives)
            { if (a == null) throw new ArgumentException("Attempted to create a Noun with an array containing a null item."); }
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create a Noun with a null origToken.");
            containedAdjectives = adjectives;
        }

        /// <summary>Add adjectives to Noun's ContainedAdjectives.</summary>
        public void AddAdjectives(Adjective[] adjectives)
        {
            containedAdjectives = adjectives.ToArray();
        }
    }

}