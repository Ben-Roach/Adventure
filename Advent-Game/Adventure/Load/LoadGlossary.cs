using Adventure.Controller;
using Adventure.Model;

namespace Adventure
{
    public static partial class Load
    {
        /// <summary>
        /// Handles loading <see cref="Entry"/> objects into the <see cref="Glossary"/>.
        /// </summary>
        public static void LoadGlossary()
        {
            // VERBS
            Glossary.Add(new VerbEntry(new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
            }));
            Glossary.Add(new VerbEntry(new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(Direction)),
                    new VerbSyntax("in", VerbAction.Placeholder),
                    new VerbSyntax("out", VerbAction.Placeholder),
                    new VerbSyntax("up", VerbAction.Placeholder),
                    new VerbSyntax("down", VerbAction.Placeholder),
            }));
            Glossary.Add(new VerbEntry(new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
            }));
            Glossary.Add(new VerbEntry(new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroup), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
            }));

            // PARTICLES
            Glossary.Add(new ParticleEntry(new[] { "at" }, "at"));
            // Remember that the following particles are similar to some directions:
            Glossary.Add(new ParticleEntry(new[] { "in", "inside" }, "in"));
            Glossary.Add(new ParticleEntry(new[] { "out" }, "out"));
            Glossary.Add(new ParticleEntry(new[] { "up" }, "up"));
            Glossary.Add(new ParticleEntry(new[] { "down" }, "down"));

            // DIRECTIONS
            Glossary.Add(new DirectionEntry(new[] { "north", "n" }, DirCode.North));
            Glossary.Add(new DirectionEntry(new[] { "east", "e" }, DirCode.South));
            Glossary.Add(new DirectionEntry(new[] { "south", "s" }, DirCode.East));
            Glossary.Add(new DirectionEntry(new[] { "west", "w" }, DirCode.West));
            Glossary.Add(new DirectionEntry(new[] { "northeast", "ne" }, DirCode.Northeast));
            Glossary.Add(new DirectionEntry(new[] { "northwest", "nw" }, DirCode.Northwest));
            Glossary.Add(new DirectionEntry(new[] { "southeast", "se" }, DirCode.Southeast));
            Glossary.Add(new DirectionEntry(new[] { "southwest", "sw" }, DirCode.Southwest));
            // Remember that the following directions are similar to some particles:
            Glossary.Add(new DirectionEntry(new[] { "u" }, DirCode.Up));
            Glossary.Add(new DirectionEntry(new[] { "d" }, DirCode.Down));
            Glossary.Add(new DirectionEntry(new[] { "enter" }, DirCode.In));
            Glossary.Add(new DirectionEntry(new[] { "exit", "outside" }, DirCode.Out));

            // COMMANDS
            Glossary.Add(new CommandEntry(new[] { "commands" }, CommandAction.Placeholder));
            Glossary.Add(new CommandEntry(new[] { "credits" }, CommandAction.Placeholder));
            Glossary.Add(new CommandEntry(new[] { "help", "?" }, CommandAction.Placeholder));
            Glossary.Add(new CommandEntry(new[] { "quit" }, CommandAction.Placeholder));
            Glossary.Add(new CommandEntry(new[] { "verbose" }, CommandAction.Placeholder));
            Glossary.Add(new CommandEntry(new[] { "brief" }, CommandAction.Placeholder));

            // CONJUNCTIONS
            Glossary.Add(new ConjunctionEntry("and"));
            Glossary.Add(new ConjunctionEntry("then"));
            Glossary.Add(new ConjunctionEntry("&"));
        }
    }
}
