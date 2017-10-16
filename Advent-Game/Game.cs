
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

            // GAME LOOP
            while (true)
            {
                Sentence sentence = new Sentence(ConsoleGUI.GetPlayerInput(), out string errorMessage);
                if (errorMessage != null) ConsoleGUI.Print(errorMessage);
                else ConsoleGUI.Print(sentence.Interpret());
                ObjectDumper.Dump(sentence);
            }
        }
    }
}