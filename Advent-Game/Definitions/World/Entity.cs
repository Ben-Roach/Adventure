
namespace World
{
    /// <summary>
    /// 
    /// </summary>
    abstract class Entity
    {
        public abstract string Identity { get; } // internal name
        public abstract string[] Nouns { get; protected set; } // nouns to refer to the item
        public abstract string[] Adjectives { get; protected set; } // adjectives to specifically refer to the item
        public abstract string ShortDesc { get; protected set; }
        public abstract string LongDesc { get; protected set; }

        public Entity()
        {

        }
    }
}