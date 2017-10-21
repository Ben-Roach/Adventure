
namespace Adventure.Controller
{
    /// <summary>
    /// Used with <see cref="Noun"/> objects to make them more specific.
    /// </summary>
    class Adjective : Node
    {
        /// <summary>
        /// Create a new <see cref="Adjective"/>.
        /// </summary>
        public Adjective(string origToken) : base(origToken)
        { }
    }
}