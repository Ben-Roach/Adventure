﻿
using System;

namespace Adventure
{
    public class GlossaryValidationException : Exception
    {
        /// <summary>
        /// Should be thrown when an error occurs in the <see cref="Language.Glossary"/>.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public GlossaryValidationException(string message)
            : base(message)
        { }
    }
}
