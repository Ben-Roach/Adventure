
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them.
    /// </summary>
    public class Glossary
    {
        /// <summary>Every <see cref="Entry"/> contained in the <see cref="Glossary"/>.</summary>
        private HashSet<Entry> entrySet;
        /// <summary>All word strings contained in the <see cref="Glossary"/>, each with the type of the <see cref="Entry"/> that contains it.</summary>
        private Dictionary<string, Type> wordDict;

        /// <summary>
        /// Create a new empty <see cref="Glossary"/>.
        /// </summary>
        public Glossary()
        {
            entrySet = new HashSet<Entry>();
            wordDict = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Create a new <see cref="Glossary"/> containing <paramref name="entries"/>.
        /// </summary>
        public Glossary(IEnumerable<Entry> entries) : this()
        {
            Add(entries);
        }

        /// <summary>
        /// Add a new <see cref="Entry"/> to the <see cref="Glossary"/>, after validating the <see cref="Entry"/>.
        /// </summary>
        /// <remarks>
        /// Always use this method in some way when adding to the <see cref="Glossary"/>. 
        /// Do not add directly to <see cref="entrySet"/>!
        /// </remarks>
        /// <param name="entry">The <see cref="Entry"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entry"/> is null.</exception>
        public void Add(Entry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            entry.Validate(this);
            foreach (string word in entry.WordGroup)
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
            string tokenLower = token.ToLower();
            foreach (Entry entry in entrySet)
            {
                if (entry.Contains(tokenLower))
                    return entry.CreateNode(tokenLower);
            }
            return new UnknownWord(token);
        }
    }
}