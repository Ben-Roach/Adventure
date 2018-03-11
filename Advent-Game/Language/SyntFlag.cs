
using System;

namespace Adventure.Language
{
    /// <summary>
    /// Flags used by <see cref="VerbPhrase"/> objects.
    /// </summary>
    [Flags]
    public enum SyntFlag
    {
        None = 0,
        /// <summary><see cref="VerbPhrase.Arg2"/> is the direct object, <see cref="VerbPhrase.Arg2"/> is the indirect object.</summary>
        SwapArgs = 1 << 0,
        /// <summary>Split the <see cref="SentenceStructure.NounCollection"/> into <see cref="SentenceStructure.Noun"/>
        /// objects and pass each to <see cref="VerbPhrase.VerbDelegate"/> individually, and allow <see cref="SentenceStructure.Noun"/>
        /// in place of <see cref="SentenceStructure.NounCollection"/> in the syntax.</summary>
        MakeSingular = 1 << 1,
    }
}