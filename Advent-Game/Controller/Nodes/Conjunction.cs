
namespace Adventure.Controller
{
    /// <summary>
    /// Represents a word used to link words and phrases together.
    /// </summary>
    class Conjunction : Node
    {
        /// <summary>
        /// Create a new <see cref="Conjunction"/>.
        /// </summary>
        public Conjunction(string origToken) : base(origToken)
        { }
    }
}