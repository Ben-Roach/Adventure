
namespace Sentence
{
    /// <summary>
    /// Used for sentence and syntax structure.
    /// </summary>
    class Particle : INode
    {
        public string OrigToken { get; }
        /// <summary>The commonly understood form of OrigToken, used to test if a word in a specific word group (from a Glossary) was used.</summary>
        public string Lemma { get; }

        /// <summary>
        /// Create a new Particle INode with an associated lemma.
        /// </summary>
        /// <param name="lemma">The commonly understood form of OrigToken.</param>
        public Particle(string origToken, string lemma)
        {
            OrigToken = origToken;
            Lemma = lemma;
        }
    }
}