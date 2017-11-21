using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Controller
{
    /// <summary>
    /// Used to wrap a <see cref="Definition"/> with a conditional delegate. Does not inherit from <see cref="Definition"/>.
    /// </summary>
    class ConditionalDef
    {
        public delegate bool DefCondition(Type nodeType, string nodeHeadword);

        public Definition Def { get; }
        public DefCondition Conditional { get; }

        public ConditionalDef(Definition def, DefCondition conditional)
        {
            Def = def;
            Conditional = conditional;
        }
    }
}
