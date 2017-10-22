
using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Model;

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

        HashSet<Tuple<string[], VerbSyntax[]>> verbs;
        HashSet<Tuple<string[], string>> particles;
        HashSet<Tuple<string[], DirCode>> directions;
        HashSet<Tuple<string[], Action>> commands;
        HashSet<Tuple<Type, string>> otherWords;

        /// <summary>
        /// Instantiate the <see cref="Glossary"/> singleton.
        /// </summary>
        private Glossary()
        {
            verbs = new HashSet<Tuple<string[], VerbSyntax[]>>
            {
                { new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounCollection), SyntFlag.MakeSingular),
                } },
                { new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(Direction)),
                    new VerbSyntax("in", VerbAction.Placeholder),
                    new VerbSyntax("out", VerbAction.Placeholder),
                    new VerbSyntax("up", VerbAction.Placeholder),
                    new VerbSyntax("down", VerbAction.Placeholder),
                } },
                { new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounCollection), SyntFlag.MakeSingular),
                } },
                { new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounCollection), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounCollection), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
                } },
            };

            particles = new HashSet<Tuple<string[], string>>
            {
                { new[] { "at" }, "at" },
                // Remember that the following particles are similar to some directions:
                { new[] { "in", "inside" }, "in" },
                { new[] { "out" }, "out" },
                { new[] { "up" }, "up" },
                { new[] { "down" }, "down" },
            };

            directions = new HashSet<Tuple<string[], DirCode>>
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

            commands = new HashSet<Tuple<string[], Action>>
            {
                { new[] { "commands" }, CommandAction.Placeholder },
                { new[] { "credits" }, CommandAction.Placeholder },
                { new[] { "help", "?" }, CommandAction.Placeholder },
                { new[] { "quit" }, CommandAction.Placeholder },
                { new[] { "verbose" }, CommandAction.Placeholder },
                { new[] { "brief" }, CommandAction.Placeholder },
            };

            // conjunctions added here, nouns and adjectives are added at runtime from entities.
            otherWords = new HashSet<Tuple<Type, string>>()
            {
                { typeof(Conjunction), "and" },
                { typeof(Conjunction), "&" },
                { typeof(Conjunction), "then" },
                // temporary testing words
                { typeof(Noun), "lamp" },
                { typeof(Adjective), "brass" },
            };
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>.
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
            foreach (Tuple<Type, string> entry in Instance.otherWords)
            {
                if (entry.Item2.ToLower() == tokenLower)
                    return (Node)Activator.CreateInstance(entry.Item1, tokenLower);
            }
            return new UnknownWord(token);
        }
    }
}