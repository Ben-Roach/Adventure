
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        /// objects (values).</summary>
        Dictionary<string, List<ConditionalDef>> wordConditionalDefs;
        /// <summary>A dictionary of all <see cref="Definition.ID"/> strings (keys), with their respective <see cref="Definition"/>
        /// objects (values).</summary>
        Dictionary<string, Definition> defIDs;

        /// <summary>The wildcard character used by syntaxes to represent variable words. Automatically considered
        /// an invalid character in <see cref="IsInvalidChar"/>.</summary>
        public char SyntaxWildcard { get; }
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
        /// <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        /// <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        /// <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(char syntaxWildcard, Func<string, string> normalize,
            Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            wordDefaultDefs = new Dictionary<string, string>();
            wordConditionalDefs = new Dictionary<string, List<ConditionalDef>>();
            defIDs = new Dictionary<string, Definition>();
            SyntaxWildcard = syntaxWildcard;
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
            //  if def with id already exists -- warning
            //  if def is invalid -- error
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
            //  if any word already exists -- warning
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
            //  if word already has conditional def with the same def id -- warning
            // add to glossary
            if (!wordConditionalDefs.ContainsKey(word))
                wordConditionalDefs[word] = new List<ConditionalDef>();
            wordConditionalDefs[word].Add(conditionalDef);
        }

        /// <summary>
        /// Creates a <see cref="Node"/> object derived from <paramref name="token"/>.
        /// </summary>
        /// <param name="token">The <see cref="Token"/> to interpret.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public Node CreateNodeFromToken(Token token)
        {
            if (wordDefaultDefs.TryGetValue(token.LookupWord, out Definition def))
            {
                return def.CreateNode(token.OrigWord);
            }
            return new UnknownNode(token.OrigWord, "UNKNOWN");
        }
    }
}