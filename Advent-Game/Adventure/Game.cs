
using Adventure.View;
using Adventure.Controller;
using Adventure.Model;

namespace Adventure
{
    /// <summary>
    /// The instance of the game.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Run the game.
        /// </summary>
        /// <param name="args"></param>
        public static void Run(string[] args)
        {
            ConsoleGUI.Setup();
            Glossary glossary = new Glossary(Load.BaseGlossaryEntries());

            while (true)
            {
                Sentence sentence = new Sentence(ConsoleGUI.GetPlayerInput(), glossary, out string errorMessage);
                if (errorMessage != null) ConsoleGUI.Print(errorMessage);
                else SentenceInterpreter.Interpret(sentence);
                ObjectDumper.Dump(sentence);
            }
        }
    }
}