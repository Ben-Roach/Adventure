
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Used as the basis of specifying and finding diegetic game objects.
    /// </summary>
    public sealed class NounNode : Node
    {
        List<AdjectiveNode> containedAdjectives;
        /// <summary><see cref="AdjectiveNode"/> objects associated with the <see cref="NounNode"/>.</summary>
        public IReadOnlyCollection<AdjectiveNode> ContainedAdjectives { get => containedAdjectives.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="NounNode"/>.
        /// </summary>
        public NounNode(string origWord, string id) : base(origWord, id)
        {
            containedAdjectives = new List<AdjectiveNode>();
        }

        /// <summary>
        /// Adds an <see cref="AdjectiveNode"/> to <see cref="containedAdjectives"/>.
        /// </summary>
        /// <param name="adj">The <see cref="AdjectiveNode"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="adj"/> is null.</exception>
        public void AddAdjective(AdjectiveNode adj)
        {
            if (adj == null) throw new ArgumentNullException(nameof(adj));
            containedAdjectives.Add(adj);
        }
    }

}