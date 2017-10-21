
using Adventure.View;
using Adventure.Controller;
using Adventure.Model;

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
                else SentenceInterpreter.Interpret(sentence);
                ObjectDumper.Dump(sentence);
            }
        }
    }
}