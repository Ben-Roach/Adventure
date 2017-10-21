using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Adventure.Controller
{
    /// <summary>
    /// A <see cref="Glossary"/> element that represents a valid usage of a <see cref="Verb"/> in a <see cref="Sentence"/>.
    /// </summary>
    public class VerbSyntax
    {
        /// <summary>
        /// Meant to contain the method to call when the <see cref="Verb"/> associated with this
        /// <see cref="VerbSyntax"/> is encountered in a <see cref="Sentence"/>.
        /// </summary>
        /// <param name="directObject">The first argument of the method. Should be the direct object of the verb (more or less).</param>
        /// <param name="indirectObject">The second argument of the method. Should be the indirect object of the verb (more or less).</param>
        public delegate void VerbSyntaxDelegate(Node directObject, Node indirectObject);

        string[] syntax;
        /// <summary>Contains wildcards (asterisks) and/or strings matching valid <see cref="SentenceStructure.Particle.Lemma"/> values.</summary>
        public ReadOnlyCollection<string> Syntax { get => Array.AsReadOnly(syntax); }
        /// <summary>The method to call on behalf of the <see cref="Verb"/>.</summary>
        public VerbSyntaxDelegate Delegate { get; }
        /// <summary>The first wildcard's Type. Must be a <see cref="SentenceStructure.Node"/> type.</summary>
        public Type Arg1 { get; }
        /// <summary>The second wildcard's Type. Must be a <see cref="SentenceStructure.Node"/> type.</summary>
        public Type Arg2 { get; }
        /// <summary>The flags for this syntax.</summary>
        public SyntFlag Flags { get; }

        /// <summary>Create a new <see cref="VerbSyntax"/> with two wildcard arguments.</summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string. Wildcards are represented by asterisks (*).</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be a <see cref="SentenceStructure.Node"/> Type.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be a <see cref="SentenceStructure.Node"/> Type.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> or <paramref name="arg2"/> are not types inherited from <see cref=Node"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, Type arg1, Type arg2, SyntFlag flags = SyntFlag.None)
        {
            syntax = syntaxString != null ? syntaxString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                : throw new ArgumentNullException(nameof(syntaxString));
            Delegate = syntaxDelegate ?? throw new ArgumentNullException(nameof(syntaxDelegate));
            Arg1 = typeof(Node).IsAssignableFrom(arg1) ? arg1
                : throw new ArgumentException(nameof(arg1) + " must be a type inherited from " + nameof(Node));
            Arg2 = typeof(Node).IsAssignableFrom(arg1) ? arg2
                : throw new ArgumentException(nameof(arg2) + " must be a type inherited from " + nameof(Node));
            Flags = flags;
        }

        /// <summary>Create a new syntax entry with one wildcard argument.</summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string. Wildcards are represented by asterisks (*).</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The wildcard's Type. Will be the direct object by default. Must be an INode Type.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> is not a type inherited from <see cref=Node"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, Type arg1, SyntFlag flags = SyntFlag.None) :
            this(syntaxString, syntaxDelegate, arg1, null, flags)
        { }

        /// <summary>Create a new syntax entry with no arguments.</summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string. Wildcards are represented by asterisks (*).</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, SyntFlag flags = SyntFlag.None) :
            this(syntaxString, syntaxDelegate, null, null, flags)
        { }
    }
}