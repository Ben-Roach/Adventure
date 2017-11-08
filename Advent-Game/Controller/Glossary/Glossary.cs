
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them, as well as variables and methods for handling words.
    /// </summary>
    public class Glossary
    {
        /// <summary>Every <see cref="Entry"/> contained in the <see cref="Glossary"/>.</summary>
        private HashSet<Entry> entrySet;
        /// <summary>All word strings contained in the <see cref="Glossary"/>, each with the type of the <see cref="Entry"/> that contains it.</summary>
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
        /// <param name="entries">The <see cref="Entry"/> objects to include in the <see cref="Glossary"/>.</param>
        /// <param name="syntaxWildcard">The character that represents a wildcard in syntaxes. Automatically considered an invalid character.</param>
        /// <param name="normalize">Normalizes player input and words in the <see cref="Glossary"/>. Used before validation.</param>
        /// <param name="isInvalidChar">Reports if a character can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        /// <param name="isInvalidWord">Reports if a word can be ignored in player input and is invalid in the <see cref="Glossary"/>.</param>
        public Glossary(IEnumerable<Entry> entries, char syntaxWildcard, Func<string, string> normalize,
            Func<char, bool> isInvalidChar, Func<string, bool> isInvalidWord)
        {
            entrySet = new HashSet<Entry>();
            wordDict = new Dictionary<string, Type>();
            SyntaxWildcard = syntaxWildcard;
            Normalize = normalize;
            IsInvalidChar = (s => isInvalidChar(s) || s == SyntaxWildcard);
            IsInvalidWord = isInvalidWord;
            Add(entries);
        }

        /// <summary>
        /// Add a new <see cref="Entry"/> to the <see cref="Glossary"/>, after validating and normalizing
        /// the <see cref="Entry"/>.
        /// </summary>
        /// <remarks>
        /// Always use this method in some way when adding to the <see cref="Glossary"/>. 
        /// Do not add directly to <see cref="entrySet"/>!
        /// All <see cref="ParticleEntry"/> items containing words used in a <see cref="VerbSyntax"/> must be present before
        /// the <see cref="VerbEntry"/> containing the <see cref="VerbSyntax"/> may be added!
        /// </remarks>
        /// <param name="entry">The <see cref="Entry"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entry"/> is null.</exception>
        public void Add(Entry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            entry.ValidateAndNormalize(this);
            foreach (string word in entry)
                wordDict[word] = entry.GetType();
            entrySet.Add(entry);
        }

        /// <summary>
        /// Add new <see cref="Entry"/> items to the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="entries">The <see cref="Entry"/> items to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entries"/> is null or contains null objects.</exception>
        public void Add(IEnumerable<Entry> entries)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            foreach (Entry e in entries)
                Add(e);
        }

        /// <summary>
        /// Reports if the glossary contains an <see cref="Entry"/> that contains <paramref name="word"/> , and gets the type of the <see cref="Entry"/>.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <param name="entryType">Set to the type of the <see cref="Entry"/> containing <paramref name="word"/> if found.</param>
        /// <returns>True if <paramref name="word"/> is in the <see cref="Glossary"/>, else false.</returns>
        public bool TryGetEntryType(string word, out Type entryType)
        {
            return wordDict.TryGetValue(word, out entryType);
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public Node CreateNodeFromToken(string token)
        {
            foreach (Entry entry in entrySet)
            {
                if (entry.Contains(token))
                    return entry.CreateNode(token);
            }
            return new UnknownWord(token);
        }
    }
}