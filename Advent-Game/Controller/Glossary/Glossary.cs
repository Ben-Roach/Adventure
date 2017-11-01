
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Contains collections with all known words, and all properties associated with them.
    /// </summary>
    public sealed class Glossary
    {
        private static readonly Glossary instance = new Glossary();
        /// <summary>The instance of the <see cref="Glossary"/> singleton.</summary>
        public static Glossary Instance { get { return instance; } }

        /// <summary>All <see cref="Entries"/> contained in the <see cref="Glossary"/>.</summary>
        private HashSet<Entry> entrySet;
        /// <summary>All words contained in the <see cref="Glossary"/>, each with the type of the <see cref="Entry"/> that contains it.</summary>
        private Dictionary<string, Type> wordDict;

        /// <summary>
        /// Instantiate the <see cref="Glossary"/> singleton.
        /// </summary>
        private Glossary()
        {
            entrySet = new HashSet<Entry>();
        }

        public static bool TryGetEntryType(string word, out Type entryType)
        {
            return Instance.wordDict.TryGetValue(word, out entryType);
        }

        /// <summary>
        /// Add a new <see cref="Entry"/> to the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="entry">The <see cref="Entry"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entry"/> is null.</exception>
        public static void Add(Entry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            Instance.entrySet.Add(entry);
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public static Node CreateNodeFromToken(string token)
        {
            string tokenLower = token.ToLower();
            foreach (Entry entry in Instance.entrySet)
            {
                if (entry.Contains(tokenLower))
                    return entry.CreateNode(tokenLower);
            }
            return new UnknownWord(token);
        }
    }
}