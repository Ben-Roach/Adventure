
using System;

namespace Adventure
{
    public class InvalidGlossaryAdditionException : Exception
    {
        /// <summary>
        /// Should be thrown when a word cannot be added to the <see cref="Glossary"/> because it already exists.
        /// </summary>
        /// <param name="word">The word that could not be added.</param>
        /// <param name="attemptedType">The type of the <see cref="Entry"/> containing <paramref name="word"/>.</param>
        /// <param name="discoveredType">The type of the <see cref="Entry"/> in the <see cref="Glossary"/> that already contains <paramref name="word"/>.</param>
        public InvalidGlossaryAdditionException(string word, Type attemptedType, Type discoveredType)
            : base("Attempted to add '" + word + "' to the glossary as a(n) "
                + attemptedType.GetType() + ", but it already exists as a(n) "
                + discoveredType.GetType() + ".")
        { }
    }
}
