﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them, as well as variables and methods for handling words.
    /// </summary>
    public class Glossary
    {
        /// <summary>A dictionary of all synonyms (keys) and their respective <see cref="Definition"/> objects (values) to use by default.</summary>
        Dictionary<string, Definition> defaultDefDict;
        /// <summary>A dictionary of synonyms (keys), each with a list of associated <see cref="ConditionalDef"/> objects.</summary>
        Dictionary<string, List<ConditionalDef>> conditionalDefDict;

        /// <summary>The wildcard character used by syntaxes to represent variable words. Automatically considered an invalid character.</summary>
        public char SyntaxWildcard { get; }
        /// <summary>Normalizes player input and words in the <see cref="Glossary"/> so they match correctly. Used before validation.</summary>
        public Func<string, string> Normalize { get; }
        /// <summary>Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.
        /// Automatically considers <see cref="SyntaxWildcard"/> to be invalid.</summary>
        public Func<char, bool> IsInvalidChar { get; }
        /// <summary>Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</summary>
        public Func<string, bool> IsInvalidWord { get; }

        /// <summary>
        /// Create a new <see cref="Glossary"/>.
        /// </summary>
        /// <param name="syntaxWildcard">The character that represents a wildcard in syntaxes. Automatically considered an invalid character.</param>
        /// <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        /// <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        /// <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(char syntaxWildcard, Func<string, string> normalize,
            Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            defaultDefDict = new Dictionary<string, Definition>();
            conditionalDefDict = new Dictionary<string, List<ConditionalDef>>();
            SyntaxWildcard = syntaxWildcard;
            Normalize = normalize;
            IsInvalidChar = (s => isInvalidChar(s) || s == SyntaxWildcard);
            IsInvalidWord = isInvalidWord;
        }

        /// <summary>
        /// Adds a new entry to the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="headwords">All of the words that the player may use to reference this entry.</param>
        /// <param name="defaultDefinition">The default <see cref="Definition"/> associated with <paramref name="headwords"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="headwords"/> contains a null item.</exception>
        /// <exception cref="GlossaryValidationException">Thrown if an item in <paramref name="headwords"/>
        /// already exists in the <see cref="Glossary"/>.</exception>
        public void AddNewEntry(IEnumerable<string> headwords, Definition defaultDefinition)
        {
            // param checking
            if (headwords == null) throw new ArgumentNullException(nameof(headwords));
            if (defaultDefinition == null) throw new ArgumentNullException(nameof(defaultDefinition));
            if (headwords.ContainsNull()) throw new ArgumentException(nameof(headwords) + "contains a null item.");
            foreach (string s in headwords)
            {
                if (defaultDefDict.ContainsKey(s))
                    throw new GlossaryValidationException("Attempted to add a default definition to an existing entry.");
            }
            // add to glossary
            foreach (string s in headwords)
            {
                defaultDefDict[s] = defaultDefinition;
            }
        }

        /// <summary>
        /// Adds a new <see cref="ConditionalDef"/> to an existing entry.
        /// </summary>
        /// <param name="headword">The headword to add the definition to.</param>
        /// <param name="conditional">The conditional that determines if <paramref name="definition"/> is used.</param>
        /// <param name="definition">The <see cref="Definition"/> to add.</param>
        /// /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public void AppendDef(string headword, Func<Type, string, bool> conditional, Definition definition)
        {
            // param checking
            if (headword == null) throw new ArgumentNullException(nameof(headword));
            if (conditional == null) throw new ArgumentNullException(nameof(conditional));
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            // validation
            if (!defaultDefDict.ContainsKey(headword))
                throw new GlossaryValidationException("Attempted to add a conditional definition to a nonexistent entry.");
            // add to glossary
            if (!conditionalDefDict.ContainsKey(headword))
                conditionalDefDict[headword] = new List<ConditionalDef>();
            conditionalDefDict[headword].Add(new ConditionalDef(definition, conditional));
        }

        /// <summary>
        /// Reports if a string contains a character that is invalid according to <see cref="IsInvalidChar"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool ContainsInvalidChar(string s)
        {
            foreach (char c in s) { if (IsInvalidChar(c)) return true; }
            return false;
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public Node CreateNodeFromToken(string token)
        {
            if (defaultDefDict.TryGetValue(token, out Definition def))
            {
                return def.CreateNode(token);
            }
            return new UnknownNode(token);
        }
    }
}