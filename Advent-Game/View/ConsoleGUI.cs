
using System;
using System.Diagnostics;

namespace Adventure.View
{
    /// <summary>
    /// Uses the Windows Console as a GUI.
    /// </summary>
    public sealed class ConsoleGUI
    {
        private static readonly ConsoleGUI instance = new ConsoleGUI();
        /// <summary>The instance of the <see cref="ConsoleGUI"/> singleton.</summary>
        public static ConsoleGUI Instance { get { return instance; } }

        private int consoleWidth;
        private int consoleHeight;
        private string consoleTitle;

        /// <summary>
        /// Instantiate the <see cref="ConsoleGUI"/> singleton.
        /// </summary>
        private ConsoleGUI()
        {
            consoleWidth = 100;
            consoleHeight = 25;
            consoleTitle = "Adventure";
        }

        /// <summary>
        /// Set the console's appearance. Should be the first method called by Main.
        /// </summary>
        public static void Setup()
        {
            Console.Title = Instance.consoleTitle;
            Console.SetWindowSize(Instance.consoleWidth, Instance.consoleHeight);
            Console.SetBufferSize(Instance.consoleWidth, Instance.consoleHeight);
            DebugSetup(); // execute this last
        }

        /// <summary>
        /// Modifies the console's appearance if compiled as a debug build.
        /// </summary>
        [Conditional("DEBUG")]
        private static void DebugSetup()
        {
            Console.Title = Instance.consoleTitle + " (Debug)";
            Console.BufferHeight = 4000;
            Print("---------[DEBUG ACTIVE]---------");
        }

        /// <summary>
        /// Prompts the player for input.
        /// </summary>
        /// <returns>The player's input.</returns>
        public static string GetPlayerInput()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Write text to the console.
        /// </summary>
        /// <param name="outputString">The text to write.</param>
        public static void Print(string outputString)
        {
            Console.WriteLine(outputString);
        }
    }
}