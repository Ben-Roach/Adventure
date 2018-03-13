
using System;
using System.Collections.Generic;

namespace Adventure.Language
{
    /// <summary>
    /// Used as the basis of specifying and finding diegetic game objects.
    /// </summary>
    public sealed class NounNode : Node
    {
        List<NounModifierNode> modifiers = new List<NounModifierNode>();
        /// <summary><see cref="Node"/> objects that modify the <see cref="NounNode"/>.</summary>
        public IReadOnlyCollection<Node> Modifiers { get => modifiers.AsReadOnly(); }

        /// <summary>
        /// Create a new <see cref="NounNode"/>.
        /// </summary>
        public NounNode(string origWord, string id) : base(origWord, id)
        { }

        /// <summary>
        /// Adds a <see cref="NounModifierNode"/> to the <see cref="Noun"/> object's <see cref="Modifiers"/>.
        /// </summary>
        /// <param name="modifier">The <see cref="NounModifierNode"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public void AddModifier(NounModifierNode modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            modifiers.Add(modifier);
        }
    }

}