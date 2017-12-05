
using Adventure.Controller;
using Adventure.View;

namespace Adventure.Model
{
    /// <summary>
    /// Methods used to interpret <see cref="Sentence"/> objects.
    /// </summary>
    static class SentenceInterpreter
    {
        /// <summary>
        /// Attempts to interpret a <see cref="Sentence"/>. Initiates game model manipulation.
        /// </summary>
        /// <param name="sentence">The <see cref="Sentence"/> to interpret.</param>
        public static void Interpret(Sentence sentence)
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] is ConjunctionNode)
                    continue;

                else if (sentence[i] is CommandNode command)
                    command.Delegate();

                else if (sentence[i] is VerbNode verb)
                {
                    foreach(VerbSyntax syntax in verb.Syntaxes)
                    {

                    }
                    // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                    // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                    // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                    // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
                }

                else if (sentence[i] is UnknownNode)
                {
                    ConsoleGUI.Print("I don't understand the word \"" + sentence[i].OrigWord + ".\"");
                    return;
                }

                else
                {
                    ConsoleGUI.Print("You lost me at \"" + sentence[i].OrigWord + ".\"");
                    return;
                }
            }
        }
    }
}
