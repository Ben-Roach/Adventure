
namespace World
{
    /// <summary>
    /// 
    /// </summary>
    sealed class Location : Entity
    {
        /// <summary>The backing field to ShortDesc and LongDesc.</summary>
        string description;

        public override string Identity { get; }
        public override string[] Nouns { get; protected set; }
        public override string[] Adjectives { get; protected set; }
        public override string ShortDesc { get { return description; } protected set { description = value; } }
        public override string LongDesc { get { return description; } protected set { description = value; } }

        public Location() : base()
        {

        }
    }
}