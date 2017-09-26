using Lexicon;
using Sentence;
using System;
using System.Collections.Generic;

class Game
{
    static void Main(string[] args)
    {
        // GAME SETUP
        Console.Title = "Text Adventure";
        Console.SetWindowSize(120, 30);

        // OBJECT INSTANTIATION
        /// <summary>Strings that are removed during tokenization.</summary>
        string[] removableTokens = new string[] { "the", "a", "an", "of" };
        Glossary glossary = new Glossary();

        while (true) // Game Loop
        {
            // INPUT
            string inputString = Console.ReadLine();

            // TOKENIZATION -- Split input string into a list of strings, while removing unnecessary words and invalid strings.
            bool validInput = InputProcessor.Tokenize(inputString, removableTokens, out List<string> tokens);
            if (!validInput)
                return;

            // PARSING -- Construct a sentence out of tokens by validating words, assigning data to them, and organizing them syntactically.
            List<INode> sentence = new List<INode>();
            foreach (string token in tokens)
            {
                sentence.Add(InputProcessor.CreateNodeFromToken(token, glossary));
            }
            sentence = InputProcessor.CollectAdjectives(sentence);
            sentence = InputProcessor.CollectNouns(sentence);

            // COMPREHENSION -- Validate sentence structure and word usage, and attempt to act upon all action words.
            InputProcessor.Comprehend(sentence);

            // WRAP-UP
            ObjectDumper.Dump(sentence);
        }
    }
}