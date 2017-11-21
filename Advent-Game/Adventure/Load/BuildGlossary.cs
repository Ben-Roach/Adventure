using Adventure.Controller;
using Adventure.Model;
using System.Collections.Generic;

namespace Adventure
{
    public static partial class Load
    {
        /// <summary>
        /// Returns the initial <see cref="Definition"/> objects to add to the <see cref="Glossary"/>.
        /// </summary>
        public static Glossary BuildGlossary()
        {
            HashSet<Definition> entries = new HashSet<Definition>
            {
                // PARTICLES
                new ParticleDef(new[] { "at" }, "at"),
                // Remember that the following particles are similar to some directions:
                new ParticleDef(new[] { "in", "inside" }, "in"),
                new ParticleDef(new[] { "out" }, "out"),
                new ParticleDef(new[] { "up" }, "up"),
                new ParticleDef(new[] { "down" }, "down"),

                // VERBS
                new VerbDef(new[] { "take", "grab" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }),
                new VerbDef(new[] { "go", "walk", "climb" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(DirectionNode)),
                    new VerbSyntax("in", VerbAction.Placeholder),
                    new VerbSyntax("out", VerbAction.Placeholder),
                    new VerbSyntax("up", VerbAction.Placeholder),
                    new VerbSyntax("down", VerbAction.Placeholder),
                }),
                new VerbDef(new[] { "examine", "describe", "ex", "x" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                }),
                new VerbDef(new[] { "look", "l" }, new[] {
                    new VerbSyntax("*", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("at *", VerbAction.Placeholder, typeof(NounGroupNode), SyntFlag.MakeSingular),
                    new VerbSyntax("", VerbAction.Placeholder)
                }),

                // DIRECTIONS
                new DirectionDef(new[] { "north", "n" }, DirCode.North),
                new DirectionDef(new[] { "east", "e" }, DirCode.South),
                new DirectionDef(new[] { "south", "s" }, DirCode.East),
                new DirectionDef(new[] { "west", "w" }, DirCode.West),
                new DirectionDef(new[] { "northeast", "ne" }, DirCode.Northeast),
                new DirectionDef(new[] { "northwest", "nw" }, DirCode.Northwest),
                new DirectionDef(new[] { "southeast", "se" }, DirCode.Southeast),
                new DirectionDef(new[] { "southwest", "sw" }, DirCode.Southwest),
                // Remember that the following directions are similar to some particles:
                new DirectionDef(new[] { "u" }, DirCode.Up),
                new DirectionDef(new[] { "d" }, DirCode.Down),
                new DirectionDef(new[] { "enter" }, DirCode.In),
                new DirectionDef(new[] { "exit", "outside" }, DirCode.Out),

                // COMMANDS
                new CommandDef(new[] { "commands" }, CommandAction.Placeholder),
                new CommandDef(new[] { "credits" }, CommandAction.Placeholder),
                new CommandDef(new[] { "help", "?" }, CommandAction.Placeholder),
                new CommandDef(new[] { "quit" }, CommandAction.Placeholder),
                new CommandDef(new[] { "verbose" }, CommandAction.Placeholder),
                new CommandDef(new[] { "brief" }, CommandAction.Placeholder),

                // CONJUNCTIONS
                new ConjunctionDef("and"),
                new ConjunctionDef("then"),
                new ConjunctionDef("&")
            };

            return new Glossary(entries, '*',  s => s.Trim().ToLower(),
                c => !(char.IsLetter(c) || char.IsNumber(c) || c == '&' || c == '?' || c == '\'' || c == '-'),
                s => (s == "the" || s == "a" || s == "an" || s == "of")
                );
        }
    }
}
