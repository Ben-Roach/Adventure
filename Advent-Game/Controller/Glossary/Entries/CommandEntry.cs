
using System;
using System.Collections.Generic;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a known <see cref="Command"/> group in the <see cref="Glossary"/>.
    /// </summary>
    public sealed class CommandEntry : Entry
    {
        /// <summary>The method to call on behalf of the <see cref="Command"/>.</summary>
        Action commandDelegate;

        /// <summary>
        /// Create a new <see cref="CommandEntry"/>.
        /// </summary>
        /// <param name="wordGroup">The words that each represent a new known <see cref="Command"/>.</param>
        /// <param name="commandDelegate">The method to call on behalf of the words in <paramref name="wordGroup"/>.</param>
        public CommandEntry(ICollection<string> wordGroup, Action commandDelegate) : base(wordGroup)
        {
            this.commandDelegate = commandDelegate ?? throw new ArgumentNullException(nameof(commandDelegate));
        }

        /// <summary>
        /// Ensures that the words in this entry are not already contained in <paramref name="glossary"/>.
        /// </summary>
        /// <param name="glossary">The <see cref="Glossary"/> to check against.</param>
        public override void Validate(Glossary glossary)
        {
            foreach (string word in WordGroup)
            {
                if (glossary.TryGetEntryType(word, out Type t) == true)
                    throw new GlossaryValidationException(word);
            }
        }

        /// <summary>
        /// Create a new <see cref="Command"/> from this entry.
        /// </summary>
        /// <returns>The new <see cref="Command"/>, created from this entry.</returns>
        public override Node CreateNode(string origToken)
        {
            return new Command(origToken, commandDelegate);
        }
    }
}
