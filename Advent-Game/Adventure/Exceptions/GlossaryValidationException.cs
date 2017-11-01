
using System;
using Adventure.Controller;

namespace Adventure
{
    public class GlossaryValidationException : Exception
    {
        /// <summary>
        /// Should be thrown when something is invalid when tested against a <see cref="Glossary"/>.
        /// </summary>
        /// <param name="item">The word that was found invalid.</param>
        public GlossaryValidationException(string item)
            : base("'" + item + "' is invalid when checked against the glossary.")
        { }

        /// <summary>
        /// Should be thrown when something is invalid when tested against a <see cref="Glossary"/>.
        /// </summary>
        /// <param name="item">The string or name of the item that was found invalid.</param>
        /// <param name="reason">The reason why the word is invalid.</param>
        public GlossaryValidationException(string item, string reason)
            : base("'" + item + "' is invalid when checked against the glossary. Reason:  " + reason)
        { }
    }
}
