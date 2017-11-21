
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        List<string> syntax;
        /// <summary>Contains wildcards and/or strings matching valid <see cref="Particle.Lemma"/> values.</summary>
        public IReadOnlyCollection<string> Syntax { get => syntax.AsReadOnly(); }
        /// <summary>The method to call on behalf of the <see cref="Verb"/>.</summary>
        public VerbSyntaxDelegate Delegate { get; }
        /// <summary>The first wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg1 { get; }
        /// <summary>The second wildcard's Type. Must be a <see cref="Node"/> type.</summary>
        public Type Arg2 { get; }
        /// <summary>The flags for this <see cref="VerbSyntax"/>.</summary>
        public SyntFlag Flags { get; }

        /// <summary>
        /// Create a new <see cref="VerbSyntax"/> with two wildcard arguments.
        /// </summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The first wildcard's Type. Will be the direct object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="arg2">The second wildcard's Type. Will be the indirect object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> or <paramref name="arg2"/> are not types inherited from <see cref="Node"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, Type arg1, Type arg2, SyntFlag flags = SyntFlag.None)
        {
            if (syntaxString == null) throw new ArgumentNullException(nameof(syntaxString));
            syntax = new List<string>(syntaxString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            Delegate = syntaxDelegate ?? throw new ArgumentNullException(nameof(syntaxDelegate));
            Arg1 = arg1 == null || typeof(Node).IsAssignableFrom(arg1) ? arg1
                : throw new ArgumentException(nameof(arg1), "Must be a type inherited from " + nameof(Node));
            Arg2 = arg2 == null || typeof(Node).IsAssignableFrom(arg1) ? arg2
                : throw new ArgumentException(nameof(arg2), "Must be a type inherited from " + nameof(Node));
            Flags = flags;
        }

        /// <summary>Create a new <see cref="VerbSyntax"/> with one wildcard argument.</summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="arg1">The wildcard's Type. Will be the direct object by default. Must be a <see cref="Node"/> Type or null.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="arg1"/> is not a type inherited from <see cref="Node"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, Type arg1, SyntFlag flags = SyntFlag.None) :
            this(syntaxString, syntaxDelegate, arg1, null, flags)
        { }

        /// <summary>Create a new <see cref="VerbSyntax"/> with no arguments.</summary>
        /// <param name="syntaxString">The represented syntax, in the form of a string.</param>
        /// <param name="syntaxDelegate">The method to call when the associated <see cref="Verb"/> is encountered is a <see cref="Sentence"/>.</param>
        /// <param name="flags">The flags for this syntax.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="syntaxString"/> or <paramref name="syntaxDelegate"/> are null.</exception>
        public VerbSyntax(string syntaxString, VerbSyntaxDelegate syntaxDelegate, SyntFlag flags = SyntFlag.None) :
            this(syntaxString, syntaxDelegate, null, null, flags)
        { }

        public void ValidateAndNormalize(Glossary glossary)
        {
            for (int i = 0; i < syntax.Count; i++)
            {
                // normalize before validating
                string word = glossary.Normalize(syntax[i]);
                if (word != glossary.SyntaxWildcard.ToString())
                {
                    // check if syntax word contains invalid characters
                    if (glossary.ContainsInvalidChar(word))
                        throw new GlossaryValidationException(word, "Syntax word contains an invalid character.");
                    // check if syntax word fails glossary validation
                    if (glossary.IsInvalidWord(word))
                        throw new GlossaryValidationException(word, "Syntax word is considered invalid by the glossary.");
                    // check that syntax word refers to a particle
                    if (!(glossary.TryGetEntryType(word, out Type t) == true && t.Equals(typeof(ParticleDef))))
                        throw new GlossaryValidationException(word, "Syntax contains non-particle word.");
                }
                // validation passed, apply normalization
                syntax[i] = word;
            }
        }
    }
}