
using System;

namespace Adventure.Language
{
    public class Token
    {
        /// <summary>The original word used by the player.</summary>
        public string OrigWord { get; private set; }
        /// <summary>The normalized word that will be used to search the <see cref="Glossary"/>.</summary>
        public string LookupWord { get; private set; }

        /// <summary>
        /// Create a new <see cref="Token"/>.
        /// </summary>
        /// <param name="origWord">The original word used by the player.</param>
        /// <param name="lookupWord">The normalized word that will be used to search the <see cref="Glossary"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null.</exception>
        public Token(string origWord, string lookupWord)
        {
            OrigWord = origWord ?? throw new ArgumentNullException(nameof(origWord));
            LookupWord = lookupWord ?? throw new ArgumentNullException(nameof(origWord));
        }
    }
}
