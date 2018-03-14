
using Adventure.Language;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains methods used with <see cref="VerbUsage.VerbActionDelegate"/> delegates to enact a <see cref="VerbNode"/>.
    /// </summary>
    static class VerbAction
    {
        static public void Placeholder(Node directObj, Node indirectObj)
        {
            System.Console.WriteLine("VerbAction.Placeholder() has executed.");
        }
    }
}
