using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentence
{
    /// <summary>
    /// Represents a collection of nouns used collectively by the player.
    /// </summary>
    class NounCollection : INode
    {
        /// <summary>For NounCollections, this is the OrigToken of the first Noun in ContainedNouns.</summary>
        public string OrigToken { get; }

        Noun[] containedNouns;
        /// <summary>Group of Nouns contained within the NounCollection.</summary>
        public IList<Noun> ContainedNouns { get { return Array.AsReadOnly(containedNouns); } }

        /// <summary>
        /// Create a new NounCollection INode containing nouns.
        /// </summary>
        /// <param name="nouns">The Nouns contained in the NounCollection.</param>
        public NounCollection(Noun[] nouns)
        {
            OrigToken = nouns[0].OrigToken;
            containedNouns = nouns.ToArray();
        }
    }
}