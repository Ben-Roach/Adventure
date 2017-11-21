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
        public Definition Def { get; }
        public Func<Type, string, bool> Conditional { get; }

        public ConditionalDef(Definition def, Func<Type, string, bool> conditional)
        {
            Def = def;
            Conditional = conditional;
        }
    }
}
