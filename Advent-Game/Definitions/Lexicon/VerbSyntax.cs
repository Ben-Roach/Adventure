using System;
using System.Diagnostics;

namespace Lexicon
{
    /// <summary>
    /// A glossary element that represents a valid Verb usage.
    /// </summary>
    class VerbSyntax
    {
        /// <summary>Contains wildcards (asterisks) and/or specific word lemmas as an array of strings.</summary>
        public string[] Syntax { get; }
        /// <summary>The method to call on behalf of the <see cref="Verb"/>. First parameter is direct object, second parameter is indirect object.</summary>
        public Action<Sentence.INode, Sentence.INode> SyntaxDelegate { get; }
        /// <summary>The flags for this syntax.</summary>
        public SyntFlags Flags { get; }
        /// <summary>The first wildcard's INode Type.</summary>
        public Type Arg1 { get; }
        /// <summary>The second wildcard's INode Type.</summary>
        public Type Arg2 { get; }

        /// <summary>Create a new syntax entry with two wildcard arguments.</summary>
        /// <param name="syntaxStr">The represented Syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call on behalf of the <see cref="Verb"/>.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be an INode Type.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be an INode Type.</param>
        public VerbSyntax(string syntaxStr, Action<Sentence.INode, Sentence.INode> syntaxDelegate, Type arg1, Type arg2, SyntFlags flags = SyntFlags.None)
        {
            Debug.Assert(typeof(Sentence.INode).IsAssignableFrom(arg1) || arg1 == null);
            Debug.Assert(typeof(Sentence.INode).IsAssignableFrom(arg2) || arg2 == null);
            Syntax = syntaxStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            SyntaxDelegate = syntaxDelegate;
            Flags = flags;
            Arg1 = arg1;
            Arg2 = arg2;
        }

        /// <summary>Create a new syntax entry with one wildcard argument.</summary>
        /// <param name="syntaxStr">The represented Syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The syntax's associated VerbAction method.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <param name="arg1">The wildcard's Type. Will be the direct object by default. Must be an INode Type.</param>
        public VerbSyntax(string syntaxStr, Action<Sentence.INode, Sentence.INode> syntaxDelegate, Type arg1, SyntFlags flags = SyntFlags.None) :
            this(syntaxStr, syntaxDelegate, arg1, null, flags)
        { }

        /// <summary>Create a new syntax entry with no arguments.</summary>
        /// <param name="syntaxStr">The represented Syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The syntax's associated VerbAction method.</param>
        /// <param name="flags">The flags for this syntax.</param>
        public VerbSyntax(string syntaxStr, Action<Sentence.INode, Sentence.INode> syntaxDelegate, SyntFlags flags = SyntFlags.None) :
            this(syntaxStr, syntaxDelegate, null, null, flags)
        { }
    }
}