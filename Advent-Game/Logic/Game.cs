using Lexicon;
using System;

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

        while (true) // Game Loop
        {
            // INPUT
            string inputString = Console.ReadLine();
            Sentence sentence = new Sentence(inputString, removableWords, glossary, out string errorMessage);
            if (errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                continue;
            }
            ObjectDumper.Dump(sentence);
            sentence.Comprehend();
        }
    }
}