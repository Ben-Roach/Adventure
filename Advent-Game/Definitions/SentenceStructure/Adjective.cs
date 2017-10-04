
namespace SentenceStructure
{
    /// <summary>
    /// Added to <see cref="Noun.ContainedAdjectives"/> to make the <see cref="Noun"/> more specific.
    /// </summary>
    class Adjective : Node
    {
        /// <summary>
        /// Create a new <see cref="Adjective"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when origToken is null.</exception>
        public Adjective(string origToken) : base(origToken)
        { }
    }
}