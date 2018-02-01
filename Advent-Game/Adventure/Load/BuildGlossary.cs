﻿using Adventure.Controller;
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

            // CONJUNCTIONS
            g.AddDef(new ConjunctionDef("and"));
            g.AddWords(new[] { "and", "then", "&" }, "and");

            // DIRECTIONS
            g.AddDef(new DirectionDef("north", DirFlag.North));
            g.AddWords(new[] { "north", "n" }, "north");
            g.AddDef(new DirectionDef("east", DirFlag.South));
            g.AddWords(new[] { "east", "e" }, "east");
            g.AddDef(new DirectionDef("south", DirFlag.East));
            g.AddWords(new[] { "south", "s" }, "south");
            g.AddDef(new DirectionDef("west", DirFlag.West));
            g.AddWords(new[] { "west", "w" }, "west");
            g.AddDef(new DirectionDef("northeast", DirFlag.Northeast));
            g.AddWords(new[] { "northeast", "ne" }, "northeast");
            g.AddDef(new DirectionDef("northwest", DirFlag.Northwest));
            g.AddWords(new[] { "northwest", "nw" }, "northwest");
            g.AddDef(new DirectionDef("southeast", DirFlag.Southeast));
            g.AddWords(new[] { "southeast", "se" }, "southeast");
            g.AddDef(new DirectionDef("southwest", DirFlag.Southwest));
            g.AddWords(new[] { "southwest", "sw" }, "southwest");
            g.AddDef(new DirectionDef("up", DirFlag.Up));
            g.AddWords(new[] { "up", "u" }, "up");
            g.AddDef(new DirectionDef("down", DirFlag.Down));
            g.AddWords(new[] { "down", "d" }, "down");
            g.AddDef(new DirectionDef("in", DirFlag.In));
            g.AddWords(new[] { "in", "inside" }, "in");
            g.AddDef(new DirectionDef("out", DirFlag.Out));
            g.AddWords(new[] { "out", "outside" }, "out");

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
