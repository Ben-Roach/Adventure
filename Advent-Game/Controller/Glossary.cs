
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them.
    /// </summary>
    public sealed class Glossary
    {
        private static readonly Glossary instance = new Glossary();
        /// <summary>The instance of the <see cref="Glossary"/> singleton.</summary>
        public static Glossary Instance { get { return instance; } }

        List<Tuple<string[], VerbSyntax[]>> verbs;
        List<Tuple<string[], string>> particles;
        List<Tuple<string[], DirCodes>> directions;
        List<Tuple<string[], Action>> commands;
        List<string[]> conjunctions;
        HashSet<string> nouns; // Will change as game objects are created and changed.
        HashSet<string> adjectives; // Will change as game objects are created and changed.

        private Glossary()
        {
            verbs = new List<Tuple<string[], VerbSyntax[]>>
            {
                { new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlags.MakeSingular)
                } },
                { new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", null, typeof(Direction))
                } },
                { new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlags.MakeSingular),
                } },
                { new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlags.MakeSingular),
                    new VerbSyntax("", null)
                } },
            };

            particles = new List<Tuple<string[], string>>
            {
                // Remember that the following particles are similar to some directions:
                { new[] { "in", "inside" }, "in" },
                { new[] { "out" }, "out" },
                { new[] { "up" }, "up" },
                { new[] { "down" }, "down" },
            };

            directions = new List<Tuple<string[], DirCodes>>
            {
                { new[] { "north", "n" }, DirCodes.North },
                { new[] { "east", "e" }, DirCodes.South },
                { new[] { "south", "s" }, DirCodes.East },
                { new[] { "west", "w" }, DirCodes.West },
                { new[] { "northeast", "ne" }, DirCodes.Northeast },
                { new[] { "northwest", "nw" }, DirCodes.Northwest },
                { new[] { "southeast", "se" }, DirCodes.Southeast },
                { new[] { "southwest", "sw" }, DirCodes.Southwest },
                // Remember that the following directions are similar to some particles:
                { new[] { "u" }, DirCodes.Up },
                { new[] { "d" }, DirCodes.Down },
                { new[] { "enter" }, DirCodes.In },
                { new[] { "exit", "outside" }, DirCodes.Out },
            };

            commands = new List<Tuple<string[], Action>>
            {
                { new[] { "commands" }, null },
                { new[] { "credits" }, null },
                { new[] { "help", "?" }, null },
                { new[] { "quit" }, null },
                { new[] { "verbose" }, null },
                { new[] { "brief" }, null },
            };

            conjunctions = new List<string[]>()
            {
                new[] { "and", "&", "then" },
            };

            nouns = new HashSet<string>();

            adjectives = new HashSet<string>();

            nouns.Add("lamp");
            adjectives.Add("brass");
        }

        /// <summary>
        /// Reports if a character is removable from a <see cref="Sentence"/>.
        /// </summary>
        /// <returns>True if <paramref name="c"/> is removable, else false.</returns>
        public static bool IsRemovableChar(char c)
        {
            if ((c >= 'A' && c <= 'z') || (c >= '0' && c <= '9') || c == ' ' || c == '\t' || c == '&' || c == '?')
                return false;
            return true;
        }

        /// <summary>
        /// Reports if a string is removable from a <see cref="Sentence"/>.
        /// </summary>
        /// <returns>True if <paramref name="s"/> is removable, else false.</returns>
        public static bool IsRemovableToken(string s)
        {
            if (s == "the" || s == "a" || s == "an" || s == "of")
                return true;
            return false;
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>, using <paramref name="glossary"/> for verification.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public static Node CreateNodeFromToken(string token)
        {
            string tokenLower = token.ToLower();
            foreach (Tuple<string[], VerbSyntax[]> entry in Instance.verbs)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Verb(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], string> entry in Instance.particles)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Particle(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], DirCodes> entry in Instance.directions)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Direction(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], Action> entry in Instance.commands)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Command(tokenLower, entry.Item2);
            }
            foreach (string[] entry in Instance.conjunctions)
            {
                if (entry.Contains(tokenLower))
                    return new Conjunction(tokenLower);
            }
            // always search writable glossaries last, to avoid accidentally hiding entries in readonly glossaries.
            foreach (string entry in Instance.nouns)
            {
                if (entry == tokenLower)
                    return new Noun(token);
            }
            foreach (string entry in Instance.adjectives)
            {
                if (entry == tokenLower)
                    return new Adjective(token);
            }
            return new UnknownWord(token);
        }
    }
}