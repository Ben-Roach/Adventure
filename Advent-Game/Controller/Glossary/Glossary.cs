
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them, as well as variables and methods for handling words.
    /// </summary>
    public class Glossary
    {
        /// <summary>A dictionary of all synonyms (keys) and their respective headwords (values).</summary>
        Dictionary<string, string> synonymDict;
        /// <summary>A dictionary of all headwords (keys) and their respective <see cref="Definition"/> objects (values) to use by default.</summary>
        Dictionary<string, Definition> defaultDefDict;
        /// <summary>A dictionary of headwords (keys), each with a list of associated <see cref="ConditionalDef"/> objects.</summary>
        Dictionary<string, List<ConditionalDef>> conditionalDefDict;

        // <summary>Every <see cref="Definition"/> contained in the <see cref="Glossary"/>.</summary>
        private HashSet<Definition> entrySet;
        // <summary>All word strings contained in the <see cref="Glossary"/>, each with the type of the <see cref="Definition"/> that contains it.</summary>
        private Dictionary<string, Type> wordDict;

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
            synonymDict = new Dictionary<string, string>();
            defaultDefDict = new Dictionary<string, Definition>();
            conditionalDefDict = new Dictionary<string, List<ConditionalDef>>();
            SyntaxWildcard = syntaxWildcard;
            Normalize = normalize;
            IsInvalidChar = (s => isInvalidChar(s) || s == SyntaxWildcard);
            IsInvalidWord = isInvalidWord;
        }

        // <summary>
        // Create a new <see cref="Glossary"/>.
        // </summary>
        // <param name="entries">The <see cref="Definition"/> objects to include in the <see cref="Glossary"/>.</param>
        // <param name="syntaxWildcard">The character that represents a wildcard in syntaxes. Automatically considered an invalid character.</param>
        // <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        // <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        // <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(IEnumerable<Definition> entries, char syntaxWildcard, Func<string, string> normalize,
            Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            entrySet = new HashSet<Definition>();
            wordDict = new Dictionary<string, Type>();
            SyntaxWildcard = syntaxWildcard;
            Normalize = normalize;
            IsInvalidChar = (s => isInvalidChar(s) || s == SyntaxWildcard);
            IsInvalidWord = isInvalidWord;
            Add(entries);
        }

        // <summary>
        // Add a new <see cref="Definition"/> to the <see cref="Glossary"/>, after validating and normalizing
        // the <see cref="Definition"/>.
        // </summary>
        // <remarks>
        // Always use this method in some way when adding to the <see cref="Glossary"/>. 
        // Do not add directly to <see cref="entrySet"/>!
        // All <see cref="ParticleDef"/> items containing words used in a <see cref="VerbSyntax"/> must be present before
        // the <see cref="VerbDef"/> containing the <see cref="VerbSyntax"/> may be added!
        // </remarks>
        // <param name="entry">The <see cref="Definition"/> to add.</param>
        // <exception cref="ArgumentNullException">Thrown if <paramref name="entry"/> is null.</exception>
        public void Add(Definition entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            // validate and normalize here!
            foreach (string word in entry)
                wordDict[word] = entry.GetType();
            entrySet.Add(entry);
        }

        // <summary>
        // Add new <see cref="Definition"/> items to the <see cref="Glossary"/>.
        // </summary>
        // <param name="entries">The <see cref="Definition"/> items to add.</param>
        // <exception cref="ArgumentNullException">Thrown if <paramref name="entries"/> is null or contains null objects.</exception>
        public void Add(IEnumerable<Definition> entries)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            else if (entries.ContainsNull()) throw new ArgumentException(nameof(entries), "Cannot contain null items.");
            foreach (Definition e in entries)
                Add(e);
        }

        // <summary>
        // Reports if the glossary contains an <see cref="Definition"/> that contains <paramref name="word"/>, and gets the type of the <see cref="Definition"/>.
        // </summary>
        // <param name="word">The word to check.</param>
        // <param name="entryType">Set to the type of the <see cref="Definition"/> containing <paramref name="word"/> if found.</param>
        // <returns>True if <paramref name="word"/> is in the <see cref="Glossary"/>, else false.</returns>
        public bool TryGetEntryType(string word, out Type entryType)
        {
            throw new NotImplementedException("TryGetEntryType");
        }

        /// <summary>
        /// Adds a new entry to the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="headword">The headword under which the entry is placed in the <see cref="Glossary"/>.</param>
        /// <param name="synonyms">All of the words that the player may use to reference this entry.</param>
        /// <param name="defaultDefinition">The default <see cref="Definition"/> associated with this entry.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="synonyms"/> contains a null item.</exception>
        /// <exception cref="GlossaryValidationException">Thrown if <paramref name="headword"/> or an item in <paramref name="synonyms"/>
        /// already exists in the <see cref="Glossary"/>.</exception>
        public void AddNewEntry(string headword, IEnumerable<string> synonyms, Definition defaultDefinition)
        {
            // param checking
            if (headword == null) throw new ArgumentNullException(nameof(headword));
            if (synonyms == null) throw new ArgumentNullException(nameof(synonyms));
            if (defaultDefinition == null) throw new ArgumentNullException(nameof(defaultDefinition));
            if (synonyms.ContainsNull()) throw new ArgumentException(nameof(defaultDefinition) + "contains a null item.");
            // dupe checking
            if (defaultDefDict.ContainsKey(headword))
                throw new GlossaryValidationException("Attempted to add an existing headword.");
            foreach (string s in synonyms)
            {
                if (synonymDict.ContainsKey(headword))
                    throw new GlossaryValidationException("Attempted to add an existing synonym.");
            }
            // add to glossary
            defaultDefDict[headword] = defaultDefinition;
            foreach (string s in synonyms)
            {
                synonymDict[s] = headword;
            }
        }

        /// <summary>
        /// Adds a new <see cref="ConditionalDef"/> to an existing entry.
        /// </summary>
        /// <param name="headword">The headword of the entry to add the definition to.</param>
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
                throw new GlossaryValidationException("Attempted to add a definition to a nonexistent headword.");
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
            foreach (Definition entry in entrySet)
            {
                if (entry.Contains(token))
                    return entry.CreateNode(token);
            }
            return new UnknownNode(token);
        }
    }
}