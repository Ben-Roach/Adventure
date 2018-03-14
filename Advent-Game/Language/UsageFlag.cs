
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
        /// <summary><see cref="VerbUsage.Arg2"/> is the direct object, <see cref="VerbUsage.Arg1"/> is the
        /// indirect object (opposite of default).</summary>
        SwapArgs = 1 << 0,
        /// <summary>if <see cref="VerbUsage.Arg1"/> is a <see cref="NounGroupNode"/>, iterate through
        /// the contained <see cref="NounNode"/> objects and pass each to <see cref="VerbUsage.ActionDelegate"/>
        /// individually (while passing <see cref="VerbUsage.Arg2"/> intact each iteration).
        /// <para><see cref="VerbUsage.Arg1"/> may also be a <see cref="NounNode"/> with no consiquence.</para></summary>
        MakeArg1Singular = 1 << 1,
        /// <summary>if <see cref="VerbUsage.Arg2"/> is a <see cref="NounGroupNode"/>, iterate through
        /// the contained <see cref="NounNode"/> objects and pass each to <see cref="VerbUsage.ActionDelegate"/>
        /// individually (while passing <see cref="VerbUsage.Arg1"/> intact each iteration).
        /// <para><see cref="VerbUsage.Arg2"/> may also be a <see cref="NounNode"/> with no consiquence.
        /// Will be ignored if <see cref="MakeArg1Singular"/> is set.</para></summary>
        MakeArg2Singular = 1 << 2,
    }
}