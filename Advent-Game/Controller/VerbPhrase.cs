
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// A <see cref="Glossary"/> element that represents a verb phrase in a <see cref="Sentence"/>, preceeded by a <see cref="VerbNode"/>.
    /// </summary>
    public class VerbPhrase
    {
        /// <summary>
        /// The method to call when a <see cref="VerbPhrase"/> is encountered.
        /// </summary>
        /// <param name="directObject">The first argument of the method. Should be the direct object (more or less).</param>
        /// <param name="indirectObject">The second argument of the method. Should be the indirect object (more or less).</param>
        public delegate void VerbPhraseDelegate(Node directObject, Node indirectObject);

        List<string> structure;
        /// <summary>Contains wildcards and/or strings matching valid <see cref="Node.ID"/> values.</summary>
        public IReadOnlyCollection<string> Structure { get => structure.AsReadOnly(); }
        /// <summary>The method to call when this <see cref="VerbPhrase"/> is encountered in a <see cref="Sentence"/>.</summary>
        public VerbPhraseDelegate VerbDelegate { get; }
        /// <summary>The first wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg1 { get; }
        /// <summary>The second wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg2 { get; }
        /// <summary>The flags for this <see cref="VerbPhrase"/>.</summary>
        public SyntFlag Flags { get; }
        /// <summary>The length of the <see cref="VerbPhrase"/>.</summary>
        public int Length { get => structure.Count; }

        /// <summary>
        /// Create a new <see cref="VerbPhrase"/> with two wildcard arguments.
        /// </summary>
        /// <param name="structure">The represented phrase structure, in the form of a string.</param>
        /// <param name="verbDelegate">The method to call when this <see cref="VerbPhrase"/> is encountered in a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="flags">The flags for this <see cref="VerbPhrase"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> or <paramref name="arg2"/> are not types inherited from
        /// <see cref="Node"/> or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="structure"/> or <paramref name="verbDelegate"/> are null.</exception>
        public VerbPhrase(string structure, VerbPhraseDelegate verbDelegate, Type arg1=null, Type arg2=null, SyntFlag flags=SyntFlag.None)
        {
            if (structure == null) throw new ArgumentNullException(nameof(structure));
            this.structure = new List<string>(structure.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            VerbDelegate = verbDelegate ?? throw new ArgumentNullException(nameof(verbDelegate));
            Arg1 = (arg1 == null || typeof(Node).IsAssignableFrom(arg1)) ? arg1
                : throw new ArgumentException(nameof(arg1), "Must be a type inherited from " + nameof(Node));
            Arg2 = (arg2 == null || typeof(Node).IsAssignableFrom(arg2)) ? arg2
                : throw new ArgumentException(nameof(arg2), "Must be a type inherited from " + nameof(Node));
            Flags = flags;
        }
    }
}