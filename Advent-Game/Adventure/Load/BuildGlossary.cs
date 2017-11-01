using Adventure.Controller;
using Adventure.Model;
using System.Collections.Generic;

namespace Adventure
{
    public static partial class Load
    {
        /// <summary>
        /// Returns the initial <see cref="Entry"/> objects to add to the <see cref="Glossary"/>.
        /// </summary>
        public static Glossary BuildGlossary()
        {
            List<Entry> entries = new List<Entry>
            {
                // VERBS
                new VerbEntry(new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                }),
                new VerbEntry(new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(Direction)),
                    new VerbSyntax("in", VerbAction.Placeholder),
                    new VerbSyntax("out", VerbAction.Placeholder),
                    new VerbSyntax("up", VerbAction.Placeholder),
                    new VerbSyntax("down", VerbAction.Placeholder),
                }),
                new VerbEntry(new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                }),
                new VerbEntry(new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
                }),

                // PARTICLES
                new ParticleEntry(new[] { "at" }, "at"),
                // Remember that the following particles are similar to some directions:
                new ParticleEntry(new[] { "in", "inside" }, "in"),
                new ParticleEntry(new[] { "out" }, "out"),
                new ParticleEntry(new[] { "up" }, "up"),
                new ParticleEntry(new[] { "down" }, "down"),

                // DIRECTIONS
                new DirectionEntry(new[] { "north", "n" }, DirCode.North),
                new DirectionEntry(new[] { "east", "e" }, DirCode.South),
                new DirectionEntry(new[] { "south", "s" }, DirCode.East),
                new DirectionEntry(new[] { "west", "w" }, DirCode.West),
                new DirectionEntry(new[] { "northeast", "ne" }, DirCode.Northeast),
                new DirectionEntry(new[] { "northwest", "nw" }, DirCode.Northwest),
                new DirectionEntry(new[] { "southeast", "se" }, DirCode.Southeast),
                new DirectionEntry(new[] { "southwest", "sw" }, DirCode.Southwest),
                // Remember that the following directions are similar to some particles:
                new DirectionEntry(new[] { "u" }, DirCode.Up),
                new DirectionEntry(new[] { "d" }, DirCode.Down),
                new DirectionEntry(new[] { "enter" }, DirCode.In),
                new DirectionEntry(new[] { "exit", "outside" }, DirCode.Out),

                // COMMANDS
                new CommandEntry(new[] { "commands" }, CommandAction.Placeholder),
                new CommandEntry(new[] { "credits" }, CommandAction.Placeholder),
                new CommandEntry(new[] { "help", "?" }, CommandAction.Placeholder),
                new CommandEntry(new[] { "quit" }, CommandAction.Placeholder),
                new CommandEntry(new[] { "verbose" }, CommandAction.Placeholder),
                new CommandEntry(new[] { "brief" }, CommandAction.Placeholder),

                // CONJUNCTIONS
                new ConjunctionEntry("and"),
                new ConjunctionEntry("then"),
                new ConjunctionEntry("&")
            };

            return new Glossary(entries, '*',  s => s.ToUpper(),
                c => !(char.IsLetter(c) || char.IsNumber(c) || c == ' ' || c == '\t' || c == '&' || c == '?' || c == '\'' || c == '-'),
                s => (s == "the" || s == "a" || s == "an" || s == "of")
                );
        }
    }
}
