
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
            Glossary glossary = Load.BuildGlossary();

            while (true)
            {
                Sentence sentence = Sentence.Parse(ConsoleGUI.GetPlayerInput(), glossary, out string errorMessage);
                if (errorMessage != null) ConsoleGUI.Print(errorMessage);
                else Sentence.Interpret(sentence);
            }
        }
    }
}