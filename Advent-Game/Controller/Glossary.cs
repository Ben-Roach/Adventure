
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all data associated with them.
    /// </summary>
    public class Glossary
    {
        /// <summary>Set of noun strings, derived from WorldObjects. Will change as WorldObjects are created and changed.</summary>
        public HashSet<string> Nouns { get; private set; }
        /// <summary>Set of adjective strings, derived from WorldObjects. Will change as WorldObjects are created and changed.</summary>
        public HashSet<string> Adjectives { get; private set; }
        /// <summary>All known verbs, each with an associated <see cref="VerbSyntax"/> array.</summary>
        public GlossarySection<VerbSyntax[]> Verbs { get; }
        /// <summary>All words representing directions.</summary>
        public GlossarySection<DirCodes> Directions { get; }
        /// <summary>All other predetermined words.</summary>
        public GlossarySection<string> Particles { get; }
        /// <summary>All known commands, each with an associated delegate.</summary>
        public GlossarySection<Action> Commands { get; }

        public Glossary()
        {
            Nouns = new HashSet<string>();

            Adjectives = new HashSet<string>();

            Verbs = new GlossarySection<VerbSyntax[]>(typeof(Verb))
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

            Directions = new GlossarySection<DirCodes>(typeof(Direction))
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

            Particles = new GlossarySection<string>(typeof(Particle))
            {
                // The below particle is considered a conjunction when chaining Nouns.
                { new[] { "and", "&", "then" }, "and" },
                // Remember that the following particles are similar to some directions:
                { new[] { "in", "inside" }, "in" },
                { new[] { "out" }, "out" },
                { new[] { "up" }, "up" },
                { new[] { "down" }, "down" },
            };

            Commands = new GlossarySection<Action>(typeof(Command))
            {
                { new[] { "commands" }, null },
                { new[] { "credits" }, null },
                { new[] { "help", "?" }, null },
                { new[] { "quit" }, null },
                { new[] { "verbose" }, null },
                { new[] { "brief" }, null },
            };
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>, using <paramref name="glossary"/> for verification.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public Node CreateNodeFromToken(string token)
        {
            string tokenLower = token.ToLower();
            foreach (Tuple<string[], VerbSyntax[]> entry in Verbs)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Verb(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], Action> entry in Commands)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Command(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], string> entry in Particles)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Particle(tokenLower, entry.Item2);
            }
            foreach (Tuple<string[], DirCodes> entry in Directions)
            {
                if (entry.Item1.Contains(tokenLower))
                    return new Direction(tokenLower, entry.Item2);
            }
            // always search writable glossaries last, to avoid accidentally hiding entries in readonly glossaries.
            foreach (string entry in Nouns)
            {
                if (entry == tokenLower)
                    return new Noun(token);
            }
            foreach (string entry in Adjectives)
            {
                if (entry == tokenLower)
                    return new Adjective(token);
            }
            return new UnknownWord(token);
        }
    }
}