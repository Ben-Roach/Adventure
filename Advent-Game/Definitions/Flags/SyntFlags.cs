
using System;

/// <summary>
/// Flags used by VerbSyntax objects.
/// </summary>
[Flags]
public enum SyntFlags
{
    None = 0,
    /// <summary>Arg2 is the direct object, Arg1 is the indirect object.</summary>
    SwapArgs = 1,
    /// <summary>Split NounCollections into Nouns and pass each individually, and allow Nouns in place of NounCollections in the syntax.</summary>
    MakeSingular = 1 << 1,
}