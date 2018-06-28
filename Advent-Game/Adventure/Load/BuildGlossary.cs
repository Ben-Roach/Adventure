
using Adventure.Controller;
using Adventure.Language;

namespace Adventure
{
    public static partial class Load
    {
        /// <summary>
        /// Builds and returns a new <see cref="Glossary"/>. Temporary.
        /// </summary>
        public static Glossary BuildGlossary()
        {
            Glossary g = new Glossary();

            // PREPOSITIONS
            g.AddDef(new PrepositionDef("NORTH", PrepFlag.North));
            g.AddWords(new[] { "north", "n" }, "NORTH");
            g.AddDef(new PrepositionDef("SOUTH", PrepFlag.South));
            g.AddWords(new[] { "east", "e" }, "SOUTH");
            g.AddDef(new PrepositionDef("EAST", PrepFlag.East));
            g.AddWords(new[] { "south", "s" }, "EAST");
            g.AddDef(new PrepositionDef("WEST", PrepFlag.West));
            g.AddWords(new[] { "west", "w" }, "WEST");
            g.AddDef(new PrepositionDef("NORTHEAST", PrepFlag.Northeast));
            g.AddWords(new[] { "northeast", "ne" }, "NORTHEAST");
            g.AddDef(new PrepositionDef("NORTHWEST", PrepFlag.Northwest));
            g.AddWords(new[] { "northwest", "nw" }, "NORTHWEST");
            g.AddDef(new PrepositionDef("SOUTHEAST", PrepFlag.Southeast));
            g.AddWords(new[] { "southeast", "se" }, "SOUTHEAST");
            g.AddDef(new PrepositionDef("SOUTHWEST", PrepFlag.Southwest));
            g.AddWords(new[] { "southwest", "sw" }, "SOUTHWEST");
            g.AddDef(new PrepositionDef("UP", PrepFlag.Up));
            g.AddWords(new[] { "u", "up" }, "UP");
            g.AddDef(new PrepositionDef("DOWN", PrepFlag.Down));
            g.AddWords(new[] { "d", "down" }, "DOWN");
            g.AddDef(new PrepositionDef("IN", PrepFlag.In));
            g.AddWords(new[] { "i", "in", "inside" }, "IN");
            g.AddDef(new PrepositionDef("OUT", PrepFlag.Out));
            g.AddWords(new[] { "o", "out", "outside" }, "OUT");
            g.AddDef(new PrepositionDef("at", PrepFlag.At));
            g.AddWords(new[] { "at" }, "AT");

            // CONJUNCTIONS
            g.AddDef(new ConjunctionDef("and"));
            g.AddWords(new[] { "and", "then", "&" }, "AND");

            // VERBS
            g.AddDef(new VerbDef("TAKE", new[] {
                    new VerbUsage("*", VerbAction.Placeholder, typeof(NounGroupNode), flags: UsageFlag.MakeArg1Singular),
                }));
            g.AddWords(new[] { "take", "grab" }, "TAKE");

            g.AddDef(new VerbDef("GO", new[] {
                    new VerbUsage("*", VerbAction.Placeholder, typeof(PrepositionNode)),
                }));
            g.AddWords(new[] { "go", "walk", "climb" }, "GO");

            g.AddDef(new VerbDef("EXAMINE", new[] {
                    new VerbUsage("*", VerbAction.Placeholder, typeof(NounGroupNode), flags: UsageFlag.MakeArg1Singular),
                }));
            g.AddWords(new[] { "examine", "describe", "ex", "x" }, "EXAMINE");

            g.AddDef(new VerbDef("LOOK", new[] {
                    new VerbUsage("*", VerbAction.Placeholder, typeof(NounGroupNode), flags: UsageFlag.MakeArg1Singular),
                    new VerbUsage("at *", VerbAction.Placeholder, typeof(NounGroupNode), flags: UsageFlag.MakeArg1Singular),
                    new VerbUsage("", VerbAction.Placeholder)
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
            g.AddDef(new NounDef("ORK"));
            g.AddWords(new[] { "ork" }, "ORK");
            g.AddDef(new AdjectiveDef("BRASS"));
            g.AddWords(new[] { "brass" }, "BRASS");

            return g;
        }
    }
}
