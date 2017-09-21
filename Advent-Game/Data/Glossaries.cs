using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sentence;

namespace Lexicon
{
    /// <summary>
    /// Contains collections with all known words, and all data associated with them.
    /// </summary>
    static class Glossaries
    {
        /// <summary>
        /// All known Verbs, with their associated syntaxes and verb action methods.
        /// </summary>
        public static Glossary<VerbSyntax[]> Verbs { get; } = new Glossary<VerbSyntax[]>(typeof(Verb))
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

        /// <summary>
        /// All known Commands, with their associated command action methods.
        /// </summary>
        public static Glossary<Action> Commands = new Glossary<Action>(typeof(Command))
        {
            { new[] { "commands" }, null },
            { new[] { "credits" }, null },
            { new[] { "help", "?" }, null },
            { new[] { "quit" }, null },
            { new[] { "verbose" }, null },
            { new[] { "brief" }, null },
        };

        /// <summary>
        /// All other predetermined words.
        /// </summary>
        public static Glossary<string> Particles = new Glossary<string>(typeof(Particle))
        {
            { new[] { "and", "&", "then" }, "and" },
            // Remember that the following particles are similar to some directions:
            { new[] { "in", "inside" }, "in" },
            { new[] { "out" }, "out" },
            { new[] { "up" }, "up" },
            { new[] { "down" }, "down" },
        };

        /// <summary>
        /// All words representing directions.
        /// </summary>
        public static Glossary<string> Directions = new Glossary<string>(typeof(Direction))
        {
            { new[] { "north", "n" }, "N" },
            { new[] { "east", "e" }, "E" },
            { new[] { "south", "s" }, "S" },
            { new[] { "west", "w" }, "W" },
            { new[] { "northeast", "ne" }, "NE" },
            { new[] { "northwest", "nw" }, "NW" },
            { new[] { "southeast", "se" }, "SE" },
            { new[] { "southwest", "sw" }, "SW" },
            // Remember that the following directions are similar to some particles:
            { new[] { "u" }, "U" },
            { new[] { "d" }, "D" },
            { new[] { "enter" }, "I" },
            { new[] { "exit", "outside" }, "O" },
        };

        /// <summary>
        /// Set of noun strings, derived from WorldObjects. Will change as WorldObjects are created and changed.
        /// </summary>
        public static HashSet<string> Nouns { get; private set; } = new HashSet<string>();

        /// <summary>
        /// Set of adjective strings, derived from WorldObjects. Will change as WorldObjects are created and changed.
        /// </summary>
        public static HashSet<string> Adjectives { get; private set; } = new HashSet<string>();
    }

}