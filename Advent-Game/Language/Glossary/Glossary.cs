
using System;
using System.Collections.Generic;

namespace Adventure.Language
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them,
    /// as well as variables and methods for validating, normalizing and interpreting words.
    /// </summary>
    public class Glossary
    {
        /// <summary>A dictionary of all words usable by the player (keys) with the ID of each word's <see cref="Definition"/>
        /// (values) to use.</summary>
        Dictionary<string, string> wordDefs;
        /// <summary>A dictionary of all <see cref="Definition.ID"/> strings (keys), with their respective <see cref="Definition"/>
        /// objects (values).</summary>
        Dictionary<string, Definition> defIDs;

        /// <summary>The wildcard character used by syntaxes to represent variable words. Automatically considered
        /// an invalid character in <see cref="IsInvalidChar"/>.</summary>
        public char SyntaxWildcard { get; }
        /// <summary>The string to use as the <see cref="Node.ID"/> for any word that is undefined in this
        /// <see cref="Glossary"/>.</summary>
        public string UnknownID { get; }
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
        /// <param name="syntaxWildcard">The character that represents a wildcard in syntaxes. Automatically considered an invalid character.
        /// Also used as <see cref="UnknownID"/>.</param>
        /// <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        /// <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        /// <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(char syntaxWildcard, Func<string, string> normalize, Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            wordDefs = new Dictionary<string, string>();
            defIDs = new Dictionary<string, Definition>();

            SyntaxWildcard = syntaxWildcard;
            UnknownID = syntaxWildcard.ToString();
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
            if (def == null) throw new ArgumentNullException(nameof(def));
            // validation
            //  if def with id already exists -- error
            //  if def is invalid -- error
            //  if unknown def id -- error
            // add def
            defIDs[def.ID] = def;
        }

        /// <summary>
        /// Adds new words to the <see cref="Glossary"/>, all sharing the same <see cref="Definition"/>.
        /// </summary>
        /// <param name="words">The words to add, that the player may use.</param>
        /// <param name="defID">The <see cref="Definition.ID"/> of the <see cref="Definition"/> associated with the words.</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if any parameter is null or <paramref name="words"/> contains a null item.</exception>
        public void AddWords(IEnumerable<string> words, string defID)
        {
            // param checking
            if (words == null) throw new ArgumentNullException(nameof(words));
            if (defID == null) throw new ArgumentNullException(nameof(defID));
            if (words.ContainsNull()) throw new ArgumentException(nameof(words) + "contains a null item.");
            // validation
            //  if def with id doesn't exist -- error
            //  if words contain invalid chars or are invalid strings -- error
            //  if any word already exists -- error
            // add words
            foreach (string word in words)
                wordDefs[word] = defID;
        }

        /// <summary>
        /// Converts the given <see cref="Token"/> list into a <see cref="Node"/> list using this <see cref="Glossary"/>.
        /// </summary>
        /// <param name="tokenList">The <see cref="Token"/> list to convert to a <see cref="Node"/> list.</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is null or <paramref name="tokenList"/> contains a null item.</exception>
        public List<Node> ConvertToNodes(List<Token> tokenList)
        {
            // param checking
            if (tokenList == null) throw new ArgumentNullException(nameof(tokenList));
            if (tokenList.ContainsNull()) throw new ArgumentException(nameof(tokenList) + "contains a null item.");

            List<Node> nodeList = new List<Node>();
            for (int i = 0; i < tokenList.Count; i++)
            {
                // look up word
                Token token = tokenList[i];
                wordDefs.TryGetValue(token.LookupWord, out string id);
                defIDs.TryGetValue(id, out Definition def);
                // check if defined
                if (def == null)
                    nodeList.Add(new UnknownNode(token.OrigWord, UnknownID));
                // use definition
                else
                    nodeList.Add(def.CreateNode(token.OrigWord));
            }
            return nodeList;
        }
    }
}