using System;

class Game
{
    static void Main(string[] args)
    {
        Console.Title = "Text Adventure";
        Console.SetWindowSize(120, 30);

        while (true) // Game Loop
        {
            string input = Console.ReadLine();
            InputProcessor.Process(input);
        }
    }
}