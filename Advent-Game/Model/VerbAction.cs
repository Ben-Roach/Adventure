﻿
using Adventure.Controller;

namespace Adventure.Model
{
    /// <summary>
    /// Contains methods used with <see cref="VerbSyntax.VerbSyntaxDelegate"/> delegates to enact a <see cref="Verb"/>.
    /// </summary>
    static class VerbAction
    {
        static public void Placeholder(Node directObj, Node indirectObj)
        {
            System.Console.WriteLine("VerbAction.Placeholder() has executed.");
        }
    }
}