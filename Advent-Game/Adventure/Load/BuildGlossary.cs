using Adventure.Controller;
using Adventure.Model;

namespace Adventure
{
    public static partial class Load
    {
        /// <summary>
        /// Builds and returns a new <see cref="Glossary"/>.
        /// </summary>
        public static Glossary BuildGlossary()
        {
            Glossary g = new Glossary('*',  s => s.Trim().ToLower(),
                c => !(char.IsLetter(c) || char.IsNumber(c) || c == '&' || c == '?' || c == '\'' || c == '-'),
                s => (s == "the" || s == "a" || s == "an" || s == "of"));

            // PARTICLES
            g.AddNewEntry(new[] { "at" }, new ParticleDef("at"));

            // VERBS
            g.AddNewEntry(new[] { "take", "grab" }, new VerbDef("take", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddNewEntry(new[] { "go", "walk", "climb" }, new VerbDef("go", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(DirectionNode)),
                }));
            g.AddNewEntry(new[] { "examine", "describe", "ex", "x" }, new VerbDef("examine", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddNewEntry(new[] { "look", "l" }, new VerbDef("look", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
                }));

            // DIRECTIONS
            g.AddNewEntry(new[] { "north", "n" }, new DirectionDef("north", DirCode.North));
            g.AddNewEntry(new[] { "east", "e" }, new DirectionDef("east", DirCode.South));
            g.AddNewEntry(new[] { "south", "s" }, new DirectionDef("south", DirCode.East));
            g.AddNewEntry(new[] { "west", "w" }, new DirectionDef("west", DirCode.West));
            g.AddNewEntry(new[] { "northeast", "ne" }, new DirectionDef("northeast", DirCode.Northeast));
            g.AddNewEntry(new[] { "northwest", "nw" }, new DirectionDef("northwest", DirCode.Northwest));
            g.AddNewEntry(new[] { "southeast", "se" }, new DirectionDef("southeast", DirCode.Southeast));
            g.AddNewEntry(new[] { "southwest", "sw" }, new DirectionDef("southwest", DirCode.Southwest));
            g.AddNewEntry(new[] { "up", "u" }, new DirectionDef("up", DirCode.Up));
            g.AddNewEntry(new[] { "down", "d" }, new DirectionDef("down", DirCode.Down));
            g.AddNewEntry(new[] { "in", "inside" }, new DirectionDef("in", DirCode.In));
            g.AddNewEntry(new[] { "out", "outside" }, new DirectionDef("out", DirCode.Out));

            // COMMANDS
            g.AddNewEntry(new[] { "commands" }, new CommandDef("commands", CommandAction.Placeholder));
            g.AddNewEntry(new[] { "credits" }, new CommandDef("credits", CommandAction.Placeholder));
            g.AddNewEntry(new[] { "help", "?" }, new CommandDef("help", CommandAction.Placeholder));
            g.AddNewEntry(new[] { "quit" }, new CommandDef("quit", CommandAction.Placeholder));
            g.AddNewEntry(new[] { "verbose" }, new CommandDef("verbose", CommandAction.Placeholder));
            g.AddNewEntry(new[] { "brief" }, new CommandDef("breif", CommandAction.Placeholder));

            // CONJUNCTIONS
            g.AddNewEntry(new[] { "and", "then", "&" }, new ConjunctionDef("&"));

            // OTHER / TEST
            g.AddNewEntry(new[] { "lamp" }, new NounDef("lamp"));
            g.AddNewEntry(new[] { "brass" }, new AdjectiveDef("brass"));

            return g;
        }
    }
}
