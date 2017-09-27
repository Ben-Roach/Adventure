using Lexicon;
using SentenceStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

class Game
{
    static void Main(string[] args)
    {
        // GAME SETUP
        Console.Title = "Text Adventure";
        Console.SetWindowSize(120, 30);

        // OBJECT INSTANTIATION
        string[] removableWords = new string[] { "the", "a", "an", "of" };
        Glossary glossary = new Glossary();

        // GAME LOOP
        while (true)
        {
            string inputString = Console.ReadLine();
            Sentence sentence = new Sentence(inputString, removableWords, glossary, out string errorMessage);
            if (errorMessage != null)
                Console.WriteLine(errorMessage);
            else
            {
                Console.WriteLine(InterpretSentence(sentence));
            }
            ObjectDumper.Dump(typeof(Sentence).GetField("baseList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sentence));
        }
    }

    /// <summary>
    /// Attempts to interpret a <see cref="Sentence"/>.
    /// </summary>
    /// <param name="sentence">The <see cref="Sentence"/> to interpret.</param>
    static string InterpretSentence(Sentence sentence)
    {
        List<INode> nodeList = sentence.NodeList;
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i] is Particle && ((Particle)nodeList[i]).Lemma == "and")
                continue;

            else if (nodeList[i] is Command command)
                command.ActionDelegate();

            else if (nodeList[i] is Verb verb)
            {
                List<VerbSyntax> syntaxList = verb.Syntaxes.ToList();
                // new verb logic here: check each word against all syntaxes in syntaxList, if end of a syntax is reached, it is potentially correct, if a word does not
                // match, throw syntax out. Once all syntaxes have been checked: if there are no potentialy correct syntaxes, use the current word in the error message.
                // Otherwise, go with the longest potentially correct syntax. Only go with an empty syntax if all other syntaxes were thrown out on the first check,
                // and throw all pending syntaxes out if the end of the sentence is reached (unless the end of the syntax is reached at the same time).
            }

            else if (nodeList[i] is UnknownWord)
            {
                return "I don't understand the word \"" + nodeList[i].OrigToken + "\".";
            }

            else
            {
                return "You lost me at \"" + nodeList[i].OrigToken + "\".";
            }
        }
        return "Something weird happened. Maybe try that again?";
    }
}