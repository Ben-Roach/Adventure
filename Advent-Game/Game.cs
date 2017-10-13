
using System.Collections.Generic;
using System.Linq;
using Adventure.View;
using Adventure.Controller;

namespace Adventure
{
    class Game
    {
        static void Main(string[] args)
        {
            // GAME SETUP
            ConsoleGUI.Setup();

            // OBJECT INSTANTIATION
            string[] removableWords = new string[] { "the", "a", "an", "of" };
            Glossary glossary = new Glossary();

            // GAME LOOP
            while (true)
            {
                Sentence sentence = new Sentence(ConsoleGUI.GetPlayerInput(), removableWords, glossary, out string errorMessage);
                if (errorMessage != null) ConsoleGUI.Print(errorMessage);
                else ConsoleGUI.Print(InterpretSentence(sentence));
                ObjectDumper.Dump(sentence);
            }
        }

        /// <summary>
        /// Attempts to interpret a <see cref="Sentence"/>.
        /// </summary>
        /// <param name="sentence">The <see cref="Sentence"/> to interpret.</param>
        static string InterpretSentence(Sentence sentence)
        {
            for (int i = 0; i < sentence.Count(); i++)
            {
                if (sentence[i] is Particle && ((Particle)sentence[i]).Lemma == "and")
                    continue;

                else if (sentence[i] is Command command)
                    command.ActionDelegate();

                else if (sentence[i] is Verb verb)
                {
                    List<VerbSyntax> syntaxList = verb.Syntaxes.ToList();
                    // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                    // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                    // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                    // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
                }

                else if (sentence[i] is UnknownWord)
                {
                    return "I don't understand the word \"" + sentence[i].OrigToken + "\".";
                }

                else
                {
                    return "You lost me at \"" + sentence[i].OrigToken + "\".";
                }
            }
            return "Something weird happened. Maybe try that again?";
        }
    }
}