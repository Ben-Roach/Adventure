using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Adventure.Controller
{
    /// <summary>
    /// A glossary element that represents a valid Verb usage.
    /// </summary>
    public class VerbSyntax
    {
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
        /// <summary>The <see cref="SyntFlag"/> for this syntax.</summary>
        public SyntFlag Flags { get; }

        /// <summary>Create a new <see cref="VerbSyntax"/> with two wildcard arguments.</summary>
        /// <param name="syntaxStr">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the verb is encountered.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be a <see cref="SentenceStructure.Node"/> Type.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be a <see cref="SentenceStructure.Node"/> Type.</param>
        /// <param name="flags">The <see cref="SyntFlag"/> for this syntax.</param>
        public VerbSyntax(string syntaxStr, VerbSyntaxDelegate syntaxDelegate, Type arg1, Type arg2, SyntFlag flags = SyntFlag.None)
        {
            syntax = syntaxStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Delegate = syntaxDelegate ?? throw new ArgumentNullException(nameof(syntaxDelegate));
            Arg1 = arg1 != null && typeof(Node).IsAssignableFrom(arg1) ? arg1
                : throw new ArgumentException(nameof(arg1) + " must not be null and must be a type inherited from " + nameof(Node));
            Arg2 = arg2 != null && typeof(Node).IsAssignableFrom(arg1) ? arg2
                : throw new ArgumentException(nameof(arg2) + " must not be null and must be a type inherited from " + nameof(Node));
            Flags = flags;
        }

        /// <summary>Create a new syntax entry with one wildcard argument.</summary>
        /// <param name="syntaxStr">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the verb is encountered.</param>
        /// <param name="arg1">The wildcard's Type. Will be the direct object by default. Must be an INode Type.</param>
        /// <param name="flags">The <see cref="SyntFlag"/> for this syntax.</param>
        public VerbSyntax(string syntaxStr, VerbSyntaxDelegate syntaxDelegate, Type arg1, SyntFlag flags = SyntFlag.None) :
            this(syntaxStr, syntaxDelegate, arg1, null, flags)
        { }

        /// <summary>Create a new syntax entry with no arguments.</summary>
        /// <param name="syntaxStr">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the verb is encountered.</param>
        /// <param name="flags">The <see cref="SyntFlag"/> for this syntax.</param>
        public VerbSyntax(string syntaxStr, VerbSyntaxDelegate syntaxDelegate, SyntFlag flags = SyntFlag.None) :
            this(syntaxStr, syntaxDelegate, null, null, flags)
        { }
    }
}