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
        public static List<Entry> BaseGlossaryEntries()
        {
            List<Entry> entries = new List<Entry>();

            // VERBS
            entries.Add(new VerbEntry(new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
            }));
            entries.Add(new VerbEntry(new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(Direction)),
                    new VerbSyntax("in", VerbAction.Placeholder),
                    new VerbSyntax("out", VerbAction.Placeholder),
                    new VerbSyntax("up", VerbAction.Placeholder),
                    new VerbSyntax("down", VerbAction.Placeholder),
            }));
            entries.Add(new VerbEntry(new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
            }));
            entries.Add(new VerbEntry(new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
            }));

            // PARTICLES
            entries.Add(new ParticleEntry(new[] { "at" }, "at"));
            // Remember that the following particles are similar to some directions:
            entries.Add(new ParticleEntry(new[] { "in", "inside" }, "in"));
            entries.Add(new ParticleEntry(new[] { "out" }, "out"));
            entries.Add(new ParticleEntry(new[] { "up" }, "up"));
            entries.Add(new ParticleEntry(new[] { "down" }, "down"));

            // DIRECTIONS
            entries.Add(new DirectionEntry(new[] { "north", "n" }, DirCode.North));
            entries.Add(new DirectionEntry(new[] { "east", "e" }, DirCode.South));
            entries.Add(new DirectionEntry(new[] { "south", "s" }, DirCode.East));
            entries.Add(new DirectionEntry(new[] { "west", "w" }, DirCode.West));
            entries.Add(new DirectionEntry(new[] { "northeast", "ne" }, DirCode.Northeast));
            entries.Add(new DirectionEntry(new[] { "northwest", "nw" }, DirCode.Northwest));
            entries.Add(new DirectionEntry(new[] { "southeast", "se" }, DirCode.Southeast));
            entries.Add(new DirectionEntry(new[] { "southwest", "sw" }, DirCode.Southwest));
            // Remember that the following directions are similar to some particles:
            entries.Add(new DirectionEntry(new[] { "u" }, DirCode.Up));
            entries.Add(new DirectionEntry(new[] { "d" }, DirCode.Down));
            entries.Add(new DirectionEntry(new[] { "enter" }, DirCode.In));
            entries.Add(new DirectionEntry(new[] { "exit", "outside" }, DirCode.Out));

            // COMMANDS
            entries.Add(new CommandEntry(new[] { "commands" }, CommandAction.Placeholder));
            entries.Add(new CommandEntry(new[] { "credits" }, CommandAction.Placeholder));
            entries.Add(new CommandEntry(new[] { "help", "?" }, CommandAction.Placeholder));
            entries.Add(new CommandEntry(new[] { "quit" }, CommandAction.Placeholder));
            entries.Add(new CommandEntry(new[] { "verbose" }, CommandAction.Placeholder));
            entries.Add(new CommandEntry(new[] { "brief" }, CommandAction.Placeholder));

            // CONJUNCTIONS
            entries.Add(new ConjunctionEntry("and"));
            entries.Add(new ConjunctionEntry("then"));
            entries.Add(new ConjunctionEntry("&"));

            return entries;
        }
    }
}
