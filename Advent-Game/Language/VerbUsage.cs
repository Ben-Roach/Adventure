
using System;
using System.Collections.Generic;

namespace Adventure.Language
{
    /// <summary>
    /// A <see cref="Glossary"/> element that represents a valid verb usage (verb phrase) in a <see cref="Sentence"/>,
    /// preceeded by a <see cref="VerbNode"/>.
    /// </summary>
    public class VerbUsage
    {
        /// <summary>
        /// The method to call when a <see cref="VerbUsage"/> is used.
        /// </summary>
        /// <param name="directObject">The first argument of the method. Should be the direct object (more or less).</param>
        /// <param name="indirectObject">The second argument of the method. Should be the indirect object (more or less).</param>
        public delegate void VerbActionDelegate(Node directObject, Node indirectObject);

        List<string> structure;
        /// <summary>Contains wildcards and/or strings matching valid <see cref="Node.ID"/> values.</summary>
        public IReadOnlyCollection<string> Structure { get => structure.AsReadOnly(); }
        /// <summary>The method to call when this <see cref="VerbUsage"/> is used in a <see cref="Sentence"/>.</summary>
        public VerbActionDelegate ActionDelegate { get; }
        /// <summary>The first wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg1 { get; }
        /// <summary>The second wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg2 { get; }
        /// <summary>The flags for this <see cref="VerbUsage"/>.</summary>
        public UsageFlag Flags { get; }
        /// <summary>The length of the <see cref="VerbUsage"/>.</summary>
        public int Length => structure.Count;

        /// <summary>
        /// Create a new <see cref="VerbUsage"/> with two wildcard arguments.
        /// </summary>
        /// <param name="structure">The represented phrase structure, in the form of a string.</param>
        /// <param name="verbDelegate">The method to call when this <see cref="VerbUsage"/> is encountered in a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="flags">The flags for this <see cref="VerbUsage"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> or <paramref name="arg2"/> are not types inherited from
        /// <see cref="Node"/> or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="structure"/> or <paramref name="verbDelegate"/> are null.</exception>
        public VerbUsage(string structure, VerbActionDelegate verbDelegate, Type arg1=null, Type arg2=null, UsageFlag flags=UsageFlag.None)
        {
            if (structure == null) throw new ArgumentNullException(nameof(structure));
            this.structure = new List<string>(structure.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            ActionDelegate = verbDelegate ?? throw new ArgumentNullException(nameof(verbDelegate));
            Arg1 = (arg1 == null || typeof(Node).IsAssignableFrom(arg1)) ? arg1
                : throw new ArgumentException(nameof(arg1), "Must be a type inherited from " + nameof(Node));
            Arg2 = (arg2 == null || typeof(Node).IsAssignableFrom(arg2)) ? arg2
                : throw new ArgumentException(nameof(arg2), "Must be a type inherited from " + nameof(Node));
            Flags = flags;
        }
    }
}