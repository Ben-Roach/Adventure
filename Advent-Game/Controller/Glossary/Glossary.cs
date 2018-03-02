
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them,
    /// as well as variables and methods for validating, normalizing and interpreting words.
    /// </summary>
    public class Glossary
    {
        /// <summary>A dictionary of all words usable by the player (keys) with the ID of each word's <see cref="Definition"/>
        /// (values) to use by default.</summary>
        Dictionary<string, string> wordDefaultDefs;
        /// <summary>A dictionary of all usable words (keys) with a list of their associated <see cref="ConditionalDef"/>
        /// objects in order of precedence (values).</summary>
        Dictionary<string, List<ConditionalDef>> wordConditionalDefs;
        /// <summary>A dictionary of all <see cref="Definition.ID"/> strings (keys), with their respective <see cref="Definition"/>
        /// objects (values).</summary>
        Dictionary<string, Definition> defIDs;

        /// <summary>The wildcard character used by syntaxes to represent variable words. Automatically considered
        /// an invalid character in <see cref="IsInvalidChar"/>.</summary>
        public char SyntaxWildcard { get; }
        /// <summary>The string to use as the <see cref="Definition.ID"/> for any word that is undefined in this
        /// <see cref="Glossary"/>.</summary>
        public string UnknownDefID { get; }
        /// <summary>Normalizes player input and words in the <see cref="Glossary"/> so they match correctly.
        /// Used before validation.</summary>
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
        /// <param name="unknownDefID">The <see cref="Definition.ID"/> to use for unknown words.</param>
        /// <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        /// <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        /// <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(char syntaxWildcard, string unknownDefID, Func<string, string> normalize,
            Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            wordDefaultDefs = new Dictionary<string, string>();
            wordConditionalDefs = new Dictionary<string, List<ConditionalDef>>();
            defIDs = new Dictionary<string, Definition>();

            SyntaxWildcard = syntaxWildcard;
            UnknownDefID = unknownDefID;
            Normalize = normalize;
            IsInvalidChar = (s => isInvalidChar(s) || s == SyntaxWildcard);
            IsInvalidWord = isInvalidWord;
        }

        /// <summary>
        /// Adds a new <see cref="Definition"/> to the glossary.
        /// </summary>
        /// <param name="def">The <see cref="Definition"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public void AddDef(Definition def)
        {
            // validation
            //  if def with id already exists -- error
            //  if def is invalid -- error
            //  if unknown def id -- error
            // add def
            defIDs[def.ID] = def ?? throw new ArgumentNullException(nameof(def));
        }

        /// <summary>
        /// Adds new words to the <see cref="Glossary"/>, all sharing the same default <see cref="Definition"/>.
        /// </summary>
        /// <param name="words">The words to add, that the player may use.</param>
        /// <param name="defaultDefID">The <see cref="Definition.ID"/> of the default <see cref="Definition"/>
        /// associated with <paramref name="words"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="words"/> contains a null item.</exception>
        public void AddWords(IEnumerable<string> words, string defaultDefID)
        {
            // param checking
            if (words == null) throw new ArgumentNullException(nameof(words));
            if (defaultDefID == null) throw new ArgumentNullException(nameof(defaultDefID));
            if (words.ContainsNull()) throw new ArgumentException(nameof(words) + "contains a null item.");
            // validation
            //  if def with id doesn't exist -- error
            //  if words contain invalid chars or are invalid strings -- error
            //  if any word already exists -- error
            // add words
            foreach (string word in words)
                wordDefaultDefs[word] = defaultDefID;
        }

        /// <summary>
        /// Associates a <see cref="Definition"/> with a word using a <see cref="ConditionalDef"/>.
        /// </summary>
        /// <param name="word">The word to associate with the <see cref="Definition"/>.</param>
        /// <param name="conditionalDef">Used to determine what <see cref="Definition"/> to use, and when.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public void AssociateDef(string word, ConditionalDef conditionalDef)
        {
            // param checking
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (conditionalDef == null) throw new ArgumentNullException(nameof(conditionalDef));
            // validation
            //  if def with id doesn't exist -- error
            //  if word contains invalid chars or is invalid string -- error
            //  if word does not have a default def -- error
            //  if word already has conditional def with the same def id -- error
            // add to glossary
            if (!wordConditionalDefs.ContainsKey(word))
                wordConditionalDefs[word] = new List<ConditionalDef>();
            wordConditionalDefs[word].Add(conditionalDef);
        }

        /// <summary>
        /// Gets the default <see cref="Definition"/> for the given word. Returns null if not found.
        /// </summary>
        public Definition GetDefaultDef(string word)
        {
            wordDefaultDefs.TryGetValue(word, out string id);
            if (id == null)
                return null;
            return GetDefFromID(id);
        }

        /// <summary>
        /// Gets a list of <see cref="ConditionalDef"/> objects associated with the given word.
        /// Returns an empty list if none found.
        /// </summary>
        public List<ConditionalDef> GetConditionalDefs(string word)
        {
            wordConditionalDefs.TryGetValue(word, out List<ConditionalDef> defs);
            if (defs == null)
                return new List<ConditionalDef>();
            return defs;
        }

        /// <summary>
        /// Gets the <see cref="Definition"/> with an ID matching the given string.
        /// Returns null if not found.
        /// </summary>
        public Definition GetDefFromID(string id)
        {
            defIDs.TryGetValue(id, out Definition def);
            return def;
        }
    }
}