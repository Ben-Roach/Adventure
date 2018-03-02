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
            Glossary g = new Glossary('*', "UNKNOWN",  s => s.Trim().ToLower(),
                c => !(char.IsLetter(c) || char.IsNumber(c) || c == '&' || c == '?' || c == '\'' || c == '-'),
                s => (s == "the" || s == "a" || s == "an" || s == "of"));

            // PARTICLES
            g.AddDef(new ParticleDef("at"));
            g.AddWords(new[] { "at" }, "at");
            g.AddDef(new ParticleDef("up"));
            g.AddWords(new[] { "up" }, "up");
            g.AddDef(new ParticleDef("down"));
            g.AddWords(new[] { "down" }, "down");
            g.AddDef(new ParticleDef("in"));
            g.AddWords(new[] { "in" }, "in");
            g.AddDef(new ParticleDef("out"));
            g.AddWords(new[] { "out" }, "out");

            // CONJUNCTIONS
            g.AddDef(new ConjunctionDef("and"));
            g.AddWords(new[] { "and", "then", "&" }, "and");

            // DIRECTIONS
            g.AddDef(new DirectionDef("direction_north", DirFlag.North));
            g.AddWords(new[] { "north", "n" }, "direction_north");
            g.AddDef(new DirectionDef("direction_east", DirFlag.South));
            g.AddWords(new[] { "east", "e" }, "direction_east");
            g.AddDef(new DirectionDef("direction_south", DirFlag.East));
            g.AddWords(new[] { "south", "s" }, "direction_south");
            g.AddDef(new DirectionDef("direction_west", DirFlag.West));
            g.AddWords(new[] { "west", "w" }, "direction_west");
            g.AddDef(new DirectionDef("direction_northeast", DirFlag.Northeast));
            g.AddWords(new[] { "northeast", "ne" }, "direction_northeast");
            g.AddDef(new DirectionDef("direction_northwest", DirFlag.Northwest));
            g.AddWords(new[] { "northwest", "nw" }, "direction_northwest");
            g.AddDef(new DirectionDef("direction_southeast", DirFlag.Southeast));
            g.AddWords(new[] { "southeast", "se" }, "direction_southeast");
            g.AddDef(new DirectionDef("direction_southwest", DirFlag.Southwest));
            g.AddWords(new[] { "southwest", "sw" }, "direction_southwest");

            g.AddDef(new DirectionDef("direction_up", DirFlag.Up));
            g.AddWords(new[] { "u" }, "direction_up");
            g.AssociateDef("up", new ConditionalDef("direction_up", null, "go"));
            g.AddDef(new DirectionDef("direction_down", DirFlag.Down));
            g.AddWords(new[] { "d" }, "direction_down");
            g.AssociateDef("down", new ConditionalDef("direction_down", null, "go"));
            g.AddDef(new DirectionDef("direction_in", DirFlag.In));
            g.AddWords(new[] { "i", "inside" }, "direction_in");
            g.AssociateDef("in", new ConditionalDef("direction_in", null, "go"));
            g.AddDef(new DirectionDef("direction_out", DirFlag.Out));
            g.AddWords(new[] { "o", "outside" }, "direction_out");
            g.AssociateDef("out", new ConditionalDef("direction_out", null, "go"));

            // VERBS
            g.AddDef(new VerbDef("take", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddWords(new[] { "take", "grab" }, "take");

            g.AddDef(new VerbDef("go", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(DirectionNode)),
                }));
            g.AddWords(new[] { "go", "walk", "climb" }, "go");

            g.AddDef(new VerbDef("examine", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }));
            g.AddWords(new[] { "examine", "describe", "ex", "x" }, "examine");

            g.AddDef(new VerbDef("look", new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
                }));
            g.AddWords(new[] { "look", "l" }, "look");

            // COMMANDS
            g.AddDef(new CommandDef("commands", CommandAction.Placeholder));
            g.AddWords(new[] { "commands" }, "commands");
            g.AddDef(new CommandDef("credits", CommandAction.Placeholder));
            g.AddWords(new[] { "credits" }, "credits");
            g.AddDef(new CommandDef("help", CommandAction.Placeholder));
            g.AddWords(new[] { "help", "?" }, "help");
            g.AddDef(new CommandDef("quit", CommandAction.Placeholder));
            g.AddWords(new[] { "quit" }, "quit");
            g.AddDef(new CommandDef("verbose", CommandAction.Placeholder));
            g.AddWords(new[] { "verbose" }, "verbose");
            g.AddDef(new CommandDef("breif", CommandAction.Placeholder));
            g.AddWords(new[] { "brief" }, "brief");

            // OTHER / TEST
            g.AddDef(new NounDef("lamp"));
            g.AddWords(new[] { "lamp" }, "lamp");
            g.AddDef(new AdjectiveDef("brass"));
            g.AddWords(new[] { "brass" }, "brass");

            return g;
        }
    }
}
