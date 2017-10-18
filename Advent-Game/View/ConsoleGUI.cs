
using System;

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

        /// <summary>
        /// Instantiate the <see cref="ConsoleGUI"/>.
        /// </summary>
        private ConsoleGUI()
        {

        }

        /// <summary>
        /// Set the console's appearance.
        /// </summary>
        public static void Setup()
        {
            Console.Title = "Adventure";
            Console.SetWindowSize(120, 30);
            if (RuntimeVals.Debugging)
            {
                Print("------DEBUG MODE ACTIVE-------");
            }
            else
            {
                // prevent scrolling if not debugging
                Console.BufferHeight = 30;
            }
        }

        /// <summary>
        /// Prompts the player for input.
        /// </summary>
        /// <returns>The player's input.</returns>
        public static string GetPlayerInput()
        {
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