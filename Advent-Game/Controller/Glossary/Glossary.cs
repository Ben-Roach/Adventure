
using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Model;

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

        /// <summary>The set of <see cref="Entries"/> contained within the <see cref="Glossary"/>.</summary>
        private HashSet<Entry> baseSet;

        /// <summary>
        /// Instantiate the <see cref="Glossary"/> singleton.
        /// </summary>
        private Glossary()
        {
            baseSet = new HashSet<Entry>();
        }

        /// <summary>
        /// Add a new <see cref="Entry"/> to the <see cref="Glossary"/>.
        /// </summary>
        /// <param name="entry">The <see cref="Entry"/> to add.</param>
        public static void Add(Entry entry)
        {
            Instance.baseSet.Add(entry);
        }

        /// <summary>
        /// Creates an <see cref="Node"/> object derived from <paramref name="token"/>.
        /// </summary>
        /// <param name="token">A word input by the player.</param>
        /// <returns>An <see cref="Node"/> that represents the <paramref name="token"/>.</returns>
        public static Node CreateNodeFromToken(string token)
        {
            string tokenLower = token.ToLower();
            foreach (Entry entry in Instance.baseSet)
            {
                if (entry.Contains(tokenLower))
                    return entry.CreateNode(tokenLower);
            }
            return new UnknownWord(token);
        }
    }
}