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
            g.AddDef(new ParticleDef("at"));
            g.AddWords(new[] { "at" }, "at");

            // CONJUNCTIONS
            g.AddDef(new ConjunctionDef("and"));
            g.AddWords(new[] { "and", "then", "&" }, "and");

            // DIRECTIONS
            g.AddDef(new DirectionDef("NORTH", DirFlag.North));
            g.AddWords(new[] { "north", "n" }, "NORTH");
            g.AddDef(new DirectionDef("SOUTH", DirFlag.South));
            g.AddWords(new[] { "east", "e" }, "SOUTH");
            g.AddDef(new DirectionDef("EAST", DirFlag.East));
            g.AddWords(new[] { "south", "s" }, "EAST");
            g.AddDef(new DirectionDef("WEST", DirFlag.West));
            g.AddWords(new[] { "west", "w" }, "WEST");
            g.AddDef(new DirectionDef("NORTHEAST", DirFlag.Northeast));
            g.AddWords(new[] { "northeast", "ne" }, "NORTHEAST");
            g.AddDef(new DirectionDef("NORTHWEST", DirFlag.Northwest));
            g.AddWords(new[] { "northwest", "nw" }, "NORTHWEST");
            g.AddDef(new DirectionDef("SOUTHEAST", DirFlag.Southeast));
            g.AddWords(new[] { "southeast", "se" }, "SOUTHEAST");
            g.AddDef(new DirectionDef("SOUTHWEST", DirFlag.Southwest));
            g.AddWords(new[] { "southwest", "sw" }, "SOUTHWEST");
            g.AddDef(new DirectionDef("UP", DirFlag.Up));
            g.AddWords(new[] { "u", "up" }, "UP");
            g.AddDef(new DirectionDef("DOWN", DirFlag.Down));
            g.AddWords(new[] { "d", "down" }, "DOWN");
            g.AddDef(new DirectionDef("IN", DirFlag.In));
            g.AddWords(new[] { "i", "in", "inside" }, "IN");
            g.AddDef(new DirectionDef("OUT", DirFlag.Out));
            g.AddWords(new[] { "o", "out", "outside" }, "OUT");

            // VERBS
            g.AddDef(new VerbDef("TAKE", new[] {
                    new VerbPhrase("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddWords(new[] { "take", "grab" }, "TAKE");

            g.AddDef(new VerbDef("GO", new[] {
                    new VerbPhrase("*", VerbAction.Placeholder, typeof(DirectionNode)),
                }));
            g.AddWords(new[] { "go", "walk", "climb" }, "GO");

            g.AddDef(new VerbDef("EXAMINE", new[] {
                    new VerbPhrase("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddWords(new[] { "examine", "describe", "ex", "x" }, "EXAMINE");

            g.AddDef(new VerbDef("LOOK", new[] {
                    new VerbPhrase("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbPhrase("at *", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbPhrase("", VerbAction.Placeholder)
                }));
            g.AddWords(new[] { "look", "l" }, "LOOK");

            // COMMANDS
            g.AddDef(new CommandDef("COMMANDS", CommandAction.Placeholder));
            g.AddWords(new[] { "commands" }, "COMMANDS");
            g.AddDef(new CommandDef("CREDITS", CommandAction.Placeholder));
            g.AddWords(new[] { "credits" }, "CREDITS");
            g.AddDef(new CommandDef("HELP", CommandAction.Placeholder));
            g.AddWords(new[] { "help", "?" }, "HELP");
            g.AddDef(new CommandDef("QUIT", CommandAction.Placeholder));
            g.AddWords(new[] { "quit" }, "QUIT");
            g.AddDef(new CommandDef("VERBOSE", CommandAction.Placeholder));
            g.AddWords(new[] { "verbose" }, "VERBOSE");
            g.AddDef(new CommandDef("BRIEF", CommandAction.Placeholder));
            g.AddWords(new[] { "brief" }, "BRIEF");

            // OTHER / TEST
            g.AddDef(new NounDef("LAMP"));
            g.AddWords(new[] { "lamp" }, "LAMP");
            g.AddDef(new AdjectiveDef("BRASS"));
            g.AddWords(new[] { "brass" }, "BRASS");

            return g;
        }
    }
}
