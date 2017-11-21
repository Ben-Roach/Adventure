
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used for verb syntax structure.
    /// </summary>
    public sealed class ParticleNode : Node
    {
        /// <summary>The commonly understood form of <see cref="OrigToken"/>, used to test if a synonym was used by the player.</summary>
        public string SyntaxName { get; }

        /// <summary>
        /// Create a new <see cref="ParticleNode"/>.
        /// </summary>
        /// <param name="syntaxName">The commonly understood form of the word used in <see cref="VerbSyntax"/> objects.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="syntaxName"/> is null.</exception>
        public ParticleNode(string origToken, string syntaxName) : base(origToken)
        {
            SyntaxName = syntaxName ?? throw new ArgumentNullException(nameof(syntaxName));
        }
    }
}