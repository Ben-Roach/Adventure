
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Flags used by <see cref="VerbSyntax"/> objects.
    /// </summary>
    [Flags]
    public enum SyntFlag
    {
        None = 0,
        /// <summary><see cref="VerbSyntax.Arg2"/> is the direct object, <see cref="VerbSyntax.Arg2"/> is the indirect object.</summary>
        SwapArgs = 1 << 0,
        /// <summary>Split the <see cref="SentenceStructure.NounCollection"/> into <see cref="SentenceStructure.Noun"/>
        /// objects and pass each to <see cref="VerbSyntax.Delegate"/> individually, and allow <see cref="SentenceStructure.Noun"/>
        /// in place of <see cref="SentenceStructure.NounCollection"/> in the syntax.</summary>
        MakeSingular = 1 << 1,
    }
}