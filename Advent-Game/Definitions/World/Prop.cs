
namespace World
{
    /// <summary>
    /// 
    /// </summary>
    sealed class Prop : Entity
    {
        /// <summary>Describes the Prop's initial location.</summary>
        public string InitLocationDesc { get; private set; }

        public override string Identity { get; }
        public override string[] Nouns { get; protected set; }
        public override string[] Adjectives { get; protected set; }
        public override string ShortDesc { get; protected set; }
        public override string LongDesc { get; protected set; }

        public Prop() : base()
        {

        }
    }
}