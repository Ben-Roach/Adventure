
using Adventure.Language;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains methods used with <see cref="VerbPhrase.VerbPhraseDelegate"/> delegates to enact a <see cref="VerbNode"/>.
    /// </summary>
    static class VerbAction
    {
        static public void Placeholder(Node directObj, Node indirectObj)
        {
            System.Console.WriteLine("VerbAction.Placeholder() has executed.");
        }
    }
}
