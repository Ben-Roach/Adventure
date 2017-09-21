using System;
using System.Collections.Generic;
using Lexicon;

namespace Sentence
{
    /// <summary>
    /// Used as the basis of performing actions on behalf of the player.
    /// </summary>
    class Verb : INode
    {
        public string OrigToken { get; }
        
        VerbSyntax[] syntaxes;
        /// <summary>SyntaxEntries that are valid for the Verb.</summary>
        public IList<VerbSyntax> Syntaxes { get { return Array.AsReadOnly(syntaxes); } }

        /// <summary>
        /// Create a new Verb INode containing syntaxes.
        /// </summary>
        /// <param name="syntaxes">SyntaxEntries that are valid for the Verb.</param>
        public Verb(string origToken, VerbSyntax[] syntaxes)
        {
            OrigToken = origToken;
            this.syntaxes = syntaxes;
        }
    }
}