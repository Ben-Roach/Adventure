
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
        List<Tuple<string[], DirCode>> directions;
        List<Tuple<string[], Action>> commands;
        List<string[]> conjunctions;
        HashSet<string> nouns; // Will change as game objects are created and changed.
        HashSet<string> adjectives; // Will change as game objects are created and changed.

        /// <summary>
        /// Instantiate the <see cref="Glossary"/>.
        /// </summary>
        private Glossary()
        {
            verbs = new List<Tuple<string[], VerbSyntax[]>>
            {
                { new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlag.MakeSingular)
                } },
                { new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", null, typeof(Direction))
                } },
                { new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlag.MakeSingular),
                } },
                { new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", null, typeof(NounCollection), SyntFlag.MakeSingular),
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

            directions = new List<Tuple<string[], DirCode>>
            {
                { new[] { "north", "n" }, DirCode.North },
                { new[] { "east", "e" }, DirCode.South },
                { new[] { "south", "s" }, DirCode.East },
                { new[] { "west", "w" }, DirCode.West },
                { new[] { "northeast", "ne" }, DirCode.Northeast },
                { new[] { "northwest", "nw" }, DirCode.Northwest },
                { new[] { "southeast", "se" }, DirCode.Southeast },
                { new[] { "southwest", "sw" }, DirCode.Southwest },
                // Remember that the following directions are similar to some particles:
                { new[] { "u" }, DirCode.Up },
                { new[] { "d" }, DirCode.Down },
                { new[] { "enter" }, DirCode.In },
                { new[] { "exit", "outside" }, DirCode.Out },
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
            foreach (Tuple<string[], DirCode> entry in Instance.directions)
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