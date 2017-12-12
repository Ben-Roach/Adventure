using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Controller
{
    /// <summary>
    /// Used to point to a <see cref="Definition"/> with a conditional delegate and a priority rank.
    /// Does not inherit from or contain a <see cref="Definition"/>.
    /// </summary>
    public class ConditionalDef
    {
        public string DefID { get; }
        public Func<Type, string, bool> Conditional { get; }
        public int Priority { get; }

        public ConditionalDef(string defID, Func<Type, string, bool> conditional, int priority)
        {
            DefID = defID;
            Conditional = conditional;
            Priority = priority;
        }
    }
}
