
using System;

namespace Adventure.Language
{
    /// <summary>
    /// Flags used by <see cref="VerbUsage"/> objects.
    /// </summary>
    [Flags]
    public enum UsageFlag
    {
        None = 0,
        /// <summary><see cref="VerbUsage.Arg2"/> is the direct object, <see cref="VerbUsage.Arg2"/> is the indirect object.</summary>
        SwapArgs = 1 << 0,
        /// <summary>Split the <see cref="NounGroupNode"/> into <see cref="NounNode"/>
        /// objects and pass each to <see cref="VerbUsage.ActionDelegate"/> individually, and allow <see cref="SentenceStructure.Noun"/>
        /// in place of <see cref="SentenceStructure.NounCollection"/> in the syntax.</summary>
        MakeSingular = 1 << 1,
    }
}