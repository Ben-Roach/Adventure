
using System;

namespace Adventure
{
    public class GlossaryValidationException : Exception
    {
        /// <summary>
        /// Should be thrown when a validation error occurs when modifying the <see cref="Language.Glossary"/>.
        /// </summary>
        /// <param name="message">The reason for the error.</param>
        public GlossaryValidationException(string message)
            : base(message)
        { }
    }
}
