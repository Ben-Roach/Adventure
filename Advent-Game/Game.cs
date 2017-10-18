
using Adventure.View;
using Adventure.Controller;

namespace Adventure
{
    class Game
    {
        static void Main(string[] args)
        {
            ConsoleGUI.Setup();

            while (true)
            {
                Sentence sentence = new Sentence(ConsoleGUI.GetPlayerInput(), out string errorMessage);
                if (errorMessage != null) ConsoleGUI.Print(errorMessage);
                else ConsoleGUI.Print(sentence.Interpret());
                if (RuntimeVals.IsDebug)
                    ObjectDumper.Dump(sentence);
            }
        }
    }
}